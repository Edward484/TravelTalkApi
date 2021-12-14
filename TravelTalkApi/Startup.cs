using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using TravelTalkApi.Data;
using TravelTalkApi.Repositories.CategoryRepository;
using TravelTalkApi.Repositories.NotificationRepository;
using TravelTalkApi.Repositories.PostRepository;
using TravelTalkApi.Repositories.TopicRepository;

namespace TravelTalkApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TravelTalk", Version = "v1" });
            });
            
            
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer("Server=localhost\\MSSQLSERVER01;Database=master;Trusted_Connection=True;"));
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<INotificationRepository, NotificationRepository>();
            services.AddTransient<IPostRepository, PostRepository>();
            services.AddTransient<ITopicRepository, TopicRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Lab2_DAW_Sgr16 v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}