**Лабораторная работа №1: паттерн "Абстрактная фабрика"**

# Описание информационной системы
Информационная система предназначена для «сборки» моделей автомобилей. 
Пользователь выбирает производителя автомобиля (BMW, Toyota, Tesla), после чего программа автоматически собирает соответствующую модель, состоящую из различных компонентов: кузова, двигателя, коробки передач и колёс. 
После сборки программа генерирует HTML-отчёт с характеристиками собранной модели. 

# Диаграмма классов
<!-- добавить диаграмму потом -->

1. **Интерфейс `ICarFactory`**
   - Методы:
     - `+ CreateBody(): Body` — метод для создания кузова.
     - `+ CreateEngine(): Engine` — метод для создания двигателя.
     - `+ CreateTransmission(): Transmission` — метод для создания коробки передач.
     - `+ CreateWheels(): Wheels` — метод для создания колёс.

2. **Абстрактные классы компонентов**
   - `Body`
   - `Engine`
   - `Transmission`
   - `Wheels`
   
   Эти классы представляют базовые компоненты автомобиля.

3. **Классы `BMWFactory`, `ToyotaFactory`, `TeslaFactory`**
   - Эти классы реализуют интерфейс `ICarFactory`, предоставляя конкретные реализации методов для создания компонентов автомобилей соответствующих производителей.

4. **Классы компонентов (`BMWBody`, `ToyotaBody`, `TeslaBody` и аналогичные для других частей авто)**
   - Эти классы наследуются от абстрактных классов компонентов и предоставляют конкретные реализации для каждого производителя.

5. **Класс `Car`**
   - Атрибуты:
     - `- Manufacturer: string` — название производителя.
     - `- Body: Body` — объект, представляющий кузов.
     - `- Engine: Engine` — объект, представляющий двигатель.
     - `- Transmission: Transmission` — объект, представляющий коробку передач.
     - `- Wheels: Wheels` — объект, представляющий колёса.
   - Методы:
     - `+ Car(manufacturer: string, factory: ICarFactory)` — конструктор, создающий автомобиль.
     - `+ SaveToHtml(): void` — метод для сохранения описания автомобиля в HTML-файл.

6. **Класс `Program` (Главная программа)**
   - Методы:
     - `+ Main()` — точка входа в программу, содержащая меню выбора производителя.
     - Запрашивает у пользователя выбор производителя.
     - Создаёт объект `Car`, используя соответствующую фабрику.
     - Вызывает метод `SaveToHtml()` для сохранения данных в файл.

# Связи между классами

### **Ассоциация:**
- Класс `Car` использует интерфейс `ICarFactory` для работы с фабриками автомобилей. Это обозначается стрелкой, указывающей на `ICarFactory`, что показывает зависимость.
- Класс `Car` агрегирует компоненты `Body`, `Engine`, `Transmission`, `Wheels`, так как они являются его составными частями.

### **Реализация:**
- Классы `BMWFactory`, `ToyotaFactory`, `TeslaFactory` реализуют интерфейс `ICarFactory`. Это показывает, что они предоставляют конкретные реализации методов, объявленных в интерфейсе.
- Классы `BMWBody`, `ToyotaBody`, `TeslaBody` (и аналогичные для других компонентов) наследуют абстрактные классы `Body`, `Engine`, `Transmission`, `Wheels`, предоставляя их конкретные реализации.

# Поток работы

1. **Выбор производителя:**
   - Пользователь выбирает производителя автомобиля из списка (BMW, Toyota, Tesla).

2. **Создание автомобиля:**
   - На основе выбранного производителя программа создаёт экземпляр соответствующей фабрики (`BMWFactory`, `ToyotaFactory`, `TeslaFactory`).
   - Фабрика создаёт конкретные компоненты автомобиля (кузов, двигатель, коробку передач, колёса).
   - Собранный автомобиль создаётся с использованием этих компонентов.

3. **Генерация отчёта:**
   - Метод `SaveToHtml()` сохраняет характеристики автомобиля в HTML-файл.
   - Пользователь получает HTML-документ с описанием собранной модели автомобиля.

# Код с паттерном "Абстрактная фабрика"

<!-- Файл AbstractFactoryCars.cs -->

```
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

```

# Код без паттерна

1. **Удаление интерфейса `ICarFactory` и конкретных фабрик:**
   - Весь код создания автомобиля теперь находится в `Program`, без выделения отдельных фабрик.
2. **Создание автомобилей в одном месте:**
   - Вместо вызова фабрики создаётся объект `Car` с жёстко заданными параметрами.
3. **Сложность расширения:**
   - Добавление новых производителей требует изменения кода `Program`, а не просто создания новой фабрики.

```
using System;
using System.IO;

class Car
{
    public string Manufacturer { get; }
    public string Body { get; }
    public string Engine { get; }
    public string Transmission { get; }
    public string Wheels { get; }

    public Car(string manufacturer, string body, string engine, string transmission, string wheels)
    {
        Manufacturer = manufacturer;
        Body = body;
        Engine = engine;
        Transmission = transmission;
        Wheels = wheels;
    }

    public void SaveToHtml()
    {
        string fileName = $"{Manufacturer}_Model.html";
        string htmlContent = $@"<html><head><title>{Manufacturer} Model</title></head><body>
            <h1>{Manufacturer} Автомобиль</h1>
            <p><strong>Кузов:</strong> {Body}</p>
            <p><strong>Двигатель:</strong> {Engine}</p>
            <p><strong>Трансмиссия:</strong> {Transmission}</p>
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

```







