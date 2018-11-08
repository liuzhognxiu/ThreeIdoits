using Components.AStar;
using Components.Struct;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSMap
{
    public int Width;
    public int Height;
    public LuaInterface.LuaTable Cells;
}

public class CSCell
{
    public int X;
    public int Y;
    public int Value;
}

public class LuaAStar {
    private static LuaAStar s_instance = null;
    public static LuaAStar Instance
    {
        get
        {
            if (s_instance == null)
                s_instance = new LuaAStar();

            return s_instance;
        }
    }
    
    private byte[,] m_map = null;
    public void Init(CSMap data)
    {
        m_map = new byte[data.Width, data.Height];
        foreach(CSCell cell in data.Cells.ToArray())
        {
            m_map[cell.X, cell.Y] = (byte)(cell.Value - 1);
        }
    }

    public List<CSCell> Search(Vector3 startPos, Vector3 endPos)
    {
        List<CSCell> ret = new List<CSCell>();
        PathFinderFast PathFinder = new PathFinderFast(m_map);        PathFinder.Formula = HeuristicFormula.Manhattan; //使用我个人觉得最快的曼哈顿A*算法
        PathFinder.SearchLimit = 2000; //即移动经过方块(20*20)不大于2000个(简单理解就是步数)

        Point2D Start = new Point2D((int)startPos.x, (int)startPos.y);
        Point2D End = new Point2D((int)endPos.x, (int)endPos.y);
        List<PathFinderNode> path = PathFinder.FindPath(Start, End); //开始寻径

        if (path == null)        {            Debug.Log("路径不存在！");
        }        else        {            string output = string.Empty;            for (int i = path.Count - 1; i >= 0; i--)            {                output = string.Format(output                    + "{0}"                    + path[i].X.ToString()                    + "{1}"                    + path[i].Y.ToString()                    + "{2}",                    "(", ",", ") ");                ret.Add(new CSCell() { X = path[i].X, Y = path[i].Y, Value = (int)MapCellStatus.Normal});
            }            Debug.Log("路径坐标分别为:" + output);
        }

        return ret;
    }
}
