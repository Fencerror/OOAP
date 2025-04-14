using System;
using System.Collections.Generic;

namespace IteratorPatternDemo
{
    // Iterator: определяет интерфейс для обхода составных объектов
    public interface IIterator
    {
        bool HasNext();
        object Next();
    }

    // Aggregate: определяет интерфейс для создания объекта-итератора
    public interface IAggregate
    {
        IIterator CreateIterator();
    }

    // ConcreteIterator: конкретная реализация итератора
    public class ConcreteIterator : IIterator
    {
        private readonly List<string> _collection;
        private int _current = 0;

        public ConcreteIterator(List<string> collection)
        {
            _collection = collection;
        }

        public bool HasNext()
        {
            return _current < _collection.Count;
        }

        public object Next()
        {
            return _collection[_current++];
        }
    }

    // ConcreteAggregate: конкретная реализация Aggregate
    public class ConcreteAggregate : IAggregate
    {
        private readonly List<string> _items = new List<string>();

        public void AddItem(string item)
        {
            _items.Add(item);
        }

        public IIterator CreateIterator()
        {
            return new ConcreteIterator(_items);
        }
    }

    // Client: использует объект Aggregate и итератор для его обхода
    class Program
    {
        static void Main(string[] args)
        {
            // Создаем коллекцию
            var collection = new ConcreteAggregate();
            collection.AddItem("Item 1");
            collection.AddItem("Item 2");
            collection.AddItem("Item 3");

            // Получаем итератор
            var iterator = collection.CreateIterator();

            // Обходим коллекцию
            while (iterator.HasNext())
            {
                Console.WriteLine(iterator.Next());
            }
        }
    }
}