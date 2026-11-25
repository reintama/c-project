using System;
using System.IO;
using System.Text;
using BookManagementApp.Services;
using BookManagementApp.UI;

namespace BookManagementApp
{

    public class Program
    {
        private const string DataFileName = "books.txt";

        public static void Main(string[] args)
        {
          
            try
            {
                Console.OutputEncoding = Encoding.UTF8;
                Console.InputEncoding = Encoding.UTF8;
            }
            catch (IOException)
            {
               
            }

            try
            {
                if (args.Length > 0 && args[0] == "--demo")
                {
                    Demo.Run();
                    return;
                }

                string path = Path.Combine(Directory.GetCurrentDirectory(), DataFileName);
                BookManager manager = new BookManager(path);

                LoadData(manager);

                ConsoleUI ui = new ConsoleUI(manager);
                ui.Run();
            }
            catch (EndOfStreamException)
            {
                Console.WriteLine();
                Console.WriteLine("შეყვანა შეწყდა. პროგრამა დასრულდა.");
            }
        }

  
        private static void LoadData(BookManager manager)
        {
            try
            {
                int skipped = manager.Load();
                Console.WriteLine("ჩატვირთულია წიგნები ფაილიდან: " + manager.Count);

                if (skipped > 0)
                {
                    Console.WriteLine("გაფრთხილება: " + skipped + " დაზიანებული ან განმეორებადი სტრიქონი გამოტოვებულია.");
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("ფაილის წაკითხვა ვერ მოხერხდა: " + ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("ფაილზე წვდომა აკრძალულია: " + ex.Message);
            }

            Console.WriteLine();
        }
    }
}
