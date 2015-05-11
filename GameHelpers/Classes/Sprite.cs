using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameHelpers.Classes
{
    public class Sprite
    {
        public Vector2 Position;

        public Texture2D SpriteTexture { get; set; }

        public Rectangle Size { get; set; }

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
        public Sprite(string initialAsset, int posX = 0, int posY = 0)
        {
            AssetName = initialAsset;
            Position = new Vector2(posX, posY);
        }

        public Sprite(string initialAsset, Vector2 initialPos)
        {
            AssetName = initialAsset;
            Position = initialPos;
        }


        //LOAD
        public void LoadContent(ContentManager contentmanager, float scale = 1.0f)
        {
            SpriteTexture = contentmanager.Load<Texture2D>("Sprites/" + AssetName);
            Scale = scale;
            SourceOnSprite = new Rectangle(0, 0, SpriteTexture.Width, SpriteTexture.Height);
        }


        //UPDATE
        protected void Update(GameTime theGameTime, Vector2 speed, Vector2 direction)
        {
            var deltaTime = (float)theGameTime.ElapsedGameTime.TotalSeconds;

            Position += direction * speed * deltaTime;
        }

        //DRAW
        public virtual void Draw(SpriteBatch spriteBatch, SpriteEffects effect = SpriteEffects.None)
        {
            spriteBatch.Draw(SpriteTexture, Position, SourceOnSprite,
                Color.White, 0.0f, Vector2.Zero, Scale, effect, 0);
        }

    }
}