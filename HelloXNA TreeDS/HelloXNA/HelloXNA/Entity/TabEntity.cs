using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Collections;

namespace HelloXNA
{
    class TabEntity : Entity
    {
        GridBoxType type;
        ToolTrayEntity Tray;
        Hashtable TabTools;

        public TabEntity(Rectangle rect, bool isSelected, bool isMouseover, GridBoxType type)
        {
            this.Rect = rect;
            this.IsSelected = isSelected;
            this.IsMouseover = isMouseover;
            this.type = type;
            TabTools = new Hashtable();
        }

        public ButtonStatus state()
        {
            ButtonStatus status;
            if (IsSelected)
                status = ButtonStatus.Pressed;
            else if (IsMouseover)
                status = ButtonStatus.MouseOver;
            else
                status = ButtonStatus.Normal;
            return status;
        }

        public GridBoxType Type
        {
            get { return type; }
            set { type = value; }
        }
    }
}
