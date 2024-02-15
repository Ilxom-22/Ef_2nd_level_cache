using Bogus;
using EasyCaching_Interceptor.Domain;
using EasyCaching_Interceptor.Persistence;

namespace EasyCaching_Interceptor.SeedData;

public static class SeedDataExtensions
{
    public static async ValueTask SeedUsersAsync(this IServiceProvider serviceProvider)
    {
        var appDbContext = serviceProvider.GetRequiredService<AppDbContext>();

        var usersFaker = new Faker<User>()
            .RuleFor(user => user.Id, Guid.NewGuid)
            .RuleFor(user => user.FirstName, data => data.Name.FirstName())
            .RuleFor(user => user.LastName, data => data.Name.LastName())
            .RuleFor(user => user.EmailAddress, data => data.Person.Email);

        await appDbContext.Users.AddRangeAsync(usersFaker.Generate(50));
        await appDbContext.SaveChangesAsync();
    }
}