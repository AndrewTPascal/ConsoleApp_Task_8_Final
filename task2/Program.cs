using System;
using System.IO;

namespace task2
{
    class Program
    {
        public static double SumFolderSize = 0;
        static void Main(string[] args)
        {
            Console.Write("Введите путь к каталогу: ");
            string path = Console.ReadLine();

            // Проверка существует ли введенный каталог. Если каталог существует, получаем все подкаталоги и файлы 
            DirectoryInfo dir = new DirectoryInfo(path);
            if (dir.Exists)
            {
                DirectoryInfo[] arrDirectories = dir.GetDirectories();
                FileInfo[] arrFiles = dir.GetFiles();

                // Передаем каталоги и файлы в метод, получаем общий размер каталога
                Console.WriteLine("Общий размер каталога: {0} байт", FolderSize(arrDirectories, arrFiles));
            }
            else Console.WriteLine("Такого каталога не существует!");
        }
        
        // Передаются массивы папок и файлов каталога
        static double FolderSize(DirectoryInfo[] arrDirectories, FileInfo[] arrFiles)
        {
            
            // Считаем сумму всех файлов в каждом каталоге
            foreach (FileInfo dir in arrFiles)
            {
                try
                {
                    Console.WriteLine("Файл: {0} Размер: {1} байт", dir.FullName, dir.Length);
                    SumFolderSize += dir.Length;
                }
                catch (Exception ex)
                { Console.WriteLine("Не удалось прочитать файл {0}: {1}", dir, ex.Message); }
            }

            // Снова проходимся по каждой директории. Вызываем рекурсивный метод 
            foreach (DirectoryInfo dir in arrDirectories)
            {
                try
                {
                    FolderSize(dir.GetDirectories(), dir.GetFiles());
                }
                catch (Exception ex)
                { Console.WriteLine("Не удалось прочитать каталог {0}: {1}", dir, ex.Message); }
            }

            return SumFolderSize;
        }
    }
}
