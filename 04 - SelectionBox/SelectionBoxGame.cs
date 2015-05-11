using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _04___SelectionBox
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class SelectionBoxGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D mDottedLine;

        Rectangle mSelectionBox;
        MouseState prevMouseState;

        public SelectionBoxGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            IsMouseVisible = true;

            mSelectionBox = new Rectangle(-1, -1, 0, 0);

            prevMouseState = Mouse.GetState();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            mDottedLine = Content.Load<Texture2D>("DottedLine");
        }

        protected override void UnloadContent()
        {

        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            var mouseState = Mouse.GetState();

            if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
            {
                mSelectionBox = new Rectangle(mouseState.X, mouseState.Y, 0, 0);
            }

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                mSelectionBox = new Rectangle(mSelectionBox.X, mSelectionBox.Y, mouseState.X - mSelectionBox.X, mouseState.Y - mSelectionBox.Y);
            }

            if (mouseState.LeftButton == ButtonState.Released)
            {
                mSelectionBox = new Rectangle(-1, -1, 0, 0);
            }

            prevMouseState = mouseState;


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            DrawHorizontalLine(mSelectionBox.Y);
            DrawHorizontalLine(mSelectionBox.Y + mSelectionBox.Height);

            DrawVerticalLine(mSelectionBox.X);
            DrawVerticalLine(mSelectionBox.X + mSelectionBox.Width);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawHorizontalLine(int thePositionY)
        {
            if (mSelectionBox.Width > 0)
            {
                for (var aCounter = 0; aCounter <= mSelectionBox.Width - 10; aCounter += 10)
                {
                    if (mSelectionBox.Width - aCounter < 0) continue;

                    spriteBatch.Draw(mDottedLine, new Rectangle(mSelectionBox.X + aCounter, thePositionY, 10, 5), Color.White);
                }
            }
            else if (mSelectionBox.Width < 0)
            {
                for (var aCounter = -10; aCounter >= mSelectionBox.Width; aCounter -= 10)
                {
                    if (mSelectionBox.Width - aCounter > 0) continue;

                    spriteBatch.Draw(mDottedLine, new Rectangle(mSelectionBox.X + aCounter, thePositionY, 10, 5), Color.White);
                }
            }
        }

        private void DrawVerticalLine(int thePositionX)
        {
            if (mSelectionBox.Height > 0)
            {
                for (var aCounter = 0; aCounter <= mSelectionBox.Height - 10; aCounter += 10)
                {
                    if (mSelectionBox.Height - aCounter < 0) continue;

                    spriteBatch.Draw(mDottedLine, new Rectangle(thePositionX, mSelectionBox.Y + aCounter, 10, 5), Color.White);
                }
            }
            else if (mSelectionBox.Height < 0)
            {
                for (var aCounter = -10; aCounter >= mSelectionBox.Height; aCounter -= 10)
                {
                    if (mSelectionBox.Height - aCounter > 0) continue;

                    spriteBatch.Draw(mDottedLine, new Rectangle(thePositionX, mSelectionBox.Y + aCounter, 10, 5), Color.White);
                }
            }
        }
    }
}
