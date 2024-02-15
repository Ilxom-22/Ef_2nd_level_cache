using EasyCaching_Interceptor.Domain;
using EasyCaching_Interceptor.Persistence;
using EFCoreSecondLevelCacheInterceptor;

namespace EasyCaching_Interceptor.Services;

public class UserService(AppDbContext context)
{
    public IQueryable<User> GetUsers()
    {
        return context.Users.AsQueryable().Cacheable(CacheExpirationMode.Sliding, TimeSpan.FromMinutes(5));
    }
}