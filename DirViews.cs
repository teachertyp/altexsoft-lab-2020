using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace NetCore
{
    //виводимо список каталогів за вказаним і пропонуємо вибрати ід каталога для перегляду файлів у ньому
    public partial class DireViews
    {

        public void Run(string fullPath)
        {
            if (!Directory.Exists(fullPath))
            {
                System.Console.WriteLine("Please input correct folder path:");
                fullPath = System.Console.ReadLine();
            }

            //якщо каталог існує
            if (Directory.Exists(fullPath))
            {
                //отримуємо масив підкаталогів
                string[] dirs = Directory.GetDirectories(fullPath, "*", SearchOption.TopDirectoryOnly);
                if (dirs.Length > 0)
                {
                    //виводимо їх на екран
                    PrintToDisplay(fullPath, dirs);
                    //пропонуємо обрати ід каталога для перегляду файлів
                    System.Console.WriteLine("----------------------------------");
                    System.Console.WriteLine("Please input iD to show files list");
                    int dirId = Convert.ToInt32(System.Console.ReadLine());

                    if (dirId > 0 && Directory.Exists(dirs[dirId - 1]))
                    {
                        fullPath = dirs[dirId - 1];
                        PrintToDisplay(fullPath, Directory.GetFiles(fullPath));
                    }
                    else
                    {
                        System.Console.WriteLine("Directory Does Not Exist");
                    }
                }
                else
                {
                    System.Console.WriteLine("Folder is empty!");
                }
            }
            else
            {
                System.Console.WriteLine("Directory Does Not Exist");
            }
        }

        //виводимо список каталогів чи файлів у вигляді псевдотаблички
        //при цьому вирівнюємо стовпчики в залежності від кількості каталогів чи файлів
        static void PrintToDisplay(string fullPath, Array data)
        {
            int i = 0;
            Array.Sort(data, StringComparer.InvariantCulture);

            Console.WriteLine("ID" + TaskService.SpaceConcat(TaskService.CountDigit(data.Length) - 1) + "| NAME");
            foreach (string e in data)
            {
                i++;
                Console.WriteLine(i + TaskService.SpaceConcat(TaskService.CountDigit(data.Length) - TaskService.CountDigit(i)) + " | " + e.Replace(fullPath + "/", ""));
            }
        }
    }
}