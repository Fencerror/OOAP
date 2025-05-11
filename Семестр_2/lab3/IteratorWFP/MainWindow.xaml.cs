using System.Linq; // добавлено для проверки условий сценария
using System.Windows;
using lab3.Collections;
using lab3.Models;
using lab3.Iterators;

namespace lab3
{
    public partial class MainWindow : Window
    {
        private FormCollection _formCollection;
        private IFormIterator? _iterator;

        public MainWindow()
        {
            InitializeComponent();

            // Создаем коллекцию блоков формы
            _formCollection = new FormCollection();

            // Блок 1: Персональная информация
            var personalInfoBlock = new FormBlock("Персональная информация");
            personalInfoBlock.AddField(new FormField("Имя"));
            personalInfoBlock.AddField(new FormField("Фамилия"));
            personalInfoBlock.AddField(new FormField("Полных лет"));
            _formCollection.AddBlock(personalInfoBlock);

            // Блок 2: Профессиональные данные
            var professionalInfoBlock = new FormBlock("Профессиональные данные");
            professionalInfoBlock.AddField(new FormField("Университет"));
            professionalInfoBlock.AddField(new FormField("Специальность"));
            professionalInfoBlock.AddField(new FormField("Курс", "Выберите ваш курс", FieldType.Dropdown, new[] { "1", "2", "3", "4", "5" }));
            _formCollection.AddBlock(professionalInfoBlock);

            // Блок 3: Опрос о спорте
            var sportsSurveyBlock = new FormBlock("Опрос о спорте");
            sportsSurveyBlock.AddField(new FormField("Как часто вы занимаетесь спортом?", "Выберите частоту", FieldType.Dropdown, new[] { "Ежедневно", "Раз в неделю", "Редко", "Никогда" }));
            sportsSurveyBlock.AddField(new FormField("Посещаете-ли секции? если да, то введите название"));
            sportsSurveyBlock.AddField(new FormField("Как вы оцениваете работу преподавателя?"));
            sportsSurveyBlock.AddField(new FormField("Что вам особенно нравится?", "Опишите, что вам нравится"));
            _formCollection.AddBlock(sportsSurveyBlock);

            if (_formCollection.GetIterator().HasNext())
            {
                _iterator = _formCollection.GetIterator();
                LoadCurrentBlock();
            }
            else
            {
                MessageBox.Show("Форма пуста.", "Ошибка");
                NextButton.IsEnabled = false;
                GenerateReportButton.IsEnabled = false;
            }
        }

        private void LoadCurrentBlock()
        {
            if (_iterator == null || !_iterator.HasNext())
            {
                MessageBox.Show("Конец коллекции. Вы можете создать отчёт.", "Конец формы");
                NextButton.IsEnabled = false;
                return;
            }

            var block = (FormBlock)_iterator.Next();

            FieldLabel.Content = block.Title;

            FieldsPanel.Children.Clear();
            foreach (var field in block.Fields)
            {
                
                var labelContent = string.IsNullOrWhiteSpace(field.Description) 
                    ? field.Label 
                    : $"{field.Label} ({field.Description})";

                var label = new System.Windows.Controls.Label
                {
                    Content = labelContent,
                    FontSize = 18,
                    Margin = new Thickness(0, 10, 0, 5)
                };
                FieldsPanel.Children.Add(label);

                if (field.Type == FieldType.Text)
                {
                    
                    var textBox = new System.Windows.Controls.TextBox
                    {
                        Text = field.Value,
                        Tag = field,
                        FontSize = 16,
                        Height = 40,
                        Margin = new Thickness(0, 0, 0, 15)
                    };
                    FieldsPanel.Children.Add(textBox);
                }
                else if (field.Type == FieldType.Dropdown)
                {
            
                    var comboBox = new System.Windows.Controls.ComboBox
                    {
                        ItemsSource = field.Options,
                        SelectedValue = field.Value,
                        Tag = field,
                        FontSize = 16,
                        Height = 40,
                        Margin = new Thickness(0, 0, 0, 15)
                    };
                    FieldsPanel.Children.Add(comboBox);
                }
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (_iterator == null) return;

            var currentBlock = (FormBlock)_iterator.Current();
            bool isValid = true;

            foreach (var child in FieldsPanel.Children)
            {
                if (child is System.Windows.Controls.TextBox textBox && textBox.Tag is FormField field)
                {
                    field.Value = textBox.Text;
                    
                    if (string.IsNullOrWhiteSpace(field.Value))
                    {
                        isValid = false;
                        MessageBox.Show($"Поле \"{field.Label}\" не может быть пустым.", "Ошибка валидации");
                        break;
                    }
                }
                else if (child is System.Windows.Controls.ComboBox comboBox && comboBox.Tag is FormField dropdownField)
                {
                    dropdownField.Value = comboBox.SelectedValue?.ToString() ?? string.Empty;
                    
                    if (string.IsNullOrWhiteSpace(dropdownField.Value))
                    {
                        isValid = false;
                        MessageBox.Show($"Выберите значение для поля \"{dropdownField.Label}\".", "Ошибка валидации");
                        break;
                    }
                }
            }

            if (isValid)
            {
                // Новый сценарий: если в блоке "Опрос о спорте" поле "Как часто вы занимаетесь спортом?" имеет значение "Никогда"
                if (currentBlock.Title == "Опрос о спорте")
                {
                    foreach (var field in currentBlock.Fields)
                    {
                        if (field.Label == "Как часто вы занимаетесь спортом?" && field.Value == "Никогда")
                        {
                            var recommendationBlock = new FormBlock("Рекомендации");
                            recommendationBlock.AddField(new FormField("Причины", "Расскажите, почему вы не занимаетесь спортом?", FieldType.Text));
                            _formCollection.AddBlock(recommendationBlock);
                            break;
                        }
                    }
                }

                LoadCurrentBlock();
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (_iterator == null || !_iterator.HasPrevious())
            {
                MessageBox.Show("Нет предыдущего блока.", "Навигация");
                return;
            }
            var block = (FormBlock)_iterator.Previous();
            FieldLabel.Content = block.Title;
            FieldsPanel.Children.Clear();
            foreach (var field in block.Fields)
            {
                var labelContent = string.IsNullOrWhiteSpace(field.Description)
                    ? field.Label
                    : $"{field.Label} ({field.Description})";

                var label = new System.Windows.Controls.Label
                {
                    Content = labelContent,
                    FontSize = 18,
                    Margin = new Thickness(0, 10, 0, 5)
                };
                FieldsPanel.Children.Add(label);

                if (field.Type == FieldType.Text)
                {
                    var textBox = new System.Windows.Controls.TextBox
                    {
                        Text = field.Value,
                        Tag = field,
                        FontSize = 16,
                        Height = 40,
                        Margin = new Thickness(0, 0, 0, 15)
                    };
                    FieldsPanel.Children.Add(textBox);
                }
                else if (field.Type == FieldType.Dropdown)
                {
                    var comboBox = new System.Windows.Controls.ComboBox
                    {
                        ItemsSource = field.Options,
                        SelectedValue = field.Value,
                        Tag = field,
                        FontSize = 16,
                        Height = 40,
                        Margin = new Thickness(0, 0, 0, 15)
                    };
                    FieldsPanel.Children.Add(comboBox);
                }
            }
        }

        private void GenerateReportButton_Click(object sender, RoutedEventArgs e)
        {
            var reportGenerator = new HtmlReportGenerator();
            reportGenerator.GenerateReport(_formCollection);

            MessageBox.Show("HTML отчёт создан", "Отчёт");
        }
    }
}