using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnlineRestaurant.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineRestaurant.test.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        private ILogger<HomeController> _logger;
        

        public HomeControllerTest()
        {
            _logger = A.Fake<ILogger<HomeController>>();

            
        }
        [Fact]
        public void Index()
        {
            // Arrange
            var controller = new HomeController(_logger);
            

            // Act
            var result = controller.Index() as ViewResult;


            // Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public void About()
        {
            // Arrange
            var controller = new HomeController(_logger);


            // Act
            var result = controller.About() as ViewResult;


            // Assert
            result.Should().NotBeNull();
        }

        [Fact]
        public void Contact()
        {
            // Arrange
            var controller = new HomeController(_logger);


            // Act
            var result = controller.Contact() as ViewResult;


            // Assert
            result.Should().NotBeNull();
        }
    }
}
