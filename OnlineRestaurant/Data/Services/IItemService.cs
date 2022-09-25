using OnlineRestaurant.Data.Base;
using OnlineRestaurant.Data.ViewModels;
using OnlineRestaurant.Models;

namespace OnlineRestaurant.Data.Services
{
    public interface IItemService:IEntityBaseRepository<MenuItem>
    {
        Task<MenuItem> GetMenuItemByIdAsync(int id);
        Task<NewMenuItemDropdownsViewModel> GetNewMenuItemDropdownsValues();
        Task AddNewMenuItemAsync(NewMenuItemViewModel data);
        Task UpdateMenuItemAsync(NewMenuItemViewModel data);
    }
}
