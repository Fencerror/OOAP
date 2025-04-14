namespace lab3.Iterators
{
    public interface IFormIterator
    {
        bool HasNext();
        object Next();
        object Current(); 
    }
}
