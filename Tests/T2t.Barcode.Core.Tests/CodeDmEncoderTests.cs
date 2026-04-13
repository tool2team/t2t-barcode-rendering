using System.Text;
using T2t.Barcode.Core.CodeDm;

namespace T2t.Barcode.Core.Tests;

/// <summary>
/// Tests for DataMatrix (CodeDm) encoder logic
/// </summary>
public class CodeDmEncoderTests
{
    private readonly ITestOutputHelper _output;

    public CodeDmEncoderTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void EncodeMatrix_ShouldReturnNonEmptyMatrix_ForSimpleText()
    {
        // Arrange
        const string testData = "TEST";

        // Act
        bool[,] result = CodeDmEncoder.EncodeMatrix(testData, Encoding.ASCII);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.GetLength(0) > 0, "Matrix should have rows");
        Assert.True(result.GetLength(1) > 0, "Matrix should have columns");
        _output.WriteLine($"Matrix dimensions: {result.GetLength(0)} x {result.GetLength(1)}");
    }

    [Fact]
    public void EncodeMatrix_ShouldReturnSquareMatrix()
    {
        // Arrange
        const string testData = "HELLO";

        // Act
        bool[,] result = CodeDmEncoder.EncodeMatrix(testData, Encoding.ASCII);

        // Assert
        int rows = result.GetLength(0);
        int cols = result.GetLength(1);
        Assert.Equal(rows, cols);
        _output.WriteLine($"Square matrix: {rows} x {cols}");
    }

    [Theory]
    [InlineData("A")]
    [InlineData("AB")]
    [InlineData("ABC")]
    [InlineData("ABCD")]
    [InlineData("ABCDE")]
    public void EncodeMatrix_ShouldHandleDifferentLengths(string text)
    {
        // Act
        bool[,] result = CodeDmEncoder.EncodeMatrix(text, Encoding.ASCII);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.GetLength(0) > 0);
        Assert.True(result.GetLength(1) > 0);
        _output.WriteLine($"Text: '{text}' -> Matrix: {result.GetLength(0)} x {result.GetLength(1)}");
    }

    [Fact]
    public void EncodeMatrix_ShouldHandleNumericData()
    {
        // Arrange
        const string testData = "123456";

        // Act
        bool[,] result = CodeDmEncoder.EncodeMatrix(testData, Encoding.ASCII);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.GetLength(0) > 0);
        Assert.True(result.GetLength(1) > 0);
        _output.WriteLine($"Numeric data encoded to: {result.GetLength(0)} x {result.GetLength(1)}");
    }

    [Fact]
    public void EncodeMatrix_ShouldHandleAlphanumericData()
    {
        // Arrange
        const string testData = "ABC123";

        // Act
        bool[,] result = CodeDmEncoder.EncodeMatrix(testData, Encoding.ASCII);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.GetLength(0) > 0);
        Assert.True(result.GetLength(1) > 0);
        _output.WriteLine($"Alphanumeric data encoded to: {result.GetLength(0)} x {result.GetLength(1)}");
    }

    [Fact]
    public void EncodeMatrix_ShouldHandleLongerText()
    {
        // Arrange
        const string testData = "HELLO WORLD 2024";

        // Act
        bool[,] result = CodeDmEncoder.EncodeMatrix(testData, Encoding.ASCII);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.GetLength(0) >= 10, "Longer text should produce larger matrix");
        Assert.True(result.GetLength(1) >= 10, "Longer text should produce larger matrix");
        _output.WriteLine($"Long text encoded to: {result.GetLength(0)} x {result.GetLength(1)}");
    }

    [Fact]
    public void EncodeMatrix_ShouldBeConsistent_ForSameInput()
    {
        // Arrange
        const string testData = "CONSISTENT";

        // Act
        bool[,] result1 = CodeDmEncoder.EncodeMatrix(testData, Encoding.ASCII);
        bool[,] result2 = CodeDmEncoder.EncodeMatrix(testData, Encoding.ASCII);

        // Assert
        Assert.Equal(result1.GetLength(0), result2.GetLength(0));
        Assert.Equal(result1.GetLength(1), result2.GetLength(1));

        // Compare matrix content
        for (int i = 0; i < result1.GetLength(0); i++)
        {
            for (int j = 0; j < result1.GetLength(1); j++)
            {
                Assert.Equal(result1[i, j], result2[i, j]);
            }
        }
        _output.WriteLine($"Consistent encoding verified for: {result1.GetLength(0)} x {result1.GetLength(1)}");
    }

    [Fact]
    public void EncodeMatrix_ShouldContainTrueAndFalseValues()
    {
        // Arrange
        const string testData = "DATAMATRIX";

        // Act
        bool[,] result = CodeDmEncoder.EncodeMatrix(testData, Encoding.ASCII);

        // Assert
        bool hasTrueValue = false;
        bool hasFalseValue = false;

        for (int i = 0; i < result.GetLength(0); i++)
        {
            for (int j = 0; j < result.GetLength(1); j++)
            {
                if (result[i, j])
                    hasTrueValue = true;
                else
                    hasFalseValue = true;

                if (hasTrueValue && hasFalseValue)
                    break;
            }
            if (hasTrueValue && hasFalseValue)
                break;
        }

        Assert.True(hasTrueValue, "Matrix should contain at least one true value (black module)");
        Assert.True(hasFalseValue, "Matrix should contain at least one false value (white module)");
        _output.WriteLine("Matrix contains both black and white modules");
    }

    [Fact]
    public void EncodeMatrix_WithASCIIEncoding_ShouldWork()
    {
        // Arrange
        const string testData = "ASCII";

        // Act
        bool[,] result = CodeDmEncoder.EncodeMatrix(testData, Encoding.ASCII);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.GetLength(0) > 0);
        _output.WriteLine($"ASCII encoding: {result.GetLength(0)} x {result.GetLength(1)}");
    }

    [Fact]
    public void EncodeMatrix_WithUTF8Encoding_ShouldWork()
    {
        // Arrange
        const string testData = "UTF8";

        // Act
        bool[,] result = CodeDmEncoder.EncodeMatrix(testData, Encoding.UTF8);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.GetLength(0) > 0);
        _output.WriteLine($"UTF8 encoding: {result.GetLength(0)} x {result.GetLength(1)}");
    }

    [Fact]
    public void EncodeMatrix_WithUnicodeEncoding_ShouldWork()
    {
        // Arrange
        const string testData = "Unicode";

        // Act
        bool[,] result = CodeDmEncoder.EncodeMatrix(testData, Encoding.Unicode);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.GetLength(0) > 0);
        _output.WriteLine($"Unicode encoding: {result.GetLength(0)} x {result.GetLength(1)}");
    }

    [Theory]
    [InlineData("A", "ASCII")]
    [InlineData("12", "ASCII")]
    [InlineData("XY", "ASCII")]
    [InlineData("99", "ASCII")]
    public void EncodeMatrix_ShortData_ShouldProduceValidMatrix(string text, string encodingName)
    {
        // Arrange
        Encoding encoding = encodingName switch
        {
            "ASCII" => Encoding.ASCII,
            "UTF8" => Encoding.UTF8,
            "Unicode" => Encoding.Unicode,
            _ => throw new ArgumentException($"Unsupported encoding: {encodingName}", nameof(encodingName))
        };

        // Act
        bool[,] result = CodeDmEncoder.EncodeMatrix(text, encoding);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.GetLength(0) >= 10, "Even short data should produce minimum size matrix");
        Assert.True(result.GetLength(1) >= 10, "Even short data should produce minimum size matrix");
        _output.WriteLine($"Short data '{text}' -> {result.GetLength(0)} x {result.GetLength(1)}");
    }

    [Fact]
    public void EncodeMatrix_WithSpecialCharacters_ShouldWork()
    {
        // Arrange
        const string testData = "A-B.C/D";

        // Act
        bool[,] result = CodeDmEncoder.EncodeMatrix(testData, Encoding.ASCII);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.GetLength(0) > 0);
        _output.WriteLine($"Special characters encoded to: {result.GetLength(0)} x {result.GetLength(1)}");
    }

    [Fact]
    public void EncodeMatrix_LargerMatrixForLongerText()
    {
        // Arrange
        const string shortText = "AB";
        const string longText = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        // Act
        bool[,] shortResult = CodeDmEncoder.EncodeMatrix(shortText, Encoding.ASCII);
        bool[,] longResult = CodeDmEncoder.EncodeMatrix(longText, Encoding.ASCII);

        // Assert
        int shortSize = shortResult.GetLength(0) * shortResult.GetLength(1);
        int longSize = longResult.GetLength(0) * longResult.GetLength(1);
        Assert.True(longSize > shortSize, "Longer text should produce larger matrix");
        _output.WriteLine($"Short: {shortResult.GetLength(0)}x{shortResult.GetLength(1)} ({shortSize} modules)");
        _output.WriteLine($"Long: {longResult.GetLength(0)}x{longResult.GetLength(1)} ({longSize} modules)");
    }

    [Fact]
    public void EncodeMatrix_ShouldHandleURL()
    {
        // Arrange
        const string testData = "HTTP://TEST.COM";

        // Act
        bool[,] result = CodeDmEncoder.EncodeMatrix(testData, Encoding.ASCII);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.GetLength(0) > 0);
        _output.WriteLine($"URL encoded to: {result.GetLength(0)} x {result.GetLength(1)}");
    }

    [Fact]
    public void EncodeMatrix_ShouldHandleEmail()
    {
        // Arrange
        const string testData = "TEST@EXAMPLE.COM";

        // Act
        bool[,] result = CodeDmEncoder.EncodeMatrix(testData, Encoding.ASCII);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.GetLength(0) > 0);
        _output.WriteLine($"Email encoded to: {result.GetLength(0)} x {result.GetLength(1)}");
    }
}
