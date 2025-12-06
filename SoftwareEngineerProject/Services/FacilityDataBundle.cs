using System;
using System.Collections.Generic;
using SoftwareEngineerProject.Database.Models;

namespace SoftwareEngineerProject.Services
{
    public class FacilityDataBundle
    {
        public FacilityTable Facility { get; set; }

        public List<EquipmentTable> Equipment { get; set; } = new List<EquipmentTable>();
        public List<LockerTable> Lockers { get; set; } = new List<LockerTable>();
        public List<MerchandiseTable> Merchandise { get; set; } = new List<MerchandiseTable>();
        public List<LockerTable> RentedLockers { get; set; } = new List<LockerTable>();
        public List<EquipmentTable> RentedEquipment { get; set; } = new List<EquipmentTable>();
    }
}
