﻿using System;
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
        public List<Hazard> Hazards = new List<Hazard>();

        private ContentManager contentManager;
        private GraphicsDeviceManager graphics;

        public static Texture2D RoadTexture;
        public static Texture2D HazardTexture;

        private readonly Random random = new Random();


        public void LoadContent(ContentManager theContentManager, GraphicsDeviceManager theGraphics)
        {
            contentManager = theContentManager;
            graphics = theGraphics;

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

                    if (road.Position.Y > graphics.PreferredBackBufferHeight)
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
                Roads[1] = new Road(Vector2.Zero, new Vector2(0, -graphics.PreferredBackBufferHeight + 2));

                foreach (var road in Roads)
                {
                    road.LoadContent(contentManager);
                }
            }

            if (random.Next(400) == 5)
            {
                var hazard = new Hazard(Vector2.Zero, new Vector2(0, speed));
                hazard.LoadContent(contentManager);
                hazard.Position = new Vector2(random.Next(graphics.PreferredBackBufferWidth), 0);
                Hazards.Add(hazard);
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

