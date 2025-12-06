using Microsoft.EntityFrameworkCore;
using SoftwareEngineerProject.Database;
using SoftwareEngineerProject.Database.Models;
using SoftwareEngineerProject.Services;
using Xunit;

namespace SoftwareEngineerProject.UnitTests
{
    public class MerchandiseServiceTest
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

        private MerchandiseTable merch = new MerchandiseTable
        {
            ID = 1,
            Price = 10.00m,
            FacilityId = 1,
            Facility = facility,
            Type = "Towel"
        };
        private DataContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new DataContext(options);
        }

        [Fact]
        public async Task AddMerchandise_AddRecord()
        {
            var context = GetInMemoryContext();
            var service = new MerchandiseService(context);

            var result = await service.AddMerchandise(merch);

            Assert.NotNull(result);
            Assert.Single(await context.MerchandiseTable.ToListAsync());
        }

        [Fact]
        public async Task DeleteMerchandise_RemoveRecord()
        {
            var context = GetInMemoryContext();
            var service = new MerchandiseService(context);

            await service.AddMerchandise(merch);

            var result = await service.DeleteMerchandise(1);

            Assert.True(result);
            var testDelete = await context.MerchandiseTable.FindAsync(1);
            Assert.Null(testDelete);
        }

        [Fact]
        public async Task DeleteMerchandise_MerchNotFound_ReturnFalse()
        {
            var context = GetInMemoryContext();
            var service = new MerchandiseService(context);

            var result = await service.DeleteMerchandise(1);

            Assert.False(result);
        }

        [Fact]
        public async Task EditMerchandise_ReturnRecord()
        {
            var context = GetInMemoryContext();
            var service = new MerchandiseService(context);

            await service.AddMerchandise(merch);

            var updatedMerch = merch;

            updatedMerch.Type = "New Type";

            var result = await service.EditMerchandise(1, updatedMerch);

            Assert.NotNull(result);
            Assert.Equal("New Type", result.Type);
        }

        [Fact]
        public async Task EditMerchandise_MerchNotFound_ReturnNull()
        {
            var context = GetInMemoryContext();
            var service = new MerchandiseService(context);

            var updatedMerch = merch;

            updatedMerch.Type = "New Type";

            var result = await service.EditMerchandise(1, updatedMerch);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllMerchandise_ReturnAllRecords()
        {
            var context = GetInMemoryContext();
            var service = new MerchandiseService(context);

            await service.AddMerchandise(merch);

            var result = await service.GetAllMerchandise();
            Assert.Equal(1, result.Count);
            Assert.Equal("Towel", result.ElementAt(0).Type);
        }

        [Fact]
        public async Task GetMerchandiseByFacility_ReturnAllFacilityMerchRecords()
        {
            var context = GetInMemoryContext();
            var service = new MerchandiseService(context);

            await service.AddMerchandise(merch);

            var result = await service.GetMerchandiseByFacility(1);
            Assert.Single(await context.MerchandiseTable.ToListAsync());
        }

        [Fact]
        public async Task GetMerchandiseById_ReturnRecord()
        {
            var context = GetInMemoryContext();
            var service = new MerchandiseService(context);

            await service.AddMerchandise(merch);

            var result = await service.GetMerchandiseById(1);

            Assert.NotNull(result);
            Assert.Equal("Towel", result.Type);
        }

        [Fact]
        public async Task GetMerchandiseById_MerchNotFound_ReturnNull()
        {
            var context = GetInMemoryContext();
            var service = new MerchandiseService(context);

            var result = await service.GetMerchandiseById(1);

            Assert.Null(result);
        }
    }
}
