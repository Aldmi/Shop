using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using WebUI.Security;

namespace WebUI.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}


        [HttpGet]
	    public ActionResult Login()
	    {
            const string adminLogin = "admin";
            const string admineRole = AdminAttribute.AdminRoleName;
            const string password = "1234567";

            var userManager = HttpContext.GetOwinContext().GetUserManager<ShopUserManager>();
            var roleManager = HttpContext.GetOwinContext().GetUserManager<RoleManager<ShopRole>>();

            if (userManager.FindByName(adminLogin) == null)
            {
                var user = new ShopUser {UserName = adminLogin};
                var result = userManager.Create(user, password);
                if (result.Succeeded)
                {
                    if (!roleManager.RoleExists(admineRole))
                    {
                        roleManager.Create(new ShopRole(admineRole));
                    }

                    userManager.AddToRole(userManager.FindByName(adminLogin).Id, admineRole);
                }
            }

            return View();
	    }


        //
        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var userManager = HttpContext.GetOwinContext().GetUserManager<ShopUserManager>();
                var authenticationManager = HttpContext.GetOwinContext().Authentication;

                var user = await userManager.FindAsync(model.Login, model.Password);
                if (user == null)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль.");
                }
                else
                {
                    var claim = await userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                    authenticationManager.SignOut();
                    authenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, 
                    claim);

                    if (string.IsNullOrEmpty(returnUrl))
                        return RedirectToAction("Index", "Home");

                    return Redirect(returnUrl);
                }
            }
            ViewBag.returnUrl = returnUrl;
            return View(model);
        }


        public ActionResult Logout()
        {
            var authenticationManager = HttpContext.GetOwinContext().Authentication;

            authenticationManager.SignOut();

            return RedirectToAction("Index");
        }
    }
}