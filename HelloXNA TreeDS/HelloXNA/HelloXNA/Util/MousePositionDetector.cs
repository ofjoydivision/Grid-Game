using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HelloXNA
{
    /// <summary>
    /// Detects the collision point of a mouse click across a 2D array of Rectangles.
    /// Assumption1: Entity array is 2-Dimensions
    /// Assumption2: Every rect in entity array is consistent.
    /// TODO: Make more efficent by making quad search instead of binary.
    /// </summary>
    class MousePositionDetector
    {
        TreeNode Root;
        Entity[,] entityArray;
        static int entityWidth;
        static int entityHeight;

        public MousePositionDetector(Entity[,] gridBox)
        {
            // set 2D array to use
            entityArray = gridBox;
            InitializeTree();
        }

        private void InitializeTree()
        {
            // Initialize Tree
            Root = new TreeNode();

            // Set starting Index
            int startIndexX = 0;
            int startIndexY = 0;

            // Look at Assumption1
            int arrayWidth = entityArray.GetLength(0);
            int arrayHeight = entityArray.GetLength(1);

            int entityLength;

            // Look at Assumption2 in class summary
            if (entityArray[0, 0].Rect.Width != entityArray[0, 0].Rect.Height)
                throw new Exception("The height and width of an entity is not symetric. Cannot create tree.");
            else 
                entityLength = entityArray[0, 0].Rect.Width;

            // Set total height and width of 2D array
            // Used to divide rect sizes
            int entityTotalWidth = entityLength * arrayWidth;
            int entityTotalHeight = entityLength * arrayHeight;

            // Start creating nodes
            DivideAndConquer(Root, startIndexX, startIndexY, arrayWidth, arrayHeight, entityTotalWidth, entityTotalHeight);

            // Print tree for debugging
            //PrintTree(Root);
        }

        private void DivideAndConquer(TreeNode root, int beginIndexX, int beginIndexY, int widthIndex, int heightIndex, int width, int height)
        {
            // Base Case: Return when single grid is reached.
            // Location maps to 2D array.
            if (widthIndex == 1 && heightIndex == 1)
            {
                root.index = new Point(beginIndexX, beginIndexY);
                root.isLeaf = true;
                return;
            }

            // Next step variables for recursion
            int nextStepIndexWidth;
            int nextStepIndexHeight;
            int nextStepWidth;
            int nextStepHeight;

            if (widthIndex == 1)
            {
                // Next step dimensions and indexes
                nextStepIndexWidth = widthIndex;
                nextStepIndexHeight = heightIndex / 2;
                nextStepWidth = width;
                nextStepHeight = height / 2;

                Point topLocation = entityArray[beginIndexX, beginIndexY].Rect.Location;
                Point bottomLocation = entityArray[beginIndexX, beginIndexY + nextStepIndexHeight].Rect.Location;

                root.Top = new TreeNode(new Rectangle(topLocation.X, topLocation.Y, nextStepWidth, nextStepHeight));
                root.Bottom = new TreeNode(new Rectangle(bottomLocation.X, bottomLocation.Y, nextStepWidth, nextStepHeight));

                root.Top.Parent = root;
                root.Bottom.Parent = root;

                DivideAndConquer(root.Top, beginIndexX, beginIndexY, nextStepIndexWidth, nextStepIndexHeight, nextStepWidth, nextStepHeight);
                DivideAndConquer(root.Bottom, beginIndexX, beginIndexY + nextStepIndexHeight, nextStepIndexWidth, nextStepIndexHeight, nextStepWidth, nextStepHeight);
            }
            else if (heightIndex == 1)
            {
                // Next step dimensions and indexes
                nextStepIndexWidth = widthIndex / 2;
                nextStepIndexHeight = heightIndex;
                nextStepWidth = width / 2;
                nextStepHeight = height;

                Point leftLocation = entityArray[beginIndexX, beginIndexY].Rect.Location;
                Point rightLocation = entityArray[beginIndexX + nextStepIndexWidth, beginIndexY].Rect.Location;

                root.Left = new TreeNode(new Rectangle(leftLocation.X, leftLocation.Y, nextStepWidth, nextStepHeight));
                root.Right = new TreeNode(new Rectangle(rightLocation.X, rightLocation.Y, nextStepWidth, nextStepHeight));

                root.Left.Parent = root;
                root.Right.Parent = root;

                DivideAndConquer(root.Left, beginIndexX, beginIndexY, nextStepIndexWidth, nextStepIndexHeight, nextStepWidth, nextStepHeight);
                DivideAndConquer(root.Right, beginIndexX + nextStepIndexWidth, beginIndexY, nextStepIndexWidth, nextStepIndexHeight, nextStepWidth, nextStepHeight);
            }
            else
            {
                // Next step dimensions and indexes
                nextStepIndexWidth = widthIndex / 2;
                nextStepIndexHeight = heightIndex / 2;
                nextStepWidth = width / 2;
                nextStepHeight = height / 2;

                // Top-eft pos of each new subset
                Point topLeftLocation = entityArray[beginIndexX, beginIndexY].Rect.Location;
                Point topRightLocation = entityArray[beginIndexX + nextStepIndexWidth, beginIndexY].Rect.Location;
                Point bottomLeftLocation = entityArray[beginIndexX, beginIndexY + nextStepIndexHeight].Rect.Location;
                Point bottomRightLocation = entityArray[beginIndexX + nextStepIndexWidth, beginIndexY + nextStepIndexHeight].Rect.Location;

                // Create node for each subset (4)
                root.TopLeft = new TreeNode(new Rectangle(topLeftLocation.X, topLeftLocation.Y, nextStepWidth, nextStepHeight));
                root.TopRight = new TreeNode(new Rectangle(topRightLocation.X, topRightLocation.Y, nextStepWidth, nextStepHeight));
                root.BottomLeft = new TreeNode(new Rectangle(bottomLeftLocation.X, bottomLeftLocation.Y, nextStepWidth, nextStepHeight));
                root.BottomRight = new TreeNode(new Rectangle(bottomRightLocation.X, bottomRightLocation.Y, nextStepWidth, nextStepHeight));

                // Set parents of each node
                root.TopLeft.Parent = root;
                root.TopRight.Parent = root;
                root.BottomLeft.Parent = root;
                root.BottomRight.Parent = root;

                // Recurse till single entity left
                DivideAndConquer(root.TopLeft, beginIndexX, beginIndexY, nextStepIndexWidth, nextStepIndexHeight, nextStepWidth, nextStepHeight);
                DivideAndConquer(root.TopRight, beginIndexX + nextStepIndexWidth, beginIndexY, nextStepIndexWidth, nextStepIndexHeight, nextStepWidth, nextStepHeight);
                DivideAndConquer(root.BottomLeft, beginIndexX, beginIndexY + nextStepIndexHeight, nextStepIndexWidth, nextStepIndexHeight, nextStepWidth, nextStepHeight);
                DivideAndConquer(root.BottomRight, beginIndexX + nextStepIndexWidth, beginIndexY + nextStepIndexHeight, nextStepIndexWidth, nextStepIndexHeight, nextStepWidth, nextStepHeight);
            }
        }

        public Point getIntersectIndex(Point mousePos)
        {
            return getIntersectIndex(Root, mousePos);
        }

        private Point getIntersectIndex(TreeNode root, Point mousePos)
        {
            if (root.isLeaf)
            {
                if (root.Rect.Contains(mousePos))
                    return root.index;
            }
            if (root.TopLeft != null)
            {
                if (root.TopLeft.Rect.Contains(mousePos))
                    return getIntersectIndex(root.TopLeft, mousePos);
            }
            if (root.TopRight != null)
            {
                if (root.TopRight.Rect.Contains(mousePos))
                    return getIntersectIndex(root.TopRight, mousePos);
            }
            if (root.BottomLeft != null)
            {
                if (root.BottomLeft.Rect.Contains(mousePos))
                    return getIntersectIndex(root.BottomLeft, mousePos);
            }
            if (root.BottomRight != null)
            {
                if (root.BottomRight.Rect.Contains(mousePos))
                    return getIntersectIndex(root.BottomRight, mousePos);
            }
            if (root.Top != null)
            {
                if (root.Top.Rect.Contains(mousePos))
                    return getIntersectIndex(root.Top, mousePos);
            }
            if (root.Bottom != null)
            {
                if (root.Bottom.Rect.Contains(mousePos))
                    return getIntersectIndex(root.Bottom, mousePos);
            }
            if (root.Left != null)
            {
                if (root.Left.Rect.Contains(mousePos))
                    return getIntersectIndex(root.Left, mousePos);
            }
            if (root.Right != null)
            {
                if (root.Right.Rect.Contains(mousePos))
                    return getIntersectIndex(root.Right, mousePos);
            }
            else
            {
                throw new Exception("Intersect was not found");
            }
            return mousePos;
        }

        public void PrintTree(TreeNode root)
        {
            if (root == null) return;
            if (root.isLeaf)
            {
                Console.WriteLine("Index:\t\tLocation:");
                Console.WriteLine(root.index + "\t" + root.Rect.Location);
                return;
            }

            PrintTree(root.TopLeft);
            PrintTree(root.TopRight);
            PrintTree(root.BottomLeft);
            PrintTree(root.BottomRight);
            PrintTree(root.Top);
            PrintTree(root.Bottom);
            PrintTree(root.Left);
            PrintTree(root.Right);
        }

        public void DrawTest(SpriteBatch spriteBatch, Point mousePos, Texture2D texture)
        {
            DrawTest(Root, mousePos, spriteBatch, texture, 0.1f);
        }


        private void DrawTest(TreeNode root, Point mousePos, SpriteBatch spriteBatch, Texture2D texture, float ctr)
        {
            Color color = new Color(219, 255, 112);
            if (root.isLeaf)
            {
                if (root.Rect.Contains(mousePos))
                {
                    spriteBatch.Draw(texture, root.Rect, color * ctr);
                }
            }
            if (root.TopLeft != null)
            {
                if (root.TopLeft.Rect.Contains(mousePos))
                {
                    spriteBatch.Draw(texture, root.Rect, color * ctr);
                    DrawTest(root.TopLeft, mousePos, spriteBatch, texture, ctr);
                }
            }
            if (root.TopRight != null)
            {
                if (root.TopRight.Rect.Contains(mousePos))
                {
                    spriteBatch.Draw(texture, root.Rect, color * ctr);
                    DrawTest(root.TopRight, mousePos, spriteBatch, texture, ctr);
                }
            }
            if (root.BottomLeft != null)
            {
                if (root.BottomLeft.Rect.Contains(mousePos))
                {
                    spriteBatch.Draw(texture, root.Rect, color * ctr);
                    DrawTest(root.BottomLeft, mousePos, spriteBatch, texture, ctr);
                }
            }
            if (root.BottomRight != null)
            {
                if (root.BottomRight.Rect.Contains(mousePos))
                {
                    spriteBatch.Draw(texture, root.Rect, color * ctr);
                    DrawTest(root.BottomRight, mousePos, spriteBatch, texture, ctr);
                }
            }
            if (root.Top != null)
            {
                if (root.Top.Rect.Contains(mousePos))
                {
                    spriteBatch.Draw(texture, root.Rect, color * ctr);
                    DrawTest(root.Top, mousePos, spriteBatch, texture, ctr);
                }
            }
            if (root.Bottom != null)
            {
                if (root.Bottom.Rect.Contains(mousePos))
                {
                    spriteBatch.Draw(texture, root.Rect, color * ctr);
                    DrawTest(root.Bottom, mousePos, spriteBatch, texture, ctr);
                }
            }
            if (root.Left != null)
            {
                if (root.Left.Rect.Contains(mousePos))
                {
                    spriteBatch.Draw(texture, root.Rect, color * ctr);
                    DrawTest(root.Left, mousePos, spriteBatch, texture, ctr);
                }
            }
            if (root.Right != null)
            {
                if (root.Right.Rect.Contains(mousePos))
                {
                    spriteBatch.Draw(texture, root.Rect, color * ctr);
                    DrawTest(root.Right, mousePos, spriteBatch, texture, ctr);
                }
            }
        }
    }
}
