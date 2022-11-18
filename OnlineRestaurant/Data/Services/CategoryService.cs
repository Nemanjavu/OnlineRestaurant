using OnlineRestaurant.Data.Base;
using OnlineRestaurant.Models;

namespace OnlineRestaurant.Data.Services
{
    public class CategoryService : EntityBaseRepository<MenuCategory>, ICategoryService
    {
        public CategoryService(ApplicationDbContext context) : base(context)
        {
        }
    }
}
