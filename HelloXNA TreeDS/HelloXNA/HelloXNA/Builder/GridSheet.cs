using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HelloXNA
{
    class GridSheet
    {
        static private GridEntity[,] GridBox;
        static private MousePositionDetector mousePosDetector;

        // Grid sizes
        static int OffsetX;
        static int OffsetY;
        static int CountX = 32; // MUST BE BASE 2
        static int CountY = 32; // MUST BE BASE 2
        static int GridSize;
        static int InnerLineWidth = 2;
        static int BorderLineWidth = 5;
        static int FrameLimitLength;
        static int GridSheetMaxLength;
        static int GridSheetWidth;
        static int GridSheetHeight;

        static Rectangle GridRect;

        // Color schemes
        static Color BorderColor = new Color(0, 160, 0);
        static Color InnerLineColor = new Color(0, 120, 0);
        static Color AreaColor = new Color(0, 40, 0);
        static Color MouseoverColor = new Color(0, 90, 0);
        static Color OccupiedColor = new Color(0, 140, 0);
        static Color SelectedColor = new Color(0, 170, 0);

        public GridSheet(int maxLength, int offsetX, int offsetY)
        {
            FrameLimitLength = maxLength;
            GridSheetMaxLength = maxLength - BorderLineWidth * 2;
            OffsetX = offsetX;
            OffsetY = offsetY;
            NormalizeGridSize();
            InitializeGridDataStructure();
            InitializeMousePositionDetector();
        }

        private static void NormalizeGridSize()
        {
            int widthCandidate = GridSheetMaxLength / CountX;
            int heightCandidtate = GridSheetMaxLength / CountY;

            // Set GridSize to max of width and height
            if (widthCandidate < heightCandidtate)
            {
                GridSize = widthCandidate;
                GridSheetWidth = CountX * widthCandidate;
                GridSheetHeight = CountY * widthCandidate;
            }
            else
            {
                GridSize = heightCandidtate;
                GridSheetWidth = CountX * heightCandidtate;
                GridSheetHeight = CountY * heightCandidtate;
            }
        }

        private static void InitializeGridDataStructure()
        {
            GridRect = new Rectangle(OffsetX + BorderLineWidth, OffsetY + BorderLineWidth, GridSheetWidth, GridSheetHeight);
            GridBox = new GridEntity[CountX, CountY];

            for (int y = 0; y < CountY; y++)
            {
                for (int x = 0; x < CountX; x++)
                {
                    int gridLocalMinX = GridSize * x + OffsetX;
                    int gridLocalMinY = GridSize * y + OffsetY;
                    Rectangle gridRect = new Rectangle(gridLocalMinX + BorderLineWidth, gridLocalMinY + BorderLineWidth, GridSize, GridSize);
                    GridBox[x, y] = new GridEntity(gridRect, GridBoxType.None, GridStatus.Vacant, false, false);
                }
            }
        }

        private static void InitializeMousePositionDetector()
        {
            mousePosDetector = new MousePositionDetector(GridBox);
        }

        public void DrawGridContent(SpriteBatch spriteBatch, Texture2D texture)
        {
         
            for (int x = 0; x < CountX; x++)
            {
                for (int y = 0; y < CountY; y++)
                {
                    GridEntity currentGrid = GridBox[x, y];

                    switch (currentGrid.Status)
                    {
                        case GridStatus.Mouseover:
                            spriteBatch.Draw(texture, currentGrid.Rect, MouseoverColor);
                            break;
                        case GridStatus.Occupied:
                            spriteBatch.Draw(texture, currentGrid.Rect, OccupiedColor);
                            break;
                        case GridStatus.Vacant:
                            spriteBatch.Draw(texture, currentGrid.Rect, AreaColor);
                            break;
                        case GridStatus.Selected:
                            spriteBatch.Draw(texture, currentGrid.Rect, currentGrid.Color);
                            break;
                    }
                }
            }
          
        }


        public void DrawGridLines(SpriteBatch spriteBatch, Texture2D texture)
        {
            int innerLineWidthOffset = InnerLineWidth / 2;
            // Draw horizontal grid lines
            for (int y = 1; y < CountY; y++)
            {
                spriteBatch.Draw(texture, new Rectangle(OffsetX + BorderLineWidth, y * GridSize + OffsetY + BorderLineWidth - innerLineWidthOffset, GridSheetWidth, InnerLineWidth), InnerLineColor);
            }

            // Draw vertical grid lines
            for (int x = 1; x < CountX; x++)
            {
                spriteBatch.Draw(texture, new Rectangle(x * GridSize + OffsetX + BorderLineWidth - innerLineWidthOffset, OffsetY + BorderLineWidth, InnerLineWidth, GridSheetHeight), InnerLineColor);
            }

            // Draw horizontal grid border
            spriteBatch.Draw(texture, new Rectangle(OffsetX, OffsetY, GridSheetWidth + BorderLineWidth * 2, BorderLineWidth), BorderColor);
            spriteBatch.Draw(texture, new Rectangle(OffsetX, CountY * GridSize + OffsetY + BorderLineWidth, GridSheetWidth + BorderLineWidth * 2, BorderLineWidth), BorderColor);

            // Draw vertical grid border
            spriteBatch.Draw(texture, new Rectangle(OffsetX, OffsetY, BorderLineWidth, GridSheetHeight + BorderLineWidth * 2), BorderColor);
            spriteBatch.Draw(texture, new Rectangle(CountX * GridSize + OffsetX + BorderLineWidth, OffsetY, BorderLineWidth, GridSheetHeight + BorderLineWidth * 2), BorderColor);
        }

        public void DrawMouseAction(Point pos, MouseState state, Color color)
        {
            if (mouseWithinGrid(pos))
            {
                if (state.LeftButton == ButtonState.Pressed)
                {
                    Point targetIndex = mousePosDetector.getIntersectIndex(pos);
                    GridEntity targetGrid = GridBox[targetIndex.X, targetIndex.Y];
                    targetGrid.Status = GridStatus.Selected;
                    targetGrid.Color = color;
                }
                else if (state.RightButton == ButtonState.Pressed)
                {
                    Point targetIndex = mousePosDetector.getIntersectIndex(pos);
                    GridEntity targetGrid = GridBox[targetIndex.X, targetIndex.Y];
                    targetGrid.Status = GridStatus.Vacant;
                }
            }
        }

        public void DrawAlgorithm(SpriteBatch spriteBatch, Texture2D texture, Point pos)
        {
            mousePosDetector.DrawTest(spriteBatch, pos, texture);
        }

        /// <summary>
        /// Check if mouse is within GridSheet.
        /// Used to minimize search loops.
        /// </summary>
        private bool mouseWithinGrid(Point pos)
        {
            bool isWithinGrid = false;

            if (GridRect.Contains(pos))
                isWithinGrid = true;

            return isWithinGrid;
        }
    }
}

