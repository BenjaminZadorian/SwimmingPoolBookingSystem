using SoftwareEngineerProject.Database.Models;
using SoftwareEngineerProject.DataStructures;
using SoftwareEngineerProject.Services.Interfaces;

namespace SoftwareEngineerProject.Services
{
    public class DataService
    {
        public CustomHashTable<string, CustomerTable> CustomerHashTable { get; private set; }
        // Cache to lookup customers quickly by ID
        public Dictionary<int, CustomerTable> CustomerIdCache { get; private set; } = new Dictionary<int, CustomerTable>();
        public CustomHashTable<string, EmployeeTable> EmployeeHashTable { get; private set; }
        public CustomHashTable<string, LessonTable> LessonHashTable { get; private set; }
        public CustomHashTable<string, PoolTable> PoolHashTable { get; private set; }

        public Dictionary<int, List<LessonTable>> CustomerLessonDict { get; private set; } = new Dictionary<int, List<LessonTable>>();


        public bool IsCustomerLoaded => CustomerHashTable != null && CustomerHashTable.GetAllEntries().Count() > 0;
        public bool IsEmployeeLoaded => EmployeeHashTable != null && EmployeeHashTable.GetAllEntries().Count() > 0;
        public bool IsLessonLoaded => LessonHashTable != null && LessonHashTable.GetAllEntries().Count() > 0;
        public bool IsCustomerLessonLoaded(int customerId) => CustomerLessonDict.ContainsKey(customerId);
        public bool IsPoolLoaded => PoolHashTable != null && PoolHashTable.GetAllEntries().Count() > 0;

        public DataService()
        {
            CustomerHashTable = new CustomHashTable<string, CustomerTable>();
        }

        // Init Functions
        public void InitCustomerHashTable(int size)
        {
            CustomerHashTable = new CustomHashTable<string, CustomerTable>(size);
        }

        public void InitEmployeeHashTable(int size)
        {
            EmployeeHashTable = new CustomHashTable<string, EmployeeTable>(size);
        }

        public void InitLessonHashTable(int size)
        {
            LessonHashTable = new CustomHashTable<string, LessonTable>(size);
        }

        public void InitPoolHashTable(int size)
        {
            PoolHashTable = new CustomHashTable<string, PoolTable>(size);
        }


        // Add Data Functions
        public void AddCustomerData(string key, CustomerTable value)
        {
            CustomerHashTable.Insert(key, value);
        }

        public void AddCacheCustomer(CustomerTable customer)
        {
            CustomerIdCache[customer.ID] = customer;
        }

        public void AddEmployeeData(string key, EmployeeTable value)
        {
            EmployeeHashTable.Insert(key, value);
        }

        public void AddLessonData(string key, LessonTable value)
        {
            LessonHashTable.Insert(key, value);
        }

        public void AddPoolData(string key, PoolTable value)
        {
            PoolHashTable.Insert(key, value);
        }

        public void AddCustomerLessons(int customerId, List<LessonTable> lessonList)
        {
            CustomerLessonDict[customerId] = lessonList;
        }


        // Get Functions
        public LessonTable GetLessonById(int lessonId)
        {
            return LessonHashTable.LookUp(lessonId.ToString());
        }

        public List<LessonTable> GetCustomerLessons(int customerId)
        {
            return CustomerLessonDict.TryGetValue(customerId, out var lessons) ? lessons : new List<LessonTable>();
        }

        public bool CheckCustomerById(int id, out CustomerTable customer)
        {
            return CustomerIdCache.TryGetValue(id, out customer);
        }
    }
}
