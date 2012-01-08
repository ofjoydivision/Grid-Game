using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace HelloXNA
{
    class GridEntity : Entity
    {
        private GridBoxType type;
        private GridStatus status;
        private Color color;

        public GridEntity(Rectangle rect, GridBoxType type, GridStatus status, bool isSelected, bool isMouseover)
        {
            this.Rect = rect;
            this.IsMouseover = isMouseover;
            this.IsSelected = isSelected;
            this.type = type;
            this.status = status;
            color = new Color(0, 170, 0);
        }

        public GridStatus Status
        {
            get { return status; }
            set { status = value; } 
        }

        public GridBoxType Type
        {
            get { return type; }
            set { type = value; }
        }

        public Color Color
        {
            get { return color; }
            set { color = value; }
        }
    }
}
