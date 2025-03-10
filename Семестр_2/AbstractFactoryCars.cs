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

public class MainForm : Form
{
    private ComboBox manufacturerComboBox;
    private Button createCarButton;
    private Image backgroundImage;

    public MainForm()
    {
        Text = "Car Assembler";
        Width = 900;
        Height = 750;
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;

        try
        {
            backgroundImage = Image.FromFile("photos/logo.png");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка загрузки фона: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        manufacturerComboBox = CreateLabeledComboBox("Выберите производителя:", new string[] { "BMW", "Tesla" }, 100);

        createCarButton = new Button
        {
            Text = "Создать автомобиль",
            Location = new Point(100, 200),
            Width = 300,
            Height = 50,
            Font = new Font("Arial", 12, FontStyle.Bold),
            BackColor = Color.LightBlue
        };
        createCarButton.Click += CreateCarButton_Click;
        Controls.Add(createCarButton);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        if (backgroundImage != null)
        {
            ColorMatrix matrix = new ColorMatrix
            {
                Matrix33 = 1f // Устанавливаем прозрачность (0 - полностью прозрачный, 1 - непрозрачный)
            };

            ImageAttributes attributes = new ImageAttributes();
            attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

            e.Graphics.DrawImage(backgroundImage, new Rectangle(0, 0, Width, Height), 0, 0, backgroundImage.Width, backgroundImage.Height, GraphicsUnit.Pixel, attributes);
        }
    }

    private ComboBox CreateLabeledComboBox(string labelText, string[] items, int y)
    {
        var label = new Label
        {
            Text = labelText,
            Location = new Point(100, y),
            AutoSize = true,
            Font = new Font("Arial", 12, FontStyle.Bold),
            BackColor = Color.Transparent,
            ForeColor = Color.White
        };
        Controls.Add(label);

        var comboBox = new ComboBox
        {
            Location = new Point(100, y + 30),
            Width = 300,
            Font = new Font("Arial", 12)
        };
        comboBox.Items.AddRange(items);
        Controls.Add(comboBox);

        return comboBox;
    }

    private void CreateCarButton_Click(object sender, EventArgs e)
    {
        string selectedManufacturer = manufacturerComboBox.SelectedItem?.ToString() ?? string.Empty;

        if (string.IsNullOrEmpty(selectedManufacturer))
        {
            MessageBox.Show("Пожалуйста, выберите производителя.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        ICarFactory factory = selectedManufacturer switch
        {
            "BMW" => new BMWFactory(),
            "Tesla" => new TeslaFactory(),
            _ => throw new ArgumentException("Неверный выбор")
        };

        Car car = new Car(selectedManufacturer, factory);
        car.SaveToHtml();

        string reportPath = Path.Combine(Environment.CurrentDirectory, $"{selectedManufacturer}_Model.html");
        try
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = reportPath,
                UseShellExecute = true
            });
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка при открытии HTML-файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
