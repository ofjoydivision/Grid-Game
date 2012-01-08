using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace HelloXNA
{
    class TreeNode
    {
        public TreeNode TopLeft;
        public TreeNode TopRight;
        public TreeNode BottomLeft;
        public TreeNode BottomRight;
        public TreeNode Top;
        public TreeNode Left;
        public TreeNode Right;
        public TreeNode Bottom;
        public TreeNode Parent;
        public Rectangle Rect;
        public Point index;
        public bool isLeaf;

        public TreeNode()
        {
            Top = null;
            Left = null;
            Right = null;
            Bottom = null;
            TopLeft = null;
            TopRight = null;
            BottomLeft = null;
            BottomRight = null;
            Rect = new Rectangle();
            index = new Point();
            isLeaf = false;
        }

        public TreeNode(Rectangle rect)
        {
            Top = null;
            Left = null;
            Right = null;
            Bottom = null;
            TopLeft = null;
            TopRight = null;
            BottomLeft = null;
            BottomRight = null;
            Rect = rect;
            index = new Point();
            isLeaf = false;
        }

        public TreeNode(Rectangle rect, Point pos)
        {
            Top = null;
            Left = null;
            Right = null;
            Bottom = null;
            TopLeft = null;
            TopRight = null;
            BottomLeft = null;
            BottomRight = null;
            Rect = rect;
            index = pos;
            isLeaf = false;
        }
    }
}
