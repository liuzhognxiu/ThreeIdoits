using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMapCellModel : NotifyPropChanged
{
    public const string MAPCELLSTATUS = "MAPCELLSTATUS";

    private MapCellStatus m_mapCellStatus;
    public MapCellStatus MapCellStatus
    {
        get
        {
            return m_mapCellStatus;
        }
        set
        {
            m_mapCellStatus = value;
            OnPropertyChanged(MAPCELLSTATUS, m_mapCellStatus);
        }
    }
}
