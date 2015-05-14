using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameHelpers.Classes.Attributes;
using Microsoft.Xna.Framework;

namespace GameHelpers.Classes
{
    [AssetNameAttr("")]
    public class FixedSprite : Sprite
    {
        public FixedSprite(Vector2 position)
            : base(position)
        {
        }
    }
}
