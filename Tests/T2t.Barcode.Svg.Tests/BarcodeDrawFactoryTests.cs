using T2t.Barcode.Core;

namespace T2t.Barcode.Svg.Tests;

/// <summary>
/// Tests for BarcodeDrawFactory
/// </summary>
public class BarcodeDrawFactoryTests
{
    [Fact]
    public void GetCode39_WithoutChecksum_ShouldReturnInstance()
    {
        // Arrange & Act
        var barcode = BarcodeDrawFactory.Code39WithoutChecksum;

        // Assert
        Assert.NotNull(barcode);
    }

    [Fact]
    public void GetCode39_WithChecksum_ShouldReturnInstance()
    {
        // Arrange & Act
        var barcode = BarcodeDrawFactory.Code39WithChecksum;

        // Assert
        Assert.NotNull(barcode);
    }

    [Fact]
    public void GetCode93_WithChecksum_ShouldReturnInstance()
    {
        // Arrange & Act
        var barcode = BarcodeDrawFactory.Code93WithChecksum;

        // Assert
        Assert.NotNull(barcode);
    }

    [Fact]
    public void GetCode128_WithChecksum_ShouldReturnInstance()
    {
        // Arrange & Act
        var barcode = BarcodeDrawFactory.Code128WithChecksum;

        // Assert
        Assert.NotNull(barcode);
    }

    [Fact]
    public void GetCodeEan13_ShouldReturnInstance()
    {
        // Arrange & Act
        var barcode = BarcodeDrawFactory.CodeEan13WithChecksum;

        // Assert
        Assert.NotNull(barcode);
    }

    [Fact]
    public void GetCodeEan8_ShouldReturnInstance()
    {
        // Arrange & Act
        var barcode = BarcodeDrawFactory.CodeEan8WithChecksum;

        // Assert
        Assert.NotNull(barcode);
    }

    [Fact]
    public void GetCodeQr_ShouldReturnInstance()
    {
        // Arrange & Act
        var barcode = BarcodeDrawFactory.CodeQr;

        // Assert
        Assert.NotNull(barcode);
    }

    [Fact]
    public void GetCodeDm_ShouldReturnInstance()
    {
        // Arrange & Act
        var barcode = BarcodeDrawFactory.CodeDm;

        // Assert
        Assert.NotNull(barcode);
    }

    [Fact]
    public void GetSymbology_ShouldReturnCorrectBarcodeType()
    {
        // Arrange & Act
        var code39 = BarcodeDrawFactory.GetSymbology(BarcodeSymbology.Code39NC);
        var code128 = BarcodeDrawFactory.GetSymbology(BarcodeSymbology.Code128);
        var qrCode = BarcodeDrawFactory.GetSymbology(BarcodeSymbology.CodeQr);

        // Assert
        Assert.NotNull(code39);
        Assert.NotNull(code128);
        Assert.NotNull(qrCode);
    }

    [Fact]
    public void GetSymbology_InvalidSymbology_ShouldThrowException()
    {
        // Arrange
        var invalidSymbology = (BarcodeSymbology)9999;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => BarcodeDrawFactory.GetSymbology(invalidSymbology));
    }
}
