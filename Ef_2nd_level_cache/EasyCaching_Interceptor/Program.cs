using EasyCaching_Interceptor.Persistence;
using EasyCaching_Interceptor.SeedData;
using EasyCaching_Interceptor.Services;
using EFCoreSecondLevelCacheInterceptor;
using MessagePack.Resolvers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<UserService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Caching Registration

const string providerName = "DemoMemoryCache";

builder.Services.AddEFSecondLevelCache(options =>
    options.UseEasyCachingCoreProvider(providerName, isHybridCache: false)
        .DisableLogging(false)
        .UseDbCallsIfCachingProviderIsDown(TimeSpan.FromMinutes(1)));

builder.Services.AddEasyCaching(option =>
{
    option.UseRedis(config =>
        {
            config.DBConfig.AllowAdmin = true;
            config.DBConfig.SyncTimeout = 10000;
            config.DBConfig.AsyncTimeout = 10000;
            config.DBConfig.Endpoints.Add(new EasyCaching.Core.Configurations.ServerEndPoint("127.0.0.1", 6379));
            config.EnableLogging = true;
            config.SerializerName = "Pack";
            config.DBConfig.ConnectionTimeout = 10000;
        }, providerName)
        .WithMessagePack(so =>
            {
                so.EnableCustomResolver = true;
                so.CustomResolvers = CompositeResolver.Create(
                    NativeDateTimeResolver.Instance,
                    ContractlessStandardResolver.Instance,
                    StandardResolverAllowPrivate.Instance
                );
            },
            "Pack");
});

builder.Services.AddDbContext<AppDbContext>((serviceProvider, options) =>
{
    options.UseNpgsql("Host=localhost;Port=5432;Database=EasyCachingTest;Username=postgres;Password=postgres")
        .AddInterceptors(serviceProvider.GetRequiredService<SecondLevelCacheInterceptor>());
});

#endregion

var app = builder.Build();

await app.Services.CreateScope().ServiceProvider.SeedUsersAsync();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

await app.RunAsync();