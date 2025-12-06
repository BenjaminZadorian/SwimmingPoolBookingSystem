using Microsoft.EntityFrameworkCore;
using SoftwareEngineerProject.Database;
using SoftwareEngineerProject.Database.Models;
using SoftwareEngineerProject.Services.Interfaces;

namespace SoftwareEngineerProject.Services
{
    public class MerchandiseService : IMerchandiseService
    {
        public readonly DataContext _context;

        public MerchandiseService(DataContext context)
        {
            _context = context;
        }

        public async Task<MerchandiseTable> AddMerchandise(MerchandiseTable mT)
        {
            _context.MerchandiseTable.Add(mT);
            await _context.SaveChangesAsync();
            return mT;
        }

        public async Task<bool> DeleteMerchandise(int id)
        {
            var dbMerchandise = await _context.MerchandiseTable.FindAsync(id);
            if (dbMerchandise != null)
            {
                _context.Remove(dbMerchandise);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<MerchandiseTable> EditMerchandise(int id, MerchandiseTable mT)
        {
            var dbMerchandise = await _context.MerchandiseTable.FindAsync(id);
            if (dbMerchandise != null)
            {
                dbMerchandise.Price = mT.Price;
                dbMerchandise.FacilityId = mT.FacilityId;
                dbMerchandise.Facility = mT.Facility;
                dbMerchandise.Type = mT.Type;
                await _context.SaveChangesAsync();
                return dbMerchandise;
            }
            return null;
        }

        public async Task<List<MerchandiseTable>> GetAllMerchandise()
        {
            var merchandise = await _context.MerchandiseTable.ToListAsync();
            return merchandise;
        }

        public async Task<List<MerchandiseTable>> GetMerchandiseByFacility(int facilityId)
        {
            var dbMerchandise = await _context.MerchandiseTable.Where(m => m.Facility.ID == facilityId).ToListAsync();
            return dbMerchandise;
        }

        public async Task<MerchandiseTable> GetMerchandiseById(int id)
        {
            return await _context.MerchandiseTable.FindAsync(id);
        }
    }
}