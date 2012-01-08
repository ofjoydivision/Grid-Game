using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace HelloXNA
{
    class Utility
    {
        public void DrawLine(
            SpriteBatch batch,
            Texture2D blank,
            float width,
            Color color,
            Vector2 point1,
            Vector2 point2)
        {
            float angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);
            float length = Vector2.Distance(point1, point2);

            batch.Draw(blank, point1, null, color,
                       angle, Vector2.Zero, new Vector2(length, width),
                       SpriteEffects.None, 0);
            
        }


        //public void DrawToolBox(SpriteBatch batch, List<>, Color borderColor)
    }
}
