using System.ComponentModel.DataAnnotations;

namespace SoftwareEngineerProject.Database.Models
{
    public class OrderTable
    {
        [Key]
        public int RowID { get; set; }
        public string OrderType { get; set; }
        public int CustomerId { get; set; }
        public CustomerTable Customer { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal OrderPrice { get; set; }
        public int? FacilityId { get; set; }
        public FacilityTable? Facility { get; set; }
    }
}
