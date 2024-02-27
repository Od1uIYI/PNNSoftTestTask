using PnnTestTask.UIHandlers;

namespace PnnTestTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConsoleUIHandler.Initialize();

            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnConsoleClosing);

            while (true)
            {
                ConsoleUIHandler.WriteMenu();
                int option;
                if (int.TryParse(Console.ReadLine(), out option) && option > 0 && option < 8)
                {
                    if (option == 1) ConsoleUIHandler.HandleGetAll();
                    else if (option == 2) ConsoleUIHandler.HandleGetMax();
                    else if (option == 3) ConsoleUIHandler.HandleGetMin();
                    else if (option == 4) ConsoleUIHandler.HandleGetAvg();
                    else if (option == 5) ConsoleUIHandler.HandlePost();
                    else if (option == 6) ConsoleUIHandler.HandleDelete();
                    else if (option == 7) break;
                }
                else ConsoleUIHandler.WarnAboutUnexpectedInput();
            }
        }

        static void OnConsoleClosing(object? sender, EventArgs e)
        {
            ConsoleUIHandler.SaveData();
        }
    }
}