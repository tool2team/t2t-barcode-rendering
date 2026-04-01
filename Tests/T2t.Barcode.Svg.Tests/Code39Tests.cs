using System.Text.RegularExpressions;

namespace T2t.Barcode.Svg.Tests;

/// <summary>
/// Tests for Code39 barcode generation
/// </summary>
public partial class Code39Tests
{
    [Fact]
    public void Code39_Draw_WithValidInput_ShouldGenerateSvg()
    {
        // Arrange
        var barcode = BarcodeDrawFactory.Code39WithoutChecksum;
        const string testData = "ABC123";

        // Act
        var result = barcode.Draw(testData, new BarcodeMetrics1d(100, 10, 200, 20));

        // Assert
        Assert.NotNull(result);
        Assert.False(string.IsNullOrEmpty(result));
        Assert.Contains("<svg", result);
    }

    [Fact]
    public void Code39_Draw_WithChecksum_ShouldGenerateSvg()
    {
        // Arrange
        var barcode = BarcodeDrawFactory.Code39WithChecksum;
        const string testData = "TEST123";

        // Act
        var result = barcode.Draw(testData, new BarcodeMetrics1d(100, 10, 200, 20));

        // Assert
        Assert.NotNull(result);
        Assert.Contains("<svg", result);
    }

    [Theory]
    [InlineData("123")]
    [InlineData("ABC")]
    [InlineData("A1B2C3")]
    public void Code39_Draw_WithDifferentInputs_ShouldGenerateSvg(string input)
    {
        // Arrange
        var barcode = BarcodeDrawFactory.Code39WithoutChecksum;

        // Act
        var result = barcode.Draw(input, new BarcodeMetrics1d(100, 10, 200, 20));

        // Assert
        Assert.NotNull(result);
        Assert.Contains("<svg", result);
    }

    [Fact]
    public void Code39_Draw_WithCustomSize_ShouldGenerateSvgWithSpecifiedDimensions()
    {
        // Arrange
        var barcode = BarcodeDrawFactory.Code39WithoutChecksum;
        const string testData = "TEST";
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

