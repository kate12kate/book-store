using EBook.Domain;
using EBook.Domain.Identity;
using EBook.Repository;
using EBook.Repository.Implementation;
using EBook.Repository.Interface;
using EBook.Service.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Stripe;
using System;

namespace EBook.Web
{
    public class Startup
    {
        private EmailSettings emailSettings;

        public Startup(IConfiguration configuration)
        {
            emailSettings = new EmailSettings();
            Configuration = configuration;
            Configuration.GetSection("EmailSettings").Bind(emailSettings);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(
                    Environment.GetEnvironmentVariable("BOOKSTORE_DBCONN")));

            services.AddIdentity<EShopAppUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
            services.AddScoped(typeof(IOrderRepository), typeof(OrderRepository));

            //services.AddScoped<EmailSettings>(es => emailSettings);
            //services.AddScoped<IEmailService, EmailService>(email => new EmailService(emailSettings));
            //services.AddScoped<IBackgroundEmailSender, BackgroundEmailSender>();
            //services.AddHostedService<EmailScopedHostedService>();

            services.Configure<StripeSettings>(Configuration.GetSection("Stripe"));


            services.AddTransient<IBookService, Service.Implementation.BookService>();
            services.AddTransient<IShoppingCartService, Service.Implementation.ShoppingCartService>();
            services.AddTransient<IOrderService, Service.Implementation.OrderService>();
            services.AddTransient<IUserService, Service.Implementation.UserService>();



            services.Configure<StripeSettings>(Configuration.GetSection("Stripe"));

            services.AddControllersWithViews()
               .AddNewtonsoftJson(options =>
               options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
           );
            services.AddRazorPages();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });

            services.AddControllers();
        }
        

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            StripeConfiguration.SetApiKey(Configuration.GetSection("Stripe")["SecretKey"]);
            //using (var scope = app.ApplicationServices.CreateScope())
            //{
            //    var services = scope.ServiceProvider;

            //    var context = services.GetRequiredService<ApplicationDbContext>();
            //    if (context.Database.GetPendingMigrations().Any())
            //    {
            //        context.Database.Migrate();
            //    }
            //}

            using var scope = app.ApplicationServices.CreateAsyncScope();
            using var db = scope.ServiceProvider.GetService<ApplicationDbContext>();
            db.Database.MigrateAsync();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
