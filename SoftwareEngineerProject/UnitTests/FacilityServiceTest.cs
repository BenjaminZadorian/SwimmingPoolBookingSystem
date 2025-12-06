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
    public class FacilityServiceTest
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

        private DataContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new DataContext(options);
        }

        [Fact]
        public async Task AddFacility_AddRecord()
        {
            var context = GetInMemoryContext();
            var service = new FacilityService(context);

            var result = await service.AddFacility(facility);

            Assert.NotNull(result);
            Assert.Single(await context.FacilityTable.ToListAsync());
        }

        [Fact]
        public async Task DeleteFacility_DeleteRecord()
        {
            var context = GetInMemoryContext();
            var service = new FacilityService(context);

            await service.AddFacility(facility);

            var result = await service.DeleteFacility(1);

            Assert.True(result);
            var testDelete = await context.FacilityTable.FindAsync(1);
            Assert.Null(testDelete);
        }

        [Fact]
        public async Task EditFacility_UpdateRecord()
        {
            var context = GetInMemoryContext();
            var service = new FacilityService(context);

            await service.AddFacility(facility);

            var newFacility = facility;
            newFacility.City = "Test City 2";

            var result = await service.EditFacility(1, newFacility);

            Assert.NotNull(result);
            Assert.Equal("Test City 2", result.City);
        }

        [Fact]
        public async Task EditFacility_FacilityNotFound_ReturnNull()
        {
            var context = GetInMemoryContext();
            var service = new FacilityService(context);

            var newFacility = facility;
            newFacility.City = "Test City 2";

            var result = await service.EditFacility(1, newFacility);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllFacilities_ReturnAllRecords()
        {
            var context = GetInMemoryContext();
            var service = new FacilityService(context);

            await service.AddFacility(facility);

            var result = await service.GetAllFacilities();

            Assert.Equal(1, result.Count);
            Assert.Equal("Name", result.ElementAt(0).Name);
        }

        [Fact]
        public async Task GetFacilityById_ReturnFacility()
        {
            var context = GetInMemoryContext();
            var service = new FacilityService(context);

            await service.AddFacility(facility);

            var result = await service.GetFacilityById(1);

            Assert.NotNull(result);
            Assert.Equal("Name", result.Name);
        }

        [Fact]
        public async Task GetFacilityById_FacilityNotFound_ReturnNull()
        {
            var context = GetInMemoryContext();
            var service = new FacilityService(context);

            var result = await service.GetFacilityById(1);

            Assert.Null(result);
        }

        [Fact]
        public async Task OpenDuration_ReturnTimeSpan()
        {
            var context = GetInMemoryContext();
            var service = new FacilityService(context);

            await service.AddFacility(facility);

            var result = await service.OpenDuration(1);

            Assert.Equal(new TimeOnly(22, 0) - new TimeOnly(6, 0), result);
        }



    }
}
