using System;
using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;
using System.Linq;
using GameHelpers.Classes;
using GameHelpers.Classes.Attributes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace _06___DriveFast
{
    [AssetNameAttr("Sprites/Road")]
    public class Road : MovingSprite
    {
        public Road(Vector2 speed, Vector2 position)
            : base(position, speed, new Vector2(1, 1))
        {

        }

    }
}
