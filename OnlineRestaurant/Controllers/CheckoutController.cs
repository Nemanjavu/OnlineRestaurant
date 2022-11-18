using Microsoft.AspNetCore.Mvc;
using Stripe;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineRestaurant.Models;
using OnlineRestaurant.Helpers;
using OnlineRestaurant.Data.ViewModels;
using OnlineRestaurant.Data.Cart;
using OnlineRestaurant.Data.Services;
using System.Security.Claims;

namespace OnlineRestaurant.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly IItemService _itemService;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrderService _orderService;
        private decimal ShoppingCartTotal;

        public CheckoutController(IItemService itemService, ShoppingCart shoppingCart, IOrderService orderService)
        {

            _itemService = itemService;
            _shoppingCart = shoppingCart;
            _orderService = orderService;
        }

        [TempData]
        public string? TotalAmount { get; set; }
        
        public async Task <IActionResult> Index()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            

            var response = new ShoppingCartViewModel()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()


            };

            ViewBag.cart = _shoppingCart.GetShoppingCartTotal();
            
            ViewBag.total = Math.Round(_shoppingCart.GetShoppingCartTotal(), 2)*100;
            ViewBag.total = Convert.ToInt64(ViewBag.total);
            long total = ViewBag.total;
            TotalAmount = total.ToString();

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userEmailAddress = User.FindFirstValue(ClaimTypes.Email);

            await _orderService.StoreOrderAsync(items, userId, userEmailAddress);
            await _shoppingCart.ClearShoppingCartAsync();

            
            return View();
        }
        public IActionResult Processing(string stripeToken, string stripeEmail)
        {
 

            var optionCust = new CustomerCreateOptions
            {
                Email = stripeEmail,
                
            };
            var serviceCust = new CustomerService();
            Customer customer = serviceCust.Create(optionCust);
            var optionsCharge = new ChargeCreateOptions
            {
                Amount = Convert.ToInt64(TempData["TotalAmount"]),
                Currency = "EUR",
                Description="Order Food amount",
                Source=stripeToken,
                ReceiptEmail=stripeEmail

            };
            var serviceCharge = new ChargeService();
            Charge charge = serviceCharge.Create(optionsCharge);
            if (charge.Status== "succeeded")
            {
                ViewBag.AmountPaid =Convert.ToDecimal(charge.Amount)%100/100+(charge.Amount)/100;
                ViewBag.Customer = customer.Name;
            }


            return View("OrderCompleted");


        }
    }
}