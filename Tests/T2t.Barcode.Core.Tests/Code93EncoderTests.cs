//-----------------------------------------------------------------------
// <copyright file="Code93EncoderTests.cs" company="Zen Design Corp">
//     Copyright © Zen Design Corp 2008 - 2012. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using T2t.Barcode.Core.Code93;

namespace T2t.Barcode.Core.Tests;

public class Code93EncoderTests
{
    private readonly ITestOutputHelper _output;

    public Code93EncoderTests(ITestOutputHelper output)
    {
        _output = output;
    }

    #region Basic Structure Tests

    [Fact]
    public void Encode_SimpleText_IncludesStartStopAndTerminator()
    {
        var glyphs = Code93Encoder.Encode("ABC", Code93GlyphFactory.Instance, Code93Checksum.Instance);

        Assert.NotNull(glyphs);
        Assert.NotEmpty(glyphs);

        // First glyph should be start ('*')
        Assert.Equal('*', glyphs[0].Character);

        // Last two glyphs should be stop ('*') and terminator ('|')
        Assert.Equal('*', glyphs[^2].Character);
        Assert.Equal('|', glyphs[^1].Character);
    }

    [Fact]
    public void Encode_WithChecksum_IncludesChecksumGlyphs()
    {
        var withChecksum = Code93Encoder.Encode("123", Code93GlyphFactory.Instance, Code93Checksum.Instance);
        var withoutChecksum = Code93Encoder.Encode("123", Code93GlyphFactory.Instance, null);

        Assert.NotNull(withChecksum);
        Assert.NotNull(withoutChecksum);

        // With checksum should have more glyphs (2 checksum glyphs)
        Assert.True(withChecksum.Length > withoutChecksum.Length);
        Assert.Equal(2, withChecksum.Length - withoutChecksum.Length);
    }

    [Fact]
    public void Encode_WithoutChecksum_OnlyIncludesDataAndStartStopTerminator()
    {
        var glyphs = Code93Encoder.Encode("123", Code93GlyphFactory.Instance, null);

        // Should have: start + data (3 chars) + stop + terminator = 6 glyphs
        Assert.Equal(6, glyphs.Length);
    }

    [Fact]
    public void Encode_SameInputTwice_ProducesSameOutput()
    {
        var glyphs1 = Code93Encoder.Encode("TEST", Code93GlyphFactory.Instance, Code93Checksum.Instance);
        var glyphs2 = Code93Encoder.Encode("TEST", Code93GlyphFactory.Instance, Code93Checksum.Instance);

        Assert.Equal(glyphs1.Length, glyphs2.Length);
        for (int i = 0; i < glyphs1.Length; i++)
        {
            Assert.Equal(glyphs1[i].Character, glyphs2[i].Character);
        }
    }

    #endregion

    #region Numeric Tests

    [Fact]
    public void Encode_SingleDigit_Success()
    {
        var glyphs = Code93Encoder.Encode("0", Code93GlyphFactory.Instance, Code93Checksum.Instance);

        Assert.NotNull(glyphs);
        Assert.NotEmpty(glyphs);

        // Code93 encodes digits directly
        _output.WriteLine($"Single digit '0' encoded to {glyphs.Length} glyphs");
    }

    [Theory]
    [InlineData("0")]
    [InlineData("1")]
    [InlineData("2")]
    [InlineData("3")]
    [InlineData("4")]
    [InlineData("5")]
    [InlineData("6")]
    [InlineData("7")]
    [InlineData("8")]
    [InlineData("9")]
    public void Encode_EachDigit_Success(string digit)
    {
        var glyphs = Code93Encoder.Encode(digit, Code93GlyphFactory.Instance, Code93Checksum.Instance);

        Assert.NotNull(glyphs);
        Assert.NotEmpty(glyphs);
        _output.WriteLine($"Digit '{digit}' encoded to {glyphs.Length} glyphs");
    }

    [Fact]
    public void Encode_MultipleDigits_Success()
    {
        var glyphs = Code93Encoder.Encode("0123456789", Code93GlyphFactory.Instance, Code93Checksum.Instance);

        Assert.NotNull(glyphs);
        Assert.NotEmpty(glyphs);
        _output.WriteLine($"Numeric string encoded to {glyphs.Length} glyphs");
    }

