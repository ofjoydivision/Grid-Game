using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Collections;

namespace HelloXNA
{
    class ToolTrayEntity : Entity
    {
        List<ToolEntity> enemyTools;
        List<ToolEntity> landscapeTools;
        List<ToolEntity> obstructionTools;

        public ToolTrayEntity(Rectangle rect, bool isSelected, bool isMouseOver)
        {
            enemyTools = new List<ToolEntity>();
            landscapeTools = new List<ToolEntity>();
            obstructionTools = new List<ToolEntity>();
            Rect = rect;
            IsMouseover = isSelected;
            IsMouseover = isMouseOver;
        }

        public void AddTool(GridBoxType type, Unit unit, Rectangle rect)
        {
            switch (type)
            {
                case GridBoxType.Enemy:
                    enemyTools.Add(new ToolEntity(unit, rect, false, false));
                    break;
                case GridBoxType.Landscape:
                    landscapeTools.Add(new ToolEntity(unit, rect, false, false));
                    break;
                case GridBoxType.Obstruction:
                    obstructionTools.Add(new ToolEntity(unit, rect, false, false));
                    break;
                default:
                    throw new Exception("Undefined GridBoxType found when adding unit to Units list: " + type.ToString());
            }
        }

        public List<ToolEntity> getTools(GridBoxType type)
        {
            switch (type)
            {
                case GridBoxType.Enemy:
                    return enemyTools;
                    break;
                case GridBoxType.Landscape:
                    return landscapeTools;
                    break;
                case GridBoxType.Obstruction:
                    return obstructionTools;
                    break;
            default:
                    throw new Exception("Undefined GridBoxType found when return unit to Units list: " + type.ToString());
            }
        }
    }
}
