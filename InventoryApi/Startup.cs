using Microsoft.EntityFrameworkCore;

namespace InventoryApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // other configurations...

            services.AddDbContext<InventoryDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddControllers();
            services.AddCors(options =>
        {
            options.AddPolicy("AllowVueApp", builder =>
            {
                builder
                    //.WithOrigins("http://localhost:5174") // Update with your Vue.js app's URL
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                //.AllowCredentials();
            });
        });

            // Other configurations...
        }



        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // ...

            app.UseCors("AllowVueApp");


            app.UseRouting();
            app.UseAuthorization();

            // // Make sure this is placed after app.UseAuthorization() and app.UseRouting()
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        // other configurations...
    }

    // other methods...

}

