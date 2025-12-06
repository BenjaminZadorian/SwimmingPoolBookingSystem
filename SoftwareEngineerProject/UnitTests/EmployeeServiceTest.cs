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
    public class EmployeeServiceTest
    {

        private static FacilityTable facility = new FacilityTable
        {
            ID = 1,
            Name = "Name",
            Address = "Address",
            City = "City",
            PostCode = "12345",
            OpenTime = new TimeOnly(6, 0),
            CloseTime = new TimeOnly(22, 0)
        };

        private EmployeeTable employee = new EmployeeTable
        {
            ID = 1,
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            Password = "hashed_password_here",
            Salt = new byte[] { 1, 2, 3, 4, 5 },
            BirthDate = new DateTime(1990, 5, 10),
            StartTime = new DateTime(2023, 1, 1, 8, 0, 0),
            EndTime = new DateTime(2023, 1, 1, 16, 0, 0),
            WorkDays = "Monday,Wednesday,Friday",
            Teaching = false,
            FacilityID = 1,
            Facility = facility
        };

private DataContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new DataContext(options);
        }

        [Fact]
        public async Task AddEmployee_AddRecord()
        {
            var context = GetInMemoryContext();
            var service = new EmployeeService(context);

            var result = await service.AddEmployee(employee);

            Assert.True(result);
            Assert.Single(await context.EmployeeTable.ToListAsync());
        }

        [Fact]
        public async Task AddEmployee_CopyEmail_ReturnFalse()
        {
            var context = GetInMemoryContext();
            var service = new EmployeeService(context);

            await service.AddEmployee(employee);

            var result = await service.AddEmployee(employee);

            Assert.False(result);
        }

        [Fact]
        public async Task DeleteEmployee_DeleteRecord()
        {
            var context = GetInMemoryContext();
            var service = new EmployeeService(context);

            await service.AddEmployee(employee);

            var result = await service.DeleteEmployee(1);

            Assert.True(result);
            var testDelete = await context.EmployeeTable.FindAsync(1);
            Assert.Null(testDelete);
        }

        [Fact]
        public async Task DeleteEmployee_EmployeeNotFound_ReturnFalse()
        {
            var context = GetInMemoryContext();
            var service = new EmployeeService(context);

            var result = await service.DeleteEmployee(1);

            Assert.False(result);
        }

        [Fact]
        public async Task EditEmployee_EditRecord()
        {
            var context = GetInMemoryContext();
            var service = new EmployeeService(context);

            await service.AddEmployee(employee);

            var updatedEmployee = new EmployeeTable
            {
                ID = 1,
                FirstName = "Jonathan",
                LastName = "Doe",
                Email = "jonathan.doe@example.com",
                Password = "hashed_password_here",
                Salt = new byte[] { 1, 2, 3, 4, 5 },
                BirthDate = new DateTime(1990, 5, 10),
                StartTime = new DateTime(2023, 1, 1, 8, 0, 0),
                EndTime = new DateTime(2023, 1, 1, 16, 0, 0),
                WorkDays = "Monday,Wednesday,Friday",
                Teaching = false,
                FacilityID = 1,
                Facility = facility
            };

            var result = await service.EditEmployee(1, updatedEmployee);

            Assert.NotNull(result);
            Assert.Equal("Jonathan", result.FirstName);
            Assert.Equal("jonathan.doe@example.com", result.Email);
        }

        [Fact]
        public async Task EditEmployee_EmployeeNotFound_ReturnNull()
        {
            var context = GetInMemoryContext();
            var service = new EmployeeService(context);

            var updatedEmployee = new EmployeeTable
            {
                ID = 1,
                FirstName = "Jonathan",
                LastName = "Doe",
                Email = "jonathan.doe@example.com",
                Password = "hashed_password_here",
                Salt = new byte[] { 1, 2, 3, 4, 5 },
                BirthDate = new DateTime(1990, 5, 10),
                StartTime = new DateTime(2023, 1, 1, 8, 0, 0),
                EndTime = new DateTime(2023, 1, 1, 16, 0, 0),
                WorkDays = "Monday,Wednesday,Friday",
                Teaching = false,
                FacilityID = 1,
                Facility = facility
            };

            var result = await service.EditEmployee(99, updatedEmployee);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllEmployees_ReturnAllRecords()
        {
            var context = GetInMemoryContext();
            var service = new EmployeeService(context);

            context.EmployeeTable.Add(employee);
            context.SaveChanges();

            var result = await service.GetAllEmployees();

            Assert.Equal(1, result.Count);
            Assert.Equal("John", result.ElementAt(0).FirstName);
        }

        [Fact]
        public async Task GetEmployeeById_ValidId_ReturnsEmployee()
        {
            var context = GetInMemoryContext();
            var service = new EmployeeService(context);

            context.EmployeeTable.Add(employee);
            context.SaveChanges();

            var result = await service.GetEmployeeById(1);

            Assert.NotNull(result);
            Assert.Equal("John", result.FirstName);
        }

        [Fact]
        public async Task GetEmployeeById_InvalidId_ReturnsNull()
        {
            var context = GetInMemoryContext();
            var service = new EmployeeService(context);

            var result = await service.GetEmployeeById(99);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetEmployeeByEmail_ValidEmail_ReturnsEmployee()
        {
            var context = GetInMemoryContext();
            var service = new EmployeeService(context);

            context.EmployeeTable.Add(employee);
            context.SaveChanges();

            var result = await service.GetEmployeeByEmail(employee.Email);

            Assert.NotNull(result);
            Assert.Equal("John", result.FirstName);
        }

        [Fact]
        public async Task GetEmployeeByEmail_InvalidEmail_ReturnsNull()
        {
            var context = GetInMemoryContext();
            var service = new EmployeeService(context);

            var result = await service.GetEmployeeByEmail("testEmail");

            Assert.Null(result);
        }

        [Fact]
        public async Task IsTeaching_TeachingIsTrue_ReturnsTrue()
        {
            var context = GetInMemoryContext();
            var service = new EmployeeService(context);

            employee.Teaching = true;

            context.EmployeeTable.Add(employee);
            context.SaveChanges();

            var result = await service.IsTeaching(1);

            Assert.True(result);
        }

        [Fact]
        public async Task IsTeaching_TeachingIsFalse_ReturnsFalse()
        {
            var context = GetInMemoryContext();
            var service = new EmployeeService(context);

            context.EmployeeTable.Add(employee);
            context.SaveChanges();

            var result = await service.IsTeaching(1);

            Assert.False(result);
        }
    }
}
