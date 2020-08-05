using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace NetCore
{
    class Program
    {

        static void Main(string[] args)
        {
            string[] arrayMethods = { "changewords", "countwords", "revstring", "dirview" };
            //Перевіряємо чи є параметри командного рядка
            if (args.Length > 0 && Array.IndexOf(arrayMethods, args[0]) != -1 && (File.Exists(args[1])||Directory.Exists(args[1])))
            {
                RunMethod(args);
            }
            else
            {
                //якщо аргументів немає просимо ввести номер методу і виконуємо його
                //решта аргументів перевіряється безпосередньо у методі, чи такий підхід неправильний?
                System.Console.WriteLine("Input task name: (changewords|countwords|revstring|dirview)");
                args[0] = System.Console.ReadLine();
                RunMethod(args);
            }
        }

        //отримуємо номер методу і виконуємо його
        static void RunMethod(string[] args)
        {

            switch (args[0])
            {
                case "changewords":
                    new ChangeWords().Run(args[1], args[2]);
                    break;
                case "countwords":
                    new CountWords().Run(args[1]);
                    break;
                case "revstring":
                    new ReverseString().Run(args[1]);
                    break;
                case "dirview":
                    new DireViews().Run(args[1]);
                    break;
                default:
                    Console.WriteLine("Input task name: (changewords|countwords|revstring|dirview)");
                    break;
            }
        }

    }
}
