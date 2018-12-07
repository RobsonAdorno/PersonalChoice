using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
using System.Web.Helpers;

[assembly: OwinStartupAttribute(typeof(ClearChoice.Startup))]
namespace ClearChoice
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //Use esse metodo para criar as especificações de configuração da autenticação
            app.UseCookieAuthentication(new CookieAuthenticationOptions {
            AuthenticationType = "ApplicationCookie",
            LoginPath = new PathString("/Home/Login")


            });

            AntiForgeryConfig.UniqueClaimTypeIdentifier = "Login";

        }
    }
}
