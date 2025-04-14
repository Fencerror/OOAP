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
        private const int MapSize = 15; // Увеличил размер карты до 15x15
        private const int CellSize = 40; // Размер клетки в пикселях

        public MainWindow()
        {
            InitializeComponent();
            GenerateTerrain();
        }

        private void GenerateTerrain()
        {
            TerrainFactory factory = new TerrainFactory();
            
            // Создаем 4 типа местности
            TerrainType grass = factory.GetTerrainType("Grass", Brushes.Green);
            TerrainType water = factory.GetTerrainType("Water", Brushes.Blue);
            TerrainType sand = factory.GetTerrainType("Sand", Brushes.Yellow);
            TerrainType mountain = factory.GetTerrainType("Mountain", Brushes.Gray);

            Random random = new Random();
            string selectedBiome = ((ComboBoxItem)BiomeComboBox.SelectedItem).Content.ToString();

            for (int i = 0; i < MapSize; i++)
            {
                for (int j = 0; j < MapSize; j++)
                {
                    TerrainType terrainType = GetTerrainForBiome(selectedBiome, random, grass, water, sand, mountain);
                    DrawTerrainCell(i, j, terrainType);
                }
            }
        }

        private TerrainType GetTerrainForBiome(string biome, Random random, 
            TerrainType grass, TerrainType water, TerrainType sand, TerrainType mountain)
        {
            switch (biome)
            {
                case "Forest":
                    if (random.Next(10) < 7)
                        return grass;
                    else
                    {
                        int option = random.Next(3);
                        return option == 0 ? water : option == 1 ? sand : mountain;
                    }
                case "Desert":
                    if (random.Next(10) < 7)
                        return sand;
                    else
                    {
                        int option = random.Next(3);
                        return option == 0 ? water : option == 1 ? grass : mountain;
                    }
                case "Islands":
                    if (random.Next(10) < 7)
                        return water;
                    else
                    {
                        int option = random.Next(3);
                        return option == 0 ? grass : option == 1 ? sand : mountain;
                    }
                case "Mountains":
                    if (random.Next(10) < 7)
                        return mountain;
                    else
                    {
                        int option = random.Next(3);
                        return option == 0 ? grass : option == 1 ? water : sand;
                    }
                default: // Random equally
                    return random.Next(4) switch
                    {
                        0 => grass,
                        1 => water,
                        2 => sand,
                        3 => mountain,
                        _ => grass
                    };
            }
        }

        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            TerrainCanvas.Children.Clear();
            GenerateTerrain();
        }

        private void DrawTerrainCell(int x, int y, TerrainType terrainType) //Принимает координаты x,y- внешнее состояние.
        {
            Rectangle rect = new Rectangle
            {
                Width = CellSize,
                Height = CellSize,
                Fill = terrainType.Color,
                Stroke = Brushes.Black,
                StrokeThickness = 0.5
            };

            Canvas.SetLeft(rect, x * CellSize);
            Canvas.SetTop(rect, y * CellSize);

            TerrainCanvas.Children.Add(rect);
        }
    }

    class TerrainType //Внутреннее состояние.
    {
        public string Texture { get; }
        public Brush Color { get; }

        public TerrainType(string texture, Brush color)
        {
            Texture = texture;
            Color = color;
        }
    }

    class TerrainFactory //Управляет кэшированием объетов 
    {
        private Dictionary<string, TerrainType> _terrainTypes = new Dictionary<string, TerrainType>();

        public TerrainType GetTerrainType(string texture, Brush color)
        {
            string key = $"{texture}_{color}";
            if (!_terrainTypes.ContainsKey(key)) //Если объект с определённым состоянием ещё не существует
            {
                _terrainTypes[key] = new TerrainType(texture, color);//Тогда создаём новый
            }
            return _terrainTypes[key]; //Иначе возвращаем повторно.
        }
    }
}