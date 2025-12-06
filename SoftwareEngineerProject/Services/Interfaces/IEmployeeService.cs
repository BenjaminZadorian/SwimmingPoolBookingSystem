using SoftwareEngineerProject.Database.Models;

namespace SoftwareEngineerProject.Services.Interfaces
{
    public interface IEmployeeService
    {
        // CRUD Operations
        Task<List<EmployeeTable>> GetAllEmployees();
        Task<EmployeeTable> GetEmployeeById(int id);
        Task<EmployeeTable> GetEmployeeByEmail(string email);
        Task<bool> AddEmployee(EmployeeTable eT);
        Task<EmployeeTable> EditEmployee(int id, EmployeeTable eT);
        Task<bool> DeleteEmployee(int id);

        // Employee Methods
        Task<bool> IsTeaching(int id);
        Task<EmployeeTable?> VerifyLogin(string email, string password);
        Task AddSaltAndHashed(int id, byte[] salt, string password);
        Task<LessonTable> GetToTeach(int id);
        Task<List<EmployeeTable>> GetEmployeesByFacility(int facilityId);
        Task<bool> MakeTeacher(int id);
    }
}
