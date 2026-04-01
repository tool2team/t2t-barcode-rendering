using System.Text.RegularExpressions;

namespace T2t.Barcode.Svg.Tests;

/// <summary>
/// Tests for DataMatrix barcode generation
/// </summary>
public class CodeDmTests
{
    [Fact]
    public void DataMatrix_Draw_WithValidInput_ShouldGenerateSvg()
    {
        // Arrange
        var barcode = BarcodeDrawFactory.CodeDm;
        const string testData = "DataMatrix123";

        // Act
        var result = barcode.Draw(testData, new BarcodeMetricsDm());

        // Assert
        Assert.NotNull(result);
        Assert.False(string.IsNullOrEmpty(result));
        Assert.Contains("<svg", result);
    }

    [Theory]
    [InlineData("12345")]
    [InlineData("ABCDEF")]
    [InlineData("Test123")]
    [InlineData("https://example.com")]
    public void DataMatrix_Draw_WithDifferentInputs_ShouldGenerateSvg(string input)
    {
        // Arrange
        var barcode = BarcodeDrawFactory.CodeDm;

        // Act
        var result = barcode.Draw(input, new BarcodeMetricsDm());

        // Assert
        Assert.NotNull(result);
        Assert.Contains("<svg", result);
    }

    [Fact]
    public void DataMatrix_Draw_WithLongText_ShouldGenerateSvg()
    {
        // Arrange
        var barcode = BarcodeDrawFactory.CodeDm;
        const string testData = "This is a longer text to encode in a DataMatrix code";

        // Act
        var result = barcode.Draw(testData, new BarcodeMetricsDm());

        // Assert
        Assert.NotNull(result);
        Assert.Contains("<svg", result);
    }

    [Fact]
    public void DataMatrix_Draw_WithCustomSize_ShouldGenerateSvgWithSpecifiedDimensions()
    {
        // Arrange
        var barcode = BarcodeDrawFactory.CodeDm;
        const string testData = "DM123";
        const int size = 200;

        // Act
        var result = barcode.Draw(testData, size);

        // Assert
        Assert.NotNull(result);
        Assert.Contains("<svg", result);
        Match heightMatch = SvgUtils.RxHeight().Match(result);
        Assert.True(heightMatch.Success, "Height attribute not found in SVG output.");
        Assert.True(float.TryParse(heightMatch.Groups[1].Value, out float height));

        Assert.True(height > 0);
    }

    [Fact]
    public void DataMatrix_Draw_WithEmptyString_ShouldThrowException()
    {
        // Arrange
        var barcode = BarcodeDrawFactory.CodeDm;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => barcode.Draw(string.Empty, new BarcodeMetricsDm()));
    }
}
