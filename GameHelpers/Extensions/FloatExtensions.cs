using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameHelpers.Extensions
{
    public static class FloatExtensions
    {
        private const float Epsilon = 0.0001f;

        public static bool FloatEquals(this float f1, float f2)
        {
            return Math.Abs(f1 - f2) < Epsilon;
        }
    }
}
