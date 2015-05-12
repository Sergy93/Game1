using System;
using System.Collections.Generic;
using System.Linq;
using GameHelpers.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace _06___DriveFast
{
    public class Road : MovingSprite
    {

        //Float value is the y position
        public const string Asset = "Road";

        public Road(Vector2 speed, Vector2 position)
            : base(Asset, position, speed, new Vector2(1, 1))
        {
        }

    }
}
