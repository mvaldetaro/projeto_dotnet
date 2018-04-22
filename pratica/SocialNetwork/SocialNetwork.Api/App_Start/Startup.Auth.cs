using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;
using SocialNetwork.Api.Models;
using SocialNetwork.Api.Providers;
using SocialNetwork.Api.App_Start;
using System;


namespace SocialNetwork.Api
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public void ConfigureAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);


            OAuthOptions = new OAuthAuthorizationServerOptions()
            {
                Provider = new ApplicationOAuthProvider(),
                TokenEndpointPath = new PathString("/Token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1)
            };

            app.UseOAuthBearerTokens(OAuthOptions);
        }
    }
}