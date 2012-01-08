using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HelloXNA
{
    class Unit
    {
        Color color;
        Texture2D texture;

        public Unit(Color color, Texture2D texture)
        {
            this.color = color;
            this.texture = texture;
        }

        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        public Color Color
        {
            get { return color; }
            set { color = value; } 
        }
    }
}
