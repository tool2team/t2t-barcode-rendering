using System.Drawing;

namespace T2t.Barcode.Drawing.Tests;

/// <summary>
/// Tests for Code39 barcode generation
/// </summary>
public class Code39Tests
{
    [Fact]
    public void Code39_Draw_WithValidInput_ShouldGenerateImage()
    {
        // Arrange
        var barcode = BarcodeDrawFactory.Code39WithoutChecksum;
        const string testData = "ABC123";

        // Act
        var result = barcode.Draw(testData, 200, 1.5f);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Width > 0);
        Assert.True(result.Height > 0);
    }

    [Fact]
    public void Code39_Draw_WithChecksum_ShouldGenerateImage()
    {
        // Arrange
        var barcode = BarcodeDrawFactory.Code39WithChecksum;
        const string testData = "TEST123";

        // Act
        var result = barcode.Draw(testData, 200, 1.5f);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Width > 0);
        Assert.True(result.Height > 0);
    }

    [Theory]
    [InlineData("123")]
    [InlineData("ABC")]
    [InlineData("A1B2C3")]
    public void Code39_Draw_WithDifferentInputs_ShouldGenerateImage(string input)
    {
        // Arrange
        var barcode = BarcodeDrawFactory.Code39WithoutChecksum;

        // Act
        var result = barcode.Draw(input, 200, 1.5f);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Width > 0);
    }

    [Fact]
    public void Code39_Draw_WithCustomSize_ShouldGenerateImageWithSpecifiedDimensions()
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
        Assert.True(maxHeight * scale >= result.Height);
    }

    [Fact]
    public void Code39_Draw_ShouldDisposeCorrectly()
    {
        // Arrange
        var barcode = BarcodeDrawFactory.Code39WithoutChecksum;
        const string testData = "TEST";

        // Act
        var result = barcode.Draw(testData, 200, 1.5f);

        // Assert
        Assert.NotNull(result);

        // Cleanup
        result.Dispose();
    }
}
