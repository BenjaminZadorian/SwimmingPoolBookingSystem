using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using SoftwareEngineerProject.Database;
using SoftwareEngineerProject.Database.Models;
using SoftwareEngineerProject.Services.Interfaces;

namespace SoftwareEngineerProject.Services
{
    public class PoolService : IPoolService
    {
        public readonly DataContext _context;
        public PoolService(DataContext context)
        {
            _context = context;
        }

        public async Task<PoolTable> AddPool(PoolTable pT)
        {
            _context.PoolTable.Add(pT);
            await _context.SaveChangesAsync();
            return pT;
        }

        public async Task<bool> DeletePool(int id)
        {
            var dbPool = await _context.PoolTable.FindAsync(id);
            if (dbPool != null)
            {
                _context.Remove(dbPool);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<PoolTable> EditPool(int id, PoolTable pT)
        {
            var dbPool = await _context.PoolTable.FindAsync(id);
            if (dbPool != null)
            {
                dbPool.Facility = pT.Facility;
                dbPool.Type = pT.Type;
                dbPool.Booked = pT.Booked;
                await _context.SaveChangesAsync();
                return dbPool;
            }
            return null;
        }

        public async Task<List<PoolTable>> GetAllPools()
        {
            var pools = await _context.PoolTable.ToListAsync();
            return pools;
        }

        public async Task<PoolTable> GetPoolById(int id)
        {
            return await _context.PoolTable.FindAsync(id);
        }
    }
}
