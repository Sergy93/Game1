using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _05___HealthBar
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class HealthBarGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D mHealthBar;

        private int BarHeight = 45;
        private float Health = 90;

        public HealthBarGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }


        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            mHealthBar = Content.Load<Texture2D>("HealthBar");
        }

        protected override void UnloadContent()
        {

        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            var mKeys = Keyboard.GetState();

            if (mKeys.IsKeyDown(Keys.Up) == true)
            {
                Health += 1;
            }

            //If the Down Arrowis pressed, decrease the Health bar
            if (mKeys.IsKeyDown(Keys.Down) == true)
            {
                Health -= 1;
            }

            Health = (int)MathHelper.Clamp(Health, 0, 100);



            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            //Draw the health for the health bar

            var healthToDraw = (int)(mHealthBar.Width * (Health / 100));

            spriteBatch.Draw(mHealthBar,
                new Rectangle(Window.ClientBounds.Width / 2 - mHealthBar.Width / 2, 30, healthToDraw, BarHeight - 1),
                new Rectangle(0, BarHeight, healthToDraw, 44),
                Color.Red);

            spriteBatch.Draw(mHealthBar,
                new Rectangle(Window.ClientBounds.Width / 2 - mHealthBar.Width / 2 + healthToDraw, 30, mHealthBar.Width - healthToDraw, BarHeight - 1),
                new Rectangle(0, BarHeight, healthToDraw, 44),
                Color.Gray);


            //Draw the box around the health bar
            spriteBatch.Draw(mHealthBar,
                new Rectangle(Window.ClientBounds.Width / 2 - mHealthBar.Width / 2, 30, mHealthBar.Width, BarHeight),
                new Rectangle(0, 0, mHealthBar.Width, BarHeight),
                Color.White);


            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
