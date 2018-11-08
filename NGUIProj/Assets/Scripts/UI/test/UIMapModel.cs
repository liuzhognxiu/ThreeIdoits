using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMapModel : NotifyPropChanged
{
    public const string MAPWIDTH= "MAPWIDTH";
    public const string MAPHEIGHT = "MAPHEIGHT";

    private int m_mapWidth;
    public int MapWidth
    {
        get
        {
            return m_mapWidth;
        }
        set
        {
            m_mapWidth = value;
            OnPropertyChanged(MAPWIDTH, m_mapWidth);
        }
    }

    public int m_mapHeight;
    public int MapHeight
    {
        get
        {
            return m_mapHeight;
        }
        set
        {
            m_mapHeight = value;
            OnPropertyChanged(MAPHEIGHT, m_mapHeight);
        }
    }
}
