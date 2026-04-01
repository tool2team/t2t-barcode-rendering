using SkiaSharp;
using Svg;
using System.ComponentModel;
using System.Text;
using T2t.Barcode.Core;

namespace T2t.Barcode.TestForms;

/// <summary>
/// <c>BarcodePanel</c> encapsulates a Windows Forms barcode control.
/// </summary>
public partial class BarcodePanel : Panel
{
    #region Private Fields
    private BarcodeSymbology _symbology;
    private string _lib;
    private int _maxBarHeight = 30;
    #endregion

    #region Public Constructors
    /// <summary>
    /// Initialises an instance of <see cref="T:BarcodePanel" />.
    /// </summary>
    public BarcodePanel()
    {
        InitializeComponent();
    }
    #endregion

    #region Public Properties
    /// <summary>
    /// Gets or sets the barcode symbology.
    /// </summary>
    /// <value>The symbology.</value>
    [Category("Appearance")]
    [DefaultValue((int)BarcodeSymbology.Unknown)]
    [TypeConverter(typeof(EnumConverter))]
    [Description("Gets/sets the barcode symbology used by this control.")]
    public BarcodeSymbology Symbology
    {
        get
        {
            return _symbology;
        }
        set
        {
            if (_symbology != value)
            {
                _symbology = value;
                RefreshBarcodeImage();
            }
        }
    }

    [Category("Appearance")]
    [DefaultValue("")]
    [Description("Gets/sets the rendering lib used by this control.")]
    public string Lib
    {
        get
        {
            return _lib;
        }
        set
        {
            if (_lib != value)
            {
                _lib = value;
                RefreshBarcodeImage();
            }
        }
    }

    /// <summary>
    /// Gets or sets the text associated with this control.
    /// </summary>
    /// <value></value>
    /// <returns>The text associated with this control.</returns>
    [Browsable(true)]
    public override string Text
    {
        get
        {
            return base.Text;
        }
        set
        {
            if (base.Text != value)
            {
                base.Text = value;
                RefreshBarcodeImage();
            }
        }
    }

    bool isHexa;
    [Browsable(true)]
    public bool IsHexa { get => isHexa; set { isHexa = value; RefreshBarcodeImage(); } }

    /// <summary>
    /// Gets or sets the maximum height of the rendered barcode.
    /// </summary>
    /// <value>The maximum height of a barcode bar in pixels.</value>
    [Category("Appearance")]
    [DefaultValue(30)]
    [Description("Gets/sets the maximum height of the rendered barcode bars.")]
    public int MaxBarHeight
    {
        get
        {
            return _maxBarHeight;
        }
        set
        {
            if (_maxBarHeight != value)
            {
                _maxBarHeight = value;
                RefreshBarcodeImage();
            }
        }
    }
    #endregion

    #region Private Methods
    static string HexToString(string hexString)
    {
        StringBuilder sb = new();

        for (int i = 0; i < hexString.Length; i += 2)
        {
            string hexChar = hexString.Substring(i, 2);
            short asciiValue = Convert.ToInt16(hexChar, 16);
            sb.Append((char)asciiValue);
        }

        return sb.ToString();
    }

    private void RefreshBarcodeImage()
    {
        // Allocate new barcode image as needed
        if (_symbology != BarcodeSymbology.Unknown && !string.IsNullOrEmpty(Text))
        {
            try
            {
                string text = IsHexa ? HexToString(Text) : Text;
                string pngFile = SanitizeFilename($"test-{Text}-{Lib}.png");
                string svgFile = SanitizeFilename($"test-{Text}-{Lib}.svg");

                switch (_lib)
                {
                    case "Drawing":
                        var drawObject1 = Drawing.BarcodeDrawFactory.GetSymbology(_symbology);
                        var metrics1 = drawObject1.GetDefaultMetrics(_maxBarHeight);
                        metrics1.Scale = 2;
                        BackgroundImage = drawObject1.Draw(text, metrics1);
                        BackgroundImage.Save(pngFile, System.Drawing.Imaging.ImageFormat.Png);
                        break;
                    case "Skia":
                        var drawObject2 = Skia.BarcodeDrawFactory.GetSymbology(_symbology);
                        var metrics2 = drawObject2.GetDefaultMetrics(_maxBarHeight);
                        metrics2.Scale = 2;
                        {
                            SKBitmap skBitmap = drawObject2.Draw(text, metrics2);
                            using var skImage = SKImage.FromBitmap(skBitmap);
                            using var data = skImage.Encode(SKEncodedImageFormat.Png, 100);
                            using var stream = data.AsStream();
                            BackgroundImage = Image.FromStream(stream);
                            BackgroundImage.Save(pngFile, System.Drawing.Imaging.ImageFormat.Png);
                        }
                        break;
                    case "Svg":
                        var drawObject3 = Svg.BarcodeDrawFactory.GetSymbology(_symbology);
                        var metrics3 = drawObject3.GetDefaultMetrics(_maxBarHeight);
                        metrics3.Scale = 2;
                        string svgStr = drawObject3.Draw(text, metrics3);
                        SvgDocument svgDoc = SvgDocument.FromSvg<SvgDocument>(svgStr);
                        Bitmap bitmap = svgDoc.Draw();
                        BackgroundImage = bitmap;
                        BackgroundImage.Save(pngFile, System.Drawing.Imaging.ImageFormat.Png);
                        File.WriteAllText(svgFile, svgStr);
                        break;
                    default:
                        BackgroundImage = null;
                        break;
                }
            }
            catch
            {
                BackgroundImage = null;
            }
        }
        else
        {
            BackgroundImage = null;
        }

        // Setup the auto-scroll minimum size
        if (BackgroundImage == null)
        {
            AutoScrollMinSize = new Size(0, 0);
        }
        else
        {
            AutoScrollMinSize = BackgroundImage.Size;
        }
    }
    #endregion

    string SanitizeFilename(string origFileName)
    {
        var invalids = Path.GetInvalidFileNameChars();
        var newName = string.Join("_", origFileName.Split(invalids, StringSplitOptions.RemoveEmptyEntries));

        return newName;
    }
}
