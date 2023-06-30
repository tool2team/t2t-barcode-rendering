namespace T2t.Barcode.Svg;

public struct T2Rect
{
    public int Top { get; set; }
    public int Left { get; set; }
    public int Right { get; set; }
    public int Bottom { get; set; }
    public int Width => Right - Left;
    public int Height => Bottom - Top;


    public T2Rect(int top, int left, int right, int bottom)
    {
        Top = top;
        Left = left;
        Right = right;
        Bottom = bottom;
    }
}