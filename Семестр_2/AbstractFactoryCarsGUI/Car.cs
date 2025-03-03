using System;
using System.IO;

namespace AbstractFactoryCarsGUI
{
    // Класс для представления модели автомобиля
    class Car
    {
        public string Manufacturer { get; }
        public Body Body { get; }
        public Engine Engine { get; }
        public Wheels Wheels { get; }
        public string Color { get; }
        public string Model { get; }
        public string Transmission { get; }

        public Car(string manufacturer, ICarFactory factory, string color, string model, string transmission)
        {
            Manufacturer = manufacturer;
            Body = factory.CreateBody();
            Engine = factory.CreateEngine();
            Wheels = factory.CreateWheels();
            Color = color;
            Model = model;
            Transmission = transmission;
        }

        public void SaveToHtml()
        {
            string fileName = $"{Manufacturer}_Model.html";
            string photoPath = $"photos/{Manufacturer}.jpg";
            string htmlContent = $@"<html><head><title>{Manufacturer} Model</title>
                <style>
                    body {{ font-family: Arial, sans-serif; }}
                    h1 {{ color: #333; }}
                    p {{ font-size: 14px; }}
                    .car-photo {{ width: 300px; height: auto; }}
                    .car-info {{ margin-top: 20px; }}
                    .car-info p {{ margin: 5px 0; }}
                </style>
                </head><body>
                <h1>{Manufacturer} Автомобиль</h1>
                <img src='{photoPath}' alt='{Manufacturer} фото' class='car-photo'>
                <div class='car-info'>
                    <p><strong>Кузов:</strong> {Body.Type}</p>
                    <p><strong>Двигатель:</strong> {Engine.Type}</p>
                    <p><strong>Колеса:</strong> {Wheels.Type}</p>
                    <p><strong>Цвет:</strong> {Color}</p>
                    <p><strong>Модель:</strong> {Model}</p>
                    <p><strong>Трансмиссия:</strong> {Transmission}</p>
                </div>
            </body></html>";

            File.WriteAllText(fileName, htmlContent);
            Console.WriteLine($"HTML-файл '{fileName}' создан.");
        }
    }
}
