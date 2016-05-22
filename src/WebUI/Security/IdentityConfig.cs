using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using WebUI.Models;

namespace WebUI.Security
{
    public class IdentityConfig
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(() => new ShopContext());
            app.CreatePerOwinContext<ShopUserManager>(ShopUserManager.Create);
            app.CreatePerOwinContext<RoleManager<ShopRole>>((options, context) =>
                new RoleManager<ShopRole>(new RoleStore<ShopRole>(context.Get<ShopContext>())));

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Home/Login"),
            });
        }
    }
}
