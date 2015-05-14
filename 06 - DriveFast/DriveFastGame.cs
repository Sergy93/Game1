using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _06___DriveFast
{
    public class DriveFastGame : Game
    {

        public static float WindowHeight;
        public static float WindowWidth;

        private GraphicsDeviceManager graphics;

        private SpriteBatch spriteBatch;

        private RoadManager RoadManager;
        private Car Car;

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

            RoadManager = new RoadManager();

            RoadManager.LoadContent(Content);

            graphics.PreferredBackBufferHeight = RoadManager.RoadTexture.Height;
            graphics.PreferredBackBufferWidth = RoadManager.RoadTexture.Width;
            graphics.ApplyChanges();

            WindowHeight = graphics.PreferredBackBufferHeight;
            WindowWidth = graphics.PreferredBackBufferWidth;

            Car = new Car(Vector2.Zero, Vector2.Zero, new Vector2(0, -1));

            var posX = WindowWidth / 2 - Car.Size.Width * 2;
            var posY = WindowHeight - Car.Size.Height;

            Car.Position = new Vector2(posX - 100, posY - 200);

            Car.LoadContent(Content);

        }


        protected override void UnloadContent()
        {
        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            RoadManager.Update(gameTime, Car.Speed.Y);
            Car.Update(gameTime);

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            RoadManager.Draw(spriteBatch);
            Car.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
