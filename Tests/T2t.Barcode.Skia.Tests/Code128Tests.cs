namespace T2t.Barcode.Skia.Tests;

/// <summary>
/// Tests for Code128 barcode generation
/// </summary>
public class Code128Tests
{
    [Fact]
    public void Code128_Draw_WithValidInput_ShouldGenerateImage()
    {
        // Arrange
        var barcode = BarcodeDrawFactory.Code128WithChecksum;
        const string testData = "ABC123456";

        // Act
        var result = barcode.Draw(testData, 200, 1.5f);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Width > 0);
        Assert.True(result.Height > 0);
    }

    [Theory]
    [InlineData("12345678")]
    [InlineData("ABCDEFGH")]
    [InlineData("Test123")]
    [InlineData("!@#$%")]
    public void Code128_Draw_WithDifferentInputs_ShouldGenerateImage(string input)
    {
        // Arrange
        var barcode = BarcodeDrawFactory.Code128WithChecksum;

        // Act
        var result = barcode.Draw(input, 200, 1.5f);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Width > 0);
    }

    [Fact]
    public void Code128_Draw_WithEmptyString_ShouldThrowException()
    {
        // Arrange
        var barcode = BarcodeDrawFactory.Code128WithChecksum;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => barcode.Draw(string.Empty, 200, 1.5f));
    }

    [Fact]
    public void Code128_Draw_WithCustomSize_ShouldGenerateImageWithSpecifiedDimensions()
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
        Assert.True(maxHeight * scale >= result.Height);
    }
}
