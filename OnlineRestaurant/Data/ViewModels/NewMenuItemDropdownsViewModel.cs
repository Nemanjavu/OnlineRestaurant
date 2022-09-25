using OnlineRestaurant.Models;

namespace OnlineRestaurant.Data.ViewModels
{
    public class NewMenuItemDropdownsViewModel
    {
        public NewMenuItemDropdownsViewModel()
        {
            MenuCategories = new List<MenuCategory>();   
        }

        public List<MenuCategory> MenuCategories { get; set; }
        
    }
}
