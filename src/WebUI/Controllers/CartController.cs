using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain;
using Domain.Services;

namespace WebUI.Controllers
{
    public class CartController : Controller
    {
		private ICartService _cartService;
		private IUnitOfWork _unitOfWork;

		public CartController(ICartService cartService, IUnitOfWork unitOfWork)
		{
			_cartService = cartService;
			_unitOfWork = unitOfWork;
		}

        // GET: Cart
        public ActionResult Index()
        {
            return View(_cartService.Get());
        }

		[HttpPost]
		public RedirectToRouteResult Add(int productId, string returnUrl)
		{
			var sale = new CartModel(_cartService, _unitOfWork);
			sale.Add(productId, 1);

			return RedirectToAction("Index", new { returnUrl });
		}


		public PartialViewResult Summary()
		{
			return PartialView(_cartService.Get());
		}
    }
}