using EasyCaching_Interceptor.Domain;
using EasyCaching_Interceptor.Persistence;
using EFCoreSecondLevelCacheInterceptor;
using Microsoft.EntityFrameworkCore;

namespace EasyCaching_Interceptor.Services;

public class UserService(AppDbContext context)
{
    public IQueryable<User> GetUsers(FilterPagination paginationOptions)
    {
        return context.Users.AsQueryable()
            .Cacheable(CacheExpirationMode.Sliding, TimeSpan.FromMinutes(5))
            .OrderBy(user => user.Id)
            .Skip((paginationOptions.PageToken - 1) * paginationOptions.PageSize)
            .Take(paginationOptions.PageSize);
    }

    public async ValueTask<User> UpdateUserAsync(User user)
    {
        var foundUser = await context.Users.FirstOrDefaultAsync(@this => @this.Id == user.Id)
                        ?? throw new ArgumentException("User not found!");

        foundUser.FirstName = user.FirstName;
        foundUser.LastName = user.LastName;

        context.Users.Update(foundUser);
        await context.SaveChangesAsync();

        return user;
    }
}