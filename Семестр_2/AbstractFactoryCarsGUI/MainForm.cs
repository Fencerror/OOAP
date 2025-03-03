using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace AbstractFactoryCarsGUI
{
    public class MainForm : Form
    {
        private ComboBox manufacturerComboBox;
        private ComboBox colorComboBox;
        private ComboBox modelComboBox;
        private ComboBox transmissionComboBox;
        private Button createCarButton;

        public MainForm()
        {
            Text = "Car Assembler";
            Width = 500;
            Height = 500;
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;

            Label manufacturerLabel = new Label
            {
                Text = "Выберите производителя:",
                Location = new Point(10, 20),
                AutoSize = true,
                Font = new Font("Arial", 12, FontStyle.Bold)
            };
            Controls.Add(manufacturerLabel);

            manufacturerComboBox = new ComboBox
            {
                Location = new Point(10, 50),
                Width = 460,
                Font = new Font("Arial", 12)
            };
            manufacturerComboBox.Items.AddRange(new string[] { "BMW", "Toyota", "Tesla" });
            Controls.Add(manufacturerComboBox);

            Label colorLabel = new Label
            {
                Text = "Выберите цвет:",
                Location = new Point(10, 90),
                AutoSize = true,
                Font = new Font("Arial", 12, FontStyle.Bold)
            };
            Controls.Add(colorLabel);

            colorComboBox = new ComboBox
            {
                Location = new Point(10, 120),
                Width = 460,
                Font = new Font("Arial", 12)
            };
            colorComboBox.Items.AddRange(new string[] { "Красный", "Синий", "Чёрный", "Белый" });
            Controls.Add(colorComboBox);

            Label modelLabel = new Label
            {
                Text = "Выберите модель:",
                Location = new Point(10, 160),
                AutoSize = true,
                Font = new Font("Arial", 12, FontStyle.Bold)
            };
            Controls.Add(modelLabel);

            modelComboBox = new ComboBox
            {
                Location = new Point(10, 190),
                Width = 460,
                Font = new Font("Arial", 12)
            };
            modelComboBox.Items.AddRange(new string[] { "Модель A", "Модель B", "Модель C" });
            Controls.Add(modelComboBox);

            Label transmissionLabel = new Label
            {
                Text = "Выберите трансмиссию:",
                Location = new Point(10, 230),
                AutoSize = true,
                Font = new Font("Arial", 12, FontStyle.Bold)
            };
            Controls.Add(transmissionLabel);

            transmissionComboBox = new ComboBox
            {
                Location = new Point(10, 260),
                Width = 460,
                Font = new Font("Arial", 12)
            };
            transmissionComboBox.Items.AddRange(new string[] { "Автоматическая", "Механическая", "Одноступенчатая" });
            Controls.Add(transmissionComboBox);

            createCarButton = new Button
            {
                Text = "Создать автомобиль",
                Location = new Point(10, 300),
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
}
