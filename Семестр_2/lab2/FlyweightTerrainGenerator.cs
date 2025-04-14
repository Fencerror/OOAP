using System;
using System.Collections.Generic;

namespace FlyweightTerrainGeneratorConsole
{
    // Интерфейс Flyweight
    interface Flyweight
    {
        string GetColor(); // Метод для получения внутреннего состояния
        void Draw(int x, int y); // Метод для работы с внешним состоянием
    }


    class TerrainType : Flyweight
    {
        private string Texture { get; }
        private string Color { get; }

        public TerrainType(string texture, string color)
        {
            Texture = texture;
            Color = color;
        }

        public string GetColor()
        {
            return Color;
        }

    }

    // Фабрика для управления клетками
    class TerrainFactory
    {
        private Dictionary<string, TerrainType> _terrainTypes = new Dictionary<string, TerrainType>();

        public TerrainType GetTerrainType(string texture, string color)
        {
            string key = $"{texture}_{color}";
            if (!_terrainTypes.ContainsKey(key))
            {
                _terrainTypes[key] = new TerrainType(texture, color);
            }
            return _terrainTypes[key];
        }
    }
    
    // Внешнее состояние - у каждого элемента индивидуальное
    class Terrain
    {
        private int _x, _y;
        private TerrainType _terrainType;

        public Terrain(int x, int y, TerrainType terrainType)
        {
            _x = x;
            _y = y;
            _terrainType = terrainType;
        }
    }

    // Клиентский код
    class Program
    {
        static void Main(string[] args)
        {
            TerrainFactory factory = new TerrainFactory();

            // Создаем типы terrain
            TerrainType grass = factory.GetTerrainType("Grass", "Green");
            TerrainType water = factory.GetTerrainType("Water", "Blue");
            TerrainType sand = factory.GetTerrainType("Sand", "Yellow");
            TerrainType mountain = factory.GetTerrainType("Mountain", "Gray");

            Terrain[,] map = new Terrain[10, 10];
            Random random = new Random();

            // Заполняем карту случайными terrain
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    int terrainIndex = random.Next(4); // Случайный выбор terrain
                    TerrainType terrainType = terrainIndex switch
                    {
                        0 => grass,
                        1 => water,
                        2 => sand,
                        3 => mountain,
                        _ => grass // По умолчанию трава
                    };
                    map[i, j] = new Terrain(i, j, terrainType);
                }
            }

            // Отображаем карту
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    map[i, j].Draw();
                }
            }
        }
    }
}