namespace ScaleMastery;

public class CircularSequence<T>
{
    private readonly IList<T> _items;
    private readonly int _length;

    private int _position = 0;
    public CircularSequence(IReadOnlyCollection<T> items)
    {
        if (AreDistinct(items))
            throw new ItemMustBeUniqueException();
        
        _items = items.ToList();
        _length = items.Count;
    }

    public T? Current 
        => _length == 0 ? default(T) : _items[_position];

    private static bool AreDistinct(IReadOnlyCollection<T> items)
    {
        return items.Count != items.Distinct().Count();
    }

    public bool MoveNext()
    {
        if (!_items.Any()) return false;
        _position = _position == _length - 1 ? 0 : _position + 1;

        return true;
    }

    public bool MovePrevious()
    {
        if (!_items.Any()) return false;
        _position = _position == 0 ? _length - 1 : _position - 1;
        return true;
    }

    public bool SetCurrent(T item)
    {
        var itemPosition = _items.IndexOf(item);
        if (WasFound(itemPosition))
            _position = itemPosition;
        
        return WasFound(itemPosition );
    }

    private static bool WasFound(int indexOf)
    {
        return indexOf != -1;
    }
    
    

    public T NextFrom(T item)
    {
        if(!SetCurrent(item))
            throw new NullReferenceException();
        
        MoveNext();
        
        if(Current is null)
            throw new CorruptedInternalStateException();
        
        return Current;
    }
    
    public T PreviousFrom(T item)
    {
        if(!SetCurrent(item))
            throw new NullReferenceException();
        
        MovePrevious();
        
        if(Current is null)
            throw new CorruptedInternalStateException();
        
        return Current;
    }
}
public class ItemMustBeUniqueException : Exception
{
}

public class CorruptedInternalStateException : Exception
{
}