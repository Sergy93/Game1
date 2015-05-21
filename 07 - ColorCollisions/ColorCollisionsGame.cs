using System;
using System.Linq;
using GameHelpers.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _07___ColorCollisions
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class ColorCollisionsGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D TrackTexture;
        Texture2D TrackOverlayTexture;
        Car car;
        RenderTarget2D trackRender;
        RenderTarget2D trackRenderRotated;

        public ColorCollisionsGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            GameServices.AddService(graphics);
            GameServices.AddService(Content);

        }

        protected override void Initialize()
        {

            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            graphics.ApplyChanges();

            car = new Car(new Vector2(270, 325), Vector2.Zero, Vector2.Zero);

            base.Initialize();
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            GameServices.AddService(spriteBatch);

            TrackOverlayTexture = Content.Load<Texture2D>("TrackOverlay");
            car.LoadContent();

            trackRender = new RenderTarget2D(graphics.GraphicsDevice, car.Size.Width + 100,
                car.Size.Height + 100);
            trackRenderRotated = new RenderTarget2D(graphics.GraphicsDevice, car.Size.Width + 100,
                car.Size.Height + 100);

        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            car.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            spriteBatch.Draw(TrackOverlayTexture, new Rectangle(0, 0, TrackOverlayTexture.Width, TrackOverlayTexture.Height), Color.White);
            car.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }


    }
}

