//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using StudentAPI.Controllers;
//using StudentAPI.Data;
//using StudentAPI.Models;

//namespace StudentAPITests
//{

//    [TestFixture]
//    public class Class10ControllerTests
//    {
//        private Class10Controller _controller;
//        private StudentDbContext _context;

//        [SetUp]
//        public void Setup()
//        {
//            // Configure DbContext to use an in-memory database
//            var options = new DbContextOptionsBuilder<StudentDbContext>()
//            .UseSqlServer("DefaultSQLConnection")
//            .Options;

//            // Initialize DbContext with the SSMS database
//            _context = new StudentDbContext(options);

//            // Seed some data for testing
//            _context.Class10s.AddRange(new List<Class10>
//            {
//                new Class10 { AdmissionNum = 1, Name = "Kavya" },
//                new Class10 { AdmissionNum = 2, Name = "Yash" },
//                new Class10 { AdmissionNum = 3, Name = "Tharun" }
//            });
//            _context.SaveChanges();

//            // Initialize Controller with the in-memory DbContext
//            _controller = new Class10Controller(_context);
//        }

//        [Test]
//        public async Task GetClass10s_ReturnsOkResultWithClass10s()
//        {
//            // Act: Call the GetClass10s method
//            var result = await _controller.GetClass10s();

//            // Assert: Verify the result
//            Assert.IsInstanceOf<OkObjectResult>(result.Result);

//            var okResult = (OkObjectResult)result.Result;
//            var class10s = (IEnumerable<Class10>)okResult.Value;

//            Assert.AreEqual(3, class10s.Count()); // Assuming 3 seeded items
//            Assert.AreEqual("Kavya", class10s.First().Name);
//            Assert.AreEqual("Tharun", class10s.Last().Name);
//        }

//        [TearDown]
//        public void TearDown()
//        {
//            // Dispose the DbContext to release resources
//            _context.Dispose();
//        }
//    }
//}

































//public class Class10ControllerTests
//{
//    private Class10Controller _controller;
//    private StudentDbContext _context;

//    [SetUp]
//    public void Setup()
//    {
//        // Configure DbContext to use SQL Server database
//        var options = new DbContextOptionsBuilder<StudentDbContext>()
//            .UseSqlServer("Server = DESKTOP-RI53L0D\\SQLEXPRESS; Database = StudentDb; TrustServerCertificate = True; Trusted_Connection= True;MultipleActiveResultSets=true")
//            .Options;

//        // Initialize DbContext with SQL Server database
//        _context = new StudentDbContext(options);

//        // Seed some data for testing
//        _context.Class10s.AddRange(new List<Class10>
//        {
//            new Class10 { AdmissionNum = 1, Name = "Kavya" },
//            new Class10 { AdmissionNum = 2, Name = "Yash" },
//            new Class10 { AdmissionNum = 3, Name = "Tharun" }
//        });
//        _context.SaveChanges();

//        // Initialize Controller with DbContext
//        _controller = new Class10Controller(_context);
//    }






//[Test]
//public async Task GetClass10s_ReturnsAllItems()
//{
//    // Act
//    var result = await _controller.GetClass10s();

//    // Assert
//    var items = (result.Result as OkObjectResult).Value as List<Class10>;
//    Assert.AreEqual(3, items.Count);
//}

//[Test]
//public async Task GetClass10_ReturnsItemWithValidId()
//{
//    // Arrange
//    int validId = 1;

//    // Act
//    var result = await _controller.GetClass10(validId);

//    // Assert
//    Assert.IsInstanceOf<ActionResult<Class10>>(result);
//    Assert.IsInstanceOf<OkObjectResult>(result.Result);
//    var item = (result.Result as OkObjectResult).Value as Class10;
//    Assert.IsNotNull(item);
//    Assert.AreEqual(validId, item.AdmissionNum);
//}

// You can add similar test methods for other controller actions like PutClass10, PostClass10, and DeleteClass10













//_controller: This field holds an instance of the Class10Controller class. It allows you to create an object
//of the controller class that you want to test. This controller object is then used to invoke the methods
//being tested, such as GetClass10s, GetClass10, PutClass10, etc.
//_context: This field holds an instance of the StudentDbContext class. In many cases, controller methods
//interact with a database context to perform CRUD operations (Create, Read, Update, Delete) on data.
//By having an instance of the database context, you can set up test data and verify that the controller
//methods behave correctly under different scenarios.