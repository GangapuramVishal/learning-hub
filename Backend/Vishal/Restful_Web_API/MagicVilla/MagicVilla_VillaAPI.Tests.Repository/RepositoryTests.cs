using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq.Expressions;
using MockQueryable.Moq;

namespace MagicVilla_VillaAPI.Tests.Repository
{
    [TestFixture]
    public class RepositoryTests
    {
        private Repository<SampleEntity> repository;
        private Mock<ApplicationDbContext> dbContextMock;
        private Mock<DbSet<SampleEntity>> dbSetMock;

        [SetUp]
        public void Setup()
        {
            dbContextMock = new Mock<ApplicationDbContext>(new DbContextOptions<ApplicationDbContext>());   // Creating a mock of ApplicationDbContext
            dbSetMock = new Mock<DbSet<SampleEntity>>();                                                   // Creating a mock of DbSet<SampleEntity>
            dbContextMock.Setup(x => x.Set<SampleEntity>()).Returns(dbSetMock.Object);                    // Setting up mock behavior for ApplicationDbContext
            repository = new Repository<SampleEntity>(dbContextMock.Object);                             // Creating an instance of Repository with the mocked ApplicationDbContext
            var options = new DbContextOptions<ApplicationDbContext>();                                 // Creating DbContextOptions for ApplicationDbContext
            dbContextMock = new Mock<ApplicationDbContext>(options);                                   // Reassigning dbContextMock with a new instance of ApplicationDbContext
        }

        [Test]
        public async Task GetEntityByIdAsync()
        {
            // Arrange
            var stubEntities = new List<SampleEntity>
            {
                new SampleEntity { Id = 1 },
                new SampleEntity { Id = 2 }
            };
            var mockSampleEntityDbSet = stubEntities.AsQueryable().BuildMockDbSet();
            dbContextMock.Setup(c => c.Set<SampleEntity>()).Returns(mockSampleEntityDbSet.Object);
            repository = new Repository<SampleEntity>(dbContextMock.Object);
            // Act
            var result = await repository.GetAsync();
            //Assert
            Assert.AreEqual(stubEntities[0], result);

        }

        [Test]
        public async Task GetAllDetailsByIdAsync()
        {
            // Arrange
            var stubEntities = new List<SampleEntity>
            {
                new SampleEntity { Id = 1 },
                new SampleEntity { Id = 2 }
            };
            var mockSampleEntityDbSet = stubEntities.AsQueryable().BuildMockDbSet();                  // Creating a mock DbSet using a list of entities
            dbContextMock.Setup(c => c.Set<SampleEntity>()).Returns(mockSampleEntityDbSet.Object);   // Setting up mock behavior for ApplicationDbContext to return the mock DbSet
            repository = new Repository<SampleEntity>(dbContextMock.Object);                        // Creating an instance of Repository with the mocked ApplicationDbContext

            // Act
            var result = await repository.GetAllAsync(entity => entity.Id > 0);                  // Calling the GetAllAsync method of the repository with a predicate

            // Assert
            Assert.AreEqual(stubEntities, result);                                            // Verifying that the result matches the expected list of entities
        } 



        [Test]
        public async Task AddAsync_Entity_To_DBSet()
        {
            //Arrange
            var entity = new SampleEntity();                                        // Creating a new instance of SampleEntity

            //Act
            await repository.CreateAsync(entity);                                 // Calling the CreateAsync method of the repository

            //Assert
            dbSetMock.Verify(x => x.AddAsync(entity, default), Times.Once);     // Verifying that the AddAsync method of DbSet was called once with the specified entity
        }

        [Test]
        public async Task DeleteAsync_RemoveEntity()
        {
            //Arrange
            var stubEntity = new SampleEntity();

            //Act
            await repository.RemoveAsync(stubEntity);

            //Assert
            dbSetMock.Verify(x => x.Remove(stubEntity), Times.Once);
        }

        //[Test]
        //public async Task SaveAsync_Saveentity()
        //{ 

        //    //Act
        //    await repository.SaveAsync();

