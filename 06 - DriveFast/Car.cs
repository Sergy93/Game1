using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using GameHelpers.Classes;
using GameHelpers.Classes.Attributes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace _06___DriveFast
{
    [AssetNameAttr("Car")]
    public class Car : MovingSprite
    {
        private const float scale = 0.3f;

        private const float MinSpeed = 100f;
        private const float MaxSpeed = 1000f;

        private const float Acceleration = 5f;

        public Car(Vector2 position, Vector2 speed, Vector2 direction)
            : base(position, speed, direction, scale)
        {
        }

        public bool Collides(Hazard hazard)
        {
            var rectCar = new Rectangle((int)Position.X, (int)Position.Y, Size.X, Size.Y);
            var rectHazard = new Rectangle((int)hazard.Position.X, (int)hazard.Position.Y, hazard.Size.X, hazard.Size.Y);

            return rectCar.Intersects(rectHazard);
        }

        public override void Update(GameTime theGameTime)
        {
            var currKeyboardState = Keyboard.GetState();

            Move(currKeyboardState);

            base.Update(theGameTime);

        }

        public void Move(KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                if (Position.X < -20)
                {
                    Speed.X = 0;
                }
                else
                {
                    Speed.X = MinSpeed;
                    Direction.X = MoveDirections.MoveLeft;
                }
            }
            else if (keyboardState.IsKeyDown(Keys.Right))
            {
                if (Position.X >= GraphicsManager.PreferredBackBufferWidth)
                {
                    Speed.X = 0;
                }
                else
                {
                    Speed.X = MinSpeed;
                    Direction.X = MoveDirections.MoveRight;
                }
            }
            else
            {
                Speed.X = 0;
            }

            if (keyboardState.IsKeyDown(Keys.Up))
            {
                Direction.Y = MoveDirections.MoveUp;

                if (Position.Y <= 0)
                {
                    Speed.Y = 0;
                }
                else
                {
                    if (Speed.Y == 0f)
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
