using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace @v44_DateTime_Person
{
    // 1. КЛАСС-ЧЕРТЁЖ: Описывает, из чего состоит один человек
    class Person
    {
        public int Id { get; set; }
        public DateTime BirthDate { get; set; }
        //public int Id { get; set; }
        public string Name { get; set; }

        // Конструктор, который создает человека из полученных данных
        public Person( int id, DateTime dt, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be empty.");
            }

            BirthDate = dt;
            Id = id;
            Name = name;
        }
    }

    // 2. КЛАСС-ДВИГАТЕЛЬ: Управляет всей программой
    class Program
    {
        // ТОТ САМЫЙ МЕТОД MAIN (Точка старта программы)
        static void Main(string[] args)
        {
            // Создаем пустой список (коробку), куда будем складывать людей
            List<Person> people = new List<Person>();

            // Запускаем бесконечный цикл для ввода данных
            while (true)
            {
                Console.Write("Date (yyyy-MM-dd): ");
                // Используем ?? "", чтобы Visual Studio не ругалась на пустой ввод
                string dateInput = Console.ReadLine() ?? "";

                // Если ввели exit — останавливаем цикл ввода
                if (dateInput.Trim().ToLower() == "exit")
                {
                    break;
                }

                // Проверяем формат даты
                if (!DateTime.TryParseExact(dateInput, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime birthDate))
                {
                    Console.WriteLine("Error: Please use yyyy-MM-dd format or type 'exit' to finish.\n");
                    continue;
                }

                Console.Write("Id (number): ");
                string idInput = Console.ReadLine() ?? "";
                if (!int.TryParse(idInput, out int id))
                {
                    Console.WriteLine("Error: Wrong format! Please enter a valid number.\n");
                    continue;
                }

                Console.Write("Name: ");
                string name = Console.ReadLine() ?? "";
                Console.WriteLine();

                // Пытаемся создать человека и добавить его в наш список
                try
                {
                    Person person = new Person( id, birthDate, name);
                   
                    people.Add(person); // Вот тут список перестает быть пустым!
                }
                catch (ArgumentException )
                {
                    Console.WriteLine(
                    "Application Error: {ex.Message}\n");
                }
                catch (Exception )
                {
                    Console.WriteLine(

                    "An unexpected error occurred: {ex.Message}\n");
                }
            }

            // ПОСЛЕ ТОГО, КАК ВВОД ЗАВЕРШЕН (ввели 'exit'):
            try
            {
                // Сортируем список по дате рождения
                var sortedPeople = people.OrderBy(p => p.BirthDate).ToList();

                Console.WriteLine();
                // Печатаем красивую таблицу с результатами
                Console.WriteLine($"{"Id",-10}{"BirthDate",-15}{"Name"}");

                foreach (var person in sortedPeople)
                {
                    string formattedDate = person.BirthDate.ToString("yyyy-MM-dd");
                    Console.WriteLine($"{person.Id,-10}{formattedDate,-15}{person.Name}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during sorting or rendering: {ex.Message}");
            }
        }
    }
}