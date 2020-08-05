using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace NetCore
{
    //службові функції
       public partial class TaskService
    {


        public static string StringTrim(string s) => s.Trim(new Char[] { ',', '!', '.', '?', '(', ')' });

        //отримуємо кількість цифр у числі
        public static int CountDigit(int a)
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

        public static string[] SplitArray(string line)
        {
            string[] separatingStrings = { " " };
            return line.Split(separatingStrings, System.StringSplitOptions.RemoveEmptyEntries);
        }


        //отримуємо кількість пробілів в залежності від довжини номер файлу чи папки для вирівнювання

        public static string SpaceConcat(int a)
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