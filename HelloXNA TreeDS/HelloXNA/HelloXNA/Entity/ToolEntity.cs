using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace HelloXNA
{
    class ToolEntity : Entity
    {
        Unit unit;

        public ToolEntity(Unit unit, Rectangle rect, bool isSelected, bool isMouseover)
        {
            this.Rect = rect;
            this.IsMouseover = isMouseover;
            this.IsSelected = isSelected;
            this.unit = unit;
        }

        public Unit Unit
        {
            get { return unit; }
            set { unit = value; }
        }
    }
}
