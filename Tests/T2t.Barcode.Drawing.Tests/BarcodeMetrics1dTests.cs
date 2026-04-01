using T2t.Barcode.Core;

namespace T2t.Barcode.Drawing.Tests;

/// <summary>
/// Tests for BarcodeMetrics1d
/// </summary>
public class BarcodeMetrics1dTests
{
    [Fact]
    public void BarcodeMetrics1d_DefaultConstructor_ShouldCreateInstance()
    {
        // Arrange & Act
        var metrics = new BarcodeMetrics1d();

        // Assert
        Assert.NotNull(metrics);
    }

    [Fact]
    public void BarcodeMetrics1d_WithParameters_ShouldCreateInstance()
    {
        // Arrange
        const int width = 200;
        const int height = 100;

        // Act
        var metrics = new BarcodeMetrics1d(width, height);

        // Assert
        Assert.NotNull(metrics);
        Assert.Equal(width, metrics.MinWidth);
        Assert.Equal(height, metrics.MinHeight);
    }

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
