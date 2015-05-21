using System;
using GameHelpers.Classes.Attributes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameHelpers.Classes
{
    public abstract class Sprite
    {
        public Texture2D SpriteTexture { get; protected set; }

        public Rectangle Size { get; set; }

        protected GraphicsDeviceManager GraphicsManager;
        protected ContentManager Content;

        private Rectangle sourceOnSprite;
        public Rectangle SourceOnSprite
        {
            get { return sourceOnSprite; }
            set
            {
                sourceOnSprite = value;
                Size = new Rectangle((int)position.X, (int)position.Y,
                    sourceOnSprite.Width / 2, sourceOnSprite.Height / 2);
            }
        }

        protected string AssetName { get; set; }

        public float Scale { get; set; }

        private Vector2 position;
        public Vector2 Position
        {
            get { return position; }
            set
            {
                position = value;
                Size = new Rectangle((int)position.X, (int)position.Y,
                     sourceOnSprite.Width / 2, sourceOnSprite.Height / 2);
            }
        }


        //CONSTRUCTORS
        protected Sprite(Vector2 position, float scale = 1.0f)
        {

            GraphicsManager = GameServices.GetService<GraphicsDeviceManager>();
            Content = GameServices.GetService<ContentManager>();

            var childType = GetType();

            //Check if the current child has the mandatory AssetName attribute defined, else we throw an exception
            if (childType.IsDefined(typeof(AssetNameAttr), false) == false)
            {
                throw new InvalidOperationException("Must implement AssetNameAttr");
            }

            AssetName = GetAssetName(childType);

            Position = position;

            Scale = scale;
        }

        //LOAD
        public virtual void LoadContent()
        {
            SpriteTexture = Content.Load<Texture2D>("Sprites/" + AssetName);
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