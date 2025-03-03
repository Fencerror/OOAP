using System;
using System.IO;

class Car
{
    public string Manufacturer { get; }
    public string Body { get; }
    public string Engine { get; }
    public string Wheels { get; }

    public Car(string manufacturer, string body, string engine, string wheels)
    {
        Manufacturer = manufacturer;
        Body = body;
        Engine = engine;
        Wheels = wheels;
    }

    public void SaveToHtml()
    {
        string fileName = $"{Manufacturer}_Model.html";
        string htmlContent = $@"<html><head><title>{Manufacturer} Model</title></head><body>
            <h1>{Manufacturer} Автомобиль</h1>
            <p><strong>Кузов:</strong> {Body}</p>
            <p><strong>Двигатель:</strong> {Engine}</p>
            <p><strong>Колеса:</strong> {Wheels}</p>
        </body></html>";

        File.WriteAllText(fileName, htmlContent);
        Console.WriteLine($"HTML-файл '{fileName}' создан.");
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Выберите производителя:");
        Console.WriteLine("1. BMW");
        Console.WriteLine("2. Toyota");
        Console.WriteLine("3. Tesla");
        Console.Write("Введите номер: ");
        
        int choice = int.Parse(Console.ReadLine());

        Car car = choice switch
        {
            1 => new Car("BMW", "Седан", "Бензиновый 3.0L", "Автоматическая", "Спортивные"),
            2 => new Car("Toyota", "Внедорожник", "Дизельный 2.5L", "Механическая", "Внедорожные"),
            3 => new Car("Tesla", "Спорткар", "Электрический", "Одноступенчатая", "Аэродинамические"),
            _ => throw new ArgumentException("Неверный выбор")
        };

        car.SaveToHtml();
    }
}