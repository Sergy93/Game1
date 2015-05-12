using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameHelpers;
using GameHelpers.Classes;
using GameHelpers.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace _02___Wizard
{
    public class Fireball : MovingSprite
    {
        private const string Assetname = "Fireball";

        public int MaxDistance { get; set; }

        public bool Visible;

        public Vector2 StartingPosition { get; set; }


        public Fireball()
            : base(Assetname, Vector2.Zero, Vector2.Zero, Vector2.Zero)
        {
        }


        public override void LoadContent(ContentManager theContentManager)
        {
            Scale = 0.3f;
            MaxDistance = 300;
            base.LoadContent(theContentManager);
        }

        public override void Update(GameTime theGameTime)
        {
            if (Vector2.Distance(StartingPosition, Position) > MaxDistance)
            {
                Visible = false;
            }

            if (Visible)
            {
                base.Update(theGameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var effect = Direction.X.Equals(1) ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            if (Visible)
            {
                base.Draw(spriteBatch, effect);
            }
        }

        public void Fire(Vector2 theStartPosition, Vector2 theSpeed, Vector2 theDirection)
        {
            Position = theStartPosition;
            StartingPosition = theStartPosition;
            Speed = theSpeed;
            Direction = theDirection;
            Visible = true;
        }
    }
}
