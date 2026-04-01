namespace T2t.Barcode.Svg.Tests;

/// <summary>
/// Tests for BarcodeMetrics1d
/// </summary>
public class BarcodeMetrics1dTests
{
    [Theory]
    [InlineData(100, 50)]
    [InlineData(300, 150)]
    [InlineData(500, 250)]
    public void BarcodeMetrics1d_WithDifferentDimensions_ShouldAcceptValues(int width, int height)
    {
        // Arrange & Act
        var metrics = new BarcodeMetrics1d(width, height);

        // Assert
        Assert.NotNull(metrics);
        Assert.Equal(width, metrics.MinWidth);
        Assert.Equal(height, metrics.MinHeight);
    }
}
