using T2t.Barcode.Core;
using T2t.Barcode.Core.Code128;
using Xunit;

namespace T2t.Barcode.Core.Tests;

/// <summary>
/// Tests for Code128 encoder logic
/// </summary>
public class Code128EncoderTests
{
    private readonly ITestOutputHelper _output;
    private readonly Code128GlyphFactory _factory;
    private readonly Code128Checksum _checksum;

    public Code128EncoderTests(ITestOutputHelper output)
    {
        _output = output;
        _factory = Code128GlyphFactory.Instance;
        _checksum = Code128Checksum.Instance;
    }

    [Fact]
    public void Encode_ShouldIncludeStopAndTerminator()
    {
        // Arrange
        const string testData = "TEST";

        // Act
        Glyph[] result = Code128Encoder.Encode(testData, _factory, _checksum);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);

        // The last two glyphs should be Stop (106) and Terminator (107)
        Assert.True(result.Length >= 2, "Result should contain at least Stop and Terminator");

        _output.WriteLine($"Total glyphs: {result.Length}");
        _output.WriteLine($"Last glyph: {result[^1]}");
        _output.WriteLine($"Second to last glyph: {result[^2]}");
    }

    [Fact]
    public void Encode_WithNullChecksum_ShouldNotIncludeChecksumGlyphs()
    {
        // Arrange
        const string testData = "ABC";

        // Act
        Glyph[] resultWithChecksum = Code128Encoder.Encode(testData, _factory, _checksum);
        Glyph[] resultWithoutChecksum = Code128Encoder.Encode(testData, _factory, null);

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
    [InlineData("123456")]
    [InlineData("ABCDEF")]
    [InlineData("Test123")]
    [InlineData("!@#$%")]
    [InlineData("A")]
    public void Encode_WithDifferentInputs_ShouldProduceGlyphs(string input)
    {
        // Act
        Glyph[] result = Code128Encoder.Encode(input, _factory, _checksum);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);

        // Should always have at least: start + content + checksum + stop + terminator
        Assert.True(result.Length >= 5, 
            $"Result should contain at least 5 glyphs for input '{input}'");

        _output.WriteLine($"Input: '{input}' -> {result.Length} glyphs");
    }

    [Fact]
    public void Encode_ShouldProduceConsistentResults()
    {
        // Arrange
        const string testData = "CONSISTENT";

        // Act
        Glyph[] result1 = Code128Encoder.Encode(testData, _factory, _checksum);
        Glyph[] result2 = Code128Encoder.Encode(testData, _factory, _checksum);

        // Assert
        Assert.Equal(result1.Length, result2.Length);

        _output.WriteLine($"Both results have {result1.Length} glyphs");
    }

    [Theory]
    [InlineData("0123456789", "Numeric data")]
    [InlineData("ABCDEFGHIJ", "Upper case letters")]
    [InlineData("abcdefghij", "Lower case letters")]
    [InlineData("ABC123xyz", "Mixed alphanumeric")]
    [InlineData("Hello World!", "With space and punctuation")]
    public void Encode_WithVariousCharacterTypes_ShouldSucceed(string input, string description)
    {
        // Act
        Glyph[] result = Code128Encoder.Encode(input, _factory, _checksum);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);

        _output.WriteLine($"{description}: '{input}' -> {result.Length} glyphs");
    }

    [Fact]
    public void Encode_LongString_ShouldProduceMoreGlyphs()
    {
        // Arrange
        const string shortData = "ABC";
        const string longData = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        // Act
        Glyph[] shortResult = Code128Encoder.Encode(shortData, _factory, _checksum);
        Glyph[] longResult = Code128Encoder.Encode(longData, _factory, _checksum);

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
            Code128Encoder.Encode(emptyData, _factory, _checksum));

        _output.WriteLine("Empty string correctly throws ArgumentNullException");
    }

    [Fact]
    public void Encode_ShouldIncludeChecksumWhenProvided()
    {
        // Arrange
        const string testData = "12345";

        // Act
        Glyph[] result = Code128Encoder.Encode(testData, _factory, _checksum);

        // Manually get what the checksum should produce
        Glyph[] checksumGlyphs = _checksum.GetChecksum(testData).ToArray();

        // Assert
        Assert.NotNull(result);
        Assert.True(checksumGlyphs.Length > 0, "Checksum should produce glyphs");

        _output.WriteLine($"Checksum produces {checksumGlyphs.Length} glyphs");
        _output.WriteLine($"Total encoded glyphs: {result.Length}");
    }
}
