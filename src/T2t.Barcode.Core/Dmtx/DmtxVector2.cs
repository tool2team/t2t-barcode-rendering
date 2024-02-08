﻿/*
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
    public class DmtxVector2
    {
        #region Constructors
        public DmtxVector2()
        {
            X = 0.0;
            Y = 0.0;
        }

        public DmtxVector2(double x, double y)
        {
            X = x;
            Y = y;
        }
        #endregion

        #region Operators
        public static DmtxVector2 operator +(DmtxVector2 v1, DmtxVector2 v2)
        {
            DmtxVector2 result = new DmtxVector2(v1.X, v1.Y);
            result.X += v2.X;
            result.Y += v2.Y;
            return result;
        }

        public static DmtxVector2 operator -(DmtxVector2 v1, DmtxVector2 v2)
        {
            DmtxVector2 result = new DmtxVector2(v1.X, v1.Y);
            result.X -= v2.X;
            result.Y -= v2.Y;
            return result;
        }

        public static DmtxVector2 operator *(DmtxVector2 v1, double factor)
        {
            return new DmtxVector2(v1.X * factor, v1.Y * factor);
        }
        #endregion

        #region Methods
        public double Cross(DmtxVector2 v2)
        {
            return X * v2.Y - Y * v2.X;
        }

        public double Norm()
        {
            double mag = Mag();
            if (mag <= DmtxConstants.DmtxAlmostZero)
            {
                return -1.0; // FIXXXME: This doesn't look clean, as noted in original dmtx source
            }
            X /= mag;
            Y /= mag;
            return mag;
        }

        public double Dot(DmtxVector2 v2)
        {
            return Math.Sqrt(X * v2.X + Y * v2.Y);
        }

        public double Mag()
        {
            return Math.Sqrt(X * X + Y * Y);
        }

        public double DistanceFromRay2(DmtxRay2 ray)
        {
            if (Math.Abs(1.0 - ray.V.Mag()) > DmtxConstants.DmtxAlmostZero)
            {
                throw new ArgumentException("DistanceFromRay2: The ray's V vector must be a unit vector");
            }
            return ray.V.Cross(this - ray.P);
        }

        public double DistanceAlongRay2(DmtxRay2 ray)
        {
            if (Math.Abs(1.0 - ray.V.Mag()) > DmtxConstants.DmtxAlmostZero)
            {
                throw new ArgumentException("DistanceAlongRay2: The ray's V vector must be a unit vector");
            }
            return (this - ray.P).Dot(ray.V);
        }

        public bool Intersect(DmtxRay2 p0, DmtxRay2 p1)
        {
            double denominator = p1.V.Cross(p0.V);
            if (Math.Abs(denominator) < DmtxConstants.DmtxAlmostZero)
            {
                return false;
            }
            double numerator = p1.V.Cross(p1.P - p0.P);
            return PointAlongRay2(p0, numerator / denominator);
        }

        public bool PointAlongRay2(DmtxRay2 ray, double t)
        {
            if (Math.Abs(1.0 - ray.V.Mag()) > DmtxConstants.DmtxAlmostZero)
            {
                throw new ArgumentException("PointAlongRay: The ray's V vector must be a unit vector");
            }
            DmtxVector2 tmp = new DmtxVector2(ray.V.X * t, ray.V.Y * t);
            X = ray.P.X + tmp.X;
            Y = ray.P.Y + tmp.Y;
            return true;
        }
        #endregion

        #region Properties

        public double X { get; set; }

        public double Y { get; set; }

        #endregion
    }
}
