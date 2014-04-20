using System;
using System.Linq;
using WaveEngine.Common.Graphics;
using WaveEngine.Common.Math;

namespace KMMobile.CF2
{
    public static class CF2Reader
    {
        public static void Read(string[] lines, VectorDrawing drawing, int width, int height)
        {
            var elements = lines.Where(s => s.StartsWith("L,") || s.StartsWith("A,"));
            foreach (var element in elements)
            {
                if (element.StartsWith("L,"))
                {
                    var parts = element.Split(',');
                    drawing.AddLine(
                        new Vector2(float.Parse(parts[4]), float.Parse(parts[5])),
                        new Vector2(float.Parse(parts[6]), float.Parse(parts[7])),
                        int.Parse(parts[1]) == 2 ? Color.Cyan : Color.Yellow);
                }
                else
                {
                    var parts = element.Split(',');
                    var startPt = new Vector2(float.Parse(parts[4]), float.Parse(parts[5]));
                    var endPt = new Vector2(float.Parse(parts[6]), float.Parse(parts[7]));
                    var centrePt = new Vector2(float.Parse(parts[8]), float.Parse(parts[9]));
                    var radius = (float)Math.Abs((centrePt - endPt).Length());
                    var arcDirection = int.Parse(parts[10]); // -1 is clockwise, 1 is anticlockwise
                    var arcAngle = (float)Math.Atan2(startPt.Y - centrePt.Y, startPt.X - centrePt.X);
                    var _startPt = (arcDirection == -1) ? endPt : startPt;
                    var _endPt = (arcDirection == -1) ? startPt : endPt;
                    if (_startPt != _endPt)
                    {
                        drawing.AddEllipse(
                            _startPt,
                            _endPt,
                            int.Parse(parts[1]) == 2 ? Color.Cyan : Color.Yellow,
                            centrePt,
                            radius,
                            radius,
                            arcAngle,
                            (float)(radius * ((Math.PI * 2.0) / 360.0)),
                            -float.MaxValue);
                    }
                }
            }
            drawing.Fit(width, height);
        }
    }
}
