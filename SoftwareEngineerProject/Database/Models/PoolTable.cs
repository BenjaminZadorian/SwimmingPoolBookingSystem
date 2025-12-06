using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftwareEngineerProject.Database.Models
{
    public class PoolTable
    {
        [Key]
        public int ID { get; set; }
        public int FacilityID { get; set; }
        public FacilityTable Facility { get; set; }
        public string Type { get; set; }
        public bool Booked { get; set; }
        public ICollection<LessonTable> Lessons { get; set; } = new List<LessonTable>();
    }
}
