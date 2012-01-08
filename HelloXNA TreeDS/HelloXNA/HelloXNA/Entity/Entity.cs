using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace HelloXNA
{
    /// <summary>
    /// This is a building block used to define attributes of a unit
    /// </summary>
    class Entity
    {
        private Rectangle rect;
        private bool isSelected;
        private bool isMouseover;

        /// <summary>
        /// Entity holds the rectangle used for collision, and local selected/mouseover mousestates.
        /// </summary>
        public Entity(Rectangle rect, bool isSelected, bool isMouseover)
        {
            this.rect = rect;
            this.isSelected = isSelected;
            this.isMouseover = isMouseover;
        }

        /// <summary>
        /// Define rect and selected and mouseover as false.
        /// </summary>
        public Entity(Rectangle rect)
        {
            this.rect = rect;
            isSelected = false;
            isMouseover = false;
        }

        public Entity()
        {
        }

        /// <summary>
        /// Rectangle of the unit.
        /// </summary>
        public Rectangle Rect
        {
            get { return rect; }
            set { rect = value; }
        }

        /// <summary>
        /// If the unit is selected, returns true.
        /// </summary>
        public bool IsSelected
        {
            get { return isSelected; }
            set { isSelected = value; }
        }

        /// <summary>
        /// Shit's mouseovered, returns true.
        /// </summary>
        public bool IsMouseover
        {
            get { return isMouseover; }
            set { isMouseover = value; }
        }
    }
}
