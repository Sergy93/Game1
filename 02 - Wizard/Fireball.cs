using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameHelpers;
using GameHelpers.Classes;
using GameHelpers.Classes.Attributes;
using GameHelpers.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace _02___Wizard
{
    [AssetNameAttr("Fireball")]
    public class Fireball : MovingSprite
    {
        public int MaxDistance { get; set; }

        public bool NeedsRemoval;

        public Vector2 StartingPosition { get; set; }

        public Fireball(Vector2 theStartPosition, Vector2 theSpeed, Vector2 theDirection)
            : base(theStartPosition, theSpeed, theDirection)
        {
            StartingPosition = theStartPosition;
        }

        public override void LoadContent()
        {
            Scale = 0.3f;
            MaxDistance = 300;
            base.LoadContent();
        }

        public override void Update(GameTime theGameTime)
        {
            base.Update(theGameTime);

            if (Vector2.Distance(StartingPosition, Position) > MaxDistance)
            {
                NeedsRemoval = true;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var effect = Direction.X.Equals(1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            base.Draw(spriteBatch, effect);
        }

    }
}
