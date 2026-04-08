//-----------------------------------------------------------------------
// <copyright file="CodeQrEncoderOptions.cs" company="Zen Design Corp">
//     Copyright © Zen Design Corp 2012. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace T2t.Barcode.Core.CodeQr;

/// <summary>
/// Options for QR Code encoding (independent of rendering).
/// </summary>
public class CodeQrEncoderOptions
{
    /// <summary>
    /// Gets or sets the error correction level.
    /// </summary>
    public QrErrorCorrection ErrorCorrection { get; set; } = QrErrorCorrection.M;

    /// <summary>
    /// Gets or sets the encoding mode.
    /// </summary>
    public QrEncodeMode EncodeMode { get; set; } = QrEncodeMode.Byte;

    /// <summary>
    /// Gets or sets the QR version (1-40). 0 for automatic selection.
    /// </summary>
    public int Version { get; set; } = 7;

    /// <summary>
    /// Gets or sets the structure append N parameter (between 2 and 16 inclusive).
    /// </summary>
    public int StructureAppendN { get; set; } = 0;

    /// <summary>
    /// Gets or sets the structure append M parameter (between 1 and 16 inclusive).
    /// </summary>
    public int StructureAppendM { get; set; } = 0;

    /// <summary>
    /// Gets or sets the structure append parity (between 0 and 255 inclusive).
    /// </summary>
    public int StructureAppendParity { get; set; } = 0;
}
