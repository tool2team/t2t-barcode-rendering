using System.Text.RegularExpressions;

namespace T2t.Barcode.Svg.Tests;

/// <summary>
/// Tests for QR Code generation
/// </summary>
public class CodeQrTests
{
    [Fact]
    public void QrCode_Draw_WithValidInput_ShouldGenerateSvg()
    {
        // Arrange
        var barcode = BarcodeDrawFactory.CodeQr;
        const string testData = "https://github.com/tool2team/t2t-barcode-rendering";

        // Act
        var result = barcode.Draw(testData, new BarcodeMetricsQr());

        // Assert
        Assert.NotNull(result);
        Assert.False(string.IsNullOrEmpty(result));
        Assert.Contains("<svg", result);
    }

    [Theory]
    [InlineData("https://example.com")]
    [InlineData("Simple text")]
    [InlineData("12345")]
    [InlineData("Test QR Code")]
    public void QrCode_Draw_WithDifferentInputs_ShouldGenerateSvg(string input)
    {
        // Arrange
        var barcode = BarcodeDrawFactory.CodeQr;

        // Act
        var result = barcode.Draw(input, new BarcodeMetricsQr());

        // Assert
        Assert.NotNull(result);
        Assert.Contains("<svg", result);
    }

    [Fact]
    public void QrCode_Draw_WithLongText_ShouldGenerateSvg()
    {
        // Arrange
        var barcode = BarcodeDrawFactory.CodeQr;
        const string testData = "This is a longer text to test QR code generation with more data that requires a larger QR code matrix";

        // Act
        var result = barcode.Draw(testData, new BarcodeMetricsQr());

        // Assert
        Assert.NotNull(result);
        Assert.Contains("<svg", result);
    }

    [Fact]
    public void QrCode_Draw_WithUnicodeCharacters_ShouldGenerateSvg()
    {
        // Arrange
        var barcode = BarcodeDrawFactory.CodeQr;
        const string testData = "Hello 世界 🌍";

        // Act
        var result = barcode.Draw(testData, new BarcodeMetricsQr());

        // Assert
        Assert.NotNull(result);
        Assert.Contains("<svg", result);
    }

    [Fact]
    public void QrCode_Draw_WithCustomSize_ShouldGenerateSvgWithSpecifiedDimensions()
    {
        // Arrange
        var barcode = BarcodeDrawFactory.CodeQr;
        const string testData = "QR Code Test";
        const int size = 300;

        // Act
        var result = barcode.Draw(testData, size);

        // Assert
        Assert.NotNull(result);
        Assert.Contains("<svg", result);
        Match heightMatch = SvgUtils.RxHeight().Match(result);
        Assert.True(heightMatch.Success, "Height attribute not found in SVG output.");
        Assert.True(float.TryParse(heightMatch.Groups[1].Value, out float height));

        Assert.True(size > height);
    }

    [Fact]
    public void QrCode_Draw_WithEmptyString_ShouldThrowException()
    {
        // Arrange
        var barcode = BarcodeDrawFactory.CodeQr;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => barcode.Draw(string.Empty, new BarcodeMetricsQr()));
    }
}
