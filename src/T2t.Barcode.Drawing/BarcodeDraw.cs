//-----------------------------------------------------------------------
// <copyright file="BarcodeDraw.cs" company="Zen Design Corp">
//     Copyright © Zen Design Corp 2008 - 2012. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace T2t.Barcode.Drawing;

	using System;
	using System.Drawing;

	/// <summary>
	/// <c>BarcodeMetrics</c> defines the measurement metrics used to render
	/// a barcode.
	/// </summary>
	[Serializable]
	public abstract class BarcodeMetrics
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="BarcodeMetrics"/> class.
		/// </summary>
		protected BarcodeMetrics()
		{
			Scale = 1;
		}

		/// <summary>
		/// Gets or sets the scale factor used to render a barcode.
		/// </summary>
		/// <value>The scale.</value>
		/// <remarks>
		/// When applied to a 1D barcode the scale is used to scale the width
		/// of barcode elements not the height.
		/// When applied to a 2D barcode the scale adjusts both width and height
		/// of barcode elements.
		/// </remarks>
		public int Scale
		{
			get;
			set;
		}
	}

	/// <summary>
	/// <c>BarcodeMetrics1d</c> defines the measurement metrics used to render
	/// a 1 dimensional barcode.
	/// </summary>
	[Serializable]
	public class BarcodeMetrics1d : BarcodeMetrics
	{
		#region Private Fields
		private int _minWidth;
		private int _maxWidth;
		private int _minHeight;
		private int _maxHeight;
		private int? _interGlyphSpacing;
		private bool _renderVertically;
		#endregion

		#region Public Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="BarcodeMetrics1d"/> class.
		/// </summary>
		public BarcodeMetrics1d()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BarcodeMetrics1d"/> class.
		/// </summary>
		/// <param name="width"></param>
		/// <param name="height"></param>
		public BarcodeMetrics1d(int width, int height)
		{
			_minWidth = _maxWidth = width;
			_minHeight = _maxHeight = height;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BarcodeMetrics1d"/> class.
		/// </summary>
		/// <param name="minWidth"></param>
		/// <param name="maxWidth"></param>
		/// <param name="height"></param>
		public BarcodeMetrics1d(int minWidth, int maxWidth, int height)
		{
			_minWidth = minWidth;
			_maxWidth = maxWidth;
			_minHeight = _maxHeight = height;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="BarcodeMetrics1d"/> class.
		/// </summary>
		/// <param name="minWidth"></param>
		/// <param name="maxWidth"></param>
		/// <param name="minHeight"></param>
		/// <param name="maxHeight"></param>
		public BarcodeMetrics1d(
			int minWidth, int maxWidth, int minHeight, int maxHeight)
		{
			_minWidth = minWidth;
			_maxWidth = maxWidth;
			_minHeight = minHeight;
			_maxHeight = maxHeight;
		}
		#endregion

		#region Public Properties
		/// <summary>
		/// Gets/sets the minimum bar width.
		/// </summary>
		public int MinWidth
		{
			get
			{
				return _minWidth;
			}
			set
			{
				_minWidth = value;
			}
		}

		/// <summary>
		/// Gets/sets the maximum bar width.
		/// </summary>
		public int MaxWidth
		{
			get
			{
				return _maxWidth;
			}
			set
			{
				_maxWidth = value;
			}
		}

		/// <summary>
		/// Gets/sets the minimum bar height.
		/// </summary>
		public int MinHeight
		{
			get
			{
				return _minHeight;
			}
			set
			{
				_minHeight = value;
			}
		}

		/// <summary>
		/// Gets/sets the maximum bar height.
		/// </summary>
		public int MaxHeight
		{
			get
			{
				return _maxHeight;
			}
			set
			{
				_maxHeight = value;
			}
		}

		/// <summary>
		/// Gets/sets the amount of inter-glyph spacing to apply.
		/// </summary>
		/// <remarks>
		/// By default this is set to -1 which forces the barcode drawing
		/// classes to use the default value specified by the symbology.
		/// </remarks>
		public int? InterGlyphSpacing
		{
			get
			{
				return _interGlyphSpacing;
			}
			set
			{
				_interGlyphSpacing = value;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether to render the barcode vertically.
		/// </summary>
		/// <value>
		/// <c>true</c> to render barcode vertically; otherwise, <c>false</c>.
		/// </value>
		public bool RenderVertically
		{
			get
			{
				return _renderVertically;
			}
			set
			{
				_renderVertically = value;
			}
		}
		#endregion
	}

	/// <summary>
	/// <c>BarcodeMetrics2d</c> defines the measurement metrics used to render
	/// a 2 dimensional barcode.
	/// </summary>
	[Serializable]
	public class BarcodeMetrics2d : BarcodeMetrics
	{
	}

	/// <summary>
	/// <c>BarcodeDraw</c> is an abstract base class for all barcode drawing
	/// classes.
	/// </summary>
	public abstract class BarcodeDraw
	{
		#region Protected Constructors
		/// <summary>
		/// Initializes a new instance of <see cref="T:T2t.Barcode.Drawing.BarcodeDraw"/> class.
		/// </summary>
		protected BarcodeDraw()
		{
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Draws the specified text using the supplied barcode metrics.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <param name="metrics">A <see cref="T:T2t.Barcode.Drawing.BarcodeMetrics"/> object.</param>
		/// <returns>
		/// An <see cref="Image"/> object containing the rendered barcode.
		/// </returns>
		public abstract Image Draw(string text, BarcodeMetrics metrics);

		/// <summary>
		/// Draws the specified text using the default barcode metrics for
		/// the specified maximum barcode height.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <param name="maxBarHeight">The maximum bar height.</param>
		/// <returns>
		/// An <see cref="Image"/> object containing the rendered barcode.
		/// </returns>
		public Image Draw(string text, int maxBarHeight)
		{
			BarcodeMetrics defaultMetrics = GetDefaultMetrics(maxBarHeight);
			return Draw(text, defaultMetrics);
		}

		/// <summary>
		/// Draws the specified text using the default barcode metrics for
		/// the specified maximum barcode height.
		/// </summary>
		/// <param name="text">The text.</param>
		/// <param name="maxBarHeight">The maximum bar height.</param>
		/// <param name="scale">
		/// The scale factor to use when rendering the barcode.
		/// </param>
		/// <returns>
		/// An <see cref="Image"/> object containing the rendered barcode.
		/// </returns>
		public Image Draw(string text, int maxBarHeight, int scale)
		{
			BarcodeMetrics defaultMetrics = GetDefaultMetrics(maxBarHeight);
			defaultMetrics.Scale = scale;
			return Draw(text, defaultMetrics);
		}

		/// <summary>
		/// Gets a <see cref="T:T2t.Barcode.Drawing.BarcodeMetrics"/> object containing default
		/// settings for the specified maximum bar height.
		/// </summary>
		/// <param name="maxHeight">The maximum barcode height.</param>
		/// <returns>A <see cref="T:T2t.Barcode.Drawing.BarcodeMetrics"/> object.</returns>
		public abstract BarcodeMetrics GetDefaultMetrics(int maxHeight);

		/// <summary>
		/// Gets a <see cref="T:BarcodeMetrics"/> object containing the print
		/// metrics needed for printing a barcode of the specified physical
		/// size on a device operating at the specified resolution.
		/// </summary>
		/// <param name="desiredBarcodeDimensions">The desired barcode dimensions in hundredth of an inch.</param>
		/// <param name="printResolution">The print resolution in pixels per inch.</param>
		/// <param name="barcodeCharLength">Length of the barcode in characters.</param>
		/// <returns>A <see cref="T:T2t.Barcode.Drawing.BarcodeMetrics"/> object.</returns>
		public abstract BarcodeMetrics GetPrintMetrics(
			Size desiredBarcodeDimensions, Size printResolution,
			int barcodeCharLength);
		#endregion
	}
