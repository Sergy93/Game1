using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameHelpers.Classes;
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

        public static Texture2D RoadTexture;
        public static Texture2D HazardTexture;


        public void LoadContent(ContentManager theContentManager)
        {
            contentManager = theContentManager;

            RoadTexture = contentManager.Load<Texture2D>("Sprites/" + Sprite.GetAssetName(typeof(Road)));
            HazardTexture = contentManager.Load<Texture2D>("Sprites/" + Sprite.GetAssetName(typeof(Hazard)));
        }

        public void Update(GameTime gameTime, float speed)
        {
            if (Roads != null)
            {
                foreach (var road in Roads)
                {

                    road.Speed.Y = speed > 0 ? speed : 0;

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

                Roads[0] = new Road(Vector2.Zero, Vector2.Zero);
                Roads[1] = new Road(Vector2.Zero, new Vector2(0, -DriveFastGame.WindowHeight + 2));

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

