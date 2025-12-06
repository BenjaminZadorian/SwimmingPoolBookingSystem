using SoftwareEngineerProject.Database.Models;

namespace SoftwareEngineerProject.Services.Interfaces
{
    public interface ILessonService
    {
        // CRUD Operations
        Task<List<LessonTable>> GetAllLessons();
        Task<LessonTable> GetLessonById(int id);
        Task<LessonTable> AddLesson(LessonTable lT);
        Task<LessonTable> EditLesson(int id, LessonTable lT);
        Task<bool> DeleteLesson(int id);

        // Lesson Methods
        Task<TimeSpan> ClassDuration(int id);

    }
}
