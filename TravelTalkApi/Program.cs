using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using TravelTalkApi.Auth.Policies.TopicAuthorPolicy;
using TravelTalkApi.Constants;
using TravelTalkApi.Data;
using TravelTalkApi.Entities;
using TravelTalkApi.Entities.Constants;
using TravelTalkApi.Repositories;
using TravelTalkApi.Services;
using TravelTalkApi.Services.NotificationService;
using TravelTalkApi.Services.UserService;
using TravelTalkApi.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Configure services

//Path to the db
var folder = Environment.SpecialFolder.LocalApplicationData;
var path = Environment.GetFolderPath(folder);
var dbPath = System.IO.Path.Join(path, "travelTalkApi.db");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite($"Data Source={dbPath}"));


builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();


builder.Services.AddAuthentication(auth =>
    {
        auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        auth.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer("AuthScheme", options =>
    {
        options.SaveToken = true;
        options.TokenValidationParameters = AuthConfig.JWTValidationConfig;
        options.Events = new JwtBearerEvents()
        {
            OnAuthenticationFailed = context =>
            {
                if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                {
                    context.Response.Headers.Add("Token-Expired", "true");
                }

                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy(RoleType.Admin,
        policy => policy.RequireRole(RoleType.Admin).RequireAuthenticatedUser().AddAuthenticationSchemes("AuthScheme")
            .Build());
    opt.AddPolicy(RoleType.User,
        policy => policy.RequireRole(RoleType.User).RequireAuthenticatedUser().AddAuthenticationSchemes("AuthScheme")
            .Build());
});


builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
builder.Services.AddScoped<SeedDb>();
builder.Services.AddSingleton<JWTUtils>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<ITopicAuthorPolicy, TopicAuthorPolicy>();
builder.Services.AddSingleton<INotificationService, NotificationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

try
{
    using (var serviceScope = app.Services.CreateScope())
    {
        var services = serviceScope.ServiceProvider;
        var seedService = services.GetRequiredService<SeedDb>();
        seedService.SeedRoles().Wait();
    }
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}

app.Run();