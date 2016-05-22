using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WebUI.Security
{
    public class AdminAttribute : AuthorizeAttribute
    {
        public const string AdminRoleName = "admin";

        public AdminAttribute()
        {
            Roles = AdminRoleName;
        }
    }
}
