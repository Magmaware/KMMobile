using System;
using System.Collections.Generic;
using System.Linq;
using WaveEngine.Common.Graphics;
using WaveEngine.Common.Math;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;

namespace KMMobile
{
    public class VectorDrawingLine
    {
        public Vector2 Start { get; set; }
        public Vector2 End { get; set; }
        public Color Color { get; set; }
    }

    public class VectorDrawing : Drawable2D
    {
        [RequiredComponent]
        public Transform2D trans2D;

        private List<VectorDrawingLine> Lines;

        public VectorDrawing()
        {
            Lines = new List<VectorDrawingLine>();
        }

        public void AddLine(
            Vector2 startPt,
            Vector2 endPt,
            Color color)
        {
            Lines.Add(
                new VectorDrawingLine { Start = startPt, End = endPt, Color = color });
        }

        public void AddEllipse(
            Vector2 startPt,
            Vector2 endPt,
            Color color,
            Vector2 centrePt,
            float majorRad,
            float minorRad,
            float angle,
            float maxLength,
            float chordTolerance)
        {
            var points = EnumEllipticalPoints(startPt, endPt, centrePt, majorRad, minorRad, angle, maxLength, chordTolerance).ToArray();
            for (int i = 0; i < points.Length - 1; i++)
                AddLine(points[i], points[i + 1], color);
        }

        private static double DegreesToRadians(double x)
        {
            return x * (Math.PI / 180.0);
        }

        private static bool Feq(double a, double b, double dTolerance = 0.0001)
        {
            if (a == b)
                return true;
            double m;
            if (a == 0 || b == 0)
                m = 1.0;
            else
            {
                var aa = a >= 0 ? a : -a;
                var ab = b >= 0 ? b : -b;
                m = Math.Max(aa, ab);
            }
            return (Math.Abs(a - b) <= (m * dTolerance));
        }

        private static int IRound(double x)
        {
            return (int)(Math.Floor(x + 0.5));
        }

        private static IEnumerable<Vector2> EnumEllipticalPoints(
            Vector2 startPt,
            Vector2 endPt,
            Vector2 centrePt,
            double majorRad,
            double minorRad,
            double angle,
            double maxLength,
            double chordTolerance)
        {
            var TWOPI = Math.PI * 2.0;
            var startAngle = Math.Atan2(startPt.Y - centrePt.Y, startPt.X - centrePt.X);
            var endAngle = Math.Atan2(endPt.Y - centrePt.Y, endPt.X - centrePt.X);
            var totalAngle = 0.0;
            if (startPt == endPt)
            {
                totalAngle = TWOPI;
            }
            else
            {
                totalAngle = endAngle - startAngle;
                if (totalAngle < 0) totalAngle += TWOPI;
            }
            var rr1 = majorRad / minorRad;
            var rr2 = minorRad / majorRad;
            var rr3 = minorRad * majorRad;
            var dth = DegreesToRadians(6.0);
            if (chordTolerance > 0 && chordTolerance < majorRad)
            {
                var dtmax = 2.0 * Math.Acos(1.0 - chordTolerance / majorRad);
                if (dth > dtmax) dth = dtmax;
            }
            var narc = IRound(totalAngle / dth * rr1);
            if (narc < 4) narc = 4;
            else if (narc > 1000) narc = 1000;
            var n2 = (int)(Math.Ceiling(majorRad * totalAngle / maxLength));
            if (narc < n2) narc = n2;
            dth = totalAngle / narc;
            var cdth = Math.Cos(dth);
            var sdth = Math.Sin(dth);
            var cosinc = Math.Cos(angle);
            var sininc = Math.Sin(angle);
            var axx = cdth + (sdth * cosinc * sininc * (rr1 - rr2));
            var z1 = minorRad * sininc;
            var z2 = majorRad * cosinc;
            var bxx = -sdth * ((z1 * z1) + (z2 * z2)) / rr3;
            z1 = minorRad * cosinc;
            z2 = majorRad * sininc;
            var cxx = sdth * ((z1 * z1) + (z2 * z2)) / rr3;
            var dxx = cdth + (sdth * cosinc * sininc * (rr2 - rr1));
            dxx -= (cxx * bxx) / axx;
            cxx /= axx;
            yield return startPt;
            if (dth == 0)
                yield break;
            var xtemp = (double)(startPt.X - centrePt.X);
            var ytemp = (double)(startPt.Y - centrePt.Y);
            var angp = 0.0;
            var ang = 0.0;
            var isPathHalfway = false;
            while (true)
            {
                xtemp = (axx * xtemp) + (bxx * ytemp);
                ytemp = (cxx * xtemp) + (dxx * ytemp);
                angp = ang;
                ang = Math.Atan2(ytemp, xtemp) - startAngle;
                if (ang < 0.0)
                    ang += TWOPI;
                if (ang < angp) break;
                if (isPathHalfway)
                {
                    if ((Math.Abs(ang) < 1.0e-4) || (Math.Abs(ang - TWOPI) < 1.0e-4))
                    {
                        ang = TWOPI;
                    }
                    if (ang > totalAngle || Feq(ang, totalAngle))
                    {
                        break;
                    }
                }
                else
                {
                    if (ang > totalAngle * 0.5)
                        isPathHalfway = true;
                }
                var point = new Vector2(
                    (float)(xtemp + centrePt.X),
                    (float)(ytemp + centrePt.Y));
                yield return point;
                if (xtemp == 0.0 && ytemp == 0.0)
                    break;
            }
            yield return endPt;
        }

        public void Fit(int width, int height)
        {
            if (Lines.Count > 0)
            {
                var minx = Lines.Min(x => Math.Min(x.Start.X, x.End.X));
                var miny = Lines.Min(x => Math.Min(x.Start.Y, x.End.Y));
                var maxx = Lines.Max(x => Math.Max(x.Start.X, x.End.X));
                var maxy = Lines.Max(x => Math.Max(x.Start.Y, x.End.Y));
                var extx = maxx - minx;
                var exty = maxy - miny;
                var scale = Math.Min(width / extx, height / exty);
                var xo = (width - (extx * scale)) / 2.0f;
                var yo = (height - (exty * scale)) / 2.0f;
                xo -= (width / 2.0f);
                yo += (height / 2.0f);
                foreach (var line in Lines)
                {
                    line.Start = new Vector2(
                        ((line.Start.X - minx) * scale) + xo,
                        (height + 1) - (((line.Start.Y - miny) * scale) + yo));
                    line.End = new Vector2(
                        ((line.End.X - minx) * scale) + xo,
                        (height + 1) - (((line.End.Y - miny) * scale) + yo));
                }
            }
        }

        public override void Draw(TimeSpan gameTime)
        {
            RenderManager.FindLayer(DefaultLayers.Alpha).AddDrawable(0, this);
        }
        
        protected override void DrawBasicUnit(int parameter)
        {
            foreach (var line in Lines)
                RenderManager.LineBatch2D.DrawLine(
                    new Vector2(
                        (line.Start.X * trans2D.XScale) + trans2D.X,
                        (line.Start.Y * trans2D.YScale) + trans2D.Y),
                    new Vector2(
                        (line.End.X * trans2D.XScale) + trans2D.X,
                        (line.End.Y * trans2D.YScale) + trans2D.Y),
                    line.Color);
        }

        protected override void Dispose(bool disposing)
        {
        }
    }
}
