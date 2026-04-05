//-----------------------------------------------------------------------
// 
//-----------------------------------------------------------------------

using System.Text;
using T2t.Barcode.Core;
using T2t.Barcode.Core.CodeDm;

namespace T2t.Barcode.Svg;

public class CodeDmBarcodeDraw : BarcodeDraw
{
    public override sealed string Draw<T>(string text, T metrics)
    {
        if (string.IsNullOrEmpty(text)) throw new ArgumentNullException(nameof(text), "text cannot be null or empty.");
        if (metrics is not BarcodeMetricsDm mDm) throw new ArgumentException($"metrics must be of type {nameof(BarcodeMetricsDm)}.", nameof(metrics));

        Encoding enc = Code2dUtility.IsUnicode(text) ? Encoding.Unicode : Encoding.ASCII;
        return DrawDm(text, mDm, enc);
    }

    public override BarcodeMetrics GetDefaultMetrics(int maxHeight)
    {
        return new BarcodeMetricsDm
        {
            Scale = 4,
            ForegroundColor = "black",
            BackgroundColor = "white"
        };
    }

    public override BarcodeMetrics GetPrintMetrics(T2Size desiredBarcodeDimensions, T2Size printResolution, int barcodeCharLength)
    {
        return GetDefaultMetrics(30);
    }

    protected virtual string DrawDm(string text, BarcodeMetricsDm metrics, Encoding encoding)
    {
        bool[,] matrix = CodeDmEncoder.EncodeMatrix(text, encoding);
        int width = (int)(matrix.Length * metrics.Scale) + 1;
        int height = (int)(matrix.Length * metrics.Scale) + 1;

        List<string> rects = new()
        {
            string.Format(RectTmpl, 0, 0, width, height, metrics.BackgroundColor)
        };

        for (int j = 0; j < matrix.GetLength(1); j++)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                if (matrix[j, i])
                {
                    rects.Add(string.Format(RectTmpl, j * metrics.Scale, i * metrics.Scale, metrics.Scale, metrics.Scale, metrics.ForegroundColor));
                }
            }
        }
        return string.Format(SvgTmpl, matrix.Length * metrics.Scale + 1, matrix.Length * metrics.Scale + 1, string.Join('\n', rects));
    }
}

public class BarcodeMetricsDm : BarcodeMetricsDm<string>
{
}
