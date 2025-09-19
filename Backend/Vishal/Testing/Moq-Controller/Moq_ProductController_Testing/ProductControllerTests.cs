using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq_ProductAPI_Testing.Controllers;
using Moq_ProductAPI_Testing.Models;
using Moq_ProductAPI_Testing.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moq_ProductController_Testing
{
    [TestFixture]
    public class ProductControllerTests
    {
        private Mock<IProductService> _productServiceMock { get; set; } = null;
        private ProductController _productController;
        [SetUp]
        public void SetUp()
        {
            _productServiceMock = new Mock<IProductService>();
            _productController = new ProductController(_productServiceMock.Object);
        }
        [Test]
        public async Task GetProductById_ReturnsProduct()
        {
            // Arrange
            _productServiceMock.Setup(p => p.GetProductById(1)).ReturnsAsync("EarBuds");
            ProductController productController = new ProductController(_productServiceMock.Object); // .Object - give's you the actual instance of the mocked IProductService interface

            // Act
            string result = await productController.GetProductById(1);

            // Assert
            Assert.AreEqual("EarBuds", result);
        }

        [Test]
        public async Task GetProductDetails_ReturnsProduct()
        {
            var employeeDTO = new Product()
            {
                ProductId = 1,
                ProductName = "GP",
                ProductCategory = "Developer"
            };
            _productServiceMock.Setup(p => p.GetProductDetails(1)).ReturnsAsync(employeeDTO);
            ProductController emp = new ProductController(_productServiceMock.Object);
            var result = await emp.GetProductDetails(1);
            Assert.True(employeeDTO.Equals(result));
        }

        [Test]
        public async Task CreateProduct_ValidModel_ReturnsOk()
        {
            // Arrange
            var model = new CreateProductModel { ProductName = "NewProduct", ProductCategory = "NewCategory" };
            _productServiceMock.Setup(p => p.CreateProduct(model)).ReturnsAsync(1); // Assuming product ID is returned as 1 after creation

            // Act
            var result = await _productController.CreateProduct(model) as IActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);
            Assert.AreEqual(1, (result as OkObjectResult).Value); // Assuming product ID is returned in case of success
        }

        [Test]
        public async Task CreateProduct_InvalidModel_ReturnsBadRequest()
        {
            // Arrange
            var invalidModel = new CreateProductModel(); // Missing required properties
            _productController.ModelState.AddModelError("ProductName", "The ProductName field is required.");

            // Act
            var result = await _productController.CreateProduct(invalidModel) as IActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

    }
}



/*_productServiceMock.Object is the mocked instance of IProductService, and it's being injected into the
 * ProductController class for testing purposes. This allows you to control the behavior of the
 * IProductService methods within your test and verify how the ProductController interacts with it.
 */


//error
//var data = _productServiceMock.Setup(p => p.GetProductById(1)).Returns(Task.FromResult<string>);
//ProductController emp = new ProductController(_productServiceMock.Object);
//string result = await emp.GetProductById(1);
//Assert.AreEqual(data.ToString(), result);