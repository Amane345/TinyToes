using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using TinyToes.Models;


namespace TinyToes
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
            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddControllersWithViews().AddNewtonsoftJson();
            //services.AddDbContext<AtDbContext>(options =>
               // options.UseSqlServer(Configuration.GetConnectionString("AtDbContext")));
            if(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")=="Production")
                services.AddDbContext<AtDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("tinytoesDb")));
            else
                services.AddDbContext<AtDbContext>(options =>
                                options.UseSqlServer(Configuration.GetConnectionString("AtDbContext")));

            services.BuildServiceProvider().GetService<AtDbContext>().Database.Migrate();
            services.AddIdentity<User, IdentityRole>(options => {
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<AtDbContext>()
             .AddDefaultTokenProviders();


            /*services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<AtDbContext>();*/

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IClothesRepository, ClothesRepository>();
            services.AddScoped<ShoppingCart>(sc => ShoppingCart.GetCart(sc));
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddHttpContextAccessor();
            services.AddSession();
            services.AddRazorPages();
           
           

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IServiceProvider serviceProvider)
        {
             app.UseDeveloperExceptionPage();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
               


                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                
            });
           AtDbContext.CreateAdminUser(app.ApplicationServices).Wait();


        }
    }
}
