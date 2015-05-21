using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using GameHelpers.Classes;
using GameHelpers.Classes.Attributes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using GameHelpers.Extensions;
using GameHelpers.Interfaces;


namespace _06___DriveFast
{
    [AssetNameAttr("Sprites/Car")]
    public class Car : MovingSprite, ICollider
    {
        private const float TheScale = 0.3f;

        private const float MinSpeed = 100f;
        private const float MaxSpeed = 1000f;

        private const float Acceleration = 5f;

        private readonly GraphicsDeviceManager theGraphics;

        public Car(Vector2 position, Vector2 speed, Vector2 direction)
            : base(position, speed, direction, TheScale)
        {
            theGraphics = GameServices.GetService<GraphicsDeviceManager>();
        }

        public bool CheckCollisions(Sprite hazard)
        {
            return Size.Intersects(hazard.Size);
        }

        public override void Update(GameTime theGameTime)
        {
            var currKeyboardState = Keyboard.GetState();

            Move(currKeyboardState);


            var fixedX = MathHelper.Clamp(Position.X, 0, theGraphics.PreferredBackBufferWidth - Size.Width / 2);
            var fixedY = MathHelper.Clamp(Position.Y, 0, theGraphics.PreferredBackBufferHeight - Size.Height / 2);

            Position = new Vector2(fixedX, fixedY);

            base.Update(theGameTime);

        }

        public void Move(KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                Speed.X = MinSpeed;
                Direction.X = MoveDirections.MoveLeft;

            }
            else if (keyboardState.IsKeyDown(Keys.Right))
            {
                Speed.X = MinSpeed;
                Direction.X = MoveDirections.MoveRight;

            }
            else
            {
                Speed.X = 0;
            }

            if (keyboardState.IsKeyDown(Keys.Up))
            {
                Direction.Y = MoveDirections.MoveUp;

                if (Speed.Y.FloatEquals(0f))
                {
                    Speed.Y = MinSpeed;
                }
                else if (Speed.Y <= MaxSpeed)
                {
                    Speed.Y += Acceleration;
                }
                else
                {
                    Speed.Y = MaxSpeed;
                }

            }
            else if (keyboardState.IsKeyDown(Keys.Down))
            {
                if (Position.Y >= GraphicsManager.PreferredBackBufferWidth)
                {
                    Speed.Y = 0;
                }
                else
                {
                    Speed.Y = MinSpeed;
                    Direction.Y = MoveDirections.MoveDown;
                }
            }
            else
            {
                Speed.Y = 0;
            }

        }
    }
}
