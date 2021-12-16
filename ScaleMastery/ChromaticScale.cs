namespace ScaleMastery;

public class ChromaticScale : CircularSequence<Note>
{
    private static readonly Note A       = NaturalNote.A;
    private static readonly Note ASharp  = NaturalNote.A.Sharpen();
    private static readonly Note B       = NaturalNote.B;
    private static readonly Note C       = NaturalNote.C;
    private static readonly Note CSharp  = NaturalNote.C.Sharpen();
    private static readonly Note D       = NaturalNote.D;
    private static readonly Note DSharp  = NaturalNote.D.Sharpen();
    private static readonly Note E       = NaturalNote.E;
    private static readonly Note F       = NaturalNote.F;
    private static readonly Note FSharp  = NaturalNote.F.Sharpen();
    private static readonly Note G       = NaturalNote.G;
    private static readonly Note GSharp  = NaturalNote.G.Sharpen();
    
    public ChromaticScale() 
        : base(new[] { A, ASharp, B, C, CSharp, D, DSharp, E, F, FSharp, G, GSharp })
    {
    }

}