using SoftwareEngineerProject.Database.Models;

namespace SoftwareEngineerProject.Services.Interfaces
{
    public interface ICustomerLessonService
    {       
        // CRUD Operations
        Task<List<CustomerLessonTable>> GetAllCustomerLessons();
        Task<CustomerLessonTable> GetCustomerLessonById(int customerId);
        Task<bool> AddCustomerLesson(CustomerLessonTable clT);
        Task<CustomerLessonTable> EditCustomerLesson(int customerId, CustomerLessonTable clT);
        Task<bool> DeleteCustomerLesson(int customerId, int lessonId);

        Task<List<LessonTable>> GetLessonsByCustomer(int customerId);
    }
}
