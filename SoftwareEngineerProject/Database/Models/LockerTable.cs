using System.ComponentModel.DataAnnotations;

namespace SoftwareEngineerProject.Database.Models
{
    public class LockerTable
    {
        [Key]
        public int ID { get; set; } 
        public bool Rented { get; set; }
        public FacilityTable Facility { get; set; }
        public int? OwnerID { get; set; }
        public CustomerTable? Owner { get; set; }
        public DateTime StartRent { get; set; }
        public DateTime EndRent { get; set; }
    }
}
