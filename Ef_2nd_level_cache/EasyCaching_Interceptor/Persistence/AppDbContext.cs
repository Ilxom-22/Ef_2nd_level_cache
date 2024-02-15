using EasyCaching_Interceptor.Domain;
using Microsoft.EntityFrameworkCore;

namespace EasyCaching_Interceptor.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
}