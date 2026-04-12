using System.Text;
using T2t.Barcode.Core.CodeQr;
using Xunit;

namespace T2t.Barcode.Core.Tests;

public class QrCodeTests
{
    private readonly ITestOutputHelper _output;

    public QrCodeTests(ITestOutputHelper output)
    {
        _output = output;
    }

    #region Encoder Matrix Tests

    [Fact]
    public void EncodeMatrix_ShouldProduceCorrectFinderPattern()
    {
        // Encode a simple text
        var options = new CodeQrEncoderOptions
        {
            Version = 1,  // Force version 1 for predictable size (21x21)
            EncodeMode = QrEncodeMode.Byte,
            ErrorCorrection = QrErrorCorrection.M
        };

        bool[][] matrix = CodeQrEncoder.EncodeMatrix("HELLO", Encoding.ASCII, options);

        // Matrix should be 21x21 for version 1
        Assert.Equal(21, matrix.Length);
        Assert.All(matrix, row => Assert.Equal(21, row.Length));

        // Print matrix for debugging
        var sb = new StringBuilder();
        sb.AppendLine($"Matrix size: {matrix.Length}x{matrix[0].Length}");
        sb.AppendLine("Matrix visualization (■ = black, □ = white):");

        // Print with Y as rows (standard orientation)
        for (int y = 0; y < matrix[0].Length; y++)
        {
            for (int x = 0; x < matrix.Length; x++)
            {
                sb.Append(matrix[x][y] ? "■" : "□");
            }
            sb.AppendLine();
        }

        _output.WriteLine(sb.ToString());

        // QR Code Version 1 has finder patterns at:
        // Top-left: (0,0) to (6,6)
        // Top-right: (14,0) to (20,6)
        // Bottom-left: (0,14) to (6,20)

        // Check top-left finder pattern (7x7 modules)
        // The pattern is: outer black square, white ring, inner black square
        // Positions (0,0), (0,6), (6,0), (6,6) should be black (corners of outer square)
        Assert.True(matrix[0][0], "Top-left corner of finder should be black");
        Assert.True(matrix[6][0], "Top-right corner of top-left finder should be black");
        Assert.True(matrix[0][6], "Bottom-left corner of top-left finder should be black");
        Assert.True(matrix[6][6], "Bottom-right corner of top-left finder should be black");

        // Position (3,3) should be black (center)
        Assert.True(matrix[3][3], "Center of top-left finder should be black");

        // Positions (2,1) should be white (part of white ring)
        Assert.False(matrix[2][1], "White ring of finder should be white");
    }

    #endregion

    #region Frame Data Tests

    [Fact]
    public void FrameData_ShouldHaveFinderPatternsInCorners()
    {
        // Generate a QR code and inspect the raw matrix
        var options = new CodeQrEncoderOptions
        {
            ErrorCorrection = QrErrorCorrection.L,
            Version = 1 // Force version 1 (21x21)
        };

        var matrix = CodeQrEncoder.EncodeMatrix("TEST", Encoding.ASCII, options);

        _output.WriteLine($"Matrix size: {matrix.Length}x{matrix[0].Length}");

        // Display top-left corner (should show finder pattern)
        _output.WriteLine("\nTop-left 10x10:");
        for (int i = 0; i < 10; i++)
        {
            var line = new StringBuilder();
            for (int j = 0; j < 10; j++)
            {
                line.Append(matrix[j][i] ? "■" : "□");
            }
            _output.WriteLine(line.ToString());
        }

        // Expected finder pattern (7x7):
        // ■■■■■■■
        // ■□□□□□■
        // ■□■■■□■
        // ■□■■■□■
        // ■□■■■□■
        // ■□□□□□■
        // ■■■■■■■

        // Check specific positions
        Assert.True(matrix[0][0], "Top-left corner should be black");
        Assert.True(matrix[6][0], "Top-right of finder should be black");
        Assert.True(matrix[0][6], "Bottom-left of finder should be black");
        Assert.False(matrix[1][1], "Inner gap should be white");
        Assert.True(matrix[3][3], "Center should be black");
    }

