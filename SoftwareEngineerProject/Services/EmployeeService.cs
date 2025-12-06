using Microsoft.EntityFrameworkCore;
using SoftwareEngineerProject.Database;
using SoftwareEngineerProject.Database.Models;
using SoftwareEngineerProject.Services.Interfaces;

namespace SoftwareEngineerProject.Services
{
    public class EmployeeService : IEmployeeService
    {

        public readonly DataContext _context;
        
        public EmployeeService(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> AddEmployee(EmployeeTable eT)
        {
            List<EmployeeTable> dbAllEmployees = await GetAllEmployees();
            bool UserExists = dbAllEmployees.Any(employee => employee.Email == eT.Email);

            if (UserExists != true)
            {
                _context.EmployeeTable.Add(eT);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            var dbEmployee = await _context.EmployeeTable.FindAsync(id);
            if (dbEmployee != null)
            {
                _context.Remove(dbEmployee);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<EmployeeTable> EditEmployee(int id, EmployeeTable eT)
        {
            var dbEmployee = await _context.EmployeeTable.FindAsync(id);
            if (dbEmployee != null)
            {
                dbEmployee.FirstName = eT.FirstName;
                dbEmployee.LastName = eT.LastName;
                dbEmployee.BirthDate = eT.BirthDate;
                dbEmployee.Email = eT.Email;
                dbEmployee.Password = eT.Password;
                dbEmployee.Salt = eT.Salt;
                dbEmployee.StartTime = eT.StartTime;
                dbEmployee.EndTime = eT.EndTime;
                dbEmployee.WorkDays = eT.WorkDays;
                dbEmployee.Teaching = eT.Teaching;
                dbEmployee.Facility = eT.Facility;
                await _context.SaveChangesAsync();
                return dbEmployee;
            }
            return null;
        }

        public async Task<List<EmployeeTable>> GetAllEmployees()
        {
            var employees = await _context.EmployeeTable.ToListAsync();
            return employees;
        }

        public async Task<EmployeeTable> GetEmployeeById(int id)
        {
            return await _context.EmployeeTable.FindAsync(id);
        }

        public async Task<EmployeeTable> GetEmployeeByEmail(string email)
        {
            var dbEmployee = _context.EmployeeTable.Where(e => e.Email == email).FirstOrDefault();
            if (dbEmployee != null)
            {
                return dbEmployee;
            }
            return null;

        }

        public async Task<bool> IsTeaching(int id)
        {
            var dbEmployee = await _context.EmployeeTable.FindAsync(id);
            if (dbEmployee.Teaching == true)
            {
                return true;
            }
            return false;
        }

        public async Task<EmployeeTable?> VerifyLogin(string email, string password)
        {
            var dbAllEmployees = await GetAllEmployees();
            var chosenEmployee = dbAllEmployees.FirstOrDefault(employee => employee.Email.Equals(email, StringComparison.InvariantCultureIgnoreCase));

            if (chosenEmployee != null && HashingService.ValidatePassword(chosenEmployee.Salt, password, chosenEmployee.Password) == true)
            {
                return chosenEmployee;
            }
            return null;
        }

        public async Task AddSaltAndHashed(int id, byte[] salt, string password)
        {
            var dbCustomer = await _context.EmployeeTable.FindAsync(id);
            if (dbCustomer != null)
            {
                dbCustomer.Salt = salt;
                dbCustomer.Password = password;
            }
            throw new Exception("Employee not found");
        }

        public async Task<LessonTable> GetToTeach(int id)
        {
            var dbEmployee = await GetEmployeeById(id);
            if (dbEmployee != null)
            {
                var toTeach = await _context.LessonTable
                    .Where(l => l.Teacher == dbEmployee)
                    .FirstOrDefaultAsync();
                return toTeach;
            }
            return null;
        }

        public async Task<List<EmployeeTable>> GetEmployeesByFacility(int facilityId)
        {
            var dbEmployees = await _context.EmployeeTable.Where(e => e.Facility.ID == facilityId).ToListAsync();
            return dbEmployees;
        }

        public async Task<bool> MakeTeacher(int id)
        {
            var dbEmployee = await _context.EmployeeTable.FindAsync(id);
            if (dbEmployee != null)
            {
                dbEmployee.Teaching = true;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
