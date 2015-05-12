using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameHelpers.Classes
{
    public class Sprite
    {
        public Vector2 Position;

        public Texture2D SpriteTexture { get; protected set; }

        public Rectangle Size { get; protected set; }

        public string AssetName;

        private Rectangle mSourceOnSprite;
        private float mScale;


        //Properties
        public Rectangle SourceOnSprite
        {
            get { return mSourceOnSprite; }
            set
            {
                mSourceOnSprite = value;
                Size = new Rectangle(0, 0, (int)(mSourceOnSprite.Width * Scale), (int)(mSourceOnSprite.Height * Scale));
            }
        }

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
        public Sprite(string initialAsset, Vector2 position, float scale = 1.0f)
        {
            AssetName = initialAsset;
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

    }
}