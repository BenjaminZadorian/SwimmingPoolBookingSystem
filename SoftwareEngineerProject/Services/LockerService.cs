using Microsoft.EntityFrameworkCore;
using SoftwareEngineerProject.Database;
using SoftwareEngineerProject.Database.Models;
using SoftwareEngineerProject.Services.Interfaces;

namespace SoftwareEngineerProject.Services
{
    public class LockerService : ILockerService
    {
        public readonly DataContext _context;

        public LockerService(DataContext context)
        {
            _context = context;
        }

        public async Task<LockerTable> AddLocker(LockerTable lT)
        {
            _context.LockerTable.Add(lT);
            await _context.SaveChangesAsync();
            return lT;
        }

        public async Task<bool> DeleteLocker(int id)
        {
            var dbLocker = await _context.LockerTable.FindAsync(id);
            if (dbLocker != null)
            {
                _context.Remove(dbLocker);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<LockerTable> EditLocker(int id, LockerTable lT)
        {
            var dbLocker = await _context.LockerTable.FindAsync(id);
            if (dbLocker != null)
            {
                dbLocker.Rented = lT.Rented;
                dbLocker.Facility = lT.Facility;
                dbLocker.Owner = lT.Owner;
                await _context.SaveChangesAsync();
                return dbLocker;
            }
            return null;
        }

        public async Task<List<LockerTable>> GetAllLockers()
        {
            var lockers = await _context.LockerTable.ToListAsync();
            return lockers;
        }

        public async Task<LockerTable> GetLockerById(int id)
        {
            return await _context.LockerTable.FindAsync(id);
        }

        public async Task<List<LockerTable>> GetLockersByFacility(int facilityId)
        {
            var dbLockers = await _context.LockerTable.Where(l => l.Facility.ID == facilityId).ToListAsync();
            return dbLockers;
        }

        public async Task<List<LockerTable>> GetLockersByCustomer(int customerId)
        {
            var dbLockers = await _context.LockerTable.Where(l => l.Owner.ID == customerId).ToListAsync();
            return dbLockers;
        }

        public async Task<bool> AssignOwner(int id, CustomerTable cT)
        {
            var dbLocker = await _context.LockerTable.FindAsync(id);
            if (dbLocker != null)
            {
                dbLocker.Owner = cT;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> AssignRentalTime(int id, DateTime startRent)
        {
            var dbLocker = await _context.LockerTable.FindAsync(id);
            if (dbLocker != null)
            {
                dbLocker.StartRent = startRent;
                dbLocker.EndRent = startRent.AddHours(1);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<LockerTable>> GetLockerOwners(int facilityId)
        {
            var lockerOwners = await _context.LockerTable
                .Include(l => l.Owner)
                .Where(l => l.Facility.ID == facilityId)
                .ToListAsync();
            return lockerOwners;
        }
    }
}
