using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using IdentityServer4;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Caching.Distributed;
using WSAuthService.Authentication;


namespace WSAuthService
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddIdentityServer()
                .AddTemporarySigningCredential()
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApiResource())
                .AddInMemoryClients(Config.GetClients())
                .AddTestUsers(Config.GetUsers());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();
            app.UseCookieAuthentication(new CookieAuthenticationOptions {
                AuthenticationScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme,
                AutomaticAuthenticate = false,
                AutomaticChallenge = false
            });

            app.UseGoogleAuthentication(new GoogleOptions {
                AuthenticationScheme = "Google",
                DisplayName = "Login With Google",
                SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme,
                ClientId = "910648818463-vfuocte751i8ntmnsubbfaa7s31chsse.apps.googleusercontent.com",
                ClientSecret = "lTlc-5UWoh_hXHe9PoX0akC_"
            });

            var schemeName = "AzureAD";
            var dataProtectionProvider = app.ApplicationServices.GetRequiredService<IDataProtectionProvider>();
            var distributedCache = app.ApplicationServices.GetRequiredService<IDistributedCache>();

            var dataProtector = dataProtectionProvider.CreateProtector(
                typeof(OpenIdConnectMiddleware).FullName,
                typeof(string).FullName, schemeName,
                "v1");

            var dataFormat = new CachedPropertiesDataFormat(distributedCache, dataProtector);

            ///
            /// Azure AD Configuration
            /// 
            

            app.UseOpenIdConnectAuthentication(new OpenIdConnectOptions {
                AuthenticationScheme = schemeName,
                DisplayName = "Pritam AzureAD",
                SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme,
                ClientId = "2beaf9f4-9eeb-4219-983e-ae8e454b70e6",
                Authority = $"https://login.microsoftonline.com/common/",
                ResponseType = OpenIdConnectResponseType.IdToken,
                StateDataFormat = dataFormat,
                TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuer = false,
                    AuthenticationType = "idsrv.external"

                }
            });

            //app.UseOpenIdConnectAuthentication(new OpenIdConnectOptions {
            //    AuthenticationScheme = schemeName,
            //    DisplayName = "Anil AzureAD",
            //    SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme,
            //    ClientId = "50b223a5-8242-4c57-91c6-ef446f9dbe08",
            //    Authority = $"https://login.microsoftonline.com/ded39aca-1c7d-456a-bc06-5c8088ce1624",
            //    ResponseType = OpenIdConnectResponseType.IdToken,
            //    StateDataFormat = dataFormat
            //});

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