    [Fact]
    public void FrameData_Version1_ShouldContainAsciiCharacters()
    {
        // Access the Core assembly resources
        var assembly = typeof(CodeQrEncoder).Assembly;
        var resourceManager = new System.Resources.ResourceManager("T2t.Barcode.Core.CoreResources", assembly);

        var frameDataBytes = (byte[])resourceManager.GetObject("qrvfr1");
        Assert.NotNull(frameDataBytes);

        _output.WriteLine($"FrameData length: {frameDataBytes.Length}");
        _output.WriteLine($"Expected for version 1 (21x21): {21*21 + 21} = {462}");

        // Display first 50 bytes
        _output.WriteLine("\nFirst 50 bytes:");
        for (int i = 0; i < Math.Min(50, frameDataBytes.Length); i++)
        {
            byte b = frameDataBytes[i];
            char c = (char)b;
            _output.WriteLine($"[{i,3}] = 0x{b:X2} = {b,3} = '{(c >= 32 && c < 127 ? c : '?')}'");
        }

        // Count value types
        int count0 = 0, count1 = 0, count48 = 0, count49 = 0, countOther = 0;
        foreach (byte b in frameDataBytes)
        {
            if (b == 0) count0++;
            else if (b == 1) count1++;
            else if (b == 48) count48++; // '0'
            else if (b == 49) count49++; // '1'
            else countOther++;
        }

        _output.WriteLine($"\nCounts:");
        _output.WriteLine($"  0 (binary): {count0}");
        _output.WriteLine($"  1 (binary): {count1}");
        _output.WriteLine($"  48 ('0'):   {count48}");
        _output.WriteLine($"  49 ('1'):   {count49}");
        _output.WriteLine($"  Other:      {countOther}");

        // Determine format
        bool isBinary = (count0 + count1) > (count48 + count49);
        _output.WriteLine($"\nFormat: {(isBinary ? "BINARY (0/1)" : "ASCII ('0'/'1')")}");
    }

    #endregion

    #region Diagonal Debug Tests

    [Fact]
    public void QrCode_HttpExample_VisualInspection()
    {
        var options = new CodeQrEncoderOptions
        {
            ErrorCorrection = QrErrorCorrection.L
        };

        var matrix = CodeQrEncoder.EncodeMatrix("http://example.com", Encoding.ASCII, options);

        _output.WriteLine($"Matrix size: {matrix.Length}x{matrix[0].Length}\n");

        // Display full matrix
        _output.WriteLine("Full matrix visualization:");
        for (int y = 0; y < matrix[0].Length; y++)
        {
            var line = new StringBuilder();
            for (int x = 0; x < matrix.Length; x++)
            {
                line.Append(matrix[x][y] ? "■" : "□");
            }
            _output.WriteLine($"{y,2}: {line}");
        }

        _output.WriteLine("\n\nExpected finder patterns:");
        _output.WriteLine("Top-left (0,0):");
        _output.WriteLine("  ■■■■■■■");
        _output.WriteLine("  ■□□□□□■");
        _output.WriteLine("  ■□■■■□■");
        _output.WriteLine("  ■□■■■□■");
        _output.WriteLine("  ■□■■■□■");
        _output.WriteLine("  ■□□□□□■");
        _output.WriteLine("  ■■■■■■■");

        // Check if diagonal shift is present
        _output.WriteLine("\n\nDiagonal shift detection:");
        _output.WriteLine("If there's a diagonal shift, each row will be shifted by 1 position.");

        // Check top-left finder corners
        bool hasShift = false;
        if (!matrix[0][6])
        {
            _output.WriteLine("WARNING: matrix[0][6] is white (should be black) - possible vertical shift");
            hasShift = true;
        }
        if (!matrix[6][0])
        {
            _output.WriteLine("WARNING: matrix[6][0] is white (should be black) - possible horizontal shift");
            hasShift = true;
        }

        if (hasShift)
        {
            _output.WriteLine("\nDIAGONAL SHIFT DETECTED!");
        }
        else
        {
            _output.WriteLine("\nNo obvious diagonal shift detected in finder pattern corners.");
        }
    }

    #endregion
}
