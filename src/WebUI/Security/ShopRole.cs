using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WebUI.Security
{
    public class ShopRole : IdentityRole
    {
        public ShopRole() : base() { }
        public ShopRole(string name) : base(name) { }
        
        // extra properties here 
    }
}
