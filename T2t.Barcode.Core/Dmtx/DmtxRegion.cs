/*
DataMatrix.Net

DataMatrix.Net - .net library for decoding DataMatrix codes.
Copyright (C) 2009/2010 Michael Faschinger

This library is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public
License as published by the Free Software Foundation; either
version 3.0 of the License, or (at your option) any later version.
You can also redistribute and/or modify it under the terms of the
GNU Lesser General Public License as published by the Free Software
Foundation; either version 3.0 of the License or (at your option)
any later version.

This library is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
General Public License or the GNU Lesser General Public License 
for more details.

You should have received a copy of the GNU General Public
License and the GNU Lesser General Public License along with this 
library; if not, write to the Free Software Foundation, Inc., 
51 Franklin Street, Fifth Floor, Boston, MA  02110-1301  USA

Contact: Michael Faschinger - michfasch@gmx.at
 
*/

namespace T2t.Barcode.Core.Dmtx
{
    public class DmtxRegion
    {
        #region Constructors

        public DmtxRegion()
        {
        }

        public DmtxRegion(DmtxRegion src)
        {
            BottomAngle = src.BottomAngle;
            BottomKnown = src.BottomKnown;
            BottomLine = src.BottomLine;
            BottomLoc = src.BottomLoc;
            BoundMax = src.BoundMax;
            BoundMin = src.BoundMin;
            FinalNeg = src.FinalNeg;
            FinalPos = src.FinalPos;
            Fit2Raw = new DmtxMatrix3(src.Fit2Raw);
            FlowBegin = src.FlowBegin;
            JumpToNeg = src.JumpToNeg;
            JumpToPos = src.JumpToPos;
            LeftAngle = src.LeftAngle;
            LeftKnown = src.LeftKnown;
            LeftLine = src.LeftLine;
            LeftLoc = src.LeftLoc;
            LocR = src.LocR;
            LocT = src.LocT;
            MappingCols = src.MappingCols;
            MappingRows = src.MappingRows;
            OffColor = src.OffColor;
            OnColor = src.OnColor;
            Polarity = src.Polarity;
            Raw2Fit = new DmtxMatrix3(src.Raw2Fit);
            RightAngle = src.RightAngle;
            RightKnown = src.RightKnown;
            RightLoc = src.RightLoc;
            SizeIdx = src.SizeIdx;
            StepR = src.StepR;
            StepsTotal = src.StepsTotal;
            StepT = src.StepT;
            SymbolCols = src.SymbolCols;
            SymbolRows = src.SymbolRows;
            TopAngle = src.TopAngle;
            TopKnown = src.TopKnown;
            TopLoc = src.TopLoc;
        }
        #endregion

        #region Methods
        #endregion

        #region Properties

        public int JumpToPos { get; set; }

        public int JumpToNeg { get; set; }

        public int StepsTotal { get; set; }

        public DmtxPixelLoc FinalPos { get; set; }

        public DmtxPixelLoc FinalNeg { get; set; }

        public DmtxPixelLoc BoundMin { get; set; }

        public DmtxPixelLoc BoundMax { get; set; }

        public DmtxPointFlow FlowBegin { get; set; }

        public int Polarity { get; set; }

        public int StepR { get; set; }

        public int StepT { get; set; }

        public DmtxPixelLoc LocR { get; set; }

        public DmtxPixelLoc LocT { get; set; }

        public int LeftKnown { get; set; }

        public int LeftAngle { get; set; }

        public DmtxPixelLoc LeftLoc { get; set; }

        public DmtxBestLine LeftLine { get; set; }

        public int BottomKnown { get; set; }

        public int BottomAngle { get; set; }

        public DmtxPixelLoc BottomLoc { get; set; }

        public DmtxBestLine BottomLine { get; set; }

        public int TopKnown { get; set; }

        public int TopAngle { get; set; }

        public DmtxPixelLoc TopLoc { get; set; }

        public int RightKnown { get; set; }

        public int RightAngle { get; set; }

        public DmtxPixelLoc RightLoc { get; set; }

        public int OnColor { get; set; }

        public int OffColor { get; set; }

        public DmtxSymbolSize SizeIdx { get; set; }

        public int SymbolRows { get; set; }

        public int SymbolCols { get; set; }

        public int MappingRows { get; set; }

        public int MappingCols { get; set; }

        public DmtxMatrix3 Raw2Fit { get; set; }

        public DmtxMatrix3 Fit2Raw { get; set; }

        #endregion
    }
}
