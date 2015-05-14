using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using GameHelpers;
using GameHelpers.Classes;
using GameHelpers.Extensions;


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
            MainCharacter = new Terrain("SquareGuy", Vector2.Zero);

            var backgScale = 2.0f;

            BackgroundElements.AddMany(new[]
           {
                  _mBackgroundOne   = new Terrain("Background01",Vector2.Zero,backgScale),
                  _mBackgroundTwo   = new Terrain("Background02",Vector2.Zero,backgScale),
                  _mBackgroundThree = new Terrain("Background03",Vector2.Zero,backgScale),
                  _mBackgroundFour  = new Terrain("Background04",Vector2.Zero,backgScale),
                  _mBackgroundFive  = new Terrain("Background05",Vector2.Zero,backgScale),
           });

            GameElements.AddMany(BackgroundElements.ToArray());

            GameElements.Add(MainCharacter);

            base.Initialize();
        }


        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);


            foreach (var sprite in BackgroundElements)
            {
                sprite.LoadContent(Content);
            }

            GameElements.First(o => o.AssetName == MainCharacter.AssetName).LoadContent(Content);

            _mBackgroundOne.Position = new Vector2(-_mBackgroundOne.Size.Width, 0);
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

            if (_mBackgroundOne.Position.X < -_mBackgroundOne.SpriteTexture.Width)
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