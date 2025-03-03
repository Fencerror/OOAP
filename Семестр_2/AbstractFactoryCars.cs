using System;
using System.IO;

// Абстрактные классы для компонентов
abstract class Body { public abstract string Type { get; } }
abstract class Engine { public abstract string Type { get; } }
abstract class Transmission { public abstract string Type { get; } }
abstract class Wheels { public abstract string Type { get; } }

// Конкретные компоненты для BMW
class BMWBody : Body { public override string Type => "Седан"; }
class BMWEngine : Engine { public override string Type => "Бензиновый 3.0L"; }
class BMWTransmission : Transmission { public override string Type => "Автоматическая"; }
class BMWWheels : Wheels { public override string Type => "Спортивные"; }

// Конкретные компоненты для Toyota
class ToyotaBody : Body { public override string Type => "Внедорожник"; }
class ToyotaEngine : Engine { public override string Type => "Дизельный 2.5L"; }
class ToyotaTransmission : Transmission { public override string Type => "Механическая"; }
class ToyotaWheels : Wheels { public override string Type => "Внедорожные"; }

// Конкретные компоненты для Tesla
class TeslaBody : Body { public override string Type => "Спорткар"; }
class TeslaEngine : Engine { public override string Type => "Электрический"; }
class TeslaTransmission : Transmission { public override string Type => "Одноступенчатая"; }
class TeslaWheels : Wheels { public override string Type => "Аэродинамические"; }

// Абстрактная фабрика автомобилей
interface ICarFactory
{
    Body CreateBody();
    Engine CreateEngine();
    Transmission CreateTransmission();
    Wheels CreateWheels();
}

// Конкретные фабрики производителей
class BMWFactory : ICarFactory
{
    public Body CreateBody() => new BMWBody();
    public Engine CreateEngine() => new BMWEngine();
    public Transmission CreateTransmission() => new BMWTransmission();
    public Wheels CreateWheels() => new BMWWheels();
}

class ToyotaFactory : ICarFactory
{
    public Body CreateBody() => new ToyotaBody();
    public Engine CreateEngine() => new ToyotaEngine();
    public Transmission CreateTransmission() => new ToyotaTransmission();
    public Wheels CreateWheels() => new ToyotaWheels();
}

class TeslaFactory : ICarFactory
{
    public Body CreateBody() => new TeslaBody();
    public Engine CreateEngine() => new TeslaEngine();
    public Transmission CreateTransmission() => new TeslaTransmission();
    public Wheels CreateWheels() => new TeslaWheels();
}

// Класс для представления модели автомобиля
class Car
{
    public string Manufacturer { get; }
    public Body Body { get; }
    public Engine Engine { get; }
    public Transmission Transmission { get; }
    public Wheels Wheels { get; }

    public Car(string manufacturer, ICarFactory factory)
    {
        Manufacturer = manufacturer;
        Body = factory.CreateBody();
        Engine = factory.CreateEngine();
        Transmission = factory.CreateTransmission();
        Wheels = factory.CreateWheels();
    }

    public void SaveToHtml()
    {
        string fileName = $"{Manufacturer}_Model.html";
        string htmlContent = $@"<html><head><title>{Manufacturer} Model</title></head><body>
            <h1>{Manufacturer} Автомобиль</h1>
            <p><strong>Кузов:</strong> {Body.Type}</p>
            <p><strong>Двигатель:</strong> {Engine.Type}</p>
            <p><strong>Трансмиссия:</strong> {Transmission.Type}</p>
            <p><strong>Колеса:</strong> {Wheels.Type}</p>
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
        ICarFactory factory = choice switch
        {
            1 => new BMWFactory(),
            2 => new ToyotaFactory(),
            3 => new TeslaFactory(),
            _ => throw new ArgumentException("Неверный выбор")
        };
        
        Car car = new Car(factory.GetType().Name.Replace("Factory", ""), factory);
        car.SaveToHtml();
    }
}
