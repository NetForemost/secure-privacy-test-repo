namespace BinaryStringAnalyzerTests;

public class BinaryStringAnalyzerTests
{
    [Test]
    public void TestGoodBinaryString()
    {
        Assert.That(BinaryStringAnalyzer.IsGoodBinaryString("1100"), Is.True);
        Assert.That(BinaryStringAnalyzer.IsGoodBinaryString("1010"), Is.True);
        Assert.That(BinaryStringAnalyzer.IsGoodBinaryString("1001"), Is.False);
        Assert.That(BinaryStringAnalyzer.IsGoodBinaryString("110"), Is.False);
    }

    [Test]
    public void TestEmptyString()
    {
        Assert.That(BinaryStringAnalyzer.IsGoodBinaryString(""), Is.True);
    }

    [Test]
    public void TestInvalidCharacters()
    {
        Assert.Throws<ArgumentException>(() => BinaryStringAnalyzer.IsGoodBinaryString("10a0"));
    }
}

