using Microsoft.EntityFrameworkCore;
using SoftwareEngineerProject.Database;
using SoftwareEngineerProject.Database.Models;
using SoftwareEngineerProject.Services.Interfaces;

namespace SoftwareEngineerProject.Services
{
    public class MembershipService : IMembershipService
    {

        public readonly DataContext _context;

        public MembershipService(DataContext context)
        {
            _context = context;
        }

        public async Task<MembershipTable> AddMembership(MembershipTable mT)
        {
            _context.MembershipTable.Add(mT);
            await _context.SaveChangesAsync();
            return mT;
        }

        public async Task<bool> DeleteMembership(int id)
        {
            var dbMembership = await _context.MembershipTable.FindAsync(id);
            if (dbMembership != null)
            {
                _context.Remove(dbMembership);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<MembershipTable> EditMembership(int id, MembershipTable mT)
        {
            var dbMembership = await _context.MembershipTable.FindAsync(id);
            if (dbMembership != null)
            {
                dbMembership.ShowerAccess = mT.ShowerAccess;
                dbMembership.Tier = mT.Tier;
                dbMembership.PoolAccessStartTime = mT.PoolAccessStartTime;
                dbMembership.PoolAccessEndTime = mT.PoolAccessEndTime;
                await _context.SaveChangesAsync();
                return dbMembership;
            }
            return null;
        }

        public async Task<List<MembershipTable>> GetAllMemberships()
        {
            var memberships = await _context.MembershipTable.ToListAsync();
            return memberships;
        }

        public async Task<MembershipTable> GetMembershipById(int id)
        {
            return await _context.MembershipTable.FindAsync(id);
        }
    }
}
