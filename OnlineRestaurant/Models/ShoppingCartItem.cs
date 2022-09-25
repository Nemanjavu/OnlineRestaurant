using System.ComponentModel.DataAnnotations;

namespace OnlineRestaurant.Models
{
    public class ShoppingCartItem
    {
        [Key]
        public int Id { get; set; }

        public MenuItem MenuItem { get; set; }
        public int Amount { get; set; }

        public string ShoppingCartId { get; set; }
    }
}
