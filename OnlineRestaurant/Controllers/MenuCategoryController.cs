using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineRestaurant.Data;
using OnlineRestaurant.Data.Services;
using OnlineRestaurant.Data.Static;
using OnlineRestaurant.Models;

namespace OnlineRestaurant.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class MenuCategoryController : Controller
    {
        private readonly ICategoryService _service;

        public MenuCategoryController(ICategoryService service)
        {
                _service = service;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            return View(data);
        }

        //Get: Categories/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name")] MenuCategory menuCategory)
        {
            if (ModelState.IsValid)
            {
                return View(menuCategory);
            }
            await _service.AddAsync(menuCategory);
            return RedirectToAction(nameof(Index));
        }

        //Get: Categories/Details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var categoriesDetails = await _service.GetByIdAsync(id);
            if (categoriesDetails == null) return View("NotFound");
            return View(categoriesDetails);
        }

        // //Get: Ctegories/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var categoriesDetails = await _service.GetByIdAsync(id);
            if (categoriesDetails == null) return View("NotFound");
            return View(categoriesDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, /*[Bind("Id,Name")]*/ MenuCategory menuCategory)
        {

            if (ModelState.IsValid)
            {
                return View(menuCategory);

            }
            //if(id == menuCategory.Id)
            //{
            //    await _service.UpdateAsync(id, menuCategory);
            //    return RedirectToAction(nameof(Index));
            //}
            await _service.UpdateAsync(id, menuCategory);
            return RedirectToAction(nameof(Index));
            //return View(menuCategory);

        }

        // //Get: Categories/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var catogoriesDetails = await _service.GetByIdAsync(id);
            if (catogoriesDetails == null) return View("NotFound");
            return View(catogoriesDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var categoriesDetails = await _service.GetByIdAsync(id);
            if (categoriesDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
