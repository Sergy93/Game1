using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace _06___DriveFast
{
    public class RoadManager
    {

        //public List<Road> Roads = new List<Road>();

        public Road[] Roads;

        private ContentManager contentManager;

        public Vector2 Speed = new Vector2(0, 100);

        public static Texture2D roadTexture;


        public void LoadContent(ContentManager theContentManager)
        {
            contentManager = theContentManager;

            roadTexture = contentManager.Load<Texture2D>("Sprites/" + Road.Asset);


        }

        public void Update(GameTime gameTime)
        {
            if (Roads != null)
            {
                foreach (var road in Roads)
                {
                    if (road.Position.Y > DriveFastGame.WindowHeight)
                    {
                        var minY = Roads.Min(r => r.Position.Y);

                        road.Position.Y = minY - road.Size.Height + 1;
                    }

                    road.Update(gameTime);
                }
            }
            else
            {
                Roads = new Road[2];

                Roads[0] = new Road(Speed, Vector2.Zero);
                Roads[1] = new Road(Speed, new Vector2(0, -DriveFastGame.WindowHeight+2));

                foreach (var road in Roads)
                {
                    road.LoadContent(contentManager);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var road in Roads)
            {
                road.Draw(spriteBatch);
            }
        }
    }
}

