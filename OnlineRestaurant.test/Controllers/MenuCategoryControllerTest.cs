using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnlineRestaurant.Controllers;
using OnlineRestaurant.Data.Services;
using OnlineRestaurant.Models;

namespace OnlineRestaurant.test.Controllers
{
    [TestClass]
    public class MenuCategoryControllerTest
    {
        private MenuCategoryController _menuCategoryController;
        private ICategoryService _service;



        public MenuCategoryControllerTest()
        {
            _service = A.Fake<ICategoryService>();

            _menuCategoryController = new MenuCategoryController(_service);
        }

        [Fact]

        public void MenuCategoryController_Index_ReturnSucces()
        {
            //Arrange
            var data = A.Fake<IEnumerable<MenuCategory>>();
            A.CallTo(() => _service.GetAllAsync()).Returns(data);

            //Act
            var result = _menuCategoryController.Index();

            //Assert
            result.Should().BeOfType<Task<IActionResult>>();
            
            
        }

        [Fact]
        public void MenuCategoryController_Detail_ReturnSucces()
        {
            //Arrange
            var id = 1;
            var categoriesDetails = A.Fake<MenuCategory>();
            A.CallTo(() => _service.GetByIdAsync(id)).Returns(categoriesDetails);

            //Act
            var result = _menuCategoryController.Details(id);

            //Assert
            result.Should().BeOfType<Task<IActionResult>>();
            result.Should().NotBeNull();

        }


        [Fact]

        public void Test_CreateMenuCategory_WithResponse()
        {
            // Arrange
            MenuCategory menuCategory = new()
            {
                Id = 4,
                Name = "TestCategory6"
                
            };
            
            A.CallTo(() => _service.AddAsync(menuCategory)).Returns(Task.CompletedTask);/*Returns(Task.CompletedTask);*/
            


            // Act
            var result = _menuCategoryController.Create(menuCategory);


            // Assert
            
            result.Should().NotBeNull();
            menuCategory.Name.Should().Be("TestCategory6");
            menuCategory.Id.Should().Be(4);

        }

        [Fact]

        public void Test_CreateMenuCategory_ReturnSucces()
        {
            //Arrange
            var createCategory = A.Fake<MenuCategory>();
            A.CallTo(() => _service.AddAsync(createCategory));
            

            //Act
            var result = _menuCategoryController.Create();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<IActionResult>>();
            
            
        }

        [Fact]

        public void Test_EditMenuCategory_WithResponse()
        {
            //Arrange
            var id = 1;
            var categoriesDetails = A.Fake<MenuCategory>();
            A.CallTo(() => _service.GetByIdAsync(id)).Returns(categoriesDetails);


            //Act
            var result = _menuCategoryController.Edit(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<IActionResult>>();
           
        }

        [Fact]
        public void Test_EditMenuCategory_ReturnSucces()
        {
            // Arrange
            var id = 1;
            var categoriesDetails = A.Fake<MenuCategory>();
            MenuCategory menuCategory = new()
            {
                Id = 1,
                Name = "TestCategory6"

            };
            A.CallTo(() => _service.UpdateAsync(id, menuCategory));

            //Act
            var result = _menuCategoryController.Edit(id);

            //Assert

            result.Should().NotBeNull();
            result.Should().BeOfType<Task<IActionResult>>();
            result.Should().Equals(menuCategory.Id);
           
            
        }
        [Fact]
        public void Test_DeleteMenuCategory_WithResponse()
        {
            //Arrange
            var id = 1;
            var categoriesDetails = A.Fake<MenuCategory>();
            A.CallTo(() => _service.DeleteAsync(id));


            //Act
            var result = _menuCategoryController.DeleteConfirm(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<IActionResult>>();
          
        }
    }
}
