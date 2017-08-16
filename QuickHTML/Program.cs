using System;
using System.IO;
using QuickHTML.Extensions;
using static System.ConsoleKey;

namespace QuickHTML
{
    class Program
    {
        ConsoleKey[] _selectKeys = { D1, D2 };

        static void Main(string[] args) =>
            new Program().Start();

        void Start()
        {
            Console.WriteLine($"press {TrimKey(D1)} to do a quick setup or hit {TrimKey(D2)} to do a long setup");

            for (;;)
            {
                var key = Console.ReadKey(true).Key;

                if (key == _selectKeys[0])
                {
                    var service = new QuickHTMLService();
                    service.CreateProject().OpenVSCode();
                    break;
                }
                else if (key == _selectKeys[1])
                {
                    SetUp();
                    break;
                }
            }

            string TrimKey(ConsoleKey key)
            {
                var keyString = key.ToString();

                if (keyString.Length != 1)
                    return keyString.Split('D')[1];
                else
                    return keyString;
            }
        }

        void SetUp()
        {
            string projectDirectory;
            string projectName;

            do
            {
                Console.WriteLine("The path you want to start in:");
                projectDirectory = Console.ReadLine();
            } while (!Directory.Exists(projectDirectory));

            Console.WriteLine("What do you want the html project to be called?:");
            projectName = Console.ReadLine();

            var service = new QuickHTMLService(projectDirectory, projectName);
            service.CreateProject().OpenVSCode();
        }
    }
}
