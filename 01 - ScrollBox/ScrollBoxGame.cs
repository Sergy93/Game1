using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using GameHelpers;


namespace ScrollBox
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class ScrollBoxGame : Game
    {
        private GraphicsDeviceManager GraphicsManager { get; set; }
        private SpriteBatch SpriteBatch { get; set; }

        private readonly List<Sprite> GameElements = new List<Sprite>();
        private readonly List<Sprite> BackgroundElements = new List<Sprite>();

        private Sprite MainCharacter { get; set; }
        private Sprite _mBackgroundOne, _mBackgroundTwo, _mBackgroundThree, _mBackgroundFour, _mBackgroundFive;

        public ScrollBoxGame()
        {
            GraphicsManager = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            MainCharacter = new Sprite("SquareGuy");

            BackgroundElements.AddMany(new[]
           {
                  _mBackgroundOne   = new Sprite("Background01"),
                  _mBackgroundTwo   = new Sprite("Background02"),
                  _mBackgroundThree = new Sprite("Background03"),
                  _mBackgroundFour  = new Sprite("Background04"),
                  _mBackgroundFive  = new Sprite("Background05"),
           });

            GameElements.AddMany(BackgroundElements.ToArray());

            GameElements.Add(MainCharacter);

            base.Initialize();
        }


        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            var backgScale = 2.0f;

            foreach (var sprite in BackgroundElements)
            {
                sprite.LoadContent(Content, backgScale);
            }

            GameElements.First(o => o.AssetName == "SquareGuy").LoadContent(Content);

            _mBackgroundOne.Position = new Vector2(-_mBackgroundOne.SpriteTexture.Width, 0);
            _mBackgroundTwo.Position = new Vector2(_mBackgroundOne.Position.X + _mBackgroundOne.Size.Width, 0);
            _mBackgroundThree.Position = new Vector2(_mBackgroundTwo.Position.X + _mBackgroundTwo.Size.Width, 0);
            _mBackgroundFour.Position = new Vector2(_mBackgroundThree.Position.X + _mBackgroundThree.Size.Width, 0);
            _mBackgroundFive.Position = new Vector2(_mBackgroundFour.Position.X + _mBackgroundFour.Size.Width, 0);


            base.LoadContent();
        }


        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here

            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();

            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (_mBackgroundOne.Position.X < -_mBackgroundOne.Size.Width)
            {
                _mBackgroundOne.Position.X = _mBackgroundFive.Position.X + _mBackgroundFive.Size.Width;
            }

            if (_mBackgroundTwo.Position.X < -_mBackgroundTwo.Size.Width)
            {
                _mBackgroundTwo.Position.X = _mBackgroundOne.Position.X + _mBackgroundOne.Size.Width;
            }

            if (_mBackgroundThree.Position.X < -_mBackgroundThree.Size.Width)
            {
                _mBackgroundThree.Position.X = _mBackgroundTwo.Position.X + _mBackgroundTwo.Size.Width;
            }

            if (_mBackgroundFour.Position.X < -_mBackgroundFour.Size.Width)
            {
                _mBackgroundFour.Position.X = _mBackgroundThree.Position.X + _mBackgroundThree.Size.Width;
            }

            if (_mBackgroundFive.Position.X < -_mBackgroundFive.Size.Width)
            {
                _mBackgroundFive.Position.X = _mBackgroundFour.Position.X + _mBackgroundFour.Size.Width;
            }

            var backgDirection = new Vector2(-1, 0);
            var backgSpeed = new Vector2(200, 0);

            _mBackgroundOne.Position += backgDirection * backgSpeed * deltaTime;
            _mBackgroundTwo.Position += backgDirection * backgSpeed * deltaTime;
            _mBackgroundThree.Position += backgDirection * backgSpeed * deltaTime;
            _mBackgroundFour.Position += backgDirection * backgSpeed * deltaTime;
            _mBackgroundFive.Position += backgDirection * backgSpeed * deltaTime;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            SpriteBatch.Begin();

            foreach (var sprite in GameElements)
            {
                sprite.Draw(SpriteBatch);
            }

            SpriteBatch.End();

            base.Draw(gameTime);
        }

    }
}