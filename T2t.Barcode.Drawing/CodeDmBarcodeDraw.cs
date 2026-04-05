//-----------------------------------------------------------------------
// 
//-----------------------------------------------------------------------

using System.Drawing;
using System.Text;
using T2t.Barcode.Core;
using T2t.Barcode.Core.CodeDm;

namespace T2t.Barcode.Drawing;

public class CodeDmBarcodeDraw : BarcodeDraw
{
    public override sealed System.Drawing.Image Draw<T>(string text, T metrics)
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
            ForegroundColor = Color.Black,
            BackgroundColor = Color.White
        };
    }

    public override BarcodeMetrics GetPrintMetrics(Size desiredBarcodeDimensions, Size printResolution, int barcodeCharLength)
    {
        throw new NotImplementedException();
    }

    protected virtual System.Drawing.Image DrawDm(string text, BarcodeMetricsDm metrics, Encoding encoding)
    {
        bool[,] matrix = CodeDmEncoder.EncodeMatrix(text, encoding);
        int width = (int)(matrix.Length * metrics.Scale) + 1;
        int height = (int)(matrix.Length * metrics.Scale) + 1;

        Bitmap image = new(width, height);
        using Graphics g = Graphics.FromImage(image);
        using SolidBrush brush = new(metrics.BackgroundColor);

        g.FillRectangle(brush, 0, 0, width, height);
        brush.Color = metrics.ForegroundColor;

        for (int j = 0; j < matrix.GetLength(1); j++)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                if (matrix[j, i])
                {
                    g.FillRectangle(brush, j * metrics.Scale, i * metrics.Scale, metrics.Scale, metrics.Scale);
                }
            }
        }

        return image;
    }
}

public class BarcodeMetricsDm : BarcodeMetricsDm<Color>
{
}
