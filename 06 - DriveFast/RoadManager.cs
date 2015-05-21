using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameHelpers.Classes;
using GameHelpers.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace _06___DriveFast
{
    public class RoadManager
    {
        //public List<Road> Roads = new List<Road>();

        public Road[] Roads;
        public List<Hazard> Hazards;

        private ContentManager contentManager;
        public GraphicsDeviceManager ThisGraphics;

        public static Texture2D RoadTexture;
        public static Texture2D HazardTexture;

        private readonly Random random = new Random();
        private int HazardTimer = 400;
        private int NextHazardCounter;

        private int middleScreenX;

        public void LoadContent(ContentManager theContentManager, List<Hazard> theHazards)
        {
            contentManager = theContentManager;

            RoadTexture = contentManager.Load<Texture2D>("Sprites/" + Sprite.GetAssetName(typeof(Road)));
            HazardTexture = contentManager.Load<Texture2D>("Sprites/" + Sprite.GetAssetName(typeof(Hazard)));

            ThisGraphics = GameServices.GetService<GraphicsDeviceManager>();

            middleScreenX = ThisGraphics.PreferredBackBufferWidth / 2;

            Hazards = theHazards;
        }

        public void Update(GameTime gameTime, float speed)
        {

            if (Roads != null)
            {
                foreach (var road in Roads)
                {

                    road.Speed.Y = speed > 0 ? speed : 0;

                    if (road.Position.Y > ThisGraphics.PreferredBackBufferHeight)
                    {
                        road.Position = new Vector2(road.Position.X, -ThisGraphics.PreferredBackBufferHeight + 3);
                    }

                    road.Update(gameTime);
                }
            }
            else
            {
                Roads = new Road[2];

                Roads[0] = new Road(Vector2.Zero, Vector2.Zero);
                Roads[1] = new Road(Vector2.Zero, new Vector2(0, -ThisGraphics.PreferredBackBufferHeight + 3));

                foreach (var road in Roads)
                {
                    road.LoadContent();
                }
            }

            if (NextHazardCounter == 0)//&& random.Next(200) == 5)
            {
                var hazardDisplacement = random.Next(10) % 2 == 0 ? -HazardTexture.Width / 2 - 45 : HazardTexture.Width / 2 + 10;

                var hazard = new Hazard(new Vector2(middleScreenX + hazardDisplacement, 0) / 2, new Vector2(0, speed));
                hazard.LoadContent();
                Hazards.Add(hazard);
                NextHazardCounter = HazardTimer;
            }

            if (NextHazardCounter != 0)
            {
                --NextHazardCounter;
            }
            foreach (var hazard in Hazards)
            {
                hazard.Update(gameTime);

            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var road in Roads)
            {
                road.Draw(spriteBatch);
            }
            foreach (var hazard in Hazards)
            {
                hazard.Draw(spriteBatch);
            }
        }

    }
}

