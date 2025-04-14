using System.Collections.Generic;
using lab3.Models;
using lab3.Iterators;

namespace lab3.Collections
{
    public class FormCollection
    {
        private List<FormBlock> _blocks = new List<FormBlock>();

        public void AddBlock(FormBlock block)
        {
            _blocks.Add(block);
        }

        public IFormIterator GetIterator()
        {
            return new FormIterator(_blocks);
        }

        public List<FormBlock> GetBlocks()
        {
            return _blocks;
        }

        private class FormIterator : IFormIterator
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
    }
}
