// See https://aka.ms/new-console-template for more information
using System.Globalization;


namespace DayPlanner
{
    class Program
    {
      private static readonly string basePath = "C:\\Users\\Stewartf\\AppData\\Local\\Temp\\DayPlanner\\";
      public static void Main() 
      {
        while (true)
        {
            DisplayMenu();

            int choice = GetMenuChoice();

            switch (choice)
            {
                case 1:
                    CreateEntry();
                    break;
                case 2:
                    ViewEntries(DateTime.Today);
                    break;
                case 3:
                    ViewEntries(DateTime.Today.AddDays(-1));
                    break;
                case 4:
                    ViewEntries(DateTime.Today.AddDays(1));
                    break;
                case 5:
                    ViewEntries(GetDate());
                    break;
                case 6:
                    ModifyEntry(GetDate());
                    break;
                case 7:
                    return;
                default:
                    Console.WriteLine("Invalid input. Please try again.");
                    break;
            }
        }
      }

      static void DisplayMenu()
      {
          Console.WriteLine("Daily Planner");
          Console.WriteLine("-------------");
          Console.WriteLine("1. Create a new entry");
          Console.WriteLine("2. View entries for today");
          Console.WriteLine("3. View entries for yesterday");
          Console.WriteLine("4. View entries for tomorrow");
          Console.WriteLine("5. View entries for a specific day");
          Console.WriteLine("6. Modify entries for a specific day");
      }

      static int GetMenuChoice()
      {
          Console.Write("Enter your choice: ");
          string input = Console.ReadLine();
          int choice;
          if (!int.TryParse(input, out choice))
          {
              return 0;
          }
          return choice;
      }

      static void CreateEntry()
      {
          DateTime date = GetDate();

          Console.Write("Enter the task: ");
          string task = Console.ReadLine();

          string entry = $"{task}\n";
          string filename = GetFilename(date);
          File.AppendAllText(filename, entry);
          Console.WriteLine("Entry added successfully!");
      }

      static void ViewEntries(DateTime date)
      {
          string filename = GetFilename(date);
          string[] lines = File.ReadAllLines(filename);
          Console.WriteLine("\n");
          Console.WriteLine($"Tasks for {date:MM/dd/yyyy}: ");
          Console.WriteLine("--------------------------------------------------");
          int i = 1;
          foreach (string line in lines)
          {
              string task = line;
              Console.WriteLine($"{i}: {task}");
              i++;
          }
          Console.WriteLine("--------------------------------------------------");
          Console.WriteLine("\n");
      }

      static void ModifyEntry(DateTime date)
      {
          string filename = GetFilename(date);
          string[] lines = File.ReadAllLines(filename);
          List<string> updatedLines = new List<string>(lines);

          Console.WriteLine($"Tasks for {date:MM/dd/yyyy}: ");
          int i = 1;
          foreach (string line in lines)
          {
              string[] parts = line.Split(',');
              string task = parts[1];
              Console.WriteLine($"{i}: {task}");
              i++;
          }

          Console.Write("Enter the number of the task you want to modify: ");
          int taskNumber = int.Parse(Console.ReadLine());

          Console.Write("Enter the new task: ");
          string newTask = Console.ReadLine();

          updatedLines[taskNumber - 1] = $"{date:MM/dd/yyyy},{newTask}";

          File.WriteAllLines(filename, updatedLines);
          Console.WriteLine("Entry modified successfully!");
      }

      static DateTime GetDate()
      {
          Console.Write("Enter the date (format: MM/DD/YYYY): ");
          string date = Console.ReadLine();
          return DateTime.ParseExact(date, "MM/dd/yyyy", CultureInfo.InvariantCulture);
      }

      static string GetFilename(DateTime date)
      {
          return $"{basePath}{date:MM-dd-yyyy}.txt";
      }
    }
}


