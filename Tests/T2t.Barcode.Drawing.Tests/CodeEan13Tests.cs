using System.Drawing;

namespace T2t.Barcode.Drawing.Tests;

/// <summary>
/// Tests for EAN-13 barcode generation
/// </summary>
public class CodeEan13Tests
{
    [Fact]
    public void Ean13_Draw_WithValidInput_ShouldGenerateImage()
    {
        // Arrange
        var barcode = BarcodeDrawFactory.CodeEan13WithChecksum;
        const string testData = "123456789012"; // 12 digits, checksum will be calculated

        // Act
        var result = barcode.Draw(testData, 200, 1.5f);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Width > 0);
        Assert.True(result.Height > 0);
    }

    [Theory]
    [InlineData("590123412345")]
    [InlineData("978020137962")]
    [InlineData("401234567890")]
    public void Ean13_Draw_WithDifferentValidBarcodes_ShouldGenerateImage(string input)
    {
        // Arrange
        var barcode = BarcodeDrawFactory.CodeEan13WithChecksum;

        // Act
        var result = barcode.Draw(input, 200, 1.5f);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Width > 0);
    }

    [Fact]
    public void Ean13_Draw_WithInvalidLength_ShouldThrowException()
    {
        // Arrange
        var barcode = BarcodeDrawFactory.CodeEan13WithChecksum;
        const string invalidData = "12345"; // Too short

        // Act & Assert
        Assert.Throws<ArgumentException>(() => barcode.Draw(invalidData, 200, 1.5f));
    }

    [Fact]
    public void Ean13_Draw_WithCustomSize_ShouldGenerateImageWithSpecifiedDimensions()
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
        Assert.True(maxHeight * scale >= result.Height);
    }
}
