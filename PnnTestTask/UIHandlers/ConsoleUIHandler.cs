using PnnTestTask.Services;
using System.Globalization;
using System.Text.Json;

namespace PnnTestTask.UIHandlers
{
    internal class ConsoleUIHandler
    {
        static BillService billService;

        public static void Initialize()
        {
            try
            {
                billService = new BillService();
            }
            catch (JsonException)
            {
                Console.WriteLine("File is corrupted, new file will be created");
            }
            catch
            {
                Console.WriteLine("Unexpected error during data extraction");
            }
        }

        public static void SaveData()
        {
            try
            {
                billService.WriteToFile();
            }
            catch
            {
                Console.WriteLine("Unexpected error during data saving");
            }
        }

        public static void WriteMenu()
        {
            Console.WriteLine("Select option:");
            Console.WriteLine("1. Get all bills");
            Console.WriteLine("2. Get bill with max bill amount");
            Console.WriteLine("3. Get bill with min bill amount");
            Console.WriteLine("4. Get your average expenses");
            Console.WriteLine("5. Add new bill");
            Console.WriteLine("6. Remove bill");
            Console.WriteLine("7. Exit");
            Console.WriteLine();
        }

        public static void WarnAboutUnexpectedInput()
        {
            Console.WriteLine("Unexpected input");
            Console.ReadLine();
            Console.Clear();
        }

        public static void HandleGetAll()
        {
            var allBills = billService.GetAllBills();
            if (allBills.Count == 0) Console.WriteLine("There are no bills in the list\n");
            else for (int i = 0; i < allBills.Count; i++)
                    Console.WriteLine($"Bill #{i + 1}:\n" +
                        $"Bill amount: {allBills[i].BillAmount}\n" +
                        $"Bill date: {allBills[i].Date.ToShortDateString()}\n" +
                        $"Bill description: {allBills[i].Description}\n");
        }

        public static void HandleGetMax()
        {
            var bill = billService.GetMaxAmountBill();
            if (bill == null) Console.WriteLine("There are no bills in the list\n");
            else Console.WriteLine($"Bill with maximum amount:\n" +
                $"Bill amount: {bill.BillAmount}\n" +
                $"Bill date: {bill.Date.ToShortDateString()}\n" +
                $"Bill description: {bill.Description}\n");
        }

        public static void HandleGetMin()
        {
            var bill = billService.GetMinAmountBill();
            if (bill == null) Console.WriteLine("There are no bills in the list\n");
            else Console.WriteLine($"Bill with minimum amount:\n" +
                $"Bill amount: {bill.BillAmount}\n" +
                $"Bill date: {bill.Date.ToShortDateString()}\n" +
                $"Bill description: {bill.Description}\n");
        }

        public static void HandleGetAvg()
        {
            Console.WriteLine("Average expenses: " + billService.GetAverageExpenses() + "\n");
        }

        public static void HandlePost()
        {
            decimal amount;
            DateTime date;
            while (true)
            {
                Console.WriteLine("Please enter bill amount:");
                if (!decimal.TryParse(Console.ReadLine(), out amount))
                    Console.WriteLine($"Invalid input, try using {CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator} as a separator for decimal parts");
                if (amount < 0) Console.WriteLine("Amount should be non-negative");
                else break;
            }
            while (true)
            {
                Console.WriteLine("Please enter bill date in format DD.MM.YYYY:");
                if (!DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", null, DateTimeStyles.None, out date)) Console.WriteLine("Invalid input");
                else break;
            }
            Console.WriteLine("Please enter bill description:");
            string description = Console.ReadLine();
            billService.PostBill(amount, date, description);
            Console.WriteLine();
        }

        public static void HandleDelete()
        {
            bool result;
            while (true)
            {
                Console.WriteLine("Please enter number of bill you want to delete:");
                int number;
                if (!int.TryParse(Console.ReadLine(), out number)) Console.WriteLine("Invalid input");
                else
                {
                    result = billService.DeleteBill(number);
                    break;
                }
            }

            if (!result) Console.WriteLine("Bill not found");
            Console.WriteLine();
        }
    }
}
