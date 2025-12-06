using Microsoft.EntityFrameworkCore;
using SoftwareEngineerProject.Database;
using SoftwareEngineerProject.Database.Models;
using SoftwareEngineerProject.Services.Interfaces;


namespace SoftwareEngineerProject.Services
{
    public class CustomerLessonService : ICustomerLessonService
    {
        public readonly DataContext _context;

        public CustomerLessonService(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> AddCustomerLesson(CustomerLessonTable clT)
        {
            if (clT == null)
            {
                throw new ArgumentNullException(nameof(clT), "CustomerLessonTable can't be null");
            }
            if (clT.CustomerID <= 0 || clT.LessonID <= 0)
            {
                return false;
            }
            _context.CustomerLessonTable.Add(clT);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCustomerLesson(int customerId, int lessonId)
        {
            var dbCustomerLesson = await _context.CustomerLessonTable.FirstOrDefaultAsync(clT => clT.CustomerID == customerId && clT.LessonID == lessonId);
            if (dbCustomerLesson != null)
            {
                _context.Remove(dbCustomerLesson);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<CustomerLessonTable> EditCustomerLesson(int customerId, CustomerLessonTable clT)
        {
            var dbCustomerLesson = await _context.CustomerLessonTable.FindAsync(customerId);
            if (dbCustomerLesson != null)
            {
                dbCustomerLesson.CustomerID = clT.CustomerID;
                dbCustomerLesson.Customer = clT.Customer;
                dbCustomerLesson.LessonID = clT.LessonID;
                dbCustomerLesson.Lesson = clT.Lesson;
                await _context.SaveChangesAsync();
                return dbCustomerLesson;
            }
            return null;
        }

        public async Task<List<CustomerLessonTable>> GetAllCustomerLessons()
        {
            var lessons = await _context.CustomerLessonTable.ToListAsync();
            return lessons;
        }

        public async Task<CustomerLessonTable> GetCustomerLessonById(int customerId)
        {
            return await _context.CustomerLessonTable.FindAsync(customerId);
        }

        public async Task<List<LessonTable>> GetLessonsByCustomer(int custId)
        {
            var dbLessons = await _context.CustomerLessonTable
                .Where(cl => cl.CustomerID == custId)
                .Include(cl => cl.Lesson)
                .Select(cl => cl.Lesson)
                .ToListAsync();
            Console.WriteLine("HERE");
            Console.WriteLine(dbLessons);

            return dbLessons;

        }
    }
}
