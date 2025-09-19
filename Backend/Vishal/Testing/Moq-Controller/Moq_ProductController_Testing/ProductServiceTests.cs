//using Microsoft.EntityFrameworkCore;
//using Moq;
//using Moq_ProductAPI_Testing.Data;
//using Moq_ProductAPI_Testing.Models;
//using Moq_ProductAPI_Testing.Services;
//using System.Linq;

//namespace Moq_ProductController_Testing
//{
//    [TestFixture]
//    public class ProductServiceTests
//    {
//        private ProductService _productService;
//        private Mock<ProductDbContext> _mockDbContext;

//        [SetUp]
//        public void Setup()
//        {
//            // Mocking DbContext and DbSet
//            var mockData = new List<Product>
//            {
//                new Product { ProductId = 1, ProductName = "TestProduct1", ProductCategory = "TestCategory1" },
//                new Product { ProductId = 2, ProductName = "TestProduct2", ProductCategory = "TestCategory2" },
//                new Product { ProductId = 3, ProductName = "TestProduct3", ProductCategory = "TestCategory3" }
//            }.AsQueryable();

//            var mockDbSet = new Mock<DbSet<Product>>();
//            mockDbSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(mockData.Provider);
//            mockDbSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(mockData.Expression);
//            mockDbSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(mockData.ElementType);
//            mockDbSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(mockData.GetEnumerator());

//            // Mocking ProductDbContext
//            _mockDbContext = new Mock<ProductDbContext>();
//            _mockDbContext.Setup(c => c.MoqProductsForTestig).Returns(mockDbSet.Object);

//            // Initialize ProductService with mocked DbContext
//            _productService = new ProductService(_mockDbContext.Object);
//        }

//        [Test]
//        public async Task GetProductById_ReturnsProductName()
//        {
//            // Arrange
//            int productId = 1;
//            string expectedProductName = "TestProduct1";

//            // Act
//            var result = await _productService.GetProductById(productId);

//            // Assert
//            Assert.IsNotNull(result);
//            Assert.AreEqual(expectedProductName, result);
//        }

//        [Test]
//        public async Task GetProductDetails_ReturnsProduct()
//        {
//            // Arrange
//            int productId = 1;
//            var expectedProduct = new Product { ProductId = 1, ProductName = "TestProduct1", ProductCategory = "TestCategory1" };

//            // Act
//            var result = await _productService.GetProductDetails(productId);

//            // Assert
//            Assert.IsNotNull(result);
//            Assert.AreEqual(expectedProduct.ProductId, result.ProductId);
//            Assert.AreEqual(expectedProduct.ProductName, result.ProductName);
//            Assert.AreEqual(expectedProduct.ProductCategory, result.ProductCategory);
//        }

//        [Test]
//        public async Task CreateProduct_ReturnsProductId()
//        {
//            // Arrange
//            var model = new CreateProductModel { ProductName = "NewProduct", ProductCategory = "NewCategory" };

//            // Act
//            var productId = await _productService.CreateProduct(model);

//            // Assert
//            Assert.IsTrue(productId > 0); // Assuming the product ID starts from 1
//        }
//    }
//}