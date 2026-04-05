//-----------------------------------------------------------------------
// 
//-----------------------------------------------------------------------

using SkiaSharp;
using System.Text;
using T2t.Barcode.Core;
using T2t.Barcode.Core.CodeDm;

namespace T2t.Barcode.Skia;

public class CodeDmBarcodeDraw : BarcodeDraw
{
    public override sealed SKBitmap Draw<T>(string text, T metrics)
    {
        if (string.IsNullOrEmpty(text)) throw new ArgumentNullException(nameof(text), "text cannot be null or empty.");
        if (metrics is not BarcodeMetricsDm mDm) throw new ArgumentException($"metrics must be of type {nameof(BarcodeMetricsDm)}.", nameof(metrics));

        Encoding enc = Code2dUtility.IsUnicode(text) ? Encoding.Unicode : Encoding.ASCII;
        return Encode(text, mDm, enc);
    }

    public override BarcodeMetrics GetDefaultMetrics(int maxHeight)
    {
        return new BarcodeMetricsDm
        {
            Scale = 4,
            ForegroundColor = SKColors.Black,
            BackgroundColor = SKColors.White
        };
    }

    public override BarcodeMetrics GetPrintMetrics(SKSizeI desiredBarcodeDimensions, SKSizeI printResolution, int barcodeCharLength)
    {
        return GetDefaultMetrics(30);
    }

    public virtual SKBitmap Encode(string content, BarcodeMetricsDm metrics, Encoding encoding)
    {
        bool[,] matrix = CodeDmEncoder.EncodeMatrix(content, encoding);

        SKPaint brush = new() { Color = metrics.BackgroundColor };
        SKBitmap image = new((int)(matrix.Length * metrics.Scale) + 1, (int)(matrix.Length * metrics.Scale) + 1);
        SKCanvas g = new(image);
        g.DrawRect(new SKRect(0, 0, image.Width, image.Height), brush);
        brush.Color = metrics.ForegroundColor;
        for (int j = 0; j < matrix.GetLength(1); j++)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                if (matrix[j,i])
                {
                    g.DrawRect(j * metrics.Scale, i * metrics.Scale, metrics.Scale, metrics.Scale, brush);
                }
            }
        }
        return image;
    }
}

public class BarcodeMetricsDm : BarcodeMetricsDm<SKColor>
{
}
