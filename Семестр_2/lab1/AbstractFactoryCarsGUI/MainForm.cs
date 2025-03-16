using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;

namespace AbstractFactoryCarsGUI
{
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
}
