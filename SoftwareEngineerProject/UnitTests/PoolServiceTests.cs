using Microsoft.EntityFrameworkCore;
using SoftwareEngineerProject.Database;
using SoftwareEngineerProject.Database.Models;
using SoftwareEngineerProject.Services;
using Xunit;
namespace SoftwareEngineerProject.UnitTests
{
    public class PoolServiceTests
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

        private PoolTable pool = new PoolTable
        {
            ID = 1,
            FacilityID = 1,
            Facility = facility,
            Type = "Indoor",
            Booked = false,
            Lessons = new List<LessonTable>()
        };
        private DataContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new DataContext(options);
        }

        [Fact]
        public async Task AddPool_AddRecord()
        {
            var context = GetInMemoryContext();
            var service = new PoolService(context);

            var result = await service.AddPool(pool);

            Assert.NotNull(result);
            Assert.Single(await context.PoolTable.ToListAsync());
        }

        [Fact]
        public async Task DeletePool_RemoveRecord()
        {
            var context = GetInMemoryContext();
            var service = new PoolService(context);

            await service.AddPool(pool);

            var result = await service.DeletePool(1);

            Assert.True(result);
            var testDelete = await context.PoolTable.FindAsync(1);
            Assert.Null(testDelete);
        }

        [Fact]
        public async Task DeletePool_PoolNotFound_RemoveFalse()
        {
            var context = GetInMemoryContext();
            var service = new PoolService(context);

            var result = await service.DeletePool(1);

            Assert.False(result);
        }

        [Fact]
        public async Task EditPool_ReturnRecord()
        {
            var context = GetInMemoryContext();
            var service = new PoolService(context);

            await service.AddPool(pool);

            var updatedPool = pool;

            updatedPool.Type = "Outdoor";

            var result = await service.EditPool(1, updatedPool);

            Assert.NotNull(result);
            Assert.Equal("Outdoor", result.Type);
        }

        [Fact]
        public async Task EditPool_PoolNotFound_ReturnNull()
        {
            var context = GetInMemoryContext();
            var service = new PoolService(context);

            var updatedPool = pool;

            updatedPool.Type = "Outdoor";

            var result = await service.EditPool(1, updatedPool);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllPools_ReturnAllRecords()
        {
            var context = GetInMemoryContext();
            var service = new PoolService(context);

            await service.AddPool(pool);

            var result = await service.GetAllPools();
            Assert.Equal(1, result.Count);
            Assert.Equal("Indoor", result.ElementAt(0).Type);
        }

        [Fact]
        public async Task GetPoolById_ReturnRecord()
        {
            var context = GetInMemoryContext();
            var service = new PoolService(context);

            await service.AddPool(pool);

            var result = await service.GetPoolById(1);

            Assert.NotNull(result);
            Assert.Equal("Indoor", result.Type);
        }


        [Fact]
        public async Task GetPoolById_PoolNotFound_ReturnNull()
        {
            var context = GetInMemoryContext();
            var service = new PoolService(context);

            var result = await service.GetPoolById(1);

            Assert.Null(result);
        }
    }
}
