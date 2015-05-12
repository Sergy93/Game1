using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameHelpers.Classes;
using Microsoft.Xna.Framework;

namespace _06___DriveFast
{
    public class Car : MovingSprite
    {
        public const string Assetname = "Car";

        private static readonly float scale = 0.3f;

        public Car(Vector2 position, Vector2 speed, Vector2 direction)
            : base(Assetname, position, speed, direction, scale)
        {
        }

    }
}
