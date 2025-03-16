using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FlyweightTerrainGeneratorWPF
{
    public partial class MainWindow : Window
    {
        private const int MapSize = 10; // Размер карты (10x10)
        private const int CellSize = 40; // Размер клетки в пикселях

        public MainWindow()
        {
            InitializeComponent();
            GenerateTerrain();
        }

        private void GenerateTerrain()
        {
            TerrainFactory factory = new TerrainFactory();
            TerrainType grass = factory.GetTerrainType("Grass", Brushes.Green);
            TerrainType water = factory.GetTerrainType("Water", Brushes.Blue);
            TerrainType sand = factory.GetTerrainType("Sand", Brushes.Yellow);
            TerrainType mountain = factory.GetTerrainType("Mountain", Brushes.Gray);
            Random random = new Random();
            
            for (int i = 0; i < MapSize; i++)
            {
                for (int j = 0; j < MapSize; j++)
                {
                    int terrainIndex = random.Next(4);
                    TerrainType terrainType = terrainIndex switch
                    {
                        0 => grass,
                        1 => water,
                        2 => sand,
                        3 => mountain,
                        _ => grass
                    };
                    DrawTerrainCell(i, j, terrainType);
                }
            }
        }

        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            TerrainCanvas.Children.Clear(); // Очищаем Canvas
            GenerateTerrain(); // Генерируем новую карту
            }

        private void DrawTerrainCell(int x, int y, TerrainType terrainType)
        {
            Rectangle rect = new Rectangle
            {
                Width = CellSize,
                Height = CellSize,
                Fill = terrainType.Color,
                Stroke = Brushes.Black,
                StrokeThickness = 1
            };

            Canvas.SetLeft(rect, x * CellSize);
            Canvas.SetTop(rect, y * CellSize);

            TerrainCanvas.Children.Add(rect);
        }
    }

    // Внутреннее состояние
    class TerrainType
    {
        public string Texture { get; }
        public Brush Color { get; }

        public TerrainType(string texture, Brush color)
        {
            Texture = texture;
            Color = color;
        }
    }

    // Фабрика для управления TerrainType
    class TerrainFactory
    {
        private Dictionary<string, TerrainType> _terrainTypes = new Dictionary<string, TerrainType>();

        public TerrainType GetTerrainType(string texture, Brush color)
        {
            string key = $"{texture}_{color}";
            if (!_terrainTypes.ContainsKey(key))
            {
                _terrainTypes[key] = new TerrainType(texture, color);
            }
            return _terrainTypes[key];
        }
    }
}