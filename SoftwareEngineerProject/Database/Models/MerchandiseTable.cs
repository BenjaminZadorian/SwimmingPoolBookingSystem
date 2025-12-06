using System.ComponentModel.DataAnnotations;

namespace SoftwareEngineerProject.Database.Models
{
    public class MerchandiseTable
    {
        [Key]
        public int ID { get; set; }
        public decimal Price { get; set; }
        public int FacilityId { get; set; }
        public FacilityTable Facility { get; set; }
        public string Type { get; set; }
    }
}
