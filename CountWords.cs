using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace NetCore
{
    //рахуємо кількість слів і виводимо кожне 10 слово
    public partial class CountWords
    {
         
        public void Run(string fullPath)
        {

            if (!File.Exists(fullPath))
            {
                System.Console.WriteLine("Please input correct file name:");
                fullPath = System.Console.ReadLine();
            }

            if (File.Exists(fullPath))
            {
                int i = 0;
                string words = "";

                try
                {
                    //читаємо рядки і склеюємо їх у одну змінну розділивши пропусками
                    foreach (string line in File.ReadLines(fullPath))
                    {
                        if (line.Length > 0)
                        {
                            words += (line + " ");
                        }
                    }

                    i = 1;
                    //розбиваємо загальний рядок на масив слів
                    string[] wordsarray = TaskService.SplitArray(words);
                    words = "";

                    //пробігаємо по масиву і генеруємо новий рядок із кожного 10 слова
                    foreach (var word in wordsarray)
                    {
                        if (i % 10 == 0)
                            words = words + TaskService.StringTrim(word) + ',';
                        i++;
                    }

                    words = words.TrimEnd(',');

                    //виводимо кількість слів і за ним рядок із слів.
                    Console.WriteLine(wordsarray.Length);
                    Console.WriteLine(words);
                    Console.WriteLine("Done!");

                }
                catch (IOException e)
                {
                    Console.WriteLine("The file could not be read:");
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                System.Console.WriteLine("File not found!");
            }
        }
    }
}