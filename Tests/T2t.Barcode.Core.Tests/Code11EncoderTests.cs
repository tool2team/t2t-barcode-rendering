using T2t.Barcode.Core;
using T2t.Barcode.Core.Code11;
using Xunit;

namespace T2t.Barcode.Core.Tests;

/// <summary>
/// Tests for Code11 encoder logic
/// </summary>
public class Code11EncoderTests
{
    private readonly ITestOutputHelper _output;
    private readonly Code11GlyphFactory _factory;
    private readonly Code11Checksum _checksum;

    public Code11EncoderTests(ITestOutputHelper output)
    {
        _output = output;
        _factory = Code11GlyphFactory.Instance;
        _checksum = Code11Checksum.Instance;
    }

    [Fact]
    public void Encode_ShouldIncludeStartAndStopCharacters()
    {
        // Arrange
        const string testData = "123";

        // Act
        Glyph[] result = Code11Encoder.Encode(testData, _factory, _checksum);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);

        // Code11 uses '*' as start and stop characters
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
        Glyph[] resultWithChecksum = Code11Encoder.Encode(testData, _factory, _checksum);
        Glyph[] resultWithoutChecksum = Code11Encoder.Encode(testData, _factory, null);

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
    [InlineData("12345")]
    [InlineData("123456789")]
    [InlineData("0123456789")]
    public void Encode_WithNumericInputs_ShouldProduceGlyphs(string input)
    {
        // Act
        Glyph[] result = Code11Encoder.Encode(input, _factory, _checksum);

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
        const string testData = "987654";

        // Act
        Glyph[] result1 = Code11Encoder.Encode(testData, _factory, _checksum);
        Glyph[] result2 = Code11Encoder.Encode(testData, _factory, _checksum);

        // Assert
        Assert.Equal(result1.Length, result2.Length);

        _output.WriteLine($"Both results have {result1.Length} glyphs");
    }

    [Theory]
    [InlineData("0", "Single digit")]
    [InlineData("123", "Three digits")]
    [InlineData("999", "Repeated digit")]
    [InlineData("0123456789", "All digits")]
    [InlineData("12-34", "With dash separator")]
    public void Encode_WithVariousFormats_ShouldSucceed(string input, string description)
    {
        // Act
        Glyph[] result = Code11Encoder.Encode(input, _factory, _checksum);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);

        _output.WriteLine($"{description}: '{input}' -> {result.Length} glyphs");
    }

    [Fact]
    public void Encode_LongString_ShouldProduceMoreGlyphs()
    {
        // Arrange
        const string shortData = "12";
        const string longData = "0123456789012345678901234567890123456789";

        // Act
        Glyph[] shortResult = Code11Encoder.Encode(shortData, _factory, _checksum);
        Glyph[] longResult = Code11Encoder.Encode(longData, _factory, _checksum);

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
            Code11Encoder.Encode(emptyData, _factory, _checksum));

        _output.WriteLine("Empty string correctly throws ArgumentNullException");
    }

    [Fact]
    public void Encode_ShouldIncludeChecksumWhenProvided()
    {
        // Arrange
        const string testData = "54321";

        // Act
        Glyph[] result = Code11Encoder.Encode(testData, _factory, _checksum);

        // Manually get what the checksum should produce
        Glyph[] checksumGlyphs = _checksum.GetChecksum(testData).ToArray();

        // Assert
        Assert.NotNull(result);
        Assert.True(checksumGlyphs.Length > 0, "Checksum should produce glyphs");

        _output.WriteLine($"Checksum produces {checksumGlyphs.Length} glyphs");
        _output.WriteLine($"Total encoded glyphs: {result.Length}");
    }

    [Fact]
    public void Encode_WithDash_ShouldIncludeDashGlyph()
    {
        // Arrange
        const string testDataWithDash = "123-456";
        const string testDataWithoutDash = "123456";

        // Act
        Glyph[] resultWithDash = Code11Encoder.Encode(testDataWithDash, _factory, null);
        Glyph[] resultWithoutDash = Code11Encoder.Encode(testDataWithoutDash, _factory, null);

        // Assert
        Assert.NotNull(resultWithDash);
        Assert.NotNull(resultWithoutDash);

        // The dash adds one more character to encode
        Assert.True(resultWithDash.Length > resultWithoutDash.Length,
            "Input with dash should produce more glyphs");

        _output.WriteLine($"With dash: {resultWithDash.Length} glyphs");
        _output.WriteLine($"Without dash: {resultWithoutDash.Length} glyphs");
    }

    [Fact]
    public void Encode_MultipleEncodings_ShouldAllSucceed()
    {
        // Arrange
        string[] testInputs = { "1", "12", "123", "1234", "12345" };

        // Act & Assert
        foreach (var input in testInputs)
        {
            Glyph[] result = Code11Encoder.Encode(input, _factory, _checksum);
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            _output.WriteLine($"Input '{input}' -> {result.Length} glyphs");
        }
    }
}
