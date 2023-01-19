using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace FinalTask
{
    [Serializable]
    // Создал серилиализованный класс Student
    class Student
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public DateTime DateOfBirth { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string path = @"D:\Разработка\skillfactory\task_8\files\Students.dat";
            string pathDeskTop = @"C:\Users\Андрей\Desktop\Students";
 
            // Проверка на существование каталога на рабочем столе
            if (!Directory.Exists(pathDeskTop))
            Directory.CreateDirectory(pathDeskTop); 
 
            // Производим десериализацию
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                Student[] student = (Student[])formatter.Deserialize(fs);
 
                foreach (Student s in student)
                {
                    Console.WriteLine("Имя: {0} Группа: {1} Дата рождения: {2}", s.Name, s.Group, s.DateOfBirth);

                    // Пишем данные в файл. Если файла нет, он будет создан
                    using (StreamWriter writer = new StreamWriter($"{pathDeskTop}\\{s.Group}.txt", true))
                    { writer.WriteLine("{0}, {1}", s.Name, s.DateOfBirth); }
                }
            }
        }
    }
}
