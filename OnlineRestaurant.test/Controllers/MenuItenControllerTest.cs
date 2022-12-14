using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnlineRestaurant.Controllers;
using OnlineRestaurant.Data.Services;
using OnlineRestaurant.Data.ViewModels;
using OnlineRestaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineRestaurant.test.Controllers
{
   
    [TestClass]

    public class MenuItemControllerTest
    {
        private MenuItemController _menuItemController;
        private IItemService _service;
    

        public MenuItemControllerTest()
        {
            _service = A.Fake<IItemService>();
            _menuItemController = new MenuItemController(_service);

        }

        [Fact]
        public void MenuItemController_Index_ReturnSucces()
        {
            //Arrange
            var allItems = A.Fake<IEnumerable<MenuItem>>();
            A.CallTo(() => _service.GetAllAsync(n => n.MenuCategory)).Returns(allItems);

            //Act
            var result = _menuItemController.Index();

            //Assert
            result.Should().BeOfType<Task<IActionResult>>();
            result.Should().NotBeNull();

        }

        [Fact]
        public void MenuItemController_Detail_ReturnSucces()
        {
            //Arrange
            var id = 1;
            var menuItemsDetail = A.Fake<MenuItem>();
            A.CallTo(() => _service.GetMenuItemByIdAsync(id)).Returns(menuItemsDetail);

            //Act
            var result = _menuItemController.Details(id);

            //Assert
            result.Should().BeOfType<Task<IActionResult>>();
            result.Should().NotBeNull();

        }
        
        [Fact]
        public void Test_MenuItemController_WithResponce()
        {
            //Arrange
            var menuItemDropdownsData = A.Fake<MenuItem>();
            MenuItem menuItem = new()
            {
                Id = 1,
                MenuName = "Main",
                Description = "dddd",
                Price = 10,
                ImageUrl = "www.dsdsd.com"
                
            };
            A.CallTo(() => _service.GetNewMenuItemDropdownsValues());

            //Act
            var result = _menuItemController.Create();
            
            
            //Assert
            result.Should().NotBeNull();
            menuItem.Price.Should().Be(10);
        }

        [Fact]
        public void Test_CreateMenuItem_ReturnSucces()
        {
            //Arrange
            NewMenuItemViewModel menuItem = new() { Id = 1, MenuName = "Starter", Description = "ffff", Price = 11, ImageUrl="www.ddd.com" };
            
            var menuItemDropdownsData = A.Fake<MenuItem>();
            A.CallTo(() => _service.GetNewMenuItemDropdownsValues());
            A.CallTo(() => _service.AddNewMenuItemAsync(menuItem));


            //Act
            var result = _menuItemController.Create();
            
            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<IActionResult>>();
            menuItem.Price.Should().Be(11);

        }

        [Fact]
        public void Test_DeleteMenuItem_WithResponse()
        {
            //Arrange
            var id = 1;
            var menuItemDetails = A.Fake<MenuItem>();
            A.CallTo(() => _service.DeleteAsync(id));


            //Act
            var result = _menuItemController.DeleteConfirm(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<IActionResult>>();

        }

        [Fact]

        public void Test_EditMenuItem_WithResponse()
        {
            //Arrange
            var id = 1;
            var menuItemDetails = A.Fake<MenuItem>();
            A.CallTo(() => _service.GetMenuItemByIdAsync(id));
            A.CallTo(() => _service.GetNewMenuItemDropdownsValues());
            

            //Act
            var result = _menuItemController.Edit(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<IActionResult>>();

        }

        [Fact]
        public void Test_EditMenuItem_ReturnSucces()
        {
            // Arrange
            var id = 1;
            A.CallTo(() => _service.GetNewMenuItemDropdownsValues());
            var menuItem = new NewMenuItemViewModel()
            {
                Id = 1,
                MenuName = "Dessert",
                Description = "sweet",
                Price = 4,
                ImageUrl = "www.sweets.com",
                //CategoryId = menuItemDetails.CategoryId,
            };
            
            A.CallTo(() => _service.UpdateMenuItemAsync(menuItem));

            //Act
            var result = _menuItemController.Edit(id, menuItem);

            //Assert

            result.Should().NotBeNull();
            result.Should().BeOfType<Task<IActionResult>>();
            result.Should().Equals(menuItem.Id);
            
           

        }



    }
}
