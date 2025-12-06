using Microsoft.EntityFrameworkCore;
using SoftwareEngineerProject.Database.Models;

namespace SoftwareEngineerProject.Database
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
            
        }

        public DbSet<FacilityTable> FacilityTable { get; set; }
        public DbSet<CustomerTable> CustomerTable { get; set; }
        public DbSet<MembershipTable> MembershipTable { get; set; }
        public DbSet<MerchandiseTable> MerchandiseTable { get; set; }
        public DbSet<EquipmentTable> EquipmentTable { get; set; }
        public DbSet<PoolTable> PoolTable { get; set; }
        public DbSet<EmployeeTable> EmployeeTable { get; set; }
        public DbSet<LessonTable> LessonTable { get; set; }
        public DbSet<LockerTable> LockerTable { get; set; }
        public DbSet<OrderTable> OrderTable { get; set; }
        public DbSet<CustomerLessonTable> CustomerLessonTable { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerTable>()
                .HasOne(c => c.Membership)
                .WithOne(m => m.Customer)
                .HasForeignKey<CustomerTable>(c => c.MembershipID)
                .OnDelete(DeleteBehavior.Cascade); // delete itself when customer is deleted

            modelBuilder.Entity<EmployeeTable>()
                .HasOne(e => e.Facility)
                .WithMany()
                .HasForeignKey(e => e.FacilityID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LockerTable>()
                .HasOne(l => l.Owner)
                .WithMany()
                .HasForeignKey(l => l.OwnerID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EquipmentTable>()
                .HasOne(e => e.Owner)
                .WithMany()
                .HasForeignKey(e => e.OwnerID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LessonTable>()
                .HasOne(l => l.Pool)
                .WithMany(p => p.Lessons)
                .HasForeignKey(l => l.PoolID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PoolTable>()
                .HasOne(p => p.Facility)
                .WithMany()
                .HasForeignKey(p => p.FacilityID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CustomerLessonTable>()
                .HasOne(cl => cl.Lesson)
                .WithMany()
                .HasForeignKey(cl => cl.LessonID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderTable>()
                .HasOne(o => o.Facility)
                .WithMany()
                .HasForeignKey(o => o.FacilityId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
