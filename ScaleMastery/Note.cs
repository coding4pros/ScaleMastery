namespace ScaleMastery;

public abstract class Note
{
    private const char SharpChar = '\u266F';
    private const char FlatChar = '\u266D';
    public abstract string DisplayName { get; }
    
    public Note Sharpen()
    {
        if (this is Flat)
            throw new InvalidOperationException();
        
        return new Sharp(this);
    }

    public Note Flatten()
    {
        if (this is Sharp)
            throw new InvalidOperationException();
        
        return new Flat(this);
    }


    private abstract class Accidental : Note
    {
        private readonly Note _note;
    
        protected Accidental(Note note)
        {
            _note = note;
        }
    
        protected abstract char AccidentalChar { get; }
    
        public override string DisplayName => _note.DisplayName + AccidentalChar;
    }

    private class Sharp : Accidental
    {
        protected override char AccidentalChar => SharpChar;


        public Sharp(Note note) : base(note)
        {
        }
    }

    private class Flat : Accidental
    {
        protected override char AccidentalChar => FlatChar;

        public Flat(Note note) : base(note)
        {
        }
    }
}

public class NaturalNote : Note
{
    private readonly CircularSequence<NaturalNote> _naturalNotes;
    public static readonly NaturalNote A = new("A");
    public static readonly NaturalNote B = new("B");
    public static readonly NaturalNote C = new("C");
    public static readonly NaturalNote D = new("D");
    public static readonly NaturalNote E = new("E");
    public static readonly NaturalNote F = new("F");
    public static readonly NaturalNote G = new("G");

    public override string DisplayName { get; }

    private NaturalNote(string displayName)
    {
        _naturalNotes = new CircularSequence<NaturalNote>(new[] { A, B, C, D, E, F, G });
        DisplayName = displayName;
    }

    public NaturalNote NextNatural()
    {
        return _naturalNotes.NextFrom(this);
    }

    public NaturalNote PreviousNatural()
    {
        return _naturalNotes.PreviousFrom(this);
    }
}



