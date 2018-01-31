using Microsoft.SharePoint.Client;
using System;
using System.Threading.Tasks;

namespace csom_dotnet_core
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(value: $"Will load using {nameof(NetCoreAppPlatformService)}");

            Task mainTask = MainAsync();
            mainTask.Wait();
        }

        static async Task MainAsync()
        {
            using (var context = new ClientContext("https://<tenant>.sharepoint.com/sites/site1/"))
            {
                context.Credentials = new SharePointOnlineCredentials("user@tenant.net", ReadPassword());
                Web web = context.Web;
                context.Load(web.Lists,
                    lists => lists.Include(list => list.Title, list => list.Id));

                await context.ExecuteQueryAsync();
                foreach (List list in web.Lists)
                {
                    Console.WriteLine("List title is: " + list.Title);
                }
            }
        }

        public static string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo info = Console.ReadKey(true);
            while (info.Key != ConsoleKey.Enter)
            {
                if (info.Key != ConsoleKey.Backspace)
                {
                    Console.Write("*");
                    password += info.KeyChar;
                }
                else if (info.Key == ConsoleKey.Backspace)
                {
                    if (!string.IsNullOrEmpty(password))
                    {
                        password = password.Substring(0, password.Length - 1);
                        int pos = Console.CursorLeft;
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                        Console.Write(" ");
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                    }
                }
                info = Console.ReadKey(true);
            }

            Console.WriteLine();
            return password;
        }
    }
}
