using System;
using System.Collections.Generic;

namespace MergingOneCollection
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] firstClass = { "Ваня", "Миша", "Слава", "Денис", "Петр", "Таня", "Илья", "Гриша", "Варя" };
            string[] secondClass = { "Света", "Настя", "Лена", "Вика", "Петр", "Таня", "Леша", "Ваня", "Варя", "Саня" };
            List<string> studentNames = new List<string>();

            Merge(studentNames, firstClass);
            Merge(studentNames, secondClass);
            ShowNames(studentNames);
        }

        private static void Merge(List<string> studentNames, string[] names)
        {
            for (int i = 0; i < names.Length; i++)
            {
                if (studentNames.Contains(names[i]))
                    continue;
                else
                    studentNames.Add(names[i]);
            }
        }

        private static void ShowNames(List<string> studentNames)
        {
            foreach (string name in studentNames)
            {
                Console.WriteLine(name);
            }
        }
    }
}