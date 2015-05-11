using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _03___FadeInOut
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Texture2D Cat;

        int mAlphaValue = 1;
        int mFadeIncrement = 3;
        private double mFadeDelay;
        private double mFadeDelayBase = .0001;

        public Game1()
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
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Cat = Content.Load<Texture2D>("CatCreature");

            mFadeDelay = mFadeDelayBase;


        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            mFadeDelay -= gameTime.ElapsedGameTime.TotalSeconds;

            if (mFadeDelay <= 0)
            {
                mFadeDelay = mFadeDelayBase;


                mAlphaValue += mFadeIncrement;
                if (mAlphaValue >= 255 || mAlphaValue <= 0)
                {
                    mFadeIncrement *= -1;
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(Cat, new Rectangle(0, 0, Cat.Width, Cat.Height),
                new Color(255, 255, 255, (byte)MathHelper.Clamp(mAlphaValue, 0, 255)));
            spriteBatch.End();

            base.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}
