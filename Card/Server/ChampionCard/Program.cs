using ChampionCard.Middleware;
using Microsoft.AspNetCore.HttpOverrides;
using Server;
using ChampionCard.ExceptionHandler;
using StackExchange.Redis;
using ChampionCard.Models.Game;
using ChampionCard.Models.Global;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContextPool<GlobalContext>(options =>
                {
#if DEBUG
                    options.UseMySQL(builder.Configuration.GetConnectionString("DatabaseConnection_Global_Dev")!);
#elif RELEASE || MAINTENANCE
                    options.UseMySQL(builder.Configuration.GetConnectionString("DatabaseConnection_Global"));
#endif
                });

builder.Services.AddDbContextPool<GameContext>(options =>
                {
#if DEBUG
                    options.UseMySQL(builder.Configuration.GetConnectionString("DatabaseConnection_Game_Dev")!);
#elif RELEASE || MAINTENANCE
                    options.UseMySQL(builder.Configuration.GetConnectionString("DatabaseConnection_Game"));
#endif
                });

#region Redis �߰�
#if DEBUG
builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnection_Dev")!));
#elif RELEASE 
builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnection")!));
#endif
#endregion
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddSingleton<DataTableManager>();

builder.Services.AddAuthorization();

//#if RELEASE || DEBUG
//builder.Services.AddControllers(config =>
//{
//    var policy = new AuthorizationPolicyBuilder()
//                     .RequireAuthenticatedUser()
//                     .Build();
//    config.Filters.Add(new AuthorizeFilter(policy));
//});
//#endif

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddControllers();
builder.Services.AddControllers().AddNewtonsoftJson();

var app = builder.Build();

//nginx�� ��� ����� ����
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});
//CustomException ����� �����߰�
app.UseExceptionHandler("/Error");

app.UseHsts();
app.UseMiddleware<MaintenanceMiddleware>();
app.UseMiddleware<TokenValidateMiddleware>();
app.UseMiddleware<PacketCheckMiddleware>();

app.MapControllers();
app.Run();
