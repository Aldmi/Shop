using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Domain.Entities;
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

            return Json(new
            {
                Total = _cartService.Get().GetTotalAmount()
            });
        }

        [HttpPost]
        public ActionResult QuantityAdd(int id)
        {
            var sale = new CartModel(_cartService, _unitOfWork);
            sale.Add(id, 1);

            OrderItem orderItemProduct = _cartService.Get().Lines.FirstOrDefault(line => line.Product.Id == id);
            if (orderItemProduct != null)
            {
                return Json(new
                {
                    Quantity = orderItemProduct.Quantity,
                    Total = _cartService.Get().GetTotalAmount()
                });
            }
            return null;
        }


        [HttpPost]
        public ActionResult QuantityRemove(int id)
        {
            var sale = new CartModel(_cartService, _unitOfWork);
            sale.Remove(id, 1);

            OrderItem orderItemProduct = _cartService.Get().Lines.FirstOrDefault(line => line.Product.Id == id);
            if (orderItemProduct != null)
            {
                return Json(new
                {
                    Quantity = orderItemProduct.Quantity,
                    Total = _cartService.Get().GetTotalAmount(),
                    IdProd= id
                });
            }
            return null;
        }


        [HttpPost]
        public ActionResult ClearLine(int id)
        {
            var sale = new CartModel(_cartService, _unitOfWork);
            sale.Clear(id);

            var cart = _cartService.Get();
            return PartialView("_CartTablePartial", cart);
        }

        [HttpPost]
        public JsonResult CreateOrder(Address address)
        {
            var cart = _cartService.Get();
            var order= new Order {DeliveryAddress = address, Items = (ICollection<OrderItem>) cart.Lines };
            _unitOfWork.Orders.Insert(order);
            _unitOfWork.SaveAsync();
            return Json(new
            {
                Message= "ЗАКАЗ по адерссу " + address.AdressLine + " ОФОРМЛЕН"
            });
        }




        public PartialViewResult Summary()
        {
            return PartialView(_cartService.Get());
        }

    }
}