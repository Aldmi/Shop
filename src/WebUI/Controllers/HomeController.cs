using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Domain.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PagedList;
using WebUI.Security;

namespace WebUI.Controllers
{
	public class HomeController : Controller
	{
        private readonly IUnitOfWork _unitOfWork;

	    public HomeController(IUnitOfWork unitOfWork)
	    {
	        _unitOfWork = unitOfWork;
	    }


	    // GET: Products
        public async Task<ActionResult> Index(int? page)
        {
            var qProduct = await _unitOfWork.Products.Get().OrderBy(p => p.Id).ToListAsync();
            var pageNumber = page ?? 1;
            var onePageOfProducts = qProduct.ToPagedList(pageNumber, 2);

            return View(onePageOfProducts);
        }


        // GET: Products/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var product = await _unitOfWork.Products.GetAsync(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
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
            //Создание и добавление нового пользователя (можно закоментить,т.к. Админ уже добавлен)-------------------------------------------------------------
            const string adminLogin = "admin";
            const string admineRole = AdminAttribute.AdminRoleName;
            const string password = "1234567";

            var userManager = HttpContext.GetOwinContext().GetUserManager<ShopUserManager>();
            var roleManager = HttpContext.GetOwinContext().GetUserManager<RoleManager<ShopRole>>();

            if (userManager.FindByName(adminLogin) == null)
            {
                var user = new ShopUser { UserName = adminLogin };
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
            //--------------------------------------------------------------------------------------------------
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