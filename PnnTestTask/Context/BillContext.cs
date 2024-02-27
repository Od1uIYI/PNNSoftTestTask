using PnnTestTask.Models;
using System.Text.Json;

namespace PnnTestTask.Context
{
    internal class BillContext
    {
        List<Bill> billList = new List<Bill>();
        const string defaultPath = "bills.json";

        public BillContext()
        {
            ReadFromFile();
        }

        public IReadOnlyList<Bill> Get() => billList;

        public void Add(Bill bill)
        {
            billList.Add(bill);
        }

        public void RemoveAt(int index)
        {
            billList.RemoveAt(index);
        }

        public void ReadFromFile()
        {
            if (File.Exists(defaultPath))
            {
                using FileStream stream = new FileStream(defaultPath, FileMode.Open);
                billList = JsonSerializer.Deserialize<List<Bill>>(stream);
            }
        }

        public void WriteToFile()
        {
            using FileStream fileStream = new FileStream(defaultPath, FileMode.Create);
            JsonSerializer.Serialize(fileStream, billList);
        }
    }
}
