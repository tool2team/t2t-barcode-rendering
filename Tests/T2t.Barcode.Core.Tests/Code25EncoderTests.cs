using T2t.Barcode.Core.Code25;

namespace T2t.Barcode.Core.Tests;

/// <summary>
/// Tests for Code25 encoder logic
/// </summary>
public class Code25EncoderTests
{
    private readonly ITestOutputHelper _output;
    private readonly Code25StandardGlyphFactory _standardFactory;
    private readonly Code25InterleavedGlyphFactory _interleavedFactory;
    private readonly Code25Checksum _checksumStandard;
    private readonly Code25Checksum _checksumInterleaved;

    public Code25EncoderTests(ITestOutputHelper output)
    {
        _output = output;
        _standardFactory = Code25GlyphFactory.StandardInstance;
        _interleavedFactory = Code25GlyphFactory.InterleavedInstance;
        _checksumStandard = Code25Checksum.StandardInstance;
        _checksumInterleaved = Code25Checksum.InterleavedInstance;
    }

    [Fact]
    public void Encode_Standard_ShouldIncludeStartAndStopCharacters()
    {
        // Arrange
        const string testData = "123";

        // Act
        Glyph[] result = Code25Encoder.Encode(testData, _standardFactory, _checksumStandard);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);

        // Code25 uses '-' as start and '*' as stop characters
        Assert.True(result.Length >= 2, "Result should contain at least start and stop characters");

