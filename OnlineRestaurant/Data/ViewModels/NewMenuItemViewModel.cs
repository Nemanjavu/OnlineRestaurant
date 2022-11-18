using OnlineRestaurant.Models;
using System.ComponentModel.DataAnnotations;

namespace OnlineRestaurant.Data.ViewModels
{
    public class NewMenuItemViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Menu Item name")]
        [Required(ErrorMessage = "Name is required")]
        public string MenuName { get; set; }

        [Display(Name = "Menu Item description")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Display(Name = "Price in €")]
        [Required(ErrorMessage = "Price is required")]
        public double Price { get; set; }

        [Display(Name = "Menu Item photo URL")]
        [Required(ErrorMessage = "Menu Item photo URL is required")]
        public string ImageUrl { get; set; }


        [Display(Name = "Select a menu category")]
        [Required(ErrorMessage = "Menu category is required")]
        public int CategoryId { get; set; }
    }
}
