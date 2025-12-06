using System.ComponentModel.DataAnnotations;

namespace SoftwareEngineerProject.Database.Models
{
    public class CustomerTable
    {
        [Key]
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public byte[] Salt { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? MembershipID { get; set; }
        public MembershipTable? Membership { get; set; }
        public int AttendanceCount { get; set; }
        public int PurchasesMade { get; set; }
    }
}
