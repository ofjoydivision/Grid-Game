using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace HelloXNA
{
    class Units
    {
        Hashtable enemyTools;
        Hashtable landscapeTools;
        Hashtable obstructionTools;

        List<string> enemyNameList;
        List<string> landscapeNameList;
        List<string> obstructionNameList;

        public Units()
        {
            enemyTools = new Hashtable();
            landscapeTools = new Hashtable();
            obstructionTools = new Hashtable();
            enemyNameList = new List<string>();
            landscapeNameList = new List<string>();
            obstructionNameList = new List<string>();
        }

        public void addUnit(GridBoxType gridType, string name, Unit unit)
        {
            switch (gridType)
            {
                case GridBoxType.Enemy:
                    enemyTools[name] = unit;
                    enemyNameList.Add(name);
                    break;
                case GridBoxType.Landscape:
                    landscapeTools[name] = unit;
                    landscapeNameList.Add(name);
                    break;
                case GridBoxType.Obstruction:
                    obstructionTools[name] = unit;
                    obstructionNameList.Add(name);
                    break;
                default:
                    throw new Exception("Undefined GridBoxType found when adding unit to Units list: " + gridType.ToString());
            }
        }

        public Unit getUnit(GridBoxType gridType, string unitName)
        {
            switch (gridType)
            {
                case GridBoxType.Enemy:
                    return (Unit) enemyTools[unitName];
                    break;
                case GridBoxType.Landscape:
                    return (Unit) landscapeTools[unitName];
                    break;
                case GridBoxType.Obstruction:
                    return (Unit)obstructionTools[unitName];
                    break;
                default:
                    throw new Exception("Undefined GridBoxType found when adding unit to Units list: " + gridType.ToString());
            }
        }

        public Hashtable EnemyTools
        {
            get { return enemyTools; }
            set { enemyTools = value; }
        }

        public Hashtable LandscapeTools
        {
            get { return landscapeTools; }
            set { landscapeTools = value; }
        }

        public Hashtable ObstructionTools
        {
            get { return obstructionTools; }
            set { obstructionTools = value; }
        }

        public int Count(GridBoxType gridType)
        {
            switch (gridType)
            {
                case GridBoxType.Enemy:
                    return enemyTools.Count;
                    break;
                case GridBoxType.Landscape:
                    return landscapeTools.Count;
                    break;
                case GridBoxType.Obstruction:
                    return obstructionTools.Count;
                    break;
                default:
                    throw new Exception("Undefined GridBoxType found when adding unit to Units list: " + gridType.ToString());
            }
        }
    }
}
