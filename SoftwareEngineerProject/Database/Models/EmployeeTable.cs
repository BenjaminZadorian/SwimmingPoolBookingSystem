using System.ComponentModel.DataAnnotations;

namespace SoftwareEngineerProject.Database.Models
{
    public class EmployeeTable
    {
        [Key]
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public byte[] Salt { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string WorkDays { get; set; }
        public bool? Teaching { get; set; }
        public int? FacilityID { get; set; }
        public FacilityTable? Facility { get; set; }
    }
}
