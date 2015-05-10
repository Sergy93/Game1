using GameHelpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _02___Wizard
{
    class Wizard : Sprite
    {
        public Wizard()
            : base(Assetname, StartY, StartX)
        {
        }

        #region Constants

        private const string Assetname = "WizardSquare";

        private const int StartY = 125;
        private const int StartX = 245;

        private const int MoveSpeed = 160;

        private const int JumpHeight = 150;

        private const int MoveUp = -1;
        private const int MoveDown = 1;
        private const int MoveLeft = -1;
        private const int MoveRight = 1;

        #endregion


        private State currState = State.Walking;

        private Vector2 vDirection = Vector2.Zero;
        private Vector2 vSpeed = Vector2.Zero;

        //Used for jumping
        private Vector2 startingPosition;

        private KeyboardState prevKeyboardState;

        private enum State
        {
            Walking,
            Jumping,
            Ducking,
        }

        //LOAD
        public void LoadContent(ContentManager contentManager)
        {
            Position = new Vector2(StartY, StartX);

            base.LoadContent(contentManager);
            SourceOnSprite = new Rectangle(0, 0, 200, SourceOnSprite.Height);
        }


        //UPDATE
        public void Update(GameTime theGameTime)
        {
            var currKeyboardState = Keyboard.GetState();

            if (currState != State.Jumping)
            {
                vSpeed = Vector2.Zero;
                vDirection = Vector2.Zero;
            }

            UpdateHorizontalMovement(currKeyboardState);

            UpdateJump(currKeyboardState);

            UpdateDuck(currKeyboardState);

            prevKeyboardState = currKeyboardState;

            base.Update(theGameTime, vSpeed, vDirection);
        }

        //DRAW
        public void Draw(SpriteBatch spriteBatch)
        {
            var effect = SpriteEffects.None;
            if (vDirection.X == 1)
            {
                effect = SpriteEffects.FlipHorizontally;
            }
            base.Draw(spriteBatch, effect);
        }


        //MOVEMENT
        private void UpdateHorizontalMovement(KeyboardState currKeyboardState)
        {
            if (currKeyboardState.IsKeyDown(Keys.Left))
            {
                vSpeed.X = MoveSpeed;
                vDirection.X = MoveLeft;
            }
            else if (currKeyboardState.IsKeyDown(Keys.Right))
            {
                vSpeed.X = MoveSpeed;
                vDirection.X = MoveRight;
            }
        }


        //JUMP
        private void UpdateJump(KeyboardState currKeyboardState)
        {
            switch (currState)
            {
                case State.Walking:
                    if (currKeyboardState.IsKeyDown(Keys.Space) && !prevKeyboardState.IsKeyDown(Keys.Space))
                    {
                        currState = State.Jumping;

                        startingPosition = Position;

                        vDirection.Y = MoveUp;

                        vSpeed = new Vector2(MoveSpeed, MoveSpeed);
                    }
                    break;

                case State.Jumping:
                    if (startingPosition.Y - Position.Y > JumpHeight)
                    {
                        vDirection.Y = MoveDown;
                    }
                    if (Position.Y > startingPosition.Y)
                    {
                        Position.Y = startingPosition.Y;
                        currState = State.Walking;
                    }
                    break;
            }
        }


        //DUCK
        private void UpdateDuck(KeyboardState currKeyboardState)
        {
            if (currKeyboardState.IsKeyDown(Keys.LeftShift))
            {
                if (currState == State.Walking)
                {
                    SourceOnSprite = new Rectangle(200, 0, 200, SourceOnSprite.Height);
                    currState = State.Ducking;
                }
            }
            else if (currState == State.Ducking)
            {
                SourceOnSprite = new Rectangle(0, 0, 200, SourceOnSprite.Height);
                currState = State.Walking;
            }
        }

    }
}


