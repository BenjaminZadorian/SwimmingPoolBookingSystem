using Microsoft.EntityFrameworkCore;
using SoftwareEngineerProject.Database;
using SoftwareEngineerProject.Database.Models;
using SoftwareEngineerProject.Services.Interfaces;

namespace SoftwareEngineerProject.Services
{
    public class LessonService : ILessonService
    {
        public readonly DataContext _context;

        public LessonService(DataContext context)
        {
            _context = context;
        }

        public async Task<LessonTable> AddLesson(LessonTable lT)
        {
            _context.LessonTable.Add(lT);
            await _context.SaveChangesAsync();
            return lT;
        }

        public async Task<bool> DeleteLesson(int id)
        {
            var dbLesson = await _context.LessonTable.FindAsync(id);
            if (dbLesson != null)
            {
                _context.Remove(dbLesson);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<LessonTable> EditLesson(int id, LessonTable lT)
        {
            var dbLesson = await _context.LessonTable.FindAsync(id);
            if (dbLesson != null)
            {
                dbLesson.PersonCount = lT.PersonCount;
                dbLesson.StartTime = lT.StartTime;
                dbLesson.EndTime = lT.EndTime;
                dbLesson.Teacher = lT.Teacher;
                dbLesson.Pool = lT.Pool;
                return lT;
            }
            throw new Exception("Lesson not found.");
        }

        public async Task<List<LessonTable>> GetAllLessons()
        {
            var lessons = await _context.LessonTable.ToListAsync();
            return lessons;
        }

        public async Task<LessonTable> GetLessonById(int id)
        {
            return await _context.LessonTable.FindAsync(id);
        }
        public async Task<TimeSpan> ClassDuration(int id)
        {
            var dbLesson = await _context.LessonTable.FindAsync(id);
            if (dbLesson != null)
            {
                return dbLesson.EndTime - dbLesson.StartTime;
            }
            throw new Exception("Lesson not found.");
        }
    }
}
