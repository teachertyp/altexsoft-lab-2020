using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace NetCore
{
    //створюємо копію файлу і шукаємо та видаляємо у ньому слово чи символ
    public partial class ChangeWords
    {

        
        public static string filePath = "";
        public static string fileName = "";
        public static string fileBackup = "";

        public static string search_word = "";

        //Просимо ввести якісь дані з клавіатури
        static string getDialog(string message){
            System.Console.Clear();
            System.Console.WriteLine(message);
            return System.Console.ReadLine();
        }
        
        public void Run(string fullPath, string word)
        {

            //якщо ім'я файлу не вказано в атрибутах то пропонуємо ввести його вручну
            if (!File.Exists(fullPath))
            {
                fullPath = getDialog("Please input correct file name:");
            }

            if (word.Length==0){
                word =getDialog("Please input search word or symbol:");
            }


            if (File.Exists(fullPath))
            {
                //отримуємо ім'я файлу якщо шлях абсолютний
                fileName = Path.GetFileName(fullPath);

                //отримуємо шлях до файлу
                filePath = Path.GetDirectoryName(fullPath);

                //генеруємо нове ім'я файлу
                fileBackup = Path.Combine(filePath, fileName + ".bak");

                //якщо файл копія не існує - створюємо копію
                if (!File.Exists(fileBackup)) { File.Copy(fullPath, fileBackup); }


                var linerows = new List<string>();


                bool foundw = false;


                //доки шукане слово порожнє просимо його ввести
                while (word.Length == 0)
                {
                    System.Console.WriteLine("Please input word or symbol:");
                    word = System.Console.ReadLine();
                }

                try
                {
                    // читаємо рядки з файлу рядки лягають у line
                    string line=File.ReadAllText(fileBackup);
                    int f = line.IndexOf(word, 0, line.Length);
                    if (f != -1)
                        {
                            foundw = true;
                            line = line.Replace(word, "");
                        }

                    //якщо не знайшли слів жодного разу (foundw=false) виводимо відповідне повідомлення
                    if (!foundw) System.Console.WriteLine("Search word not found!");
                    else
                    {
                        File.WriteAllText(fullPath,line);
                    }
                }
                catch (IOException e)
                {
                    Console.WriteLine("The file could not be read:");
                    Console.WriteLine(e.Message);
                }
                System.Console.WriteLine("Done!");
            }
            else
            {
                System.Console.WriteLine("File not found!");
            }





        }
    }
}