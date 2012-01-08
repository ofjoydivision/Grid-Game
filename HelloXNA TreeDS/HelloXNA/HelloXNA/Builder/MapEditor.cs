using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HelloXNA
{
    class MapEditor
    {
        // Window Dimensios
        static int WindowWidth;
        static int WindowHeight;

        // Color schemes
        static Color FrameColor = new Color(255, 255, 255);

        // Enum size
        static int NumGridBoxType;
        static int NumButtonStatus;
        static int NumGridStatus;
        static int NumBuilderState;

        // Formatting
        static int Padding = 50;


        // GridSheet Size Layout by pixel
        static int GridFrameWidth = 400;
        static int GridFrameHeight = 400;


        static GridSheet grid;
        static ToolBox toolBox;

        public MapEditor(int windowWidth, int windowHeight)
        {
            WindowWidth = windowWidth;
            WindowHeight = windowHeight;
            InitializeGridSheet();
            InitializeNumEnum();
            InitializeToolBox();
        }

        private static void InitializeToolBox()
        {
            toolBox = new ToolBox(WindowWidth, WindowHeight, Padding);
        }

        private static void InitializeGridSheet()
        {
            int GridSheetMaxLength = WindowHeight - Padding;
            //if (GridFrameWidth >

            grid = new GridSheet(GridSheetMaxLength, Padding, Padding);
        }

        private static void InitializeNumEnum()
        {
            NumButtonStatus = Enum.GetValues(typeof(ButtonStatus)).Length;
            NumGridBoxType = Enum.GetValues(typeof(GridBoxType)).Length;
            NumGridStatus = Enum.GetValues(typeof(GridStatus)).Length;
            NumBuilderState = Enum.GetValues(typeof(BuilderState)).Length;
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            // Draw the content first
            grid.DrawGridContent(spriteBatch, texture);

            // Outlining borders will over lap them
            grid.DrawGridLines(spriteBatch, texture);

            // Test Collision Algorithm
            //grid.DrawAlgorithm(spriteBatch, texture, new Point( Mouse.GetState().X,Mouse.GetState().Y));

            toolBox.DrawToolBox(spriteBatch, texture);

            toolBox.DrawToolBoxTray(spriteBatch, texture);
            toolBox.DrawToolBoxTrayFrame(spriteBatch, texture);

            toolBox.DrawTools(spriteBatch, texture);
        }

        public void MouseUpdate(Point pos, MouseState state)
        {
            toolBox.MouseAction(pos, state);

            if (toolBox.currentUnit != null)
            {
                grid.DrawMouseAction(pos, state, toolBox.currentUnit.Color);
            }
        }

        private static void DrawToolBoxFrame(SpriteBatch spriteBatch, Texture2D texture)
        {
 
        }

        public void LoadTool(GridBoxType type, string name, Texture2D text, Color color)
        {
            toolBox.LoadTool(type, name, text, color);
        }

        public void EndLoadTool()
        {
            toolBox.InitializeTools();
        }
    }
}
