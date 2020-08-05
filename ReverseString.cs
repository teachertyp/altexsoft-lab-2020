using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace NetCore
{
    //Виводимо на екран третє речення з перевернутими у ньому словами
    public partial class ReverseString
    {
        public void Run(string fullPath)
        {
            if (!File.Exists(fullPath))
            {
                System.Console.WriteLine("Please input correct file name:");
                fullPath = System.Console.ReadLine();
            }
            //тут можна замінити на  else, але це позбавить зайвої перевірки на існування файлу і виведення коректного повідомлення
            //або щось треба підучити
            if (File.Exists(fullPath))
            {
                int i = 0;
                string words = "";
                int prev_dot = 0;
                int _dot = 0;
                int count_r = 0;
                try
                {
                    //читаємо рядки і склеюємо їх у одну змінну розділивши пропусками
                    words = File.ReadAllText(fullPath);
                    //шукаємо у суцільному рядку 3 крапку запам'ятовучи індекс попередньої крапки
                    for (i = 0; i < words.Length; i++)
                    {
                        if (words[i] == '.' && i > 0)
                        {
                            prev_dot = _dot;
                            _dot = i;
                            count_r++;
                            //якщо знайшли третью крапку копіюємо речення між поточною і попередньою крапками
                            if (count_r == 3)
                            {
                                words = words.Substring(prev_dot, _dot - prev_dot);
                            }
                        }
                    }
                    //отримуємо масив слів речення
                    string[] wordsarray = TaskService.SplitArray(words);

                    words = "";
                    //пробігаючи по масиву перевертаємо кожне слово і додаємо до рядка
                    foreach (var word in wordsarray)
                    {
                        string output = new string(word.ToCharArray().Reverse().ToArray());
                        words += (output + " ");
                    }
                    //виводимо результат
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