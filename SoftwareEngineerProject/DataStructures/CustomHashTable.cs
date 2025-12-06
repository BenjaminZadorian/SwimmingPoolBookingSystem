using System.Collections;
using System.Security.Cryptography;

namespace SoftwareEngineerProject.DataStructures
{
    public class CustomHashTable<TKey, TValue>
    {

        private int TABLE_SIZE;
        private int MAX_INPUT = 256;
        private int DOUBLE_HASH_PRIME;
        private static readonly ulong FNV_OFFSET_BASIS = 14695981039346656037;
        private static readonly ulong FNV_PRIME = 1099511628211;
        public List<CustomNode> HashTable;
        private int CurrentKeys;

        // Empty Constructor
        public CustomHashTable()
        {
            TABLE_SIZE = 0;
            HashTable = new List<CustomNode>();
            DOUBLE_HASH_PRIME = GetDoubleHashPrime();
            CurrentKeys = 0;
            InitHashTable();
        }


        // Constructor to set table size for hash table
        public CustomHashTable(int table_size)
        {
            TABLE_SIZE = table_size;
            HashTable = new List<CustomNode>();
            DOUBLE_HASH_PRIME = GetDoubleHashPrime();
            CurrentKeys = 0;
            InitHashTable();
        }

        // Create a custom node class to store the key, data and state, for logical deletion
        public class CustomNode
        {
            public TKey Key { get; set; }
            public TValue Value { get; set; }

            public int State { get; set; }

            public CustomNode()
            {
                Key = default;
                Value = default;
                State = 0;
            }

            public CustomNode(TKey key, TValue value)
            {
                Key = key;
                Value = value;
                State = 1;
            }
        }

        // Fill the hashtable with empty custom nodes with no data and an empty state
        private void InitHashTable()
        {
            for (int i = 0; i < TABLE_SIZE; i++)
            {
                HashTable.Add(new CustomNode());
            }
        }

        // Check if the hash table is full
        public bool IsFull()
        {
            return TABLE_SIZE == CurrentKeys;
        }

        // Hash the customer/employee name to a value
        public int Hash1(string key)
        {
            try
            {
                if (key.Length > MAX_INPUT)
                {
                    Console.WriteLine("Error: cannot store a key larger than 256 characters");
                    return -1;
                }
                ulong hash_value = FNV_OFFSET_BASIS;

                foreach (char c in key)
                {
                    hash_value = hash_value ^ c;
                    hash_value = hash_value * FNV_PRIME;
                }
                ulong result = hash_value % (ulong)TABLE_SIZE;
                return (int)result;
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("Error: attempted to store a null input");
                return -1;
            }

        }

        // Creates the double hashing offset for dealing with collisions
        // Key will always be a string as it will store only the customer/employee name
        public int Hash2(string key)
        {
            int hash_value = 0;
            foreach (char c in key)
            {
                hash_value = hash_value % DOUBLE_HASH_PRIME;
            }
            return hash_value + 1;
        }

        public int GetDoubleHashPrime()
        {
            // Create an array of boolean values the size of the table
            bool[] primeArray = new bool[TABLE_SIZE];
            // Set all values to true and assume all numbers are prime
            for (int i = 0; i < primeArray.Length; i++)
            {
                primeArray[i] = true;
            }

            // Iterate through the array up until the square root of the table size
            // If a number is prime, set all multiples of that number to false as they cannot be prime
            for (int i = 2; i < Math.Sqrt(TABLE_SIZE); i++)
            {
                if (primeArray[i - 1] == true)
                {
                    for (int j = (int)Math.Pow(i, 2); j <= TABLE_SIZE; j = i + j)
                    {
                        primeArray[j - 1] = false;
                    }
                }
            }

            // Iterate backwards through the array and return the index of the first instance of true
            // This gives me the largest prime number less than the table size
            for (int i = primeArray.Length; i > 2; i--)
            {
                if (primeArray[i - 1] == true)
                {
                    return i;
                }
            }
            // Return -1 if no prime is found
            return -1;

        }

        // return true if value is inserted into the table, false otherwise
        public bool Insert(TKey key, TValue value)
        {
            if (key == null || value == null)
            {
                Console.WriteLine("Error: Invalid Input");
                return false;
            }

            if (IsFull() == true)
            {
                Console.WriteLine("Error: Table is Full");
            }

            int index = Hash1((string)(object)key);
            int offset = Hash2((string)(object)key);

            if (HashTable[index] == null)
            {
                HashTable[index] = new CustomNode();
            }

            // Check that the index is not empty
            while (HashTable[index].State != 0)
            {
                // If index is a deleted node, insert at that index
                if (HashTable[index].State == -1)
                {
                    break;
                }
                index = (index + offset) % TABLE_SIZE;
            }

            HashTable[index] = new CustomNode(key, value);
            CurrentKeys += 1;
            return true;
        }

        public TValue LookUp(TKey key)
        {
            int index = Hash1((string)(object)key);
            int offset = Hash2((string)(object)key);
            int startPos = index;
            bool firstLoop = true;

            // Check that the value at that index is not empty or a deleted node
            while (true)
            {
                // If that spot is empty return null
                if (HashTable[index].State == 0)
                {
                    return default;
                }
                // If the data matches, return that object
                else if (HashTable[index].Key.Equals(key))
                {
                    return HashTable[index].Value;
                }
                // If you have searched the whole hash table, return null
                else if (index == startPos && firstLoop == false)
                {
                    return default;
                }
                // If the node was deleted, update the index and continue the search
                else
                {
                    index = (index + offset) % TABLE_SIZE;
                }
                // If nothing is found, a whole loop is completed
                firstLoop = false;
            }
        }

        // Return true if employee object is deleted, false otherwise
        public bool Delete(TKey key)
        {
            // If input does not exist, return false
            if (LookUp(key) == null)
            {
                return false;
            }

            int index = Hash1((string)(object)key);
            int offset = Hash2((string)(object)key);

            while (HashTable[index].State != 0)
            {
                if (HashTable[index].Key.Equals(key))
                {
                    HashTable[index].State = -1;
                    HashTable[index].Value = default;
                    CurrentKeys -= 1;
                    return true;
                }
                else
                {
                    index = (index + offset) % TABLE_SIZE;
                }
            }

            return false;
        }

        public void PrintTable()
        {
            for (int i = 0; i < TABLE_SIZE; i++)
            {
                if (HashTable[i].Value == null)
                {
                    Console.WriteLine("Null");
                }
                else
                {
                    Console.WriteLine(HashTable[i].Value);
                }
            }
        }

        public int GetSize()
        {
            return TABLE_SIZE;
        }

        public List<KeyValuePair<TKey, TValue>> GetAllEntries()
        {
            return HashTable.Where(node => node != null && node.State == 1).Select(node => new KeyValuePair<TKey, TValue>(node.Key, node.Value)).ToList();
        }
    }
}