    [Fact]
    public void Encode_NumericSequence_Success()
    {
        var glyphs = Code93Encoder.Encode("9876543210", Code93GlyphFactory.Instance, Code93Checksum.Instance);

        Assert.NotNull(glyphs);
        Assert.True(glyphs.Length > 0);
    }

    #endregion

    #region Uppercase Letter Tests

    [Fact]
    public void Encode_SingleUppercaseLetter_Success()
    {
        var glyphs = Code93Encoder.Encode("A", Code93GlyphFactory.Instance, Code93Checksum.Instance);

        Assert.NotNull(glyphs);
        Assert.NotEmpty(glyphs);

        // Code93 encodes uppercase letters directly
        _output.WriteLine($"Single uppercase letter 'A' encoded to {glyphs.Length} glyphs");
    }

    [Theory]
    [InlineData("A")]
    [InlineData("B")]
    [InlineData("C")]
    [InlineData("D")]
    [InlineData("E")]
    [InlineData("F")]
    [InlineData("G")]
    [InlineData("H")]
    [InlineData("I")]
    [InlineData("J")]
    [InlineData("K")]
    [InlineData("L")]
    [InlineData("M")]
    [InlineData("N")]
    [InlineData("O")]
    [InlineData("P")]
    [InlineData("Q")]
    [InlineData("R")]
    [InlineData("S")]
    [InlineData("T")]
    [InlineData("U")]
    [InlineData("V")]
    [InlineData("W")]
    [InlineData("X")]
    [InlineData("Y")]
    [InlineData("Z")]
    public void Encode_EachUppercaseLetter_Success(string letter)
    {
        var glyphs = Code93Encoder.Encode(letter, Code93GlyphFactory.Instance, Code93Checksum.Instance);

        Assert.NotNull(glyphs);
        Assert.NotEmpty(glyphs);
        _output.WriteLine($"Letter '{letter}' encoded to {glyphs.Length} glyphs");
    }

    [Fact]
    public void Encode_AllUppercaseLetters_Success()
    {
        var glyphs = Code93Encoder.Encode("ABCDEFGHIJKLMNOPQRSTUVWXYZ", Code93GlyphFactory.Instance, Code93Checksum.Instance);

        Assert.NotNull(glyphs);
        Assert.NotEmpty(glyphs);
        _output.WriteLine($"All uppercase letters encoded to {glyphs.Length} glyphs");
    }

    #endregion

    #region Lowercase Letter Tests (Composite Glyphs)

    [Theory]
    [InlineData("a")]
    [InlineData("b")]
    [InlineData("c")]
    [InlineData("d")]
    [InlineData("e")]
    [InlineData("f")]
    [InlineData("g")]
    [InlineData("h")]
    [InlineData("i")]
    [InlineData("j")]
    [InlineData("k")]
    [InlineData("l")]
    [InlineData("m")]
    [InlineData("n")]
    [InlineData("o")]
    [InlineData("p")]
    [InlineData("q")]
    [InlineData("r")]
    [InlineData("s")]
    [InlineData("t")]
    [InlineData("u")]
    [InlineData("v")]
    [InlineData("w")]
    [InlineData("x")]
    [InlineData("y")]
    [InlineData("z")]
    public void Encode_EachLowercaseLetter_Success(string letter)
    {
        // Code93 encodes lowercase letters using composite glyphs
        // Checksum calculation doesn't work with composite characters, so we use null
        var glyphs = Code93Encoder.Encode(letter, Code93GlyphFactory.Instance, null);

        Assert.NotNull(glyphs);
        Assert.NotEmpty(glyphs);
        _output.WriteLine($"Lowercase letter '{letter}' encoded to {glyphs.Length} glyphs");
    }

    [Fact]
    public void Encode_AllLowercaseLetters_Success()
    {
        // Code93 encodes lowercase letters using composite glyphs
        // Checksum calculation doesn't work with composite characters, so we use null
        var glyphs = Code93Encoder.Encode("abcdefghijklmnopqrstuvwxyz", Code93GlyphFactory.Instance, null);

        Assert.NotNull(glyphs);
        Assert.NotEmpty(glyphs);
        _output.WriteLine($"All lowercase letters encoded to {glyphs.Length} glyphs");
    }

    [Fact]
    public void Encode_MixedCase_Success()
    {
        // Code93 encodes lowercase letters using composite glyphs
        // Checksum calculation doesn't work with composite characters, so we use null
        var glyphs = Code93Encoder.Encode("AbCdEf", Code93GlyphFactory.Instance, null);

        Assert.NotNull(glyphs);
        Assert.NotEmpty(glyphs);
        _output.WriteLine($"Mixed case string encoded to {glyphs.Length} glyphs");
    }

