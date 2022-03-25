using System;
using System.Collections.Generic;
using ConcessionStandProject;
using Microsoft.AspNetCore.Mvc;

namespace PointOfSale.Controllers
{
    [Route("{controller}")]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        //gives access to the orders and products that are stored

        public OrderController(IOrderRepository orderRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }
        [HttpPost]
        public IActionResult AddItem(Order orderFromForm, string sku)
        {
            var order = _orderRepository.Find(orderFromForm.OrderId);

            var product = _productRepository.Find(Convert.ToInt32(sku));

            order.Add(product);

            _orderRepository.Update(order); //updates order in storage

            return Redirect($"order/{order.OrderId}");
            //go to order (controller) at this OrderId.

        }

        public IActionResult Index()
        {
            var order = new Order();
            _orderRepository.CreateOrder(order);
            return Redirect($"order/{order.OrderId}");
        }

        [Route("{orderId}")]
        public IActionResult Index(Guid orderId)
        {
            var order = _orderRepository.Find(orderId);
            ViewBag.Products = _productRepository.GetAllProducts();
            return View(order);
        }

        [HttpPost]
        [Route("generate-receipt")]
        public IActionResult GenerateReceipt(Order orderFromForm)
        {
            var order = _orderRepository.Find(orderFromForm.OrderId);
            order.Submit();
            _orderRepository.Update(order);
            return Redirect($"{order.OrderId}/Receipt");
        }    
        [Route("{orderId}/Receipt")]
        public IActionResult Receipt(Guid orderId)
        {
            var order = _orderRepository.Find(orderId);
            return View(order);
        }

        [Route("orderhistory")]
        public IActionResult OrderHistory()
        {
            
            var orders = _orderRepository.GetAllOrders();
            return View(orders);

        }
    }


}
