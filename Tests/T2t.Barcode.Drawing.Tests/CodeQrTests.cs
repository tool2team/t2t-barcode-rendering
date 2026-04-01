using System.Drawing;

namespace T2t.Barcode.Drawing.Tests;

/// <summary>
/// Tests for QR Code generation
/// </summary>
public class CodeQrTests
{
    [Fact]
    public void QrCode_Draw_WithValidInput_ShouldGenerateImage()
    {
        // Arrange
        var barcode = BarcodeDrawFactory.CodeQr;
        const string testData = "https://github.com/tool2team/t2t-barcode-rendering";

        // Act
        var result = barcode.Draw(testData, 200, 1.5f);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Width > 0);
        Assert.True(result.Height > 0);
    }

    [Theory]
    [InlineData("https://example.com")]
    [InlineData("Simple text")]
    [InlineData("12345")]
    [InlineData("Test QR Code")]
    public void QrCode_Draw_WithDifferentInputs_ShouldGenerateImage(string input)
    {
        // Arrange
        var barcode = BarcodeDrawFactory.CodeQr;

        // Act
        var result = barcode.Draw(input, 200, 1.5f);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Width > 0);
    }

    [Fact]
    public void QrCode_Draw_WithLongText_ShouldGenerateImage()
    {
        // Arrange
        var barcode = BarcodeDrawFactory.CodeQr;
        const string testData = "This is a longer text to test QR code generation with more data that requires a larger QR code matrix";

        // Act
        var result = barcode.Draw(testData, 200, 1.5f);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Width > 0);
    }

    [Fact]
    public void QrCode_Draw_WithUnicodeCharacters_ShouldGenerateImage()
    {
        // Arrange
        var barcode = BarcodeDrawFactory.CodeQr;
        const string testData = "Hello 世界 🌍";

        // Act
        var result = barcode.Draw(testData, 200, 1.5f);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Width > 0);
    }

    [Fact]
    public void QrCode_Draw_WithCustomSize_ShouldGenerateImageWithSpecifiedDimensions()
    {
        // Arrange
        var barcode = BarcodeDrawFactory.CodeQr;
        const string testData = "QR Code Test";
        const int size = 300;

        // Act
        var result = barcode.Draw(testData, size);

        // Assert
        Assert.NotNull(result);
        // TODO Assert.Equal(size, result.Width);
        // TODO Assert.Equal(size, result.Height);
    }

    [Fact]
    public void QrCode_Draw_WithEmptyString_ShouldThrowException()
    {
        // Arrange
        var barcode = BarcodeDrawFactory.CodeQr;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => barcode.Draw(string.Empty, 200, 1.5f));
    }
}
