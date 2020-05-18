namespace MyRentals.Web
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Text;
    using MediatR;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.OpenApi.Models;
    using MyRentals.Persistence;
    using MyRentals.Service.Messages;
    using MyRentals.Web.Authorization;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
            this.AdminEmails = new HashSet<string>(configuration.GetSection("AdminEmails").Get<IEnumerable<string>>());
            var authSection = configuration.GetSection("Authentication");
            this.ClientSecret = authSection["ClientSecret"];
            this.ClientId = authSection["ClientId"];
        }

        private IConfiguration Configuration { get; }
        private ISet<string> AdminEmails { get; }
        private const string Authority = "https://accounts.google.com";
        private readonly string ClientSecret;
        private readonly string ClientId;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllersWithViews();

            services.AddWeb();
            services.AddPersistence();
            services.Configure<AdminEmailsSetting>(this.Configuration, o => { o.BindNonPublicProperties = true; });

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.Authority = Authority;
                o.Audience = ClientId;
                o.RequireHttpsMetadata = false;
                o.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(ClientSecret)),

                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            services.AddAuthorization(o =>
            {
                o.AddPolicy(Policies.Admin, p =>
                {
                    p.RequireAuthenticatedUser();
                    p.RequireClaim(ClaimTypes.Email, this.AdminEmails);
                });
                o.AddPolicy(Policies.RealtorOrAdmin, p =>
                {
                    p.RequireAuthenticatedUser();
                    p.AddRequirements(new RealtorOrAdminRequirement(this.AdminEmails));
                });
            });

            services.AddMediatR(typeof(GetApartments).Assembly);

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyRentals API", Version = "v1" });
                c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme()
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows()
                    {
                        Implicit = new OpenApiOAuthFlow()
                        {
                            AuthorizationUrl = new Uri("https://accounts.google.com/o/oauth2/auth"),
                            Scopes = new Dictionary<string, string> { {"email", "View Email"}, { "profile", "View Profile" } }
                        }, 
                    }
                });
                c.OperationFilter<OAuth2OperationFilter>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, MyRentalsContext myRentalsContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();
            app.UseAuthentication().UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                //c.OAuthClientId("implicit");
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.OAuthConfigObject.ClientId = ClientId;
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "dev");
                }
            });
            myRentalsContext.SeedDatabase();
        }
    }
}
