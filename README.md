# Barcode Rendering Library

The bar-code rendering library quite simply encapsulates the native rendering of barcode symbologies without requiring third-party fonts.

The framework makes it easy to define new symbologies and use the resultant images from web or forms applications in a variety of usage scenarios.

Currently supported bar-code symbologies;

* Code 11 with or without checksum
* Code 25 standard/interleaved with or without checksum
* Code 39 with or without checksum
* Code 93 with checksum only
* Code 128 with checksum only
* Code EAN 13 with checksum only
* Code EAN 8 with checksum only
* Code PDF417 2D (still alpha – still broken)
* Code QR 2D (new)

## Integration

* T2t.Barcode.Drawing : windows only depending on System.Drawing.Common
* T2t.Barcode.Skia : multi platform depending on SkiaSharp
* T2t.Barcode.Svg : multi platform, no dependency
