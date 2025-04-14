using System.Windows;
using System.Windows.Controls;
using lab3.Collections;
using lab3.Models;

namespace lab3.Views
{
    public partial class ReportWindow : Window
    {
        public ReportWindow(FormCollection formCollection)
        {
            InitializeComponent();

            // Генерируем отчет
            foreach (var block in formCollection.GetBlocks()) // Используем GetBlocks вместо GetFields
            {
                var blockTitle = new TextBlock
                {
                    Text = block.Title,
                    FontSize = 16,
                    FontWeight = FontWeights.Bold,
                    Margin = new Thickness(0, 10, 0, 5)
                };
                ReportContent.Children.Add(blockTitle);

                foreach (var field in block.Fields)
                {
                    var textBlock = new TextBlock
                    {
                        Text = $"{field.Label}: {field.Value}",
                        FontSize = 14,
                        Margin = new Thickness(0, 5, 0, 5)
                    };
                    ReportContent.Children.Add(textBlock);
                }
            }
        }
    }
}