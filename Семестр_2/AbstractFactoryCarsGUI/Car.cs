using System;
using System.IO;
using System.Windows.Forms; // Добавлено для использования MessageBox, MessageBoxButtons и MessageBoxIcon

namespace AbstractFactoryCarsGUI
{
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
}
