using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameHelpers.Classes;
using GameHelpers.Classes.Attributes;
using Microsoft.Xna.Framework;

namespace _06___DriveFast
{
    [AssetNameAttr("Hazard")]
    class Hazard : Sprite
    {
        public Hazard(Vector2 position, float scale = 1)
            : base(position, scale)
        {
        }
    }
}
