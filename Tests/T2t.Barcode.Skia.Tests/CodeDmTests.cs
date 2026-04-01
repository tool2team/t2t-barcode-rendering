using SkiaSharp;

namespace T2t.Barcode.Skia.Tests;

/// <summary>
/// Tests for DataMatrix barcode generation
/// </summary>
public class CodeDmTests
{
    // TODO [Fact]
    public void DataMatrix_Draw_WithValidInput_ShouldGenerateImage()
    {
        // Arrange
        var barcode = BarcodeDrawFactory.CodeDm;
        const string testData = "DataMatrix123";

        // Act
        var result = barcode.Draw(testData, 200, 1.5f);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Width > 0);
        Assert.True(result.Height > 0);
    }

    // TODO [Theory]
    [InlineData("12345")]
    [InlineData("ABCDEF")]
    [InlineData("Test123")]
    [InlineData("https://example.com")]
    public void DataMatrix_Draw_WithDifferentInputs_ShouldGenerateImage(string input)
    {
        // Arrange
        var barcode = BarcodeDrawFactory.CodeDm;

        // Act
        var result = barcode.Draw(input, 200, 1.5f);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Width > 0);
    }

    // TODO [Fact]
    public void DataMatrix_Draw_WithLongText_ShouldGenerateImage()
    {
        // Arrange
        var barcode = BarcodeDrawFactory.CodeDm;
        const string testData = "This is a longer text to encode in a DataMatrix code";

        // Act
        var result = barcode.Draw(testData, 200, 1.5f);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Width > 0);
    }

    // TODO [Fact]
    public void DataMatrix_Draw_WithCustomSize_ShouldGenerateImageWithSpecifiedDimensions()
    {
        // Arrange
        var barcode = BarcodeDrawFactory.CodeDm;
        const string testData = "DM123";
        const int size = 200;

        // Act
        var result = barcode.Draw(testData, size);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(size, result.Width);
        Assert.Equal(size, result.Height);
    }

    // TODO [Fact]
    public void DataMatrix_Draw_WithEmptyString_ShouldThrowException()
    {
        // Arrange
        var barcode = BarcodeDrawFactory.CodeDm;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => barcode.Draw(string.Empty, 200, 1.5f));
    }
}
