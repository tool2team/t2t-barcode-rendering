using System.Text.RegularExpressions;

namespace T2t.Barcode.Svg.Tests;

/// <summary>
/// Tests for Code128 barcode generation
/// </summary>
public class Code128Tests
{
    [Fact]
    public void Code128_Draw_WithValidInput_ShouldGenerateSvg()
    {
        // Arrange
        var barcode = BarcodeDrawFactory.Code128WithChecksum;
        const string testData = "ABC123456";

        // Act
        var result = barcode.Draw(testData, new BarcodeMetrics1d(100, 10));

        // Assert
        Assert.NotNull(result);
        Assert.False(string.IsNullOrEmpty(result));
        Assert.Contains("<svg", result);
    }

    [Theory]
    [InlineData("12345678")]
    [InlineData("ABCDEFGH")]
    [InlineData("Test123")]
    [InlineData("!@#$%")]
    public void Code128_Draw_WithDifferentInputs_ShouldGenerateSvg(string input)
    {
        // Arrange
        var barcode = BarcodeDrawFactory.Code128WithChecksum;

        // Act
        var result = barcode.Draw(input, new BarcodeMetrics1d(100, 10));

        // Assert
        Assert.NotNull(result);
        Assert.Contains("<svg", result);
    }

    [Fact]
    public void Code128_Draw_WithEmptyString_ShouldThrowException()
    {
        // Arrange
        var barcode = BarcodeDrawFactory.Code128WithChecksum;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => barcode.Draw(string.Empty, new BarcodeMetrics1d(100, 10)));
    }

    [Fact]
    public void Code128_Draw_WithCustomSize_ShouldGenerateSvgWithSpecifiedDimensions()
    {
        // Arrange
        var barcode = BarcodeDrawFactory.Code128WithChecksum;
        const string testData = "CODE128TEST";
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
