//-----------------------------------------------------------------------
// 
//-----------------------------------------------------------------------

using System.Text;
using T2t.Barcode.Core.Dmtx;

namespace T2t.Barcode.Core.CodeDm;

/// <summary>
/// Provides DataMatrix encoding logic shared across all rendering platforms.
/// </summary>
public static class CodeDmEncoder
{
    /// <summary>
    /// Encodes text into a DataMatrix boolean matrix.
    /// </summary>
    /// <param name="text">The text to encode.</param>
    /// <param name="encoding">The character encoding to use.</param>
    /// <returns>A boolean matrix representing the DataMatrix code.</returns>
    public static bool[,] EncodeMatrix(string text, Encoding encoding)
    {
        DmtxEncoderOptions options = new() { Encoding = encoding };
        DmtxEncode encoder = new()
        {
            ModuleSize = options.ModuleSize,
            MarginSize = options.MarginSize,
            SizeIdxRequest = options.SizeIdx,
            Scheme = options.Scheme
        };

        byte[] valAsByteArray = encoder.GetRawDataAndSetEncoding(text, options);
        bool[,] matrix = encoder.CalculateDmCode(valAsByteArray);

        return matrix;
    }
}
