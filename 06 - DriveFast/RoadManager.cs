using System;
using System.Collections.Generic;
using System.Linq;
using GameHelpers.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace _06___DriveFast
{
    public class RoadManager : MovingSprite
    {

        //Float value is the y position
        private const string Asset = "Road";

        public RoadManager()
            : base(Asset, Vector2.Zero, new Vector2(0, 1), new Vector2(0, 1))
        {
        }

        public override void LoadContent(ContentManager contManager)
        {
            base.LoadContent(contManager);
        }

        public void Update(GameTime gameTime, float speed)
        {
            //if (Roads.All(r => r.Value != 0.0f))
            //{
            //    Roads.Add(originPosition, 0);
            //}

            //var temp = Roads;
            //var keys = new List<Rectangle>(temp.Keys);
            //foreach (var key in keys)
            //{
            //    temp[key] += speed;
            //}
            //Roads = temp;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

    }
}
