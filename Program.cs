using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Xml.Serialization;



namespace Converter

{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите путь до файла:");
            string filePath = Console.ReadLine();

            Console.WriteLine("Содержимое файла:");
            string content = File.ReadAllText(filePath);
            Console.WriteLine(content);

            Console.WriteLine("Нажмите F1 для сохранения файла и Escape для выхода.");
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            if (keyInfo.Key == ConsoleKey.F1)
            {
                SaveFile(filePath, content);
            }
            else if (keyInfo.Key == ConsoleKey.Escape)
            {
                Console.WriteLine("Выход");
            }
        }

        static void SaveFile(string filePath, string content)
        {
            string extension = Path.GetExtension(filePath);

            if (extension == ".txt")
            {
                File.WriteAllText(filePath, content);
            }
            else if (extension == ".json")
            {
                var figures = JsonSerializer.Deserialize<List<Figure>>(content);
                File.WriteAllText(filePath, JsonSerializer.Serialize(figures));
            }
            else if (extension == ".xml")
            {
                var figures = new List<Figure>();
                using (StringReader stringReader = new StringReader(content))
                {
                    XmlSerializer serializer = new XmlSerializer(figures.GetType());
                    figures = (List<Figure>)serializer.Deserialize(stringReader);
                }

                using (StringWriter stringWriter = new StringWriter())
                {
                    XmlSerializer serializer = new XmlSerializer(figures.GetType());
                    serializer.Serialize(stringWriter, figures);
                    File.WriteAllText(filePath, stringWriter.ToString());
                }
            }

            Console.WriteLine("Файл успешно сохранён.");
        }
    }
}
