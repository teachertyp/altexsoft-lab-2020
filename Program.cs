using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
namespace NetCore
{
    class Program
    {
        public static string filePath = "";
        public static string fileName = "";
        public static string fullPath = "";

        public static string fileBackup = "";

        public static string search_word = "";

        static void Main(string[] args)
        {
            //Перевіряємо чи є параметри командного рядка
            if (args.Length > 0)
            {
                //якщо кількість параметрів більше одного, це значить, 
                //якщо прийшла адреса файлу і записуємо його у fullPath
                if (args.Length > 1)
                    fullPath = args[1];
                //якщо в параметрах є пошукове слово
                if (args.Length > 2)
                    search_word = args[2];

                // перевіряємо номер методу і виконуємо його
                action_method(Convert.ToInt32(args[0]));
            }
            else
            {
                //якщо аргументів немає просимо ввести номер методу і виконуємо його
                System.Console.WriteLine("Select method: (1|2|3|4)");
                action_method(Convert.ToInt32(System.Console.ReadLine()));

            }
        }

        //отримуємо номер методу і виконуємо його
        static void action_method(int M)
        {
            switch (M)
            {
                case 1:
                    Method1();
                    break;
                case 2:
                    Method2();
                    break;
                case 3:
                    Method3();
                    break;
                case 4:
                    Method4();
                    break;
                default:
                    Console.WriteLine("Inpun number method: (1|2|3|4)");
                    break;
            }
        }

        //створюємо копію файлу і шукаємо та видаляємо у ньому слово чи символ
        static void Method1()
        {


            if (!File.Exists(fullPath))
            {
                System.Console.WriteLine("Please input correct file name:");
                fullPath = System.Console.ReadLine();
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
                int i = 0;

                bool foundw = false;

                string word = search_word;
                string line1 = "";
                //доки шукане слово порожнє просимо його ввести
                while (word.Length == 0)
                {
                    System.Console.WriteLine("Please input word or symbol:");
                    word = System.Console.ReadLine();
                }

                try
                {
                    // читаємо рядки з файлу рядки лягають у line
                    foreach (string line in File.ReadLines(fileBackup))
                    {

                        //перевіряємо чи є у рядку шуканий символ
                        int f = line.IndexOf(word, 0, line.Length);
                        //якщо є то міняємо його на порожній символ і рядок заносимо у список
                        if (f != -1)
                        {
                            foundw = true;
                            line1 = line.Replace(word, "");
                            if (line1.Length > 0) linerows.Add(line1);
                        }
                        else
                        {
                            if (line.Length > 0) linerows.Add(line);
                        }

                        if (line.Length > 0)
                        {
                            i++;
                        }
                    }

                    //якщо не знайшли слів жодного разу (foundw=false) виводимо відповідне повідомлення
                    //інакше пишемо у файл змінені рядки 
                    if (!foundw) System.Console.WriteLine("Search word not found!");
                    else
                    {
                        i--;
                        using (StreamWriter outputFile = new StreamWriter(fullPath))
                        {
                            foreach (string line in linerows)
                            {

                                if (i > 0)
                                {
                                    outputFile.WriteLine(line);
                                    outputFile.Write("\n");
                                    i--;
                                }
                                else
                                {
                                    outputFile.Write(line);
                                }
                            }
                        }
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
        //рахуємо кількість слів і виводимо кожне 10 слово
        static void Method2()
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
                    string[] wordsarray = _split_array(words);
                    words = "";

                    //пробігаємо по масиву і генеруємо новий рядок із кожного 10 слова
                    foreach (var word in wordsarray)
                    {
                        if (i % 10 == 0)
                            words = words + _trim(word) + ',';
                        i++;
                    }

                    words = words.TrimEnd(',');

                    //виводимо кількість слів і за ним рядок із слів.
                    Console.WriteLine(wordsarray.Length);
                    Console.WriteLine(words);

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

        //Виводимо на екран третє речення з перевернутими у ньому словами
        static void Method3()
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
                int prev_dot = 0;
                int _dot = 0;
                int count_r = 0;
                try
                {
                    //читаємо рядки і склеюємо їх у одну змінну розділивши пропусками
                    foreach (string line in File.ReadLines(fullPath))
                    {

                        if (line.Length > 0)
                        {
                            words += line;
                        }
                    }
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
                    string[] wordsarray = _split_array(words);
                    words = "";
                    //пробігаючи по масиву перевертаємо кожне слово і додаємо до рядка
                    foreach (var word in wordsarray)
                    {
                        string output = new string(word.ToCharArray().Reverse().ToArray());
                        words += (output + " ");
                    }
                    //виводимо результат
                    Console.WriteLine(words);

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

        //виводимо список каталогів за вказаним і пропонуємо вибрати ід каталога для перегляду файлів у ньому
        static void Method4()
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
                    display(dirs);
                    //пропонуємо обрати ід каталога для перегляду файлів
                    System.Console.WriteLine("----------------------------------");
                    System.Console.WriteLine("Please input iD to show files list");
                    int GUID = Convert.ToInt32(System.Console.ReadLine());

                    if (GUID > 0 && Directory.Exists(dirs[GUID - 1]))
                    {
                        fullPath = dirs[GUID - 1];
                        display(Directory.GetFiles(fullPath));
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
        static void display(Array data)
        {
            int i = 0;
            Array.Sort(data, StringComparer.InvariantCulture);

            Console.WriteLine("ID" + sstr(_countdig(data.Length) - 1) + "| NAME");
            foreach (string e in data)
            {
                i++;
                Console.WriteLine(i + sstr(_countdig(data.Length) - _countdig(i)) + " | " + e.Replace(fullPath + "/", ""));
            }
        }
        static string _trim(string s) => s.Trim(new Char[] { ',', '!', '.', '?', '(', ')' });


        //розбиваємо рядок на масив
        //потім можна передавати сепаратор як аргумент

        static string[] _split_array(string line)
        {
            string[] separatingStrings = { " " };
            return line.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);
        }

        //отримуємо кількість цифр у числі
        static int _countdig(int a)
        {
            if (a == 0) return 1;
            int c = 0;
            while (a > 0)
            {
                a = a / 10;
                c++;
            }
            return c;
        }
        //отримуємо кількість пробілів в залежності від довжини номер файлу чи папки для вирівнювання

        static string sstr(int a)
        {
            string s = "";
            for (int i = 0; i < a; i++)
            {
                s += " ";
            }
            return s;
        }

    }
}
