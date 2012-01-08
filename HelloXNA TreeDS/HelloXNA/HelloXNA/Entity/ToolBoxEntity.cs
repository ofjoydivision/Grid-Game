using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace HelloXNA
{
    class ToolBoxEntity : Entity
    {
        List<TabEntity> Tabs;
        public ToolBoxEntity(Rectangle rect, bool isSelected, bool isMouseOver)
        {
            this.Rect = rect;
            this.IsSelected = isSelected;
            this.IsMouseover = isMouseOver;
            Tabs = new List<TabEntity>();
        }

        public void insertTab(TabEntity tab)
        {
            Tabs.Add(tab);
        }

    }
}
