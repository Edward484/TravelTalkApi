using System;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using TravelTalkApi.Auth.Policies.PostAuthorPolicy;
using TravelTalkApi.Auth.Policies.TopicAuthorPolicy;
using TravelTalkApi.Constants;
using TravelTalkApi.Data;
using TravelTalkApi.Entities;
using TravelTalkApi.Entities.Constants;
using TravelTalkApi.Repositories;
using TravelTalkApi.Services;
using TravelTalkApi.Services.AdminService;
using TravelTalkApi.Services.NotificationService;
using TravelTalkApi.Services.PostService;
using TravelTalkApi.Services.UserService;
using TravelTalkApi.Utilities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Configure services

//Path to the db
var folder = Environment.SpecialFolder.LocalApplicationData;
var path = Environment.GetFolderPath(folder);
var dbPath = System.IO.Path.Join(path, "travelTalkApi.db");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite($"Data Source={dbPath}"));

var allowedOrigins = "allowed_origins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: allowedOrigins,
        policyBuilder => { policyBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().Build(); });
});


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
builder.Services.AddScoped<ITopicAuthorPolicy, TopicAuthorPolicy>();
builder.Services.AddScoped<IPostAuthorPolicy, PostAuthorPolicy>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IAdminService, AdminService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(allowedOrigins);

app.UseAuthorization();

app.MapControllers();

app.Use(async (context, next) =>
{
    context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
    context.Response.Headers.Add("Access-Control-Allow-Methods", "POST, GET, OPTIONS, PUT, HEAD, DELETE, PATCH");
    await next();
});


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