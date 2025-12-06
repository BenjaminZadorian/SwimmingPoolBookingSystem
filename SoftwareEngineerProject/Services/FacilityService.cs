using Microsoft.EntityFrameworkCore;
using SoftwareEngineerProject.Database;
using SoftwareEngineerProject.Database.Models;
using SoftwareEngineerProject.Services.Interfaces;

namespace SoftwareEngineerProject.Services
{
    public class FacilityService : IFacilityService
    {
        public readonly DataContext _context;
        private readonly IEquipmentService _equipmentService;
        private readonly ILockerService _lockerService;
        private readonly IMerchandiseService _merchandiseService;
        private readonly ICustomerService _customerService;

        public FacilityService(IEquipmentService equipmentService, ILockerService lockerService, IMerchandiseService merchandiseService, ICustomerService customerService, DataContext context)
        {
            _equipmentService = equipmentService;
            _lockerService = lockerService;
            _merchandiseService = merchandiseService;
            _customerService = customerService;
            _context = context;
        }

        public FacilityService(DataContext context)
        {
            _context = context;
        }

        public async Task<FacilityTable> AddFacility(FacilityTable fT)
        {
            _context.FacilityTable.Add(fT);
            await _context.SaveChangesAsync();
            return fT;
        }

        public async Task<bool> DeleteFacility(int id)
        {
            var dbFacility = await _context.FacilityTable.FindAsync(id);
            if (dbFacility != null)
            {
                _context.Remove(dbFacility);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<FacilityTable> EditFacility(int id, FacilityTable fT)
        {
            var dbFacility = await _context.FacilityTable.FindAsync(id);
            if (dbFacility != null)
            {
                dbFacility.Name = fT.Name;
                dbFacility.Address = fT.Address;
                dbFacility.City = fT.City;
                dbFacility.PostCode = fT.PostCode;
                dbFacility.OpenTime = fT.OpenTime;
                dbFacility.CloseTime = fT.CloseTime;
                await _context.SaveChangesAsync();
                return dbFacility;
            }
            return null;
        }

        public async Task<List<FacilityTable>> GetAllFacilities()
        {
            var facilities = await _context.FacilityTable.ToListAsync();
            return facilities;
        }

        public async Task<FacilityTable> GetFacilityById(int id)
        {
            return await _context.FacilityTable.FindAsync(id);
        }

        public async Task<TimeSpan> OpenDuration(int id)
        {
            var dbFacility = await _context.FacilityTable.FindAsync(id);
            if (dbFacility != null)
            {
                TimeSpan duration = dbFacility.CloseTime - dbFacility.OpenTime;
                return duration;
            }
            throw new Exception("Facility not found.");
        }

        public async Task<FacilityDataBundle> GetFacilityBundle(int facilityId, int customerId)
        {
            var facilityTask = await GetFacilityById(facilityId);
            var equipmentTask = await _equipmentService.GetEquipmentByFacility(facilityId);
            var lockersTask = await _lockerService.GetLockersByFacility(facilityId);
            var merchTask = await _merchandiseService.GetMerchandiseByFacility(facilityId);
            var rentedLockersTask = await _lockerService.GetLockersByCustomer(customerId);
            var rentedEquipTask = await _equipmentService.GetEquipmentByCustomer(customerId);


            return new FacilityDataBundle
            {
                Facility = facilityTask,
                Equipment = equipmentTask,
                Lockers = lockersTask,
                Merchandise = merchTask,
                RentedLockers = rentedLockersTask,
                RentedEquipment = rentedEquipTask
            };
        }

    }
}
