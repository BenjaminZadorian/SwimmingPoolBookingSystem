using System.ComponentModel.DataAnnotations;

namespace SoftwareEngineerProject.Database.Models
{
    public class CustomerLessonTable
    {
        [Key]
        public int ID { get; set; }

        public int CustomerID { get; set; }
        public CustomerTable Customer { get; set; }

        public int LessonID { get; set; }
        public LessonTable Lesson { get; set; }
    }
}
