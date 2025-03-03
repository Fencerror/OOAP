using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

// Абстрактные классы для компонентов
abstract class Body { public abstract string Type { get; } }
abstract class Engine { public abstract string Type { get; } }
abstract class Wheels { public abstract string Type { get; } }

// Конкретные компоненты для BMW
class BMWBody : Body { public override string Type => "Седан"; }
class BMWEngine : Engine { public override string Type => "Бензиновый 3.0L"; }
class BMWWheels : Wheels { public override string Type => "Спортивные"; }

// Конкретные компоненты для Toyota
class ToyotaBody : Body { public override string Type => "Внедорожник"; }
class ToyotaEngine : Engine { public override string Type => "Дизельный 2.5L"; }
class ToyotaWheels : Wheels { public override string Type => "Внедорожные"; }

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

class ToyotaFactory : ICarFactory
{
    public Body CreateBody() => new ToyotaBody();
    public Engine CreateEngine() => new ToyotaEngine();
    public Wheels CreateWheels() => new ToyotaWheels();
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
        MessageBox.Show($"HTML-файл '{fileName}' создан.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}

public class MainForm : Form
{
    private ComboBox manufacturerComboBox;
    private ComboBox colorComboBox;
    private ComboBox modelComboBox;
    private ComboBox transmissionComboBox;
    private Button createCarButton;
    private PictureBox logoPictureBox;

    public MainForm()
    {
        Text = "Car Assembler";
        Width = 500;
        Height = 500;
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;

        logoPictureBox = new PictureBox
        {
            Image = Image.FromFile("logo.png"),
            Location = new Point(10, 10),
            Size = new Size(460, 100),
            SizeMode = PictureBoxSizeMode.StretchImage
        };
        Controls.Add(logoPictureBox);

        Label manufacturerLabel = new Label
        {
            Text = "Выберите производителя:",
            Location = new Point(10, 120),
            AutoSize = true,
            Font = new Font("Arial", 12, FontStyle.Bold)
        };
        Controls.Add(manufacturerLabel);

        manufacturerComboBox = new ComboBox
        {
            Location = new Point(10, 150),
            Width = 460,
            Font = new Font("Arial", 12)
        };
        manufacturerComboBox.Items.AddRange(new string[] { "BMW", "Toyota", "Tesla" });
        Controls.Add(manufacturerComboBox);

        Label colorLabel = new Label
        {
            Text = "Выберите цвет:",
            Location = new Point(10, 190),
            AutoSize = true,
            Font = new Font("Arial", 12, FontStyle.Bold)
        };
        Controls.Add(colorLabel);

        colorComboBox = new ComboBox
        {
            Location = new Point(10, 220),
            Width = 460,
            Font = new Font("Arial", 12)
        };
        colorComboBox.Items.AddRange(new string[] { "Красный", "Синий", "Чёрный", "Белый" });
        Controls.Add(colorComboBox);

        Label modelLabel = new Label
        {
            Text = "Выберите модель:",
            Location = new Point(10, 260),
            AutoSize = true,
            Font = new Font("Arial", 12, FontStyle.Bold)
        };
        Controls.Add(modelLabel);

        modelComboBox = new ComboBox
        {
            Location = new Point(10, 290),
            Width = 460,
            Font = new Font("Arial", 12)
        };
        modelComboBox.Items.AddRange(new string[] { "Модель A", "Модель B", "Модель C" });
        Controls.Add(modelComboBox);

        Label transmissionLabel = new Label
        {
            Text = "Выберите трансмиссию:",
            Location = new Point(10, 330),
            AutoSize = true,
            Font = new Font("Arial", 12, FontStyle.Bold)
        };
        Controls.Add(transmissionLabel);

        transmissionComboBox = new ComboBox
        {
            Location = new Point(10, 360),
            Width = 460,
            Font = new Font("Arial", 12)
        };
        transmissionComboBox.Items.AddRange(new string[] { "Автоматическая", "Механическая", "Одноступенчатая" });
        Controls.Add(transmissionComboBox);

        createCarButton = new Button
        {
            Text = "Создать автомобиль",
            Location = new Point(10, 400),
            Width = 460,
            Height = 40,
            Font = new Font("Arial", 12, FontStyle.Bold),
            BackColor = Color.LightBlue
        };
        createCarButton.Click += CreateCarButton_Click;
        Controls.Add(createCarButton);
    }

    private void CreateCarButton_Click(object sender, EventArgs e)
    {
        string selectedManufacturer = manufacturerComboBox.SelectedItem?.ToString() ?? string.Empty;
        string selectedColor = colorComboBox.SelectedItem?.ToString() ?? string.Empty;
        string selectedModel = modelComboBox.SelectedItem?.ToString() ?? string.Empty;
        string selectedTransmission = transmissionComboBox.SelectedItem?.ToString() ?? string.Empty;

        if (string.IsNullOrEmpty(selectedManufacturer) || string.IsNullOrEmpty(selectedColor) || string.IsNullOrEmpty(selectedModel) || string.IsNullOrEmpty(selectedTransmission))
        {
            MessageBox.Show("Пожалуйста, выберите все параметры.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        ICarFactory factory = selectedManufacturer switch
        {
            "BMW" => new BMWFactory(),
            "Toyota" => new ToyotaFactory(),
            "Tesla" => new TeslaFactory(),
            _ => throw new ArgumentException("Неверный выбор")
        };

        Car car = new Car(selectedManufacturer, factory, selectedColor, selectedModel, selectedTransmission);
        car.SaveToHtml();
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
