using Microsoft.EntityFrameworkCore;
using SoftwareEngineerProject.Database;
using SoftwareEngineerProject.Database.Models;
using SoftwareEngineerProject.Services;
using Xunit;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoftwareEngineerProject.UnitTests
{
    public class CustomerLessonServiceTest
    {
        private DataContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new DataContext(options);
        }

        // Test to add a customer lesson link
        [Theory]
        [InlineData(1, 1, 1)]
        [InlineData(2, 2, 4)]
        public async Task AddCustomerLesson_AddRecord(int id, int customerId, int lessonId)
        {
            var context = GetInMemoryContext();
            var service = new CustomerLessonService(context);

            var clT = new CustomerLessonTable
            {
                ID = id,
                CustomerID = customerId,
                LessonID = lessonId
            };

            var result = await service.AddCustomerLesson(clT);

            var storedCustomerLesson = await context.CustomerLessonTable.FindAsync(id);

            Assert.True(result);
            Assert.Equal(customerId, storedCustomerLesson.CustomerID);
            Assert.Equal(lessonId, storedCustomerLesson.LessonID);
        }


        // Test if AddCustomerLesson is null
        [Fact]
        public async Task AddCustomerLesson_Null_ThrowNullException()
        {
            var context = GetInMemoryContext();
            var service = new CustomerLessonService(context);

            await Assert.ThrowsAsync<ArgumentNullException>(async () => await service.AddCustomerLesson(null));
        }

        // Test if AddCustomerLesson with missing fields
        [Fact]
        public async Task AddCustomerLesson_MissingFields_ReturnFalse()
        {
            var context = GetInMemoryContext();
            var service = new CustomerLessonService(context);

            var clT = new CustomerLessonTable
            {
                ID = 2,
                CustomerID = 0,
                LessonID = 0
            };

            var result = await service.AddCustomerLesson(clT);
            Assert.False(result);
        }

        // Test CustomerLesson Deletion
        [Fact]
        public async Task DeleteCustomerLesson_DeleteAndReturnTrue()
        {
            var context = GetInMemoryContext();
            var service = new CustomerLessonService(context);

            var clT = new CustomerLessonTable { ID = 1, CustomerID = 1, LessonID = 1 };
            await service.AddCustomerLesson(clT);
            await context.SaveChangesAsync();

            var result = await service.DeleteCustomerLesson(1, 1);

            Assert.True(result);

            var testDelete = await context.CustomerLessonTable.FindAsync(1);
            Assert.Null(testDelete);
            
        }

        // Test CustomerLesson Editing
        [Fact]
        public async Task EditCustomerLesson_UpdateValues()
        {
            var context = GetInMemoryContext();
            var service = new CustomerLessonService(context);

            var clT1 = new CustomerLessonTable { ID = 1, CustomerID = 1, LessonID = 1 };
            await service.AddCustomerLesson(clT1);
            await context.SaveChangesAsync();

            var updatedClt = new CustomerLessonTable { ID = 1, CustomerID = 2, LessonID = 3 };

            var result = await service.EditCustomerLesson(1, updatedClt);

            Assert.NotNull(result);
            Assert.Equal(2, result.CustomerID);
            Assert.Equal(3, result.LessonID);
        }

        // Test if CustomerLesson does not exist
        [Fact]
        public async Task EditCustomerLesson_NotExistingResult_ReturnNull()
        {
            var context = GetInMemoryContext();
            var service = new CustomerLessonService(context);

            var result = await service.EditCustomerLesson(1, new CustomerLessonTable { ID = 999 });

            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllCustomerLessons_ReturnFullTable()
        {
            var context = GetInMemoryContext();
            var service = new CustomerLessonService(context);

            context.CustomerLessonTable.AddRange(
                new CustomerLessonTable { ID = 1, CustomerID = 1, LessonID = 1 },
                new CustomerLessonTable { ID = 2, CustomerID = 2, LessonID = 2 }
            );
            context.SaveChanges();

            var result = await service.GetAllCustomerLessons();

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetCustomerLessonById_ValidId_ReturnResult()
        {
            var context = GetInMemoryContext();
            var service = new CustomerLessonService(context);

            context.CustomerLessonTable.Add(new CustomerLessonTable { ID = 1, CustomerID = 1, LessonID = 2 });
            context.SaveChangesAsync();

            var result = await service.GetCustomerLessonById(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.CustomerID);
            Assert.Equal(2, result.LessonID);
        }

        [Fact]
        public async Task GetLessonByCustomer_ReturnAllLinkedLessons()
        {
            var context = GetInMemoryContext();
            var service = new CustomerLessonService(context);

            var testFacility = new FacilityTable
            {
                ID = 1,
                Name = "Test Facility",
                Address = "Test Address",
                City = "Test City",
                PostCode = "Test PostCode",
                OpenTime = new TimeOnly(06, 00),
                CloseTime = new TimeOnly(22, 00)
            };

            var testEmployee = new EmployeeTable
            {
                ID = 1,
                FirstName = "Alex",
                LastName = "Johnson",
                Email = "alex.johnson@example.com",
                Password = "hashedpassword123", // Ideally you'd hash this in real code
                Salt = Encoding.UTF8.GetBytes("randomsalt"), // Simulated salt value
                BirthDate = new DateTime(1990, 5, 15),
                StartTime = new DateTime(2025, 4, 17, 8, 0, 0),
                EndTime = new DateTime(2025, 4, 17, 16, 0, 0),
                WorkDays = "Mon,Tue,Wed,Thu,Fri",
                Teaching = true,
                FacilityID = 1,
                Facility = testFacility
            };

            var testPool = new PoolTable
            {
                ID = 1,
                FacilityID = 1,
                Facility = testFacility,
                Type = "Indoor",
                Booked = false,
                Lessons = new List<LessonTable>()
            };

            var lesson1 = new LessonTable
            {
                ID = 1,
                Type = "Swimming",
                PersonCount = 0,
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(1),
                PoolID = 1,
                Pool = testPool
            };

            testPool.Lessons.Add(lesson1);

            context.LessonTable.Add(lesson1);
            context.CustomerLessonTable.Add(new CustomerLessonTable { ID = 1, CustomerID = 1, Lesson = lesson1, LessonID = 1 });

            context.SaveChanges();

            var result = await service.GetLessonsByCustomer(1);

            Assert.Equal(1, result.Count);
            Assert.Contains(result, l => l.Type == "Swimming");


        }

    }
}
