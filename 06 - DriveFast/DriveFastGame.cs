using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _06___DriveFast
{
    public class DriveFastGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        RoadManager roadMng = new RoadManager();
        private Car Car;
        private Vector2 Speed = new Vector2(0, 1);

        public DriveFastGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            roadMng.LoadContent(Content);

            graphics.PreferredBackBufferHeight = roadMng.SpriteTexture.Height;
            graphics.PreferredBackBufferWidth = roadMng.SpriteTexture.Width;
            graphics.ApplyChanges();


            Car = new Car(new Vector2(0, 0), Speed, new Vector2(0, -1));

            var posX = graphics.PreferredBackBufferWidth / 2 - Car.Size.Width * 2;
            var posY = graphics.PreferredBackBufferHeight - Car.Size.Height;

            Car.Position = new Vector2(posX, posY - 100);

            Car.LoadContent(Content);

        }


        protected override void UnloadContent()
        {
        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            roadMng.Update(gameTime, 1);
            Car.Update(gameTime);

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            roadMng.Draw(spriteBatch);
            Car.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
