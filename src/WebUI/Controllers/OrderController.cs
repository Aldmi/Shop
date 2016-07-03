using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Entities;
using Domain.Interfaces;
using WebGrease.Css.Extensions;
using WebUI.ViewModel;

namespace WebUI.Controllers
{
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        // GET: Order
        public ActionResult Index()
        {
            var orders = _unitOfWork.Orders.Get().Include(item => item.Items);

            var ordersVm = new List<OrderViewModel>();
            foreach (var order in orders)
            {
                var item = new OrderViewModel {Number = order.Id, DeliveryAddress = order.DeliveryAddress};
                foreach (OrderItem orderItem in order.Items)
                {
                    item.OrderItems.Add(new OrderViewModel.Line {Product = orderItem.Product, Quantity = orderItem.Quantity});                  
                }

                item.TotalPrice = item.OrderItems.Sum(i => (i.Quantity * i.Product.UnitPrice));
                ordersVm.Add(item);
            }

            ViewBag.SumPriceOrder = ordersVm.Sum(i => i.TotalPrice);

            return View(ordersVm);
        }
    }
}