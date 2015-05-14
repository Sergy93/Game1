using System;
using System.Linq;
using GameHelpers.Classes.Attributes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameHelpers.Classes
{
    public abstract class Sprite
    {
        public Texture2D SpriteTexture { get; protected set; }

        public Rectangle Size { get; protected set; }

        public Vector2 Position;

        protected string AssetName { get; set; }

        private GraphicsDeviceManager graphics;


        //Properties
        private Rectangle mSourceOnSprite;
        public Rectangle SourceOnSprite
        {
            get { return mSourceOnSprite; }
            set
            {
                mSourceOnSprite = value;
                Size = new Rectangle(0, 0, (int)(mSourceOnSprite.Width * Scale), (int)(mSourceOnSprite.Height * Scale));
            }
        }



        private float mScale;
        public float Scale
        {
            get { return mScale; }
            set
            {
                mScale = value;
                //Recalculate the Size of the Sprite with the new scale
                Size = new Rectangle(0, 0, (int)(SourceOnSprite.Width * Scale), (int)(SourceOnSprite.Height * Scale));
            }
        }


        //CONSTRUCTORS
        protected Sprite(Vector2 position, float scale = 1.0f)
        {

            var thisType = GetType();

            if (thisType.IsDefined(typeof(AssetNameAttr), false) == false)
            {
                throw new InvalidOperationException("Must implement AssetNameAttr");
            }


            AssetName = GetAssetName(thisType);
            Position = position;
            Scale = scale;
        }

        //LOAD
        public virtual void LoadContent(ContentManager contentmanager)
        {
            SpriteTexture = contentmanager.Load<Texture2D>("Sprites/" + AssetName);
            SourceOnSprite = new Rectangle(0, 0, SpriteTexture.Width, SpriteTexture.Height);
        }


        //UPDATE
        public virtual void Update(GameTime theGameTime)
        {

        }

        //DRAW
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Draw(spriteBatch, SpriteEffects.None);
        }

        public virtual void Draw(SpriteBatch spriteBatch, SpriteEffects effect)
        {
            spriteBatch.Draw(SpriteTexture, Position, SourceOnSprite,
                Color.White, 0.0f, Vector2.Zero, Scale, effect, 0);
        }


        public static string GetAssetName(Type type)
        {
            var attribute = type.GetCustomAttributes(typeof(AssetNameAttr), true)[0];

            return ((AssetNameAttr)attribute).AssetName;
        }
    }
}