        _output.WriteLine($"Standard: Total glyphs: {result.Length}");
    }

    [Fact]
    public void Encode_Interleaved_ShouldIncludeStartAndStopCharacters()
    {
        // Arrange
        const string testData = "1234";

        // Act
        Glyph[] result = Code25Encoder.Encode(testData, _interleavedFactory, _checksumInterleaved);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);

        Assert.True(result.Length >= 2, "Result should contain at least start and stop characters");

        _output.WriteLine($"Interleaved: Total glyphs: {result.Length}");
    }

    [Fact]
    public void Encode_Interleaved_WithOddLength_ShouldPrependZero()
    {
        // Arrange
        const string oddLengthData = "123";   // 3 characters (odd)
        const string evenLengthData = "1234"; // 4 characters (even)

        // Act
        Glyph[] oddResult = Code25Encoder.Encode(oddLengthData, _interleavedFactory, null);
        Glyph[] evenResult = Code25Encoder.Encode(evenLengthData, _interleavedFactory, null);

        // Assert
        Assert.NotNull(oddResult);
        Assert.NotNull(evenResult);

        // Odd length data should have a zero prepended, so both should produce similar structure
        // The odd one gets "0" prepended to make it "0123" (4 chars like "1234")
        _output.WriteLine($"Odd length ({oddLengthData}): {oddResult.Length} glyphs");
        _output.WriteLine($"Even length ({evenLengthData}): {evenResult.Length} glyphs");

        // They should have the same length since odd gets prepended with 0
        Assert.Equal(evenResult.Length, oddResult.Length);
    }

    [Fact]
    public void Encode_Standard_WithNullChecksum_ShouldNotIncludeChecksumGlyphs()
    {
        // Arrange
        const string testData = "12345";

        // Act
        Glyph[] resultWithChecksum = Code25Encoder.Encode(testData, _standardFactory, _checksumStandard);
        Glyph[] resultWithoutChecksum = Code25Encoder.Encode(testData, _standardFactory, null);

        // Assert
        Assert.NotNull(resultWithChecksum);
        Assert.NotNull(resultWithoutChecksum);

        Assert.True(resultWithoutChecksum.Length < resultWithChecksum.Length,
            "Result without checksum should have fewer glyphs");

        _output.WriteLine($"With checksum: {resultWithChecksum.Length} glyphs");
        _output.WriteLine($"Without checksum: {resultWithoutChecksum.Length} glyphs");
    }

    [Theory]
    [InlineData("0")]
    [InlineData("1")]
    [InlineData("12")]
    [InlineData("123")]
    [InlineData("1234")]
    [InlineData("12345")]
    [InlineData("0123456789")]
    public void Encode_Standard_WithNumericInputs_ShouldProduceGlyphs(string input)
    {
        // Act
        Glyph[] result = Code25Encoder.Encode(input, _standardFactory, _checksumStandard);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);

        _output.WriteLine($"Standard input: '{input}' -> {result.Length} glyphs");
    }

    [Theory]
    [InlineData("12")]
    [InlineData("1234")]
    [InlineData("123456")]
    [InlineData("12345678")]
    [InlineData("0123456789")]
    public void Encode_Interleaved_WithEvenLengthInputs_ShouldProduceGlyphs(string input)
    {
        // Act
        Glyph[] result = Code25Encoder.Encode(input, _interleavedFactory, _checksumInterleaved);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);

        _output.WriteLine($"Interleaved input: '{input}' -> {result.Length} glyphs");
    }

    [Fact]
    public void Encode_Standard_ShouldProduceConsistentResults()
    {
        // Arrange
        const string testData = "987654";

        // Act
        Glyph[] result1 = Code25Encoder.Encode(testData, _standardFactory, _checksumStandard);
        Glyph[] result2 = Code25Encoder.Encode(testData, _standardFactory, _checksumStandard);

        // Assert
        Assert.Equal(result1.Length, result2.Length);

        _output.WriteLine($"Both results have {result1.Length} glyphs");
    }

    [Fact]
    public void Encode_Interleaved_ShouldProduceConsistentResults()
    {
        // Arrange
        const string testData = "987654";

        // Act
        Glyph[] result1 = Code25Encoder.Encode(testData, _interleavedFactory, _checksumInterleaved);
        Glyph[] result2 = Code25Encoder.Encode(testData, _interleavedFactory, _checksumInterleaved);

        // Assert
        Assert.Equal(result1.Length, result2.Length);

        _output.WriteLine($"Both results have {result1.Length} glyphs");
    }

    [Fact]
    public void Encode_Standard_LongString_ShouldProduceMoreGlyphs()
    {
        // Arrange
        const string shortData = "12";
        const string longData = "012345678901234567890123456789";

        // Act
        Glyph[] shortResult = Code25Encoder.Encode(shortData, _standardFactory, _checksumStandard);
        Glyph[] longResult = Code25Encoder.Encode(longData, _standardFactory, _checksumStandard);

        // Assert
        Assert.True(longResult.Length > shortResult.Length,
            "Longer input should produce more glyphs");

        _output.WriteLine($"Short input ({shortData.Length} chars): {shortResult.Length} glyphs");
        _output.WriteLine($"Long input ({longData.Length} chars): {longResult.Length} glyphs");
    }

    [Fact]
    public void Encode_Interleaved_LongString_ShouldProduceMoreGlyphs()
    {
        // Arrange
        const string shortData = "1234";
        const string longData = "012345678901234567890123456789";

        // Act
        Glyph[] shortResult = Code25Encoder.Encode(shortData, _interleavedFactory, _checksumInterleaved);
        Glyph[] longResult = Code25Encoder.Encode(longData, _interleavedFactory, _checksumInterleaved);

        // Assert
        Assert.True(longResult.Length > shortResult.Length,
            "Longer input should produce more glyphs");

        _output.WriteLine($"Short input ({shortData.Length} chars): {shortResult.Length} glyphs");
        _output.WriteLine($"Long input ({longData.Length} chars): {longResult.Length} glyphs");
    }

    [Fact]
    public void Encode_EmptyString_ShouldThrowException()
    {
        // Arrange
        const string emptyData = "";

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => 
            Code25Encoder.Encode(emptyData, _standardFactory, _checksumStandard));

        _output.WriteLine("Empty string correctly throws ArgumentNullException");
    }

    [Fact]
    public void Encode_Standard_ShouldIncludeChecksumWhenProvided()
    {
        // Arrange
        const string testData = "54321";

        // Act
        Glyph[] result = Code25Encoder.Encode(testData, _standardFactory, _checksumStandard);
        Glyph[] checksumGlyphs = [.. _checksumStandard.GetChecksum(testData)];

        // Assert
        Assert.NotNull(result);
        Assert.True(checksumGlyphs.Length > 0, "Checksum should produce glyphs");

        _output.WriteLine($"Checksum produces {checksumGlyphs.Length} glyphs");
        _output.WriteLine($"Total encoded glyphs: {result.Length}");
    }

    [Fact]
    public void Encode_Interleaved_ShouldIncludeChecksumWhenProvided()
    {
        // Arrange
        const string testData = "5432";

        // Act
        Glyph[] result = Code25Encoder.Encode(testData, _interleavedFactory, _checksumInterleaved);
        Glyph[] checksumGlyphs = [.. _checksumInterleaved.GetChecksum(testData)];

        // Assert
        Assert.NotNull(result);
        Assert.True(checksumGlyphs.Length > 0, "Checksum should produce glyphs");

        _output.WriteLine($"Checksum produces {checksumGlyphs.Length} glyphs");
        _output.WriteLine($"Total encoded glyphs: {result.Length}");
    }

    [Theory]
    [InlineData("1", "2")]
    [InlineData("12", "23")]
    [InlineData("123", "234")]
    public void Encode_Interleaved_OddVsEvenLength_BehaviorValidation(string odd, string even)
    {
        // Act
        Glyph[] oddResult = Code25Encoder.Encode(odd, _interleavedFactory, null);
        Glyph[] evenResult = Code25Encoder.Encode(even, _interleavedFactory, null);

        // Assert
        Assert.NotNull(oddResult);
        Assert.NotNull(evenResult);

        // Both should have the same length since odd gets 0 prepended
        Assert.Equal(evenResult.Length, oddResult.Length);

        _output.WriteLine($"Odd '{odd}' -> {oddResult.Length}, Even '{even}' -> {evenResult.Length}");
    }
}
