using System;
using System.IO;

namespace task1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите путь к каталогу: ");
            string path = Console.ReadLine();
            
            // Проверка существует ли введенный каталог. Если каталог существует, получаем все подкаталоги и файлы 
            if (Directory.Exists(path))
            {
                string[] arrDir = Directory.GetFileSystemEntries(path);
                foreach (string dir in arrDir)
                {
                    // Если это каталог
                    if (Directory.Exists(dir)) 
                    {
                        // Вычисляем разницу в минутах между текущей датой и датой последнего изменения каталога
                        // Если разница больше 30 минут, удаляем каталог
                        double lastTime = (DateTime.Now - Directory.GetLastWriteTime(dir)).TotalMinutes;
                        if (lastTime > 30) 
                        {
                            // Проверка на исключения
                            try 
                            {
                                Directory.Delete(dir, true);
                                Console.WriteLine("Каталог удален: {0}", dir); 
                            }
                            catch (Exception ex)
                            { Console.WriteLine("Ошибка при удалении каталога {0}: {1}", dir, ex.Message); }
                        }
                    }

                    // Иначе если это файл, делаем те же действия с файлом
                    else if (File.Exists(dir))
                    {
                        double lastTime = (DateTime.Now - File.GetLastWriteTime(dir)).TotalMinutes;
                                                
                        if (lastTime > 30)
                        {
                            try
                            {
                                File.Delete(dir);
                                Console.WriteLine("Файл удален: {0}", dir);
                            }
                            catch (Exception ex)
                            { Console.WriteLine("Ошибка при удалении файла {0}: {1}", dir, ex.Message); }
                        }
                    }
                }

            }
            else Console.WriteLine("Такого каталога не существует!");
        }
    }
}
