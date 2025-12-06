using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace SoftwareEngineerProject.Database.Models
{
    public class LessonTable
    {
        [Key]
        public int ID { get; set; }
        public string Type { get; set; }
        public int PersonCount { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public EmployeeTable Teacher { get; set; }
        public int PoolID { get; set; }
        public PoolTable Pool { get; set; }
    }
}
