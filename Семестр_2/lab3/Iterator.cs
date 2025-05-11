using System;
using System.Collections.Generic;

namespace IteratorPatternDemo
{
    // Iterator: интерфейс для обхода элементов
    public interface IIterator
    {
        bool HasNext();
        object Next();
        object Current();
    }

    // FormCollection: интерфейс для создания итератора
    public interface IFormCollection
    {
        IIterator CreateIterator();
    }

    // ConcreteIterator: реализация итератора
    public class FormIterator : IIterator
    {
        private readonly List<FormBlock> _blocks;
        private int _position = 0;

        public FormIterator(List<FormBlock> blocks)
        {
            _blocks = blocks;
        }

        public bool HasNext()
        {
            return _position < _blocks.Count;
        }

        public object Next()
        {
            return _blocks[_position++];
        }

        public object Current()
        {
            return _blocks[_position - 1];
        }
    }

    // ConcreteFormCollection: реализация FormCollection
    public class FormCollection : IFormCollection
    {
        private readonly List<FormBlock> _blocks = new List<FormBlock>();

        public void AddBlock(FormBlock block)
        {
            _blocks.Add(block);
        }

        public IIterator CreateIterator()
        {
            return new FormIterator(_blocks);
        }
    }

    // FormBlock: блок формы
    public class FormBlock
    {
        public string Title { get; }
        public List<FormField> Fields { get; }

        public FormBlock(string title)
        {
            Title = title;
            Fields = new List<FormField>();
        }

        public void AddField(FormField field)
        {
            Fields.Add(field);
        }
    }

    // FormField: поле формы
    public class FormField
    {
        public string Label { get; }
        public string Value { get; set; }

        public FormField(string label)
        {
            Label = label;
            Value = string.Empty;
        }
    }

    // Client: демонстрация работы
    class Program
    {
        static void Main(string[] args)
        {
            // Создаем коллекцию блоков формы
            var formCollection = new FormCollection();

            // Блок 1: Персональная информация
            var personalInfoBlock = new FormBlock("Персональная информация");
            personalInfoBlock.AddField(new FormField("Имя"));
            personalInfoBlock.AddField(new FormField("Фамилия"));
            personalInfoBlock.AddField(new FormField("Возраст"));
            formCollection.AddBlock(personalInfoBlock);

            // Блок 2: Профессиональные данные
            var professionalInfoBlock = new FormBlock("Профессиональные данные");
            professionalInfoBlock.AddField(new FormField("Университет"));
            professionalInfoBlock.AddField(new FormField("Специальность"));
            formCollection.AddBlock(professionalInfoBlock);

            // Итерация по блокам формы
            var iterator = formCollection.CreateIterator();
            while (iterator.HasNext())
            {
                var block = (FormBlock)iterator.Next();
                Console.WriteLine($"Блок: {block.Title}");
                foreach (var field in block.Fields)
                {
                    Console.WriteLine($"  Поле: {field.Label}, Значение: {field.Value}");
                }
            }
        }
    }
}