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
        public static string fullPath = "lorem.txt";

        public static string fileBackup = "";



        static void Main(string[] args)
        {
            //System.Console.WriteLine(args.Length);
            if (args.Length > 0)
            {
                if (args.Length > 1)
                    fullPath = args[1];
                switch (args[0])
                {
                    case "1":
                        Method1();
                        break;
                    case "2":
                        Method2();
                        break;
                    case "3":
                        Method3();
                        break;
                    case "4":
                        Method4();
                        break;
                }
            }
            else
            {
                System.Console.WriteLine("Select method: (1|2|3|4)");
                int M = 0;
                while (M == 0)
                {
                    M = Convert.ToInt32(System.Console.ReadLine());
                }

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
                }

            }
        }

        static void Method1()
        {


            if (!File.Exists(fullPath))
            {
                System.Console.WriteLine("Please input correct file name:");
                fullPath = System.Console.ReadLine();
            }
            if (File.Exists(fullPath))
            {

                fileName = Path.GetFileName(fullPath);

                filePath = Path.GetDirectoryName(fullPath);
                fileBackup = Path.Combine(filePath, fileName + ".bak");

                if (!File.Exists(fileBackup)) { File.Copy(fullPath, fileBackup); }


                var linerows = new List<string>();
                int i = 0;

                bool foundw = false;

                string word = "";
                string line1 = "";
                while (word.Length == 0)
                {
                    System.Console.WriteLine("Please input word or symbol:");
                    word = System.Console.ReadLine();
                }

                try
                {   // Open the text file using a stream reader.
                    foreach (string line in File.ReadLines(fileBackup))
                    {
                        // Read the stream to a string, and write the string to the console.

                        int f = line.IndexOf(word, 0, line.Length);

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

            }
            else
            {
                System.Console.WriteLine("File not found!");
            }





        }

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
                    foreach (string line in File.ReadLines(fullPath))
                    {

                        if (line.Length > 0)
                        {
                            words += (line + " ");
                        }



                    }

                    string[] separatingStrings = { " " };
                    i = 1;
                    string[] wordsarray = _split_array(words);
                    words = "";
                    foreach (var word in wordsarray)
                    {
                        if (i % 10 == 0)
                            words = words + _trim(word) + ',';
                        i++;
                    }
                    words = words.TrimEnd(',');
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

        static void Method3()
        {

            if (!File.Exists(fullPath))
            {
                System.Console.WriteLine("Please input correct file name:");
                fullPath = System.Console.ReadLine();
            }
            if (File.Exists(fullPath))
            {
                var linerows = new List<string>();
                int i = 0;
                string words = "";
                try
                {
                    foreach (string line in File.ReadLines(fullPath))
                    {

                        if (line.Length > 0)
                        {
                            i++;
                            if (i == 3)
                            {
                                words = line;
                            }

                        }



                    }
                    string[] wordsarray = _split_array(words);
                    words = "";
                    foreach (var word in wordsarray)
                    {
                        string output = new string(word.ToCharArray().Reverse().ToArray());
                        words += (output + " ");
                    }
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

        static void Method4()
        {
            if (!Directory.Exists(fullPath))
            {
                System.Console.WriteLine("Please input correct folder path:");
                fullPath = System.Console.ReadLine();
            }

            if (Directory.Exists(fullPath))
            {
                string[] dirs = Directory.GetDirectories(fullPath, "*", SearchOption.TopDirectoryOnly);
                if (dirs.Length > 0)
                {
                    display(dirs);
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



        static string[] _split_array(string line)
        {
            string[] separatingStrings = { " " };
            return line.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);
        }

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
