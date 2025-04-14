using System.Collections.Generic;
using lab3.Models;
using lab3.Iterators;

namespace lab3.Collections
{
    public class FormIterator : IFormIterator
    {
        private readonly List<FormField> _fields;
        private int _position = 0;

        public FormIterator(List<FormField> fields)
        {
            _fields = fields;
        }

        public bool HasNext()
        {
            return _position < _fields.Count;
        }

        public object Next()
        {
            return _fields[_position++];
        }

        public object Current()
        {
            return _fields[_position - 1];
        }
    }
}
