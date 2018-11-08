using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
/// <summary>
/// 用在数据较多，并且较频繁遍历value的字典序，数据较少的情况下，用BetterList代替,不建议Clear接口，由于Dir本身产生的内存就是List的好几倍，如果频繁的Clear，Add也是对GC Collect的影响
/// 有额外的内存开销，减少gc collect
/// </summary>
/// <typeparam name="Key"></typeparam>
/// <typeparam name="Value"></typeparam>
public class CSBetterDic<Key,Value>
{
    public CSBetterDic()
    {

    }
    public CSBetterDic(bool isHasKey, bool isHasValue)
    {
        mIsHasKeys = isHasKey;
        mIsHasValues = isHasValue;
    }

    private Dictionary<Key, Value> mDic = new Dictionary<Key, Value>();
    public Dictionary<Key, Value> Dic
    {
        get { return mDic; }
        set { mDic = value; }
    }

    private bool mIsHasKeys = false;

    public bool mIsHasValues = false;

    private CSBetterList<Key> mKeys = null;
    private CSBetterList<Key> Keys
    {
        get
        {
            if (mKeys == null)
            {
                mKeys = new CSBetterList<Key>();
            }
            return mKeys;
        }
    }

    private CSBetterList<Value> mValues = null;
    private CSBetterList<Value> Values
    {
        get {
            if (mValues == null)
            {
                mValues = new CSBetterList<Value>();
            }
            return mValues; }
    }

    public Value this[Key i]
    {
        get { return mDic[i]; }
        set
        {
            if (mIsHasValues)
            {
                mValues.Remove(mDic[i]);
                mValues.Add(value);
            }
            mDic[i] = value;
        }
    }

    public int Count
    {
        get { return mDic.Count; } 
    }

    public Key GetKey(int index)
    {
        if (!mIsHasKeys) return default(Key);
        if (index >= 0 && index < Count)
        {
            return Keys[index];
        }
        return default(Key);
    }

    public Value GetValue(int index)
    {
        if (!mIsHasValues) return default(Value);
        if(index>=0&&index<Count)
        {
            return Values[index];
        }
        return default(Value);
    }

    public void Add(Key key,Value value,bool isCovery = false)
    {
        if(!mDic.ContainsKey(key))
        {
            mDic.Add(key, value);
            if (mIsHasKeys) Keys.Add(key);
            if(mIsHasValues)Values.Add(value);
        }
        else
        {
            if (isCovery)
            {
                if (mIsHasKeys)
                {
                    int index = Keys.IndexOf(key);
                    Keys[index] = key;
                }

                if (mIsHasValues)
                {
                    int index = Values.IndexOf(mDic[key]);
                    Values[index] = value;
                }

                mDic[key] = value;
            }
        }
    }

    public void Remove(Key key)
    {
        if (mDic.ContainsKey(key))
        {
            Value value = mDic[key];
            if (mIsHasKeys) Keys.Remove(key);
            if(mIsHasValues)Values.Remove(value);
            mDic.Remove(key);
        }
    }

    public bool ContainsKey(Key key)
    {
        return mDic.ContainsKey(key);
    }

    public bool ContainsValue(Value value)
    {
        return mDic.ContainsValue(value);
    }

    public void Clear()
    {
        mDic.Clear();
        if (mIsHasKeys) Keys.Clear();
        if(mIsHasValues)Values.Clear();
    }

    public void FindAll(Predicate<Value> match, CSBetterList<Value> list)
    {
        Values.FindAll(match, list);
    }

    public void FindAll(CSBetterList<Value>.CompareFunc_2 match, object obj, CSBetterList<Value> list)
    {
        Values.FindAll(match, obj, list);
    }

    public Value Find(Predicate<Value> match)
    {
        return Values.Find(match);
    }

    public Value Find(CSBetterList<Value>.CompareFunc_2 match, object obj)
    {
        return Values.Find(match, obj);
    }

    public void GetRange(int startIndex, int count, CSBetterList<Value> list)
    {
        Values.GetRange(startIndex, count, list);
    }
}
