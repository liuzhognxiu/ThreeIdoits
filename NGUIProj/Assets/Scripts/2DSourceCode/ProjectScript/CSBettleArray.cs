using UnityEngine;
using System.Collections;

public class CSBettlerArray<T>
{
    private T[] mArrayData = null;
    public T[] ArrayData
    {
        get { return mArrayData; }
        set { mArrayData = value; }
    }

    public T this[int i]
    {
        get
        {
            if (i < mSize)
            {
                return mArrayData[i];
            }
            return default(T);
        }
    }

    private int mSize = 0;
    public int Size
    {
        get { return mSize; }
        set {
            if (mSize == value) return;
            mSize = value; 
            if(mSize>0)
            {
                if (mArrayData == null || mSize > mArrayData.Length)
                {
                    mArrayData = new T[mSize];
                }
            }            
        }
    }


}
