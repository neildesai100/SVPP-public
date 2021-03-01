using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SVPP.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using SVPP.Services;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Authorization;
using SVPP.Authorization;

namespace SVPP
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
            services.AddControllersWithViews();
            services.AddDbContext<DatabaseContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DatabaseContext")));
            services.Configure<IdentityOptions>(options =>
            {
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(20);
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(20);

                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            services.AddMvc();

            services.AddDefaultIdentity<IdentityUser>(
                    options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddDefaultUI()
                .AddEntityFrameworkStores<SVPPContext>();

            services.AddTransient<IEmailSender, EmailSender>();
            services.Configure<AuthMessageSenderOptions>(Configuration);

            services.AddRazorPages();

            services.AddAuthorization(options =>

            {

                options.AddPolicy("loggedinpolicy",

                    builder => builder.RequireRole("Admin", "Partner", "Nonprofit"));

                options.AddPolicy("allpolicy",

                    builder => builder.RequireRole("Admin", "Partner", "Nonprofit", "Guest"));

                options.AddPolicy("adminonlypolicy",

                    builder => builder.RequireRole("Admin"));

                options.AddPolicy("guestpolicy",

                    builder => builder.RequireRole("Guest"));

                options.AddPolicy("adminandpartnerpolicy",

                    builder => builder.RequireRole("Admin", "Partner"));

                options.AddPolicy("adminpartnerguestpolicy",

                    builder => builder.RequireRole("Admin", "Partner", "Guest"));

                options.AddPolicy("adminandnonprofitpolicy",

                    builder => builder.RequireRole("Admin", "Nonprofit"));

            });

            services.AddScoped<IAuthorizationHandler, NonprofitIsOwnerAuthorizationHandler>();

            services.AddScoped<IAuthorizationHandler, PartnerIsOwnerAuthorizationHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            app.UseRouting();

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

            CreateRoles(serviceProvider).Wait(); // seed db with admin users and roles
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
        //this method seeds the database with specified roles and 2 admin users
        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            string[] roles = { "Admin", "Nonprofit", "Partner", "Guest" }; // if ever want to delete one of these default roles, have to delete it here or db will automatically create it upon startup

            IdentityResult roleResult;
            foreach (var role in roles)
            {
                var roleExist = await RoleManager.RoleExistsAsync(role);
                if (!roleExist)
                {
                    // create the roles and seed them into the database
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(role));
                }

            }

            // seed database with admin users 
            string superuser1PWD = Configuration["SuperUser1PWD"]; //Secret manager
            string superuser2PWD = Configuration["SuperUser2PWD"];

            // check exist ??

            var _user1 = await UserManager.FindByEmailAsync("leigh@svppittsburgh.org");
            var _user2 = await UserManager.FindByEmailAsync("LCamerlengo@fivestardev.com");

            if (_user1 == null) //if not, create default admin one
            {
                IdentityUser superuser1 = new IdentityUser
                {
                    UserName = "leigh@svppittsburgh.org",

                    Email = "leigh@svppittsburgh.org",

                    EmailConfirmed = true,
                };

                IdentityResult createSuperuser = await UserManager.CreateAsync(superuser1, superuser1PWD);
                if (createSuperuser.Succeeded)
                {
                    // tie new user to role of admin
                    await UserManager.AddToRoleAsync(superuser1, "Admin");
                }
            }

            if (_user2 == null) // default admin two
            {
                IdentityUser superuser2 = new IdentityUser

                {
                    UserName = "LCamerlengo@fivestardev.com",

                    Email = "LCamerlengo@fivestardev.com",

                    EmailConfirmed = true,
                };

                IdentityResult createSuperuser2 = await UserManager.CreateAsync(superuser2, superuser2PWD);
                if (createSuperuser2.Succeeded)
                {
                    // tie new user to role of admin

                    await UserManager.AddToRoleAsync(superuser2, "Admin");
                }

            }
        }
    }
}