        //    //Assert
        //    //dbContextMock.Verify(x => x.SaveChangesAsync(default), Times.Once);
        //    dbContextMock.Setup(x => x.SaveChangesAsync(default)).ReturnsAsync(1); // Assuming SaveChangesAsync returns a Task<int>
        //}

        [Test]
        public async Task GetAsync_ReturnsNull_WhenEntityNotFound()
        {
            // Arrange
            var stubEntities = new List<SampleEntity>
            {
                new SampleEntity { Id = 1 },
                new SampleEntity { Id = 2 }
            };
            var mockSampleEntityDbSet = stubEntities.AsQueryable().BuildMockDbSet();                  // Creating a mock DbSet using a list of entities
            dbContextMock.Setup(c => c.Set<SampleEntity>()).Returns(mockSampleEntityDbSet.Object);   // Setting up mock behavior for ApplicationDbContext to return the mock DbSet
            repository = new Repository<SampleEntity>(dbContextMock.Object);

            // Act
            var result = await repository.GetAsync(entity => entity.Id == 3); // Assuming entity with Id 1 does not exist

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task GetAllAsync_ReturnsEmptyList_WhenNoEntitiesFound()
        {
            // Arrange: No entities in the database
            var stubEntities = new List<SampleEntity>(); // Empty list of entities
            var mockSampleEntityDbSet = stubEntities.AsQueryable().BuildMockDbSet(); // Mock DbSet with empty list
            dbContextMock.Setup(c => c.Set<SampleEntity>()).Returns(mockSampleEntityDbSet.Object);
            repository = new Repository<SampleEntity>(dbContextMock.Object);

            // Act
            var result = await repository.GetAllAsync();

            // Assert
            Assert.IsEmpty(result);
        }

        //This test case ensures that the SaveAsync method does not call SaveChangesAsync on the database context when there are no changes to be saved.
        [Test]
        public async Task SaveAsync_DoesNotSave_WhenNoChangesExist()
        {
            // Arrange: No changes have been made to entities in the database

            // Act
            await repository.SaveAsync();

            // Assert
            dbContextMock.Verify(x => x.SaveChangesAsync(default), Times.Never);
        }
    }
    public class SampleEntity
    {
        public int Id { get; set; }
    }
}































// Dont Use this code 
//private Repository<Villa> _repository;
//private Mock<ApplicationDbContext> _dbContextMock;
//private Mock<DbSet<Villa>> _dbSetMock;

//[SetUp]
//public void Setup()
//{
//    // Mock ApplicationDbContext and DbSet
//    _dbContextMock = new Mock<ApplicationDbContext>();
//    _dbSetMock = new Mock<DbSet<Villa>>();
//    _dbContextMock.Setup(x => x.Set<Villa>()).Returns(_dbSetMock.Object);

//    _repository = new Repository<Villa>(_dbContextMock.Object);
//}

//[Test]
//public async Task CreateAsync_ValidEntity_SavesToDatabase()
//{
//    // Arrange
//    var entity = new Villa { /* Initialize with valid data */ };

//    // Act
//    await _repository.CreateAsync(entity);

//    // Assert
//    _dbSetMock.Verify(x => x.AddAsync(entity, default), Times.Once);
//    _dbContextMock.Verify(x => x.SaveChangesAsync(default), Times.Once);
//}

//// Write similar tests for other repository methods...

//[Test]
//public async Task GetAsync_WithFilter_ReturnsEntity()
//{
//    // Arrange
//    Expression<Func<Villa, bool>> filter = v => v.Id == 1; // Example filter
//    var data = new List<Villa> { new Villa { Id = 1 } }; // Sample data for DbSet
//    _dbSetMock.Setup(x => x.AsNoTracking()).Returns(_dbSetMock.Object);
//    _dbSetMock.Setup(x => x.FirstOrDefaultAsync(filter, default)).ReturnsAsync(data[0]);

//    // Act
//    var result = await _repository.GetAsync(filter);

//    // Assert
//    Assert.IsNotNull(result);
//    Assert.AreEqual(1, result.Id);
//}

//// Write similar tests for other repository methods...
