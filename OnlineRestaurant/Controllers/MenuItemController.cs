using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineRestaurant.Data;
using OnlineRestaurant.Data.Services;
using OnlineRestaurant.Data.ViewModels;

namespace OnlineRestaurant.Controllers
{
    public class MenuItemController : Controller
    {
        private readonly IItemService _service;

        public MenuItemController(IItemService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var allItems = await _service.GetAllAsync(n => n.MenuCategory);
            return View(allItems);
        }

        public async Task<IActionResult> Filter(string searchString)
        {
            var allMenuItems = await _service.GetAllAsync(n => n.MenuCategory);

            if (!string.IsNullOrEmpty(searchString))
            {
                //var filteredResult = allMovies.Where(n => n.Name.ToLower().Contains(searchString.ToLower()) || n.Description.ToLower().Contains(searchString.ToLower())).ToList();

                var filteredResultNew = allMenuItems.Where(n => string.Equals(n.MenuName, searchString, StringComparison.CurrentCultureIgnoreCase) || string.Equals(n.Description, searchString, StringComparison.CurrentCultureIgnoreCase)).ToList();

                return View("Index", filteredResultNew);
            }

            return View("Index", allMenuItems);
        }

        //GET: MenuItems/Details/1
        // [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var menuItemsDetail = await _service.GetMenuItemByIdAsync(id);
            return View(menuItemsDetail);
        }

        //GET: MenuItems/Create
        public async Task<IActionResult> Create()
        {
            var menuItemDropdownsData = await _service.GetNewMenuItemDropdownsValues();

            ViewBag.Categories = new SelectList(menuItemDropdownsData.MenuCategories, "Id", "Name");
            

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(NewMenuItemViewModel menuItem)
        {
            if (!ModelState.IsValid)
            {
                var menuItemDropdownsData = await _service.GetNewMenuItemDropdownsValues();

                ViewBag.Categories = new SelectList(menuItemDropdownsData.MenuCategories, "Id", "Name");
                
                return View(menuItem);
            }

            await _service.AddNewMenuItemAsync(menuItem);
            return RedirectToAction(nameof(Index));
        }

        //GET: MenuItems/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var menuItemDetails = await _service.GetMenuItemByIdAsync(id);
            if (menuItemDetails == null) return View("NotFound");

            var response = new NewMenuItemViewModel()
            {
                Id = menuItemDetails.Id,
                MenuName = menuItemDetails.MenuName,
                Description = menuItemDetails.Description,
                Price = menuItemDetails.Price,
                ImageUrl = menuItemDetails.ImageUrl,
                CategoryId = menuItemDetails.CategoryId,
            };

            var menuItemDropdownsData = await _service.GetNewMenuItemDropdownsValues();
            ViewBag.Categories = new SelectList(menuItemDropdownsData.MenuCategories, "Id", "Name");
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewMenuItemViewModel menuItem)
        {
            if (id != menuItem.Id) return View("NotFound");

            if (!ModelState.IsValid)
            {
                var menuItemDropdownsData = await _service.GetNewMenuItemDropdownsValues();

                ViewBag.Categories = new SelectList(menuItemDropdownsData.MenuCategories, "Id", "Name");
                return View(menuItem);
            }

            await _service.UpdateMenuItemAsync(menuItem);
            return RedirectToAction(nameof(Index));
        }

        // //Get: Categories/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var menuItemDetails = await _service.GetMenuItemByIdAsync(id);
            if (menuItemDetails == null) return View("NotFound");
            return View(menuItemDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var menuItemDetails = await _service.GetMenuItemByIdAsync(id);
            if (menuItemDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
