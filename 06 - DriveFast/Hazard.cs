using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using GameHelpers.Classes;
using GameHelpers.Classes.Attributes;
using GameHelpers.Interfaces;
using Microsoft.Xna.Framework;

namespace _06___DriveFast
{
    [AssetNameAttr("Sprites/Hazard")]
    public class Hazard : MovingSprite
    {

        private readonly static Vector2 TheDirection = new Vector2(0, MoveDirections.MoveDown);

        public Hazard(Vector2 position, Vector2 speed)
            : base(position, speed, TheDirection, 0.6f)
        {
        }

    }
}
