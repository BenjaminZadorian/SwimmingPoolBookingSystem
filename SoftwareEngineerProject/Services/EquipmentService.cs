using Microsoft.EntityFrameworkCore;
using SoftwareEngineerProject.Database;
using SoftwareEngineerProject.Database.Models;
using SoftwareEngineerProject.Services.Interfaces;

namespace SoftwareEngineerProject.Services
{
    public class EquipmentService : IEquipmentService
    {

        public readonly DataContext _context;

        public EquipmentService(DataContext context)
        {
            _context = context;
        }

        public async Task<EquipmentTable> AddEquipment(EquipmentTable eT)
        {
            _context.EquipmentTable.Add(eT);
            await _context.SaveChangesAsync();
            return eT;
        }

        public async Task<bool> DeleteEquipment(int id)
        {
            var dbEquipment = await _context.EquipmentTable.FindAsync(id);
            if (dbEquipment != null)
            {
                _context.Remove(dbEquipment);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<EquipmentTable> EditEquipment(int id, EquipmentTable eT)
        {
            var dbEquipment = await _context.EquipmentTable.FindAsync(id);
            if (dbEquipment != null)
            {
                dbEquipment.Rented = eT.Rented;
                dbEquipment.Type = eT.Type;
                dbEquipment.Price = eT.Price;
                dbEquipment.Facility = eT.Facility;
                dbEquipment.Owner = eT.Owner;
                await _context.SaveChangesAsync();
                return dbEquipment;
            }
            throw new Exception("Equipment not found.");
        }

        public async Task<List<EquipmentTable>> GetAllEquipment()
        {
            var equipment = await _context.EquipmentTable.ToListAsync();
            return equipment;
        }
        public async Task<EquipmentTable> GetEquipmentById(int id)
        {
            return await _context.EquipmentTable.FindAsync(id);
        }
        public async Task<EquipmentTable> AssignOwner(int id, CustomerTable customer)
        {
            var dbEquipment = await _context.EquipmentTable.FindAsync(id);
            if (dbEquipment != null && customer != null)
            {
                dbEquipment.Owner = customer;
                await _context.SaveChangesAsync();
                return dbEquipment;
            }
            return null;
        }

        public async Task<List<EquipmentTable>> GetEquipmentByFacility(int facilityId)
        {
            var dbEquipment = await _context.EquipmentTable.Where(e => e.Facility.ID == facilityId).ToListAsync();
            return dbEquipment;
        }

        public async Task<List<EquipmentTable>> GetEquipmentByCustomer(int customerId)
        {
            var dbEquipment = await _context.EquipmentTable.Where(e => e.Owner.ID == customerId).ToListAsync();
            return dbEquipment;
        }

        public async Task<bool> AssignRentalTime(int id, DateTime startRent)
        {
            var dbEquipment = await _context.EquipmentTable.FindAsync(id);
            if (dbEquipment != null)
            {
                dbEquipment.StartRent = startRent;
                dbEquipment.EndRent = startRent.AddHours(1);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<EquipmentTable>> GetEquipmentOwners(int facilityId)
        {
            var equipmentWithOwners = await _context.EquipmentTable.Include(e => e.Owner).Where(e => e.Facility.ID == facilityId).ToListAsync();
            return equipmentWithOwners;
        }
    }
}
