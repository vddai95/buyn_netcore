using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using byin_netcore_transver;
using byin_netcore_business.Interfaces;
using caching_componant.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using byin_netcore_business.UseCases.UserBusiness;
using byin_netcore_business.UseCases.UserBusiness.Authorization;
using byin_netcore_business.UseCases.RoleBusiness;
using byin_netcore_business.UseCases.RoleBusiness.Authorization;
using byin_netcore_business.UseCases.AuthorizationBusiness;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;
using byin_netcore_business.UseCases.ProductBusiness.Authorization;
using byin_netcore_business.UseCases.ProductBusiness;
using AutoMapper;
using byin_netcore_data.AutoMapper;
using BL = byin_netcore_business.Entity;
using DL = byin_netcore_data.Model;
using byin_netcore_data.BusinessImplementation;
using byin_netcore_data.Interfaces;
using byin_netcore_data.EntityRepository;
using google_storage_componant;
using google_storage_componant.Configuration;
using byin_netcore_business.UseCases.FileBusiness;

namespace byin_netcore
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
            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure strongly typed settings objects for redis
            var redisConfigSection = Configuration.GetSection("RedisConfiguration");
            services.Configure<RedisConfiguration>(redisConfigSection);

            // configure strongly typed settings objects for google cloud storage
            var googleConfigSection = Configuration.GetSection("GoogleStorageConfiguration");
            services.Configure<GoogleStorageConfiguration>(googleConfigSection);

            // configure database
            services.AddDbContext<DL.MyContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            //services.AddControllers(options =>
            //    options.Filters.Add(new HttpResponseExceptionFilter()));

            services.AddControllers();

            services.AddIdentityCore<IdentityUser>(opt =>
            {
                opt.Password.RequiredLength = 8;
                opt.Password.RequireDigit = true;
                opt.Password.RequireLowercase = true;
                opt.Password.RequireUppercase = true;
                opt.Password.RequireNonAlphanumeric = true;

                opt.User.RequireUniqueEmail = true;
                opt.SignIn.RequireConfirmedEmail = true;
            }).AddRoles<IdentityRole>().AddDefaultTokenProviders().AddEntityFrameworkStores<DL.MyContext>();
            services.Configure<DataProtectionTokenProviderOptions>(o =>
                o.TokenLifespan = TimeSpan.FromHours(3)
            );
            // ===== Configure Identity =======
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "auth_cookie";
                options.Cookie.SameSite = SameSiteMode.None;
                options.LoginPath = new PathString("/api/contests");
                options.AccessDeniedPath = new PathString("/api/contests");

                // Not creating a new object since ASP.NET Identity has created
                // one already and hooked to the OnValidatePrincipal event.
                // See https://github.com/aspnet/AspNetCore/blob/5a64688d8e192cacffda9440e8725c1ed41a30cf/src/Identity/src/Identity/IdentityServiceCollectionExtensions.cs#L56
                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return Task.CompletedTask;
                };
            });

            services.AddAuthentication(o =>
            {
                o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                    options.Events = new JwtBearerEvents()
                    {
                        OnAuthenticationFailed = context =>
                        {
                            var exception = context.Exception;
                            return Task.CompletedTask;
                        }
                    };
                });
            services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
            });

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IUserBusiness, UserBusiness>();
            services.AddTransient<IAuthorizationHandler, UserAuthorizationHandler>();
            services.AddScoped<IRoleBusiness, RoleBusiness>();
            services.AddTransient<IAuthorizationHandler, RoleAuthorizationHandler>();
            services.AddScoped<IAuthorizationBusiness, AuthorizationBusiness>();
            services.AddScoped<IApplicationUser, ApplicationUser>();

            services.AddScoped<IEntityRepository<DL.Product>, EntityRepository<DL.Product>>();
            services.AddScoped<IEntityRepository<DL.FilePath>, EntityRepository<DL.FilePath>>();
            services.AddScoped<IEntityRepository<DL.Order>, EntityRepository<DL.Order>>();
            services.AddScoped<IEntityRepository<DL.OrderEntity>, EntityRepository<DL.OrderEntity>>();
            services.AddScoped<IEntityRepository<DL.ProductCategory>, EntityRepository<DL.ProductCategory>>();
            services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();

            services.AddTransient<IRepository<BL.Product>, Repository<BL.Product, DL.Product>>();
            services.AddTransient<IAuthorizationHandler, ProductCategoryAuthorizationHandler>();
            services.AddTransient<IAuthorizationHandler, ProductAuthorizationHandler>();
            services.AddScoped<IProductBusiness, ProductBusiness>();

            services.AddSingleton<ICloudStorage, GoogleCloudStorage>();
            services.AddScoped<IFileBusiness, FileBusiness>();
            services.AddScoped<IFilePathRepository, FilePathRepository>();
            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseExceptionHandler("/ErrorLocal");
            }
            else
            {
                //app.UseExceptionHandler("/Error");
                app.UseExceptionHandler("/ErrorProd");
            }

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
