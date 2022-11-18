using OnlineRestaurant.Data.Cart;
using System.ComponentModel.DataAnnotations;

namespace OnlineRestaurant.Data.ViewModels
{
    public class ShoppingCartViewModel
    {
        public ShoppingCart ShoppingCart { get; set; }
        public double ShoppingCartTotal { get; set; }
        

    }
}
