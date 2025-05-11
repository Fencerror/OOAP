**Лабораторная работа №2: паттерн "Приспособленец"**

# Описание информационной системы

Система предназначена для генерации и визуализации ландшафта, состоящего из различных типов terrain (трава, вода, песок, горы). Основная задача системы — демонстрация применения паттерна проектирования Приспособленец (Flyweight) для оптимизации использования памяти и повышения производительности.

# Диаграмма классов
![Flyweight drawio](https://github.com/user-attachments/assets/0e454b97-a8f3-4707-b36f-07164e41c637)


# Описание диаграммы

**Класс TerrainType**
 - Класс представляет внутреннее состояние (общие данные для всех клеток одного типа).
 - Атрибуты: Texture, Color.
 - Методы: Draw(x, y).

**Класс TerrainFactory**
 - Фабрика для управления объектами TerrainType. Обеспечивает повторное использование объектов.
 - Атрибуты: _terrainTypes.
 - Методы: GetTerrainType(texture, color).

**Класс Terrain**
 - Класс представляет внешнее состояние (уникальные данные для каждой клетки).
 - Атрибуты: _x, _y, _terrainType.
 - Методы: Draw(). 

**Класс Program**
 - Клиентский код, который создаёт карту и отображает её.
 - Методы: Main(args).

# Алгоритм работы с приложением:

1. Пользователь запускает систему.

2. Система генерирует карту 10x10 клеток, случайным образом заполняя её terrain (трава, вода, песок, горы).

3. Система отображает карту в консоли или графическом интерфейсе, показывая тип и координаты каждой клетки.

4. Пользователь может повторить генерацию для создания новой карты.

# Код с паттерном "Абстрактная фабрика"

Паттерн позволяет сэкономить оперативную память и повысить производительность, поскольку разделяет данные на внутреннее состояние (общие данные) и внешнее состояние (уникальные данные).
 - Внутреннее состояние  - текстура и цвет - хранится в объектах TerrainType
 - Внешнее состояние  - координаты - хранится в объектах Terrain

<!-- Файл FlyweightTerrainGenerator.cs -->

```
using System;
using System.Collections.Generic;

namespace FlyweightTerrainGeneratorConsole
{
    class TerrainType
    {
        public string Texture { get; }
        public string Color { get; }
        public TerrainType _terrainType; // Добавлено по диаграмме (хотя это странно)

        public TerrainType(string texture, string color)
        {
            Texture = texture;
            Color = color;
        }

        public void DisplayAt(int x, int y) // Переименовано по диаграмме
        {
            Console.WriteLine($"Drawing {Texture} ({Color}) at ({x}, {y})");
        }
    }

    class TerrainFactory
    {
        private Dictionary<string, TerrainType> _terrainTypes 
            = new Dictionary<string, TerrainType>();

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

    class Terrain
    {
        private int _x; // integer в диаграмме
        private int _y; // integer в диаграмме
        private TerrainType _terrainType;

        public Terrain(int x, int y, TerrainType terrainType)
        {
            _x = x;
            _y = y;
            _terrainType = terrainType;
        }

        public void Render() // Переименовано по диаграмме
        {
            _terrainType.DisplayAt(_x, _y); // Используем новый метод
        }
    }

    class Draw // Новый класс по диаграмме
    {
        public void Draw() // Пустой метод как в диаграмме
        {
            // Реализация не указана в диаграмме
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            TerrainFactory factory = new TerrainFactory();
            Draw drawer = new Draw(); // Создаем экземпляр Draw

            TerrainType grass = factory.GetTerrainType("Grass", "Green");
            TerrainType water = factory.GetTerrainType("Water", "Blue");
            TerrainType sand = factory.GetTerrainType("Sand", "Yellow");
            TerrainType mountain = factory.GetTerrainType("Mountain", "Gray");

            Terrain[,] map = new Terrain[10, 10];
            Random random = new Random();

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
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
                    map[i, j] = new Terrain(i, j, terrainType);
                }
            }

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    map[i, j].Render(); // Используем переименованный метод
                    drawer.Draw(); // Вызываем Draw (хотя его назначение неясно)
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

**Паттерн Приспособленец позволяет:**

 - Экономить память за счёт разделения общих данных между множеством объектов.
 - Повысить производительность за счёт уменьшения количества создаваемых объектов и нагрузки на сборщик мусора.

**Преимущества:**

 - Экономия памяти: общие данные (текстура и цвет) хранятся один раз и используются всеми клетками.
 - Уменьшение количества объектов: вместо 100 объектов (для карты 10x10) создаётся всего 4 объекта (TerrainType для травы, воды, песка и гор)


**Недостатки:**

 - Немного сложнее в реализации из-за необходимости разделения внутреннего и внешнего состояния.
