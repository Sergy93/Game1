using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameHelpers.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameHelpers.Classes
{
    public abstract class MovingSprite : Sprite
    {
        public Vector2 Speed;
        public Vector2 Direction;

        private float rotation;
        public float Rotation
        {
            get { return rotation; }
            set
            {
                rotation = value;
                //var rotate = Matrix.CreateRotationY(rotation);
                //var transformedPoint = Vector2.Transform(new Vector2(Size.X, Size.Y), rotate);

                //Size = new Rectangle((int)transformedPoint.X, (int)transformedPoint.Y, Size.Width, Size.Height);
            }
        }

        protected internal sealed class MoveDirections
        {
            public static float
                MoveUp = -1,
                MoveDown = 1,
                MoveLeft = -1,
                MoveRight = 1;
        }

        protected MovingSprite(Vector2 position, Vector2 speed, Vector2 direction,
            float scale = 1.0f, float rotation = 1.0f)
            : base(position, scale)
        {
            Speed = speed;
            Direction = direction;
            Rotation = rotation;
        }

        public override void Update(GameTime theGameTime)
        {
            var deltaTime = (float)theGameTime.ElapsedGameTime.TotalSeconds;

            Position += Direction * Speed * deltaTime;
        }

        //DRAW
        public override void Draw(SpriteBatch spriteBatch)
        {
            Draw(spriteBatch, SpriteEffects.None);
        }

        public override void Draw(SpriteBatch spriteBatch, SpriteEffects effect)
        {
            spriteBatch.Draw(SpriteTexture, Position, SourceOnSprite,
                Color.White,
                Rotation,
                new Vector2(Size.Width / 2, Size.Height / 2),
                Scale, effect, 0);
        }
    }
}
