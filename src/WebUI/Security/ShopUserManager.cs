using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using WebUI.Models;

namespace WebUI.Security
{
    public class ShopUserManager : UserManager<ShopUser>
    {
        public ShopUserManager(IUserStore<ShopUser> store)
            : base(store)
        {
        }

        // this method is called by Owin therefore best place to configure your User Manager
        public static ShopUserManager Create(IdentityFactoryOptions<ShopUserManager> options, IOwinContext context)
        {
            var manager = new ShopUserManager(
                new UserStore<ShopUser>(context.Get<ShopContext>()));

            // optionally configure your manager
            // ...

            return manager;
        }
    }
}
