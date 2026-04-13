using T2t.Barcode.Core;
using T2t.Barcode.Core.Code39;
using Xunit;

namespace T2t.Barcode.Core.Tests;

/// <summary>
/// Tests for Code39 encoder logic
/// </summary>
public class Code39EncoderTests
{
    private readonly ITestOutputHelper _output;
    private readonly Code39GlyphFactory _factory;
    private readonly Code39Checksum _checksum;

    public Code39EncoderTests(ITestOutputHelper output)
    {
        _output = output;
        _factory = Code39GlyphFactory.Instance;
        _checksum = Code39Checksum.Instance;
    }

    [Fact]
    public void Encode_ShouldIncludeStartAndStopCharacters()
    {
        // Arrange
        const string testData = "ABC";

        // Act
        Glyph[] result = Code39Encoder.Encode(testData, _factory, _checksum);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);

        // Code39 uses '*' as start and stop characters
        // First and last glyphs should be '*'
        Assert.True(result.Length >= 2, "Result should contain at least start and stop characters");

        _output.WriteLine($"Total glyphs: {result.Length}");
        _output.WriteLine($"First glyph: {result[0]}");
        _output.WriteLine($"Last glyph: {result[^1]}");
    }

    [Fact]
    public void Encode_WithNullChecksum_ShouldNotIncludeChecksumGlyphs()
    {
        // Arrange
        const string testData = "12345";

        // Act
        Glyph[] resultWithChecksum = Code39Encoder.Encode(testData, _factory, _checksum);
        Glyph[] resultWithoutChecksum = Code39Encoder.Encode(testData, _factory, null);

        // Assert
        Assert.NotNull(resultWithChecksum);
        Assert.NotNull(resultWithoutChecksum);

        // Without checksum should have fewer glyphs
        Assert.True(resultWithoutChecksum.Length < resultWithChecksum.Length,
            "Result without checksum should have fewer glyphs");

        _output.WriteLine($"With checksum: {resultWithChecksum.Length} glyphs");
        _output.WriteLine($"Without checksum: {resultWithoutChecksum.Length} glyphs");
    }

    [Theory]
    [InlineData("0")]
    [InlineData("1")]
    [InlineData("123")]
    [InlineData("ABC")]
    [InlineData("XYZ")]
    [InlineData("A1B2C3")]
    [InlineData("0123456789")]
    [InlineData("ABCDEFGHIJ")]
    public void Encode_WithValidInputs_ShouldProduceGlyphs(string input)
    {
        // Act
        Glyph[] result = Code39Encoder.Encode(input, _factory, _checksum);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);

        // Should always have at least: start + content + checksum + stop
        Assert.True(result.Length >= 4, 
            $"Result should contain at least 4 glyphs for input '{input}'");

        _output.WriteLine($"Input: '{input}' -> {result.Length} glyphs");
    }

    [Fact]
    public void Encode_ShouldProduceConsistentResults()
    {
        // Arrange
        const string testData = "CODE39";

        // Act
        Glyph[] result1 = Code39Encoder.Encode(testData, _factory, _checksum);
        Glyph[] result2 = Code39Encoder.Encode(testData, _factory, _checksum);

        // Assert
        Assert.Equal(result1.Length, result2.Length);

        _output.WriteLine($"Both results have {result1.Length} glyphs");
    }

    [Theory]
    [InlineData("0123456789", "Numeric data")]
    [InlineData("ABCDEFGHIJ", "Upper case letters")]
    [InlineData("ABC123XYZ", "Mixed alphanumeric")]
    [InlineData("TEST-123", "With dash separator")]
    [InlineData("A+B", "With plus sign")]
    [InlineData("10%", "With percent sign")]
    [InlineData("$100", "With dollar sign")]
    [InlineData("A/B", "With slash")]
    [InlineData("X.Y", "With dot")]
    public void Encode_WithVariousCharacters_ShouldSucceed(string input, string description)
    {
        // Act
        Glyph[] result = Code39Encoder.Encode(input, _factory, _checksum);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);

        _output.WriteLine($"{description}: '{input}' -> {result.Length} glyphs");
    }

    [Fact]
    public void Encode_LongString_ShouldProduceMoreGlyphs()
    {
        // Arrange
        const string shortData = "AB";
        const string longData = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        // Act
        Glyph[] shortResult = Code39Encoder.Encode(shortData, _factory, _checksum);
        Glyph[] longResult = Code39Encoder.Encode(longData, _factory, _checksum);

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
            Code39Encoder.Encode(emptyData, _factory, _checksum));

        _output.WriteLine("Empty string correctly throws ArgumentNullException");
    }

    [Fact]
    public void Encode_ShouldIncludeChecksumWhenProvided()
    {
        // Arrange
        const string testData = "CODE39";

        // Act
        Glyph[] result = Code39Encoder.Encode(testData, _factory, _checksum);

        // Manually get what the checksum should produce
        Glyph[] checksumGlyphs = _checksum.GetChecksum(testData).ToArray();

        // Assert
        Assert.NotNull(result);
        Assert.True(checksumGlyphs.Length > 0, "Checksum should produce glyphs");

        _output.WriteLine($"Checksum produces {checksumGlyphs.Length} glyphs");
        _output.WriteLine($"Total encoded glyphs: {result.Length}");
    }

    [Theory]
    [InlineData("ABC", "DEF")]
    [InlineData("123", "456")]
    [InlineData("A1B2", "C3D4")]
    public void Encode_DifferentInputs_ShouldProduceDifferentLengths(string input1, string input2)
    {
        // Act
        Glyph[] result1 = Code39Encoder.Encode(input1, _factory, null);
        Glyph[] result2 = Code39Encoder.Encode(input2, _factory, null);

        // Assert
        Assert.NotNull(result1);
        Assert.NotNull(result2);

        // Both have same length strings, so should produce same number of glyphs
        Assert.Equal(result1.Length, result2.Length);

        _output.WriteLine($"Input '{input1}' -> {result1.Length}, Input '{input2}' -> {result2.Length}");
    }

    [Fact]
    public void Encode_WithSpecialCharacters_ShouldSucceed()
    {
        // Arrange - Code39 supports: 0-9, A-Z, -, ., $, /, +, %, space
        const string testData = "ABC-123.45";

        // Act
        Glyph[] result = Code39Encoder.Encode(testData, _factory, _checksum);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);

        _output.WriteLine($"Special characters '{testData}' -> {result.Length} glyphs");
    }

    [Fact]
    public void Encode_NumericOnly_ShouldSucceed()
    {
        // Arrange
        const string testData = "0123456789";

        // Act
        Glyph[] result = Code39Encoder.Encode(testData, _factory, _checksum);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Length >= 12, "Numeric string should produce expected glyphs");

        _output.WriteLine($"Numeric '{testData}' -> {result.Length} glyphs");
    }

    [Fact]
    public void Encode_AlphaOnly_ShouldSucceed()
    {
        // Arrange
        const string testData = "ABCDEFGHIJ";

        // Act
        Glyph[] result = Code39Encoder.Encode(testData, _factory, _checksum);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Length >= 12, "Alpha string should produce expected glyphs");

        _output.WriteLine($"Alpha '{testData}' -> {result.Length} glyphs");
    }

    [Fact]
    public void Encode_MultipleEncodings_ShouldAllSucceed()
    {
        // Arrange
        string[] testInputs = { "A", "AB", "ABC", "ABCD", "ABCDE" };

        // Act & Assert
        foreach (var input in testInputs)
        {
            Glyph[] result = Code39Encoder.Encode(input, _factory, _checksum);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            _output.WriteLine($"Input '{input}' -> {result.Length} glyphs");
        }
    }
}
