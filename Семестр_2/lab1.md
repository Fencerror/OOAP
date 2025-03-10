**Лабораторная работа №1: паттерн "Абстрактная фабрика"**

# Описание информационной системы
Информационная система предназначена для «сборки» моделей автомобилей. 
Пользователь выбирает производителя автомобиля (BMW, Tesla), после чего программа автоматически собирает соответствующую модель, состоящую из различных компонентов: кузова, двигателя и колёс. 
После сборки программа генерирует HTML-отчёт с характеристиками собранной модели. 

# Диаграмма классов

![AbstractFactory drawio](https://github.com/user-attachments/assets/c94c252b-32ee-44b6-bc71-51585e457d8a)



1. **Интерфейс `ICarFactory`**
   - Методы:
     - `+ CreateBody(): Body` — метод для создания кузова.
     - `+ CreateEngine(): Engine` — метод для создания двигателя.
     - `+ CreateWheels(): Wheels` — метод для создания колёс.

2. **Абстрактные классы компонентов**
   - `Body`
   - `Engine`
   - `Wheels`
   
   Эти классы представляют базовые компоненты автомобиля.

3. **Классы `BMWFactory`, `TeslaFactory`**
   - Эти классы реализуют интерфейс `ICarFactory`, предоставляя конкретные реализации методов для создания компонентов автомобилей соответствующих производителей.

4. **Классы компонентов (`BMWBody`, `TeslaBody` и аналогичные для других частей авто)**
   - Эти классы наследуются от абстрактных классов компонентов и предоставляют конкретные реализации для каждого производителя.

5. **Класс `Car`**
   - Атрибуты:
     - `- Manufacturer: string` — название производителя.
     - `- Body: Body` — объект, представляющий кузов.
     - `- Engine: Engine` — объект, представляющий двигатель.
     - `- Wheels: Wheels` — объект, представляющий колёса.
   - Методы:
     - `+ Car(manufacturer: string, factory: ICarFactory)` — конструктор, создающий автомобиль.

# Алгоритм работы с приложением:

1. **Выбор производителя:**
   - Пользователь выбирает производителя автомобиля из списка (BMW, Tesla).

2. **Создание автомобиля:**
   - На основе выбранного производителя программа создаёт экземпляр соответствующей фабрики (`BMWFactory`, `TeslaFactory`).
   - Фабрика создаёт конкретные компоненты автомобиля (кузов, двигатель, колёса).
   - Собранный автомобиль создаётся с использованием этих компонентов.

3. **Генерация отчёта:**
   - Метод `SaveToHtml()` сохраняет характеристики автомобиля в HTML-файл.
   - Пользователь получает HTML-документ с описанием собранной модели автомобиля.

# Код с паттерном "Абстрактная фабрика"

<!-- Файл AbstractFactoryCars.cs -->

```
using System;
using System.IO;
using System.Windows.Forms;

// Абстрактные классы для компонентов
abstract class Body { public abstract string Type { get; } }
abstract class Engine { public abstract string Type { get; } }
abstract class Wheels { public abstract string Type { get; } }

// Конкретные компоненты для BMW
class BMWBody : Body { public override string Type => "Седан"; }
class BMWEngine : Engine { public override string Type => "Бензиновый 3.0L"; }
class BMWWheels : Wheels { public override string Type => "Спортивные"; }

// Конкретные компоненты для Tesla
class TeslaBody : Body { public override string Type => "Спорткар"; }
class TeslaEngine : Engine { public override string Type => "Электрический"; }
class TeslaWheels : Wheels { public override string Type => "Аэродинамические"; }

// Абстрактная фабрика автомобилей
interface ICarFactory
{
    Body CreateBody();
    Engine CreateEngine();
    Wheels CreateWheels();
}

// Конкретные фабрики производителей
class BMWFactory : ICarFactory
{
    public Body CreateBody() => new BMWBody();
    public Engine CreateEngine() => new BMWEngine();
    public Wheels CreateWheels() => new BMWWheels();
}

class TeslaFactory : ICarFactory
{
    public Body CreateBody() => new TeslaBody();
    public Engine CreateEngine() => new TeslaEngine();
    public Wheels CreateWheels() => new TeslaWheels();
}

// Класс для представления модели автомобиля
class Car
{
    public string Manufacturer { get; }
    public Body Body { get; }
    public Engine Engine { get; }
    public Wheels Wheels { get; }

    public Car(string manufacturer, ICarFactory factory)
    {
        Manufacturer = manufacturer;
        Body = factory.CreateBody();
        Engine = factory.CreateEngine();
        Wheels = factory.CreateWheels();
    }

    public void SaveToHtml()
    {
        try
        {
            string fileName = Path.Combine(Environment.CurrentDirectory, $"{Manufacturer}_Model.html");
            string photoPath = Path.Combine(Environment.CurrentDirectory, $"photos/{Manufacturer}.jpg");
            string htmlContent = $@"<html><head><title>{Manufacturer} Model</title>
                <style>
                    body {{ font-family: Arial, sans-serif; display:flex; flex-direction: column; align-items: center; background: rgb(2,0,36);background: linear-gradient(90deg, rgba(2,0,36,1) 0%, rgba(0,0,0,1) 0%, rgba(98,0,0,1) 100%); color:#999897; }}
                    h1 {{ color: #333; }}
                    .main-container {{ display: flex; flex-direction: row; align-items: center; margin-top: 20px; }}
                    .car-info {{ margin-right: 30px; }}
                    p {{ font-size: 18px; }}
                    .car-photo {{ width: 500px; height: auto; }}
                    .car-info {{ margin-top: 20px; }}
                    .car-info p {{ margin: 5px 0; }}
                </style>
                </head><body>
                <h1>Автомобиль: {Manufacturer} </h1>
                <div class='main-container'>
                    <div class='car-info'>
                        <p><strong>Кузов:</strong> {Body.Type}</p>
                        <p><strong>Двигатель:</strong> {Engine.Type}</p>
                        <p><strong>Колеса:</strong> {Wheels.Type}</p>
                    </div>
                    <div class ='car-photo'>
                        <img src='{photoPath}' alt='{Manufacturer} фото' class='car-photo'>
                    </div>
                </div>
            </body></html>";

            File.WriteAllText(fileName, htmlContent);
            MessageBox.Show($"HTML-файл '{fileName}' создан.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при создании HTML-файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}

namespace AbstractFactoryCarsGUI
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
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
        Console.WriteLine("3. Tesla");
        Console.Write("Введите номер: ");
        
        int choice = int.Parse(Console.ReadLine());

        Car car = choice switch
        {
            1 => new Car("BMW", "Седан", "Бензиновый 3.0L", "Автоматическая", "Спортивные"),
            3 => new Car("Tesla", "Спорткар", "Электрический", "Одноступенчатая", "Аэродинамические"),
            _ => throw new ArgumentException("Неверный выбор")
        };

        car.SaveToHtml();
    }
}
```
# Вывод

Таким образом, паттерн AbstractFactory будет хорошим решением, если необходимо создавать объекты семействами, то есть группами.

**Преимущества:**
 - Удобен при создании групп взаимосвязанных объектов
**Недостатки:**
 - Неудобен при добавлении нового объекта в семейство (надо расширять все реализованные фабрики)
 - Трудно реализовать, если семейства имеют разное количество объектов

