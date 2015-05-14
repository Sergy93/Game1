using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameHelpers.Classes
{
    public abstract class MovingSprite : Sprite
    {
        public Vector2 Speed;
        public Vector2 Direction;

        protected sealed internal class MoveDirections
        {
            public static float
                MoveUp = -1,
                MoveDown = 1,
                MoveLeft = -1,
                MoveRight = 1;
        }

        protected MovingSprite(Vector2 position, Vector2 speed, Vector2 direction, float scale = 1.0f)
            : base(position, scale)
        {
            Speed = speed;
            Direction = direction;
        }

        public override void Update(GameTime theGameTime)
        {
            var deltaTime = (float)theGameTime.ElapsedGameTime.TotalSeconds;

            Position += Direction * Speed * deltaTime;
        }
    }
}
