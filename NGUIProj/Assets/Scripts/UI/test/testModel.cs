using UnityEngine;
using System.Collections;

public class testModel : NotifyPropChanged//, INotifyPropChanged
{
    public const string COUNT = "test_count";

    private int m_count = 0;
    public int Count
    {
        get
        {
            return m_count;
        }
        set
        {
            m_count = value;
            OnPropertyChanged(COUNT, m_count);
        }
    }
}
