using System.Web.Mvc;
using Domain.Interfaces;
using Domain.Models;

namespace WebUI.Controllers
{
    public class CartController : Controller
    {
		private readonly ICartService _cartService;
		private readonly IUnitOfWork _unitOfWork;

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
		public ActionResult Add(int id)
		{
			var sale = new CartModel(_cartService, _unitOfWork);
			sale.Add(id, 1);

            return Json(new { Total = _cartService.Get().GetTotalAmount() });
        }


		public PartialViewResult Summary()
		{
			return PartialView(_cartService.Get());
		}

    }
}