using NUnit.Framework;

namespace ScaleMastery.UnitTest;

public class CircularSequenceTest
{
    private class GenericItem 
    {
        private bool Equals(GenericItem other)
        {
            return Id == other.Id;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((GenericItem)obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }

        public static bool operator ==(GenericItem? left, GenericItem? right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(GenericItem? left, GenericItem? right)
        {
            return !Equals(left, right);
        }

        public int Id { get; init; }
    }

    [Test]
    public void AllItemsUnique()
    {
        var items = new [] {new GenericItem { Id = 1 } , new GenericItem { Id = 1 }};
        Assert.Catch<ItemMustBeUniqueException>(() =>
        {
            new CircularSequence<GenericItem>(items);
        });
    }
    
    [Test]
    public void CurrentIsNullWhenEmpty()
    {
        var items = System.Array.Empty<GenericItem>();
        var sequence = new CircularSequence<GenericItem>(items);
        Assert.That(sequence.Current == null);
    }
    
    [Test]
    public void CurrentIsFirstItem()
    {
        var items = new [] {new GenericItem { Id = 1 } , new GenericItem { Id = 2 }, new GenericItem { Id = 3 }};
        var sequence = new CircularSequence<GenericItem>(items);
        Assert.That(sequence.Current == items[0]);
    }
    
    [Test]
    public void  MoveNextCurrentIsSecondItem()
    {
        var items = new [] {new GenericItem { Id = 1 } , new GenericItem { Id = 2 }, new GenericItem { Id = 3 }};
        var sequence = new CircularSequence<GenericItem>(items);
        Assert.True(sequence.MoveNext());
        Assert.That(sequence.Current == items[1]);
    }
    
    [Test]
    public void  MovePreviousCurrentIsThirdItem()
    {
        var items = new [] {new GenericItem { Id = 1 } , new GenericItem { Id = 2 }, new GenericItem { Id = 3 }};
        var sequence = new CircularSequence<GenericItem>(items);
        Assert.True(sequence.MovePrevious());
        Assert.That(sequence.Current == items[2]);
    }
    
        
    [Test]
    public void  MoveNextCurrentIsThirdItem()
    {
        var items = new [] {new GenericItem { Id = 1 } , new GenericItem { Id = 2 }, new GenericItem { Id = 3 }};
        var sequence = new CircularSequence<GenericItem>(items);
        Assert.True(sequence.MoveNext());
        Assert.True(sequence.MoveNext());
        Assert.That(sequence.Current == items[2]);
    }
    
            
    [Test]
    public void  MovePreviousCurrentIsSecondItem()
    {
        var items = new [] {new GenericItem { Id = 1 } , new GenericItem { Id = 2 }, new GenericItem { Id = 3 }};
        var sequence = new CircularSequence<GenericItem>(items);
        Assert.True(sequence.MovePrevious());
        Assert.True(sequence.MovePrevious());
        Assert.That(sequence.Current == items[1]);
    }
    
    [Test]
    public void  MoveNextCurrentIsFirstItemAgain()
    {
        var items = new [] {new GenericItem { Id = 1 } , new GenericItem { Id = 2 }, new GenericItem { Id = 3 }};
        var sequence = new CircularSequence<GenericItem>(items);
        Assert.True(sequence.MoveNext());
        Assert.True(sequence.MoveNext());
        Assert.True(sequence.MoveNext());
        Assert.That(sequence.Current == items[0]);
    }
    
        
    [Test]
    public void  MovePreviousCurrentIsFirstItemAgain()
    {
        var items = new [] {new GenericItem { Id = 1 } , new GenericItem { Id = 2 }, new GenericItem { Id = 3 }};
        var sequence = new CircularSequence<GenericItem>(items);
        Assert.True(sequence.MovePrevious());
        Assert.True(sequence.MovePrevious());
        Assert.True(sequence.MovePrevious());
        Assert.That(sequence.Current == items[0]);
    }
    
    [Test]
    public void MoveNextWhenEmpty()
    {
        var items = System.Array.Empty<GenericItem>();
        var sequence = new CircularSequence<GenericItem>(items);
        
        Assert.False(sequence.MoveNext());
    }
    
        
    [Test]
    public void MovePreviousWhenEmpty()
    {
        var items = System.Array.Empty<GenericItem>();
        var sequence = new CircularSequence<GenericItem>(items);
        
        Assert.False(sequence.MovePrevious());
    }

    [Test]
    public void SetCurrent()
    {        
        var items = new [] {new GenericItem { Id = 1 } , new GenericItem { Id = 2 }, new GenericItem { Id = 3 }};
        var sequence = new CircularSequence<GenericItem>(items);
        sequence.SetCurrent(items[1]);
        Assert.That(sequence.Current == items[1]);
        Assert.False(sequence.SetCurrent(new GenericItem{ Id = 5}));
    }

    [Test]
    public void NextFrom()
    {
        var items = new[] { new GenericItem { Id = 1 }, new GenericItem { Id = 2 }, new GenericItem { Id = 3 } };
        var sequence = new CircularSequence<GenericItem>(items);
        Assert.That(sequence.NextFrom(items[0]) == items[1]);
        Assert.That(sequence.Current == items[1]);
        Assert.That(sequence.NextFrom(items[1]) == items[2]);
        Assert.That(sequence.Current == items[2]);
        Assert.That(sequence.NextFrom(items[2]) == items[0]);
        Assert.That(sequence.Current == items[0]);
    }
    
    [Test]
    public void PreviousFrom()
    {
        var items = new [] {new GenericItem { Id = 1 } , new GenericItem { Id = 2 }, new GenericItem { Id = 3 }};
        var sequence = new CircularSequence<GenericItem>(items);
        
        Assert.That(sequence.PreviousFrom(items[0]) == items[2]);
        Assert.That(sequence.Current == items[2]);
        
        Assert.That(sequence.PreviousFrom(items[1]) == items[0]);
        Assert.That(sequence.Current == items[0]);
        
        Assert.That(sequence.PreviousFrom(items[2]) == items[1]);
        Assert.That(sequence.Current == items[1]);
    }
}