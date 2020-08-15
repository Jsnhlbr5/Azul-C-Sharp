using System;
using System.Drawing;

namespace AzulApp
{
    static class Utils
    {
        internal static Bitmap getImage(this Color c)
        {
            return (Bitmap)Properties.Resources.ResourceManager.GetObject(c.ToString());
        }

        internal static Point Scale(this Point p, float f)
        {
            int x = (int)(p.X * f);
            int y = (int)(p.Y * f);
            return new Point(x, y);
        }

        internal static double rand()
        {
            return random.NextDouble();
        }

        private static readonly Random random = new Random();
    }
}
