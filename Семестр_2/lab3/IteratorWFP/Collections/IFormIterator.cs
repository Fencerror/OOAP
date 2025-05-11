namespace lab3.Iterators
{
    public interface IFormIterator
    {
        bool HasNext();
        object Next();
        object Current();
        // Добавлены методы для перехода назад:
        bool HasPrevious();
        object Previous();
    }
}
