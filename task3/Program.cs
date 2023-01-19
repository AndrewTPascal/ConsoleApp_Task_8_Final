using System;
using System.IO;

namespace task3
{
    class Program
    {
        public static double SumFolderSize;
        public static int SumDelFiles;
        static void Main(string[] args)
        {
            Console.Write("Введите путь к каталогу: ");
            string path = Console.ReadLine();

            // Проверка существует ли введенный каталог.
            if (Directory.Exists(path))
            {
                //Вызываем методы для получения размера каталога и очистки

                double folderSize = FolderSize(path);
                Console.WriteLine();

                CleanerDirectory(path);
                Console.WriteLine();

                double newFolderSize = FolderSize(path);

                Console.WriteLine("\nИсходный размер каталога: {0} байт", folderSize);
                Console.WriteLine("Файлов удалено: {0}. Освобождено места: {1} байт", SumDelFiles, folderSize-newFolderSize);
                Console.WriteLine("Текущий размер каталога: {0} байт", newFolderSize);
            }
            else Console.WriteLine("Такого каталога не существует!");
        }

        // Получаем файлы и подкаталоги в текущей папке
        static double FolderSize(string path) 
        {
            SumFolderSize = 0;
            DirectoryInfo dir = new DirectoryInfo(path);
            DirectoryInfo[] arrDirectories = dir.GetDirectories();
            FileInfo[] arrFiles = dir.GetFiles();
            return CalculateFolderSize(arrDirectories, arrFiles);
        }
                
        // Вычисляем размер папок через рекурсию
        static double CalculateFolderSize(DirectoryInfo[] arrDirectories, FileInfo[] arrFiles)
        {

            
            foreach (FileInfo dir in arrFiles)
            {
                try
                {
                    Console.WriteLine("Файл: {0} Размер: {1} байт", dir.FullName, dir.Length);
                    SumFolderSize += dir.Length;
                }
                catch (Exception ex)
                { Console.WriteLine("Не удалось прочитать файл {0}: {1}", dir.FullName, ex.Message); }
            }

            foreach (DirectoryInfo dir in arrDirectories)
            {
                try
                {
                    CalculateFolderSize(dir.GetDirectories(), dir.GetFiles());
                }
                catch (Exception ex)
                { Console.WriteLine("Не удалось прочитать каталог {0}: {1}", dir.FullName, ex.Message); }
            }

            return SumFolderSize;
        }

        // Метод для удаления файлов и каталогов, которые не использовались более 30 минут
        static void CleanerDirectory(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            DirectoryInfo[] arrDirectories = dir.GetDirectories();
            FileInfo[] arrFiles = dir.GetFiles();
            SumDelFiles = 0;

            foreach (FileInfo file in arrFiles)
            {
                double lastTime = (DateTime.Now - file.LastWriteTime).TotalMinutes;
                if (lastTime > 30)
                {
                    try 
                    {
                        file.Delete();
                        SumDelFiles++;
                        Console.WriteLine("Файл удален: {0}", file.FullName);
                    }
                    catch (Exception ex)
                    { Console.WriteLine("Не удалось прочитать файл {0}: {1}", file.FullName, ex.Message); }
                }
            }

            foreach (DirectoryInfo directory in arrDirectories)
            {
                double lastTime = (DateTime.Now - directory.LastWriteTime).TotalMinutes;
                if (lastTime > 30)
                {
                    try
                    {
                        CalcFilesInDirectory(directory);
                        directory.Delete(true);
                        Console.WriteLine("Каталог удален: {0}", directory.FullName);
                    }
                    catch (Exception ex)
                    { Console.WriteLine("Не удалось прочитать каталог {0}: {1}", directory.FullName, ex.Message); }
                }
            }
        }

        static void CalcFilesInDirectory(DirectoryInfo dir) 
        {
            SumDelFiles += dir.GetFiles().Length;
            DirectoryInfo[] arrDirectories = dir.GetDirectories();

            foreach (DirectoryInfo directory in arrDirectories)
            {
                try
                {
                    CalcFilesInDirectory(directory);
                }
                catch (Exception ex)
                { Console.WriteLine("Не удалось прочитать каталог {0}: {1}", directory.FullName, ex.Message); }
            }

        }

    }
}
