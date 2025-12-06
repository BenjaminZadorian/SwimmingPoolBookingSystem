using System.ComponentModel.DataAnnotations;

namespace SoftwareEngineerProject.Database.Models
{
    public class MembershipTable
    {
        [Key]
        public int ID { get; set; }
        public decimal Price { get; set; }
        public bool ShowerAccess { get; set; }
        public string Tier { get; set; }
        public TimeOnly PoolAccessStartTime { get; set; }
        public TimeOnly PoolAccessEndTime { get; set; }
        public CustomerTable Customer { get; set; }
    }
}
