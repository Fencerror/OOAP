**Лабораторная работа №2: паттерн "Приспособленец"**

# Описание информационной системы


# Диаграмма классов



# Описание диаграммы


# Алгоритм работы с приложением:



# Код с паттерном "Абстрактная фабрика"

<!-- Файл FlyweightTerrainGenerator.cs -->

```
using System;
using System.Collections.Generic;

namespace FlyweightTerrainGeneratorConsole
{
    // Внутреннее состояние
    class TerrainType
    {
        public string Texture { get; }
        public string Color { get; }

        public TerrainType(string texture, string color)
        {
            Texture = texture;
            Color = color;
        }

        public void Draw(int x, int y)
        {
            Console.WriteLine($"Drawing {Texture} ({Color}) at ({x}, {y})");
        }
    }

    // Фабрика для управления TerrainType
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

    // Внешнее состояние
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

        public void Draw()
        {
            _terrainType.Draw(_x, _y);
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
```

# Код без паттерна

Код делает то же самое, но без использования паттерна Приспособленец. Каждая клетка хранит все свои данные, что приводит к избыточному использованию памяти.

```
using System;

namespace TerrainGeneratorWithoutFlyweight
{
    // Класс для клетки карты
    class Terrain
    {
        public int X { get; }
        public int Y { get; }
        public string Texture { get; }
        public string Color { get; }

        public Terrain(int x, int y, string texture, string color)
        {
            X = x;
            Y = y;
            Texture = texture;
            Color = color;
        }

        public void Draw()
        {
            Console.WriteLine($"Drawing {Texture} ({Color}) at ({X}, {Y})");
        }
    }

    // Клиентский код
    class Program
    {
        static void Main(string[] args)
        {
            Terrain[,] map = new Terrain[10, 10];
            Random random = new Random();

            // Заполняем карту случайными terrain
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    int terrainIndex = random.Next(4); // Случайный выбор terrain
                    string texture, color;

                    switch (terrainIndex)
                    {
                        case 0:
                            texture = "Grass";
                            color = "Green";
                            break;
                        case 1:
                            texture = "Water";
                            color = "Blue";
                            break;
                        case 2:
                            texture = "Sand";
                            color = "Yellow";
                            break;
                        case 3:
                            texture = "Mountain";
                            color = "Gray";
                            break;
                        default:
                            texture = "Grass";
                            color = "Green";
                            break;
                    }

                    map[i, j] = new Terrain(i, j, texture, color);
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
```
# Вывод



**Преимущества:**

 - Экономия памяти: общие данные (текстура и цвет) хранятся один раз и используются всеми клетками.
 - Уменьшение количества объектов: вместо 100 объектов (для карты 10x10) создаётся всего 4 объекта (TerrainType для травы, воды, песка и гор)


**Недостатки:**

 - Немного сложнее в реализации из-за необходимости разделения внутреннего и внешнего состояния.