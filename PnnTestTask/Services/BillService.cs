using PnnTestTask.Context;
using PnnTestTask.Models;

namespace PnnTestTask.Services
{
    internal class BillService
    {
        BillContext billContext = new BillContext();

        public IReadOnlyList<Bill> GetAllBills() => billContext.Get();

        public Bill? GetMaxAmountBill() => billContext.Get().MaxBy(b => b.BillAmount);

        public Bill? GetMinAmountBill() => billContext.Get().MinBy(b => b.BillAmount);

        public decimal GetAverageExpenses() => billContext.Get().Average(b => b.BillAmount);

        public void PostBill(decimal amount, DateTime date, string description)
        {
            billContext.Add((new Bill(amount, date, description)));
        }

        public bool DeleteBill(int index)
        {
            if (billContext.Get().Count >= index && index > 0)
            {
                billContext.RemoveAt(index - 1);
                return true;
            }
            else return false;
        }

        public void WriteToFile()
        {
            billContext.WriteToFile();
        }
    }
}
