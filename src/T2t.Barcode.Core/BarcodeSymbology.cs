namespace T2t.Barcode.Core;


/// <summary>
/// <c>BarcodeSymbology</c> defines the supported barcode symbologies.
/// </summary>
public enum BarcodeSymbology
{
    /// <summary>
    /// Unknown symbology.
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// Code 39 (aka Code 3 of 9) without checksum
    /// </summary>
    Code39NC = 1,

    /// <summary>
    /// Code 39 (aka Code 3 of 9) with checksum
    /// </summary>
    Code39C = 2,

    /// <summary>
    /// Code 93 with checksum
    /// </summary>
    Code93 = 3,

    /// <summary>
    /// Code 128 with checksum
    /// </summary>
    Code128 = 4,

    /// <summary>
    /// Code 11 without checksum
    /// </summary>
    Code11NC = 5,

    /// <summary>
    /// Code 11 with checksum
    /// </summary>
    Code11C = 6,

    /// <summary>
    /// Code EAN-13 with checksum
    /// </summary>
    CodeEan13 = 7,

    /// <summary>
    /// Code EAN-8 with checksum
    /// </summary>
    CodeEan8 = 8,

    /// <summary>
    /// Code 25 standard without checksum
    /// </summary>
    Code25StandardNC = 9,

    /// <summary>
    /// Code 25 standard with checksum
    /// </summary>
    Code25StandardC = 10,

    /// <summary>
    /// Code 25 interleaved without checksum
    /// </summary>
    Code25InterleavedNC = 11,

    /// <summary>
    /// Code 25 interleaved with checksum
    /// </summary>
    Code25InterleavedC = 12,

    /// <summary>
    /// Code PDF 417 (2D symbology with variable error correction)
    /// </summary>
    [Obsolete("Broken")]
    CodePdf417 = 13,

    /// <summary>
    /// Code QR (2D symbology with error correction)
    /// </summary>
    CodeQr = 14,
}
