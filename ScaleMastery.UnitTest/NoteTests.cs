using System;
using NUnit.Framework;

namespace ScaleMastery.UnitTest;

public class NoteTests
{
    [TestCase()]
    public void CanCreateInstanceEachNaturalNote()
    {
        var testCases = new[]
        {
            (NaturalNote.A, "A"), 
            (NaturalNote.B, "B"), 
            (NaturalNote.C, "C"),
            (NaturalNote.D, "D"),
            (NaturalNote.E, "E"),
            (NaturalNote.F, "F"),
            (NaturalNote.G, "G")
        };
        
        foreach (var (note, expected) in testCases)
        {
            Assert.IsInstanceOf<NaturalNote>(note);
            Assert.That(note.DisplayName == expected);
        }
    }

    [Test]
    public void CanGetNextNaturalNote()
    {
        var testCases = new[]
        {
            (NaturalNote.A, NaturalNote.B),
            (NaturalNote.B, NaturalNote.C),
            (NaturalNote.C, NaturalNote.D),
            (NaturalNote.D, NaturalNote.E),
            (NaturalNote.E, NaturalNote.F),
            (NaturalNote.F, NaturalNote.G),
            (NaturalNote.G, NaturalNote.A)
        };
    
        foreach (var (note, expected) in testCases)
        {
            Assert.That(expected == note.NextNatural());
        }
    }
    
    [Test]
    public void CanGetPreviousNaturalNote()
    {
        var testCases = new[]
        {
            (NaturalNote.A, NaturalNote.G),
            (NaturalNote.B, NaturalNote.A),
            (NaturalNote.C, NaturalNote.B),
            (NaturalNote.D, NaturalNote.C),
            (NaturalNote.E, NaturalNote.D),
            (NaturalNote.F, NaturalNote.E),
            (NaturalNote.G, NaturalNote.F)
        };
    
        foreach (var (note, expected) in testCases)
        {
            Assert.That(expected == note.PreviousNatural());
        }
    }
    
    [Test]
    public void ShouldNotMixSharpsAndFlats()
    {
        var note = NaturalNote.A;
        var sharpenedNote = note.Sharpen();
        Assert.Throws<InvalidOperationException>(() => sharpenedNote.Flatten());
        
        var flattenedNote = note.Flatten();
        Assert.Throws<InvalidOperationException>(() => flattenedNote.Sharpen());
    }
}