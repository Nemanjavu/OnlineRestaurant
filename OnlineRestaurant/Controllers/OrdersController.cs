using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineRestaurant.Data.Cart;
using OnlineRestaurant.Data.Services;
using OnlineRestaurant.Data.ViewModels;
using Stripe;
using System.Security.Claims;


namespace OnlineRestaurant.Controllers
{
    [Authorize]
    
    public class OrdersController : Controller
    {
        private readonly IItemService _itemService;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrderService _orderService;

        public OrdersController(IItemService itemService, ShoppingCart shoppingCart, IOrderService orderService)
        {

            _itemService = itemService;
            _shoppingCart = shoppingCart;
            _orderService = orderService;
        }

        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);

            var orders = await _orderService.GetOrdersByUserIdAndRoleAsync(userId, userRole);


            return View(orders);
        }

       

        public IActionResult ShoppingCart()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var response = new ShoppingCartViewModel()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
               
                
            };
            

            return View(response);
        }


        
            public async Task<IActionResult> AddItemToShoppingCart(int id)
        {
            var item = await _itemService.GetMenuItemByIdAsync(id);
            if(item != null)
            {
                _shoppingCart.AddItemToCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> RemoveItemToShoppingCart(int id)
        {
            var item = await _itemService.GetMenuItemByIdAsync(id);
            if (item != null)
            {
                _shoppingCart.RemoveItemFromCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> CompleteOrder()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userEmailAddress = User.FindFirstValue(ClaimTypes.Email);

            await _orderService.StoreOrderAsync(items, userId, userEmailAddress);
            await _shoppingCart.ClearShoppingCartAsync();

            return View("OrderCompleted");
        }

       



    }
}
