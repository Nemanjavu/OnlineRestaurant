using Microsoft.EntityFrameworkCore;
using OnlineRestaurant.Data.Base;
using OnlineRestaurant.Data.ViewModels;
using OnlineRestaurant.Models;

namespace OnlineRestaurant.Data.Services
{
    public class ItemService : EntityBaseRepository<MenuItem>, IItemService
    {
        private readonly ApplicationDbContext _context;
        public ItemService(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddNewMenuItemAsync(NewMenuItemViewModel data)
        {
            var newMenuItem = new MenuItem()
            {
                MenuName = data.MenuName,
                Description = data.Description,
                Price = data.Price,
                ImageUrl = data.ImageUrl,
                CategoryId = data.CategoryId,

            };
            await _context.MenuItems.AddAsync(newMenuItem);
            await _context.SaveChangesAsync();
        }

        public async Task<MenuItem> GetMenuItemByIdAsync(int id)
        {
            var menuItemDetails = await _context.MenuItems
                .Include(c => c.MenuCategory)
                .FirstOrDefaultAsync(n => n.Id == id);

            return menuItemDetails;
        }

        public async Task<NewMenuItemDropdownsViewModel> GetNewMenuItemDropdownsValues()
        {
            var response = new NewMenuItemDropdownsViewModel()
            {

                MenuCategories = await _context.MenuCategories.OrderBy(n => n.Name).ToListAsync()

            };

            return response;
        }

        public async Task UpdateMenuItemAsync(NewMenuItemViewModel data)
        {
            var dbMeniIte = await _context.MenuItems.FirstOrDefaultAsync(n => n.Id == data.Id);

            if (dbMeniIte != null)
            {
                dbMeniIte.MenuName = data.MenuName;
                dbMeniIte.Description = data.Description;
                dbMeniIte.Price = data.Price;
                dbMeniIte.ImageUrl = data.ImageUrl;
                dbMeniIte.CategoryId = data.CategoryId;
                await _context.SaveChangesAsync();
            }
        }


    }
}
