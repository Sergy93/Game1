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

        public override void Update(GameTime theGameTime)
        {
            var currKeyboardState = Keyboard.GetState();

            if (currKeyboardState.GetPressedKeys().Count() == 0)
            {
                Speed = Vector2.Zero;
                Direction = Vector2.Zero;
            }

            MoveHorizontal(currKeyboardState);

            base.Update(theGameTime);

        }

        public void MoveHorizontal(KeyboardState keyboardState)
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

            if (keyboardState.IsKeyDown(Keys.Up))
            {
                Direction.Y = MoveDirections.MoveUp;

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
            else if (keyboardState.IsKeyDown(Keys.Down))
            {
                Speed.Y = MinSpeed;
                Direction.Y = MoveDirections.MoveDown;
            }
            Debug.WriteLine(Speed.Y);
        }


    }
}
