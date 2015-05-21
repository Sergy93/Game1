using System.Collections.Generic;
using GameHelpers.Classes;
using GameHelpers.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _06___DriveFast
{
    public class DriveFastGame : Game
    {

        private readonly GraphicsDeviceManager graphics;

        private SpriteBatch spriteBatch;

        private RoadManager roadManager;
        private Car car;

        private List<Hazard> Hazards = new List<Hazard>();

        private const float GameSpeed = 50;

        public DriveFastGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            GameServices.AddService(graphics);
            GameServices.AddService(Content);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            roadManager = new RoadManager();

            roadManager.LoadContent(Content, Hazards);

            graphics.PreferredBackBufferHeight = RoadManager.RoadTexture.Height;
            graphics.PreferredBackBufferWidth = RoadManager.RoadTexture.Width;
            graphics.ApplyChanges();

            car = new Car(Vector2.Zero, Vector2.Zero, new Vector2(0, -1));

            var posX = graphics.PreferredBackBufferWidth / 2 - car.Size.Width * 2;
            var posY = graphics.PreferredBackBufferWidth - car.Size.Height;

            car.Position = new Vector2(posX - 100, posY - 200);

            car.LoadContent();

        }


        protected override void UnloadContent()
        {
        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            roadManager.Update(gameTime, GameSpeed);
            car.Update(gameTime);

            foreach (var hazard in Hazards)
            {
                if (car.CheckCollisions(hazard))
                {
                    Exit();
                }

            }

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            roadManager.Draw(spriteBatch);
            car.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
