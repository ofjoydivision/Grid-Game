using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace HelloXNA
{
    class ToolBox
    {
        static int NumTabs;
        static TabEntity[] Tabs;
        static Units Units;

        public GridBoxType currentTab = GridBoxType.Enemy;
        public Unit currentUnit = null; 

        // Tool lists
        static List<ToolEntity> enemyToolList = new List<ToolEntity>();
        static List<ToolEntity> terrainToolList = new List<ToolEntity>();
        static List<ToolEntity> obstructionToolList = new List<ToolEntity>();

        // Enum size
        static int NumGridBoxType;
        static int NumButtonStatus;
        static int NumGridStatus;
        static int NumBuilderState;

        // Toolbox sizes
        static int ToolBoxOffsetX = 1100;
        static int ToolBoxOffsetY = 50;
        static int ToolBorderLineWidth = 5;
        static int ToolBoxWidth;
        static int ToolBoxHeight = 5;
        static int TabPadding = 3;
        static int TabHeight;
        static int TabWidth;
        static int ToolBoxPadding;

        // ToolBox Tray
        static ToolTrayEntity ToolTray;
        static int ToolTrayHeight;
        static int ToolTrayWidth;
        static int ToolTrayPadding = 3;
        static int ToolTrayOffsetY;
        static int ToolTrayOffsetX;
        static int NumToolPerRow = 3;

        // Tool Dimesnsions
        static int ToolSize;
        static int ToolOffsetX;
        static int ToolOffsetY;

        // Window settings
        static int WindowWidth;
        static int WindowHeight;

        // Color Schemes
        static Color TabMouseoverColor = new Color(255, 255, 0);
        static Color TabUnselectedColor = new Color(200, 200, 0);
        static Color TabSelectedColor = new Color(180, 180, 0);
        static Color TabFrameColor = new Color(180, 180, 0);

        public ToolBox(int windowWidth, int windowHeight, int padding)
        {
            WindowHeight = windowHeight;
            WindowWidth = windowWidth;
            ToolBoxPadding = padding;
            InitializeNumEnum();
            InitializeDimensions();
            InitializeTab();
            InitializeToolTray();
            InitializeTrayTools();
        }

        private void InitializeTrayTools()
        {
            int trayWidth = ToolTrayWidth + ToolBorderLineWidth * 2;
            //ToolSize = ((trayWidth - ToolTrayPadding * (NumToolPerRow)) / NumToolPerRow) - ToolTrayPadding;
            ToolSize = (ToolBoxWidth - (NumToolPerRow + 1) * ToolTrayPadding + ToolBorderLineWidth) / NumToolPerRow;
            Units = new Units();
        }

        public void InitializeTools()
        {
            ToolOffsetX = ToolTrayOffsetX + ToolTrayPadding;
            ToolOffsetY = ToolTrayOffsetY + ToolTrayPadding;

            int toolOffsetLength = ToolSize + ToolTrayPadding;

            int columnIndex, rowIndex;

            columnIndex = 0;
            rowIndex = -1;

            Console.WriteLine("InitializeTools");
            foreach (DictionaryEntry enemyUnit in Units.EnemyTools)
            {
                if (columnIndex % NumToolPerRow == 0)
                {
                    rowIndex++;
                    columnIndex = 0;
                }

                ToolTray.AddTool(GridBoxType.Enemy, (Unit) enemyUnit.Value, new Rectangle(ToolOffsetX - ToolTrayPadding + toolOffsetLength * columnIndex, 5 + ToolOffsetY - ToolTrayPadding + toolOffsetLength * rowIndex, ToolSize, ToolSize));
                //ToolTray.AddTool(GridBoxType.Enemy, (Unit)enemyUnit.Value, new Rectangle(ToolTrayOffsetX - ToolTrayPadding + toolOffsetLength * columnIndex, ToolTrayOffsetY - ToolTrayPadding + toolOffsetLength * rowIndex, ToolSize, ToolSize));
                columnIndex++;
            }

            columnIndex = 0;
            rowIndex = -1;

            foreach (DictionaryEntry landscapeUnit in Units.LandscapeTools)
            {
                if (columnIndex % NumToolPerRow == 0)
                {
                    rowIndex++;
                    columnIndex = 0;
                }

                ToolTray.AddTool(GridBoxType.Landscape, (Unit)landscapeUnit.Value, new Rectangle(ToolOffsetX - ToolTrayPadding + toolOffsetLength * columnIndex, 5 + ToolOffsetY - ToolTrayPadding + toolOffsetLength * rowIndex, ToolSize, ToolSize));
                //ToolTray.AddTool(GridBoxType.Enemy, (Unit)enemyUnit.Value, new Rectangle(ToolTrayOffsetX - ToolTrayPadding + toolOffsetLength * columnIndex, ToolTrayOffsetY - ToolTrayPadding + toolOffsetLength * rowIndex, ToolSize, ToolSize));
                columnIndex++;
            }

            columnIndex = 0;
            rowIndex = -1;

            foreach (DictionaryEntry obstructionUnit in Units.ObstructionTools)
            {
                if (columnIndex % NumToolPerRow == 0)
                {
                    rowIndex++;
                    columnIndex = 0;
                }

                ToolTray.AddTool(GridBoxType.Obstruction, (Unit)obstructionUnit.Value, new Rectangle(ToolOffsetX - ToolTrayPadding + toolOffsetLength * columnIndex, 5 + ToolOffsetY - ToolTrayPadding + toolOffsetLength * rowIndex, ToolSize, ToolSize));
                //ToolTray.AddTool(GridBoxType.Enemy, (Unit)enemyUnit.Value, new Rectangle(ToolTrayOffsetX - ToolTrayPadding + toolOffsetLength * columnIndex, ToolTrayOffsetY - ToolTrayPadding + toolOffsetLength * rowIndex, ToolSize, ToolSize));
                columnIndex++;
            }
        }

        public void LoadTool(GridBoxType type, string name, Texture2D texture, Color color)
        {
            Console.WriteLine("Now Loading " + name);
            Unit unit = new Unit(color, texture);
            Units.addUnit(type, name, unit);
        }

        private void InitializeDimensions()
        {
            NumTabs = NumGridBoxType - 1;
            ToolBoxOffsetX = WindowWidth * 8 / 10;
            ToolBoxWidth = WindowWidth - ToolBoxOffsetX - ToolBoxPadding;
            ToolBoxHeight = WindowHeight - ToolBoxPadding * 2;
            ToolBoxOffsetY = ToolBoxPadding;
            TabWidth = (ToolBoxWidth - (NumTabs + 1) * TabPadding + ToolBorderLineWidth) / NumTabs;
            TabHeight = ToolBoxHeight / 30;
        }

        public void DrawToolBox(SpriteBatch spriteBatch, Texture2D texture)
        {
            DrawToolBoxFrame(spriteBatch, texture);
            DrawTabs(spriteBatch, texture);
        }

        private static void InitializeTab()
        {
            int tabIndex = 0;
            Array gridTypeEnumArray = Enum.GetValues(typeof(GridBoxType));
            Tabs = new TabEntity[NumTabs];
            foreach (GridBoxType type in gridTypeEnumArray)
            {
                if (type.ToString() == "None") continue;
                Tabs[tabIndex] = new TabEntity(
                    new Rectangle(ToolBoxOffsetX + (tabIndex + 1) * TabPadding + TabWidth * tabIndex, TabPadding + ToolBoxPadding + ToolBorderLineWidth, TabWidth, TabHeight), false, false, type);
                tabIndex++;
            }
        }

        private static void InitializeToolTray()
        {
            ToolTrayHeight = ToolBoxHeight - TabPadding * 2 - TabHeight - ToolTrayPadding - ToolBorderLineWidth;
            ToolTrayWidth = ToolBoxWidth - ToolTrayPadding * 2 + ToolBorderLineWidth;
            ToolTrayOffsetY = ToolBoxPadding + TabPadding * 2 + TabHeight + ToolBorderLineWidth;
            ToolTrayOffsetX = ToolBoxOffsetX + ToolTrayPadding;
            ToolTray = new ToolTrayEntity(new Rectangle(ToolTrayOffsetX, ToolTrayOffsetY, ToolTrayWidth, ToolTrayHeight), false, false);

            //Tray.ToolList[GridBoxType.Enemy] = enemyToolList;
            //Tray.ToolList[GridBoxType.Landscape] = terrainToolList;
            //Tray.ToolList[GridBoxType.Obstruction] = obstructionToolList;
        }

        public void DrawToolBoxTrayFrame(SpriteBatch spriteBatch, Texture2D texture)
        {
            // Draw Horizontal Lines
            spriteBatch.Draw(texture, new Rectangle(ToolTrayOffsetX, ToolTrayOffsetY, ToolTrayWidth, ToolBorderLineWidth), Color.Black);
            spriteBatch.Draw(texture, new Rectangle(ToolTrayOffsetX, ToolTrayOffsetY + ToolTrayHeight - ToolBorderLineWidth, ToolTrayWidth, ToolBorderLineWidth), Color.Black);

            // Draw Vertical Lines
            spriteBatch.Draw(texture, new Rectangle(ToolTrayOffsetX, ToolTrayOffsetY, ToolBorderLineWidth, ToolTrayHeight), Color.Black);
            spriteBatch.Draw(texture, new Rectangle(ToolTrayOffsetX + ToolTrayWidth - ToolBorderLineWidth, ToolTrayOffsetY, ToolBorderLineWidth, ToolTrayHeight), Color.Black);
        }

        private static void InitializeNumEnum()
        {
            NumButtonStatus = Enum.GetValues(typeof(ButtonStatus)).Length;
            NumGridBoxType = Enum.GetValues(typeof(GridBoxType)).Length;
            NumGridStatus = Enum.GetValues(typeof(GridStatus)).Length;
            NumBuilderState = Enum.GetValues(typeof(BuilderState)).Length;
        }

        private void DrawToolBoxFrame(SpriteBatch spriteBatch, Texture2D texture)
        {
            // Draw Horizontal toolbox border
            spriteBatch.Draw(texture, new Rectangle(ToolBoxOffsetX + ToolBorderLineWidth - ToolBorderLineWidth, ToolBoxPadding, ToolBoxWidth + ToolBorderLineWidth * 2, ToolBorderLineWidth), TabFrameColor);
            spriteBatch.Draw(texture, new Rectangle(ToolBoxOffsetX + ToolBorderLineWidth - ToolBorderLineWidth, ToolBoxOffsetY + ToolBoxHeight, ToolBoxWidth + ToolBorderLineWidth * 2, ToolBorderLineWidth), TabFrameColor);

            // Draw Vertical ToolBox Border
            spriteBatch.Draw(texture, new Rectangle(ToolBoxOffsetX - ToolBorderLineWidth, ToolBoxPadding, ToolBorderLineWidth, ToolBoxHeight + ToolBorderLineWidth), TabFrameColor);
            spriteBatch.Draw(texture, new Rectangle(ToolBoxWidth + ToolBoxOffsetX + ToolBorderLineWidth, ToolBoxOffsetY, ToolBorderLineWidth, ToolBoxHeight + ToolBorderLineWidth), TabFrameColor);
        }

        private static void DrawTabs(SpriteBatch spriteBatch, Texture2D texture)
        {
            for (int tabIndex = 0; tabIndex < NumTabs; tabIndex++)
            {
                spriteBatch.Draw(texture, Tabs[tabIndex].Rect, TabUnselectedColor);
            }
        }

        public void DrawToolBoxTray(SpriteBatch spriteBatch, Texture2D texture)
        {
            int numEnemyUnits = Units.Count(GridBoxType.Enemy);
            int numLandscapeUnits = Units.Count(GridBoxType.Landscape);
            int numObstructionUnits = Units.Count(GridBoxType.Obstruction);

            for (int enemyIndex = 0; enemyIndex < numEnemyUnits; enemyIndex++)
            {

            }
            //for (int unitIndex = 0; unitIndex < Units.
        }

        public void DrawTools(SpriteBatch spriteBatch, Texture2D texture)
        {
            int numUnits = Units.Count(currentTab);

            foreach (ToolEntity tool in ToolTray.getTools(currentTab))
            {
                spriteBatch.Draw(texture, tool.Rect, tool.Unit.Color);
            }
        }

        public void MouseAction(Point pos, MouseState state)
        {
            if (state.LeftButton == ButtonState.Pressed)
            {
                foreach (TabEntity tab in Tabs)
                {
                    if (tab.Rect.Contains(pos))
                    {
                        tab.IsSelected = true;
                        currentTab = tab.Type;
                    }
                    else tab.IsSelected = false;
                }

                if (ToolTray.Rect.Contains(pos))
                {
                    foreach (ToolEntity tool in ToolTray.getTools(currentTab))
                    {
                        if (tool.Rect.Contains(pos))
                        {
                            tool.IsSelected = true;
                            currentUnit = tool.Unit;
                        }
                        else tool.IsSelected = false;
                    }
                }
            }
        }
    }


}
