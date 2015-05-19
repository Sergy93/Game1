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
    [AssetNameAttr("Hazard")]
    public class Hazard : MovingSprite, ISolid
    {

        private readonly static Vector2 direction = new Vector2(0, MoveDirections.MoveDown);

        public Hazard(Vector2 position, Vector2 speed)
            : base(position, speed, direction, 0.7f)
        {
        }

    }
}