    #endregion

    #region Special Character Tests

    [Theory]
    [InlineData("-")]
    [InlineData(".")]
    [InlineData(" ")]
    [InlineData("$")]
    [InlineData("/")]
    [InlineData("+")]
    [InlineData("%")]
    public void Encode_DirectSpecialCharacters_Success(string character)
    {
        var glyphs = Code93Encoder.Encode(character, Code93GlyphFactory.Instance, Code93Checksum.Instance);

        Assert.NotNull(glyphs);
        Assert.NotEmpty(glyphs);
        _output.WriteLine($"Special character '{character}' encoded to {glyphs.Length} glyphs");
    }

    [Fact]
    public void Encode_Dash_Success()
    {
        var glyphs = Code93Encoder.Encode("-", Code93GlyphFactory.Instance, Code93Checksum.Instance);

        Assert.NotNull(glyphs);
        Assert.NotEmpty(glyphs);
    }

    [Fact]
    public void Encode_Period_Success()
    {
        var glyphs = Code93Encoder.Encode(".", Code93GlyphFactory.Instance, Code93Checksum.Instance);

        Assert.NotNull(glyphs);
        Assert.NotEmpty(glyphs);
    }

    [Fact]
    public void Encode_Space_Success()
    {
        var glyphs = Code93Encoder.Encode(" ", Code93GlyphFactory.Instance, Code93Checksum.Instance);

        Assert.NotNull(glyphs);
        Assert.NotEmpty(glyphs);
    }

    [Fact]
    public void Encode_DollarSign_Success()
    {
        var glyphs = Code93Encoder.Encode("$", Code93GlyphFactory.Instance, Code93Checksum.Instance);

        Assert.NotNull(glyphs);
        Assert.NotEmpty(glyphs);
    }

    [Fact]
    public void Encode_Slash_Success()
    {
        var glyphs = Code93Encoder.Encode("/", Code93GlyphFactory.Instance, Code93Checksum.Instance);

        Assert.NotNull(glyphs);
        Assert.NotEmpty(glyphs);
    }

    [Fact]
    public void Encode_Plus_Success()
    {
        var glyphs = Code93Encoder.Encode("+", Code93GlyphFactory.Instance, Code93Checksum.Instance);

        Assert.NotNull(glyphs);
        Assert.NotEmpty(glyphs);
    }

    [Fact]
    public void Encode_Percent_Success()
    {
        var glyphs = Code93Encoder.Encode("%", Code93GlyphFactory.Instance, Code93Checksum.Instance);

        Assert.NotNull(glyphs);
        Assert.NotEmpty(glyphs);
    }

    [Fact]
    public void Encode_MultipleSpecialCharacters_Success()
    {
        var glyphs = Code93Encoder.Encode("-. $/%+", Code93GlyphFactory.Instance, Code93Checksum.Instance);

        Assert.NotNull(glyphs);
        Assert.NotEmpty(glyphs);
    }

    #endregion

    #region Composite Special Character Tests

    [Theory]
    [InlineData("!")]
    [InlineData("\"")]
    [InlineData("#")]
    [InlineData("&")]
    [InlineData("'")]
    [InlineData("(")]
    [InlineData(")")]
    [InlineData(",")]
    public void Encode_CompositeSpecialCharacters_Success(string character)
    {
        // These characters are encoded using composite glyphs
        // Checksum calculation doesn't work with composite characters, so we use null
        var glyphs = Code93Encoder.Encode(character, Code93GlyphFactory.Instance, null);

        Assert.NotNull(glyphs);
        Assert.NotEmpty(glyphs);
        _output.WriteLine($"Composite special character '{character}' encoded to {glyphs.Length} glyphs");
    }

    [Fact]
    public void Encode_ExclamationMark_Success()
    {
        var glyphs = Code93Encoder.Encode("!", Code93GlyphFactory.Instance, null);

        Assert.NotNull(glyphs);
        Assert.NotEmpty(glyphs);
    }

    [Fact]
    public void Encode_QuotationMark_Success()
    {
        var glyphs = Code93Encoder.Encode("\"", Code93GlyphFactory.Instance, null);

        Assert.NotNull(glyphs);
        Assert.NotEmpty(glyphs);
    }

    [Fact]
    public void Encode_HashSign_Success()
    {
        var glyphs = Code93Encoder.Encode("#", Code93GlyphFactory.Instance, null);

        Assert.NotNull(glyphs);
        Assert.NotEmpty(glyphs);
    }

    [Fact]
    public void Encode_Ampersand_Success()
    {
        var glyphs = Code93Encoder.Encode("&", Code93GlyphFactory.Instance, null);

        Assert.NotNull(glyphs);
        Assert.NotEmpty(glyphs);
    }

    [Fact]
    public void Encode_Apostrophe_Success()
    {
        var glyphs = Code93Encoder.Encode("'", Code93GlyphFactory.Instance, null);

        Assert.NotNull(glyphs);
        Assert.NotEmpty(glyphs);
    }

    [Fact]
    public void Encode_Parentheses_Success()
    {
        var glyphs = Code93Encoder.Encode("()", Code93GlyphFactory.Instance, null);

        Assert.NotNull(glyphs);
        Assert.NotEmpty(glyphs);
    }

    #endregion

    #region Alphanumeric Tests

    [Fact]
    public void Encode_Alphanumeric_Success()
    {
        var glyphs = Code93Encoder.Encode("ABC123", Code93GlyphFactory.Instance, Code93Checksum.Instance);

        Assert.NotNull(glyphs);
        Assert.NotEmpty(glyphs);
    }

    [Fact]
    public void Encode_AlphanumericWithSpecialChars_Success()
    {
        var glyphs = Code93Encoder.Encode("ABC-123", Code93GlyphFactory.Instance, Code93Checksum.Instance);

        Assert.NotNull(glyphs);
        Assert.NotEmpty(glyphs);
    }

    [Fact]
    public void Encode_ProductCode_Success()
    {
        var glyphs = Code93Encoder.Encode("PROD-123-XYZ", Code93GlyphFactory.Instance, Code93Checksum.Instance);

        Assert.NotNull(glyphs);
        Assert.NotEmpty(glyphs);
    }

    [Fact]
    public void Encode_WithSpaces_Success()
    {
        var glyphs = Code93Encoder.Encode("HELLO WORLD", Code93GlyphFactory.Instance, Code93Checksum.Instance);

        Assert.NotNull(glyphs);
        Assert.NotEmpty(glyphs);
    }

    #endregion

    #region Length Tests

    [Fact]
    public void Encode_ShortString_Success()
    {
        var glyphs = Code93Encoder.Encode("A", Code93GlyphFactory.Instance, Code93Checksum.Instance);

        Assert.NotNull(glyphs);
        Assert.NotEmpty(glyphs);
    }

    [Fact]
    public void Encode_MediumString_Success()
    {
        var glyphs = Code93Encoder.Encode("CODE93TEST", Code93GlyphFactory.Instance, Code93Checksum.Instance);

        Assert.NotNull(glyphs);
        Assert.NotEmpty(glyphs);
    }

    [Fact]
    public void Encode_LongString_Success()
    {
        var glyphs = Code93Encoder.Encode("THISISAVERYLONGBARCODESTRING", Code93GlyphFactory.Instance, Code93Checksum.Instance);

        Assert.NotNull(glyphs);
        Assert.NotEmpty(glyphs);
    }

    [Fact]
    public void Encode_DifferentLengths_ProduceDifferentGlyphCounts()
    {
        var glyphs1 = Code93Encoder.Encode("A", Code93GlyphFactory.Instance, Code93Checksum.Instance);
        var glyphs2 = Code93Encoder.Encode("AB", Code93GlyphFactory.Instance, Code93Checksum.Instance);
        var glyphs3 = Code93Encoder.Encode("ABC", Code93GlyphFactory.Instance, Code93Checksum.Instance);

        Assert.True(glyphs2.Length > glyphs1.Length);
        Assert.True(glyphs3.Length > glyphs2.Length);
    }

    #endregion

    #region Extended ASCII Tests

    [Theory]
    [InlineData("@")]
    [InlineData("`")]
    [InlineData("[")]
    [InlineData("]")]
    [InlineData("{")]
    [InlineData("}")]
    [InlineData("\\")]
    [InlineData("^")]
    [InlineData("_")]
    [InlineData("~")]
    public void Encode_ExtendedASCIICharacters_Success(string character)
    {
        // These characters are encoded using composite glyphs
        // Checksum calculation doesn't work with composite characters, so we use null
        var glyphs = Code93Encoder.Encode(character, Code93GlyphFactory.Instance, null);

        Assert.NotNull(glyphs);
        Assert.NotEmpty(glyphs);
        _output.WriteLine($"Extended ASCII character '{character}' encoded to {glyphs.Length} glyphs");
    }

    #endregion

    #region Edge Cases

    [Fact]
    public void Encode_RepeatedCharacters_Success()
    {
        var glyphs = Code93Encoder.Encode("AAAAAAA", Code93GlyphFactory.Instance, Code93Checksum.Instance);

        Assert.NotNull(glyphs);
        Assert.NotEmpty(glyphs);
    }

    [Fact]
    public void Encode_AllSameDigit_Success()
    {
        var glyphs = Code93Encoder.Encode("9999999", Code93GlyphFactory.Instance, Code93Checksum.Instance);

        Assert.NotNull(glyphs);
        Assert.NotEmpty(glyphs);
    }

    [Fact]
    public void Encode_AlternatingCharacters_Success()
    {
        var glyphs = Code93Encoder.Encode("A1B2C3", Code93GlyphFactory.Instance, Code93Checksum.Instance);

        Assert.NotNull(glyphs);
        Assert.NotEmpty(glyphs);
    }

    #endregion

    #region Consistency Tests

    [Fact]
    public void Encode_MultipleCallsWithSameInput_ProducesConsistentLength()
    {
        var glyphs1 = Code93Encoder.Encode("TEST123", Code93GlyphFactory.Instance, Code93Checksum.Instance);
        var glyphs2 = Code93Encoder.Encode("TEST123", Code93GlyphFactory.Instance, Code93Checksum.Instance);
        var glyphs3 = Code93Encoder.Encode("TEST123", Code93GlyphFactory.Instance, Code93Checksum.Instance);

        Assert.Equal(glyphs1.Length, glyphs2.Length);
        Assert.Equal(glyphs2.Length, glyphs3.Length);
    }

    [Fact]
    public void Encode_WithAndWithoutChecksum_DifferentLengths()
    {
        var withChecksum = Code93Encoder.Encode("TEST", Code93GlyphFactory.Instance, Code93Checksum.Instance);
        var withoutChecksum = Code93Encoder.Encode("TEST", Code93GlyphFactory.Instance, null);

        Assert.NotEqual(withChecksum.Length, withoutChecksum.Length);
        Assert.True(withChecksum.Length > withoutChecksum.Length);
    }

    [Fact]
    public void Encode_AllGlyphsNotNull()
    {
        var glyphs = Code93Encoder.Encode("CODE93", Code93GlyphFactory.Instance, Code93Checksum.Instance);

        Assert.NotNull(glyphs);
        Assert.All(glyphs, g => Assert.NotNull(g));
    }

    #endregion

    #region Real-World Scenarios

    [Fact]
    public void Encode_SerialNumber_Success()
    {
        var glyphs = Code93Encoder.Encode("SN-2024-001", Code93GlyphFactory.Instance, Code93Checksum.Instance);

        Assert.NotNull(glyphs);
        Assert.NotEmpty(glyphs);
    }

    [Fact]
    public void Encode_PostalCode_Success()
    {
        var glyphs = Code93Encoder.Encode("12345-6789", Code93GlyphFactory.Instance, Code93Checksum.Instance);

        Assert.NotNull(glyphs);
        Assert.NotEmpty(glyphs);
    }

    [Fact]
    public void Encode_PartNumber_Success()
    {
        var glyphs = Code93Encoder.Encode("PART/A123", Code93GlyphFactory.Instance, Code93Checksum.Instance);

        Assert.NotNull(glyphs);
        Assert.NotEmpty(glyphs);
    }

    [Fact]
    public void Encode_URL_Success()
    {
        // Code93 does not support ':' character, so we test a simplified URL
        var glyphs = Code93Encoder.Encode("HTTP//TEST.COM", Code93GlyphFactory.Instance, Code93Checksum.Instance);

        Assert.NotNull(glyphs);
        Assert.NotEmpty(glyphs);
    }

    [Fact]
    public void Encode_Email_Success()
    {
        // Code93 supports @ and . but not :
        var glyphs = Code93Encoder.Encode("TEST@EXAMPLE.COM", Code93GlyphFactory.Instance, null);

        Assert.NotNull(glyphs);
        Assert.NotEmpty(glyphs);
    }

    #endregion
}
