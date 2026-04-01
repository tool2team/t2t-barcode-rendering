using System.Text.RegularExpressions;
using static System.Formats.Asn1.AsnWriter;

namespace T2t.Barcode.Svg.Tests;

/// <summary>
/// Tests for EAN-13 barcode generation
/// </summary>
public class CodeEan13Tests
{
    [Fact]
    public void Ean13_Draw_WithValidInput_ShouldGenerateSvg()
    {
        // Arrange
        var barcode = BarcodeDrawFactory.CodeEan13WithChecksum;
        const string testData = "123456789012"; // 12 digits, checksum will be calculated

        // Act
        var result = barcode.Draw(testData, new BarcodeMetrics1d(100, 10));

        // Assert
        Assert.NotNull(result);
        Assert.False(string.IsNullOrEmpty(result));
        Assert.Contains("<svg", result);
    }

    [Theory]
    [InlineData("590123412345")]
    [InlineData("978020137962")]
    [InlineData("401234567890")]
    public void Ean13_Draw_WithDifferentValidBarcodes_ShouldGenerateSvg(string input)
    {
        // Arrange
        var barcode = BarcodeDrawFactory.CodeEan13WithChecksum;

        // Act
        var result = barcode.Draw(input, new BarcodeMetrics1d(100, 10));

        // Assert
        Assert.NotNull(result);
        Assert.Contains("<svg", result);
    }

    [Fact]
    public void Ean13_Draw_WithInvalidLength_ShouldThrowException()
    {
        // Arrange
        var barcode = BarcodeDrawFactory.CodeEan13WithChecksum;
        const string invalidData = "12345"; // Too short

        // Act & Assert
        Assert.Throws<ArgumentException>(() => barcode.Draw(invalidData, new BarcodeMetrics1d(100, 10)));
    }

    [Fact]
    public void Ean13_Draw_WithCustomSize_ShouldGenerateSvgWithSpecifiedDimensions()
    {
        // Arrange
        var barcode = BarcodeDrawFactory.CodeEan13WithChecksum;
        const string testData = "123456789012";
        const int maxHeight = 400;
        const float scale = 1.5f;

        // Act
        var result = barcode.Draw(testData, maxHeight, scale);

        // Assert
        Assert.NotNull(result);
        Assert.Contains("<svg", result);
        Match heightMatch = SvgUtils.RxHeight().Match(result);
        Assert.True(heightMatch.Success, "Height attribute not found in SVG output.");
        Assert.True(float.TryParse(heightMatch.Groups[1].Value, out float height));

        Assert.True(maxHeight * scale >= height);
    }
}
