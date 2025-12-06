using Microsoft.EntityFrameworkCore;
using SoftwareEngineerProject.Database;
using SoftwareEngineerProject.Database.Models;
using SoftwareEngineerProject.Services;
using Xunit;

namespace SoftwareEngineerProject.UnitTests
{
    public class MembershipServiceTest
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

        private MembershipTable membership = new MembershipTable
        {
            ID = 1,
            Price = 10.00m,
            ShowerAccess = false,
            Tier = "Gold",
            PoolAccessStartTime = new TimeOnly(12, 00),
            PoolAccessEndTime = new TimeOnly(16, 00)
        };

        private DataContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new DataContext(options);
        }

        [Fact]
        public async Task AddMembership_AddRecord()
        {
            var context = GetInMemoryContext();
            var service = new MembershipService(context);

            var result = await service.AddMembership(membership);

            Assert.NotNull(result);
            Assert.Single(await context.MembershipTable.ToListAsync());
        }

        [Fact]
        public async Task DeleteMembership_RemoveRecord()
        {
            var context = GetInMemoryContext();
            var service = new MembershipService(context);
            await service.AddMembership(membership);

            var result = await service.DeleteMembership(1);

            Assert.True(result);
            var testDelete = await context.MembershipTable.FindAsync(1);
            Assert.Null(testDelete);
        }

        [Fact]
        public async Task DeleteMembership_MembershipNotFound_ReturnFalse()
        {
            var context = GetInMemoryContext();
            var service = new MembershipService(context);

            var result = await service.DeleteMembership(1);

            Assert.False(result);
        }

        [Fact]
        public async Task EditMembership_ReturnRecord()
        {
            var context = GetInMemoryContext();
            var service = new MembershipService(context);
            await service.AddMembership(membership);

            var updatedMembership = membership;

            updatedMembership.Tier = "Bronze";

            var result = await service.EditMembership(1, updatedMembership);

            Assert.NotNull(result);
            Assert.Equal("Bronze", result.Tier);
        }

        [Fact]
        public async Task EditMembership_MembershipNotFound_ReturnNull()
        {
            var context = GetInMemoryContext();
            var service = new MembershipService(context);

            var updatedMembership = membership;

            updatedMembership.Tier = "Bronze";

            var result = await service.EditMembership(1, updatedMembership);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllMemberships_ReturnAllRecords()
        {
            var context = GetInMemoryContext();
            var service = new MembershipService(context);
            await service.AddMembership(membership);

            var result = await service.GetAllMemberships();
            Assert.Equal(1, result.Count);
            Assert.Equal("Gold", result.ElementAt(0).Tier);
        }

        [Fact]
        public async Task GetMembershipById_ReturnRecord()
        {
            var context = GetInMemoryContext();
            var service = new MembershipService(context);
            await service.AddMembership(membership);

            var result = await service.GetMembershipById(1);

            Assert.NotNull(result);
            Assert.Equal("Gold", result.Tier);
        }

        [Fact]
        public async Task GetMembershipById_RecordNotFound_ReturnNull()
        {
            var context = GetInMemoryContext();
            var service = new MembershipService(context);

            var result = await service.GetMembershipById(1);

            Assert.Null(result);
        }
    }
}
