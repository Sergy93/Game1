using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameHelpers.Classes;
using GameHelpers.Classes.Attributes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _07___ColorCollisions
{
    [AssetNameAttr("Car")]
    public class Car : MovingSprite
    {

        const float ThisScale = 0.5f;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D trackTexture;

        private RenderTarget2D trackRender;

        public Car(Vector2 position, Vector2 speed, Vector2 direction, float rotation = 1)
            : base(position, speed, direction, ThisScale, rotation)
        {

        }

        public override void LoadContent()
        {
            graphics = GameServices.GetService<GraphicsDeviceManager>();
            spriteBatch = GameServices.GetService<SpriteBatch>();

            trackTexture = Content.Load<Texture2D>("Track");


            base.LoadContent();

            trackRender = new RenderTarget2D(graphics.GraphicsDevice, Size.Width, Size.Height);
        }

        public override void Update(GameTime theGameTime)
        {
            var gamePad = GamePad.GetState(PlayerIndex.One);
            var keyBoard = Keyboard.GetState();

            if (gamePad.Buttons.Back == ButtonState.Pressed || keyBoard.IsKeyDown(Keys.Escape))
            {
                //Exit();
            }

            Rotation += (float)(gamePad.ThumbSticks.Left.X * 3.0f * theGameTime.ElapsedGameTime.TotalSeconds);

            if (keyBoard.IsKeyDown(Keys.Up) || keyBoard.IsKeyDown(Keys.Left))
            {
                Rotation -= (float)(3.0f * theGameTime.ElapsedGameTime.TotalSeconds);
            }
            else if (keyBoard.IsKeyDown(Keys.Down) || keyBoard.IsKeyDown(Keys.Right))
            {
                Rotation += (float)(3.0f * theGameTime.ElapsedGameTime.TotalSeconds);
            }

            var moveIncrement = (int)(200 * theGameTime.ElapsedGameTime.TotalSeconds);

            if (CollidesWithTrack(moveIncrement) == false)
            {
                Position += new Vector2((float)(moveIncrement * Math.Cos(Rotation)), (float)(moveIncrement * Math.Sin(Rotation)));
            }

        }

        private bool CollidesWithTrack(int moveInc)
        {

            var xPos = (float)(-Size.Width / 2 + Position.X + moveInc * Math.Cos(Rotation));
            var yPos = (float)(-Size.Height / 2 + Position.Y + moveInc * Math.Sin(Rotation));

            var collisionCheckTexture = CreateTrackCollisionTexture(xPos, yPos);

            var carPixels = Size.Width * Size.Height;

            var myColors = new Color[carPixels];

            collisionCheckTexture.GetData<Color>(0,
                new Rectangle(collisionCheckTexture.Width / 2 - Size.Width / 2,
                    collisionCheckTexture.Height / 2 - Size.Height / 2,
                    Size.Width,
                    Size.Height),
                myColors,
                0, carPixels);

            return myColors.Any(color => color != Color.Gray);
        }

        private Texture2D CreateTrackCollisionTexture(float nextX, float nextY)
        {
            graphics.GraphicsDevice.Clear(ClearOptions.Target, Color.Red, 0, 0);

            spriteBatch.Begin();

            spriteBatch.Draw(
                trackRender,
                Size, null,
                Color.White,
                -Rotation,
                new Vector2(trackRender.Width / 2, trackRender.Height / 2),
                SpriteEffects.None, 0);

            spriteBatch.End();

            graphics.GraphicsDevice.SetRenderTarget(null);

            return trackRender;
        }
    }
}
