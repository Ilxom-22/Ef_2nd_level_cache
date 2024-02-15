using EasyCaching_Interceptor.Domain;
using EasyCaching_Interceptor.Persistence;

namespace EasyCaching_Interceptor.Services;

public class UserService(AppDbContext context)
{
    public IQueryable<User> GetUsers()
    {
        return context.Users.AsQueryable();
    }
}