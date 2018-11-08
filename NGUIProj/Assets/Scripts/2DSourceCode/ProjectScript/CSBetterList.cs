//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2015 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;
using System.Collections.Generic;
using System.Diagnostics;
using System;
/// <summary>
/// This improved version of the System.Collections.Generic.List that doesn't release the buffer on Clear(),
/// resulting in better performance and less garbage collection.
/// PRO: BetterList performs faster than List when you Add and Remove items (although slower if you remove from the beginning).
/// CON: BetterList performs worse when sorting the list. If your operations involve sorting, use the standard List instead.
/// </summary>

public class CSBetterList<T>
{
    /// <summary>
    /// Direct access to the buffer. Note that you should not use its 'Length' parameter, but instead use BetterList.size.
    /// </summary>

    public List<T> buffer = new List<T>();

    /// <summary>
    /// Direct access to the buffer's size. Note that it's only public for speed and efficiency. You shouldn't modify it.
    /// </summary>

    public int size = 0;

    public int Count
    {
        get { return size; }
    }
    /// <summary>
    /// For 'foreach' functionality.
    /// </summary>

    [DebuggerHidden]
    [DebuggerStepThrough]
    public IEnumerator<T> GetEnumerator()
    {
        if (buffer != null)
        {
            for (int i = 0; i < size; ++i)
            {
                yield return buffer[i];
            }
        }
    }

    /// <summary>
    /// Convenience function. I recommend using .buffer instead.
    /// </summary>

    [DebuggerHidden]
    public T this[int i]
    {
        get { return buffer[i]; }
        set { buffer[i] = value; }
    }

    /// <summary>
    /// Trim the unnecessary memory, resizing the buffer to be of 'Length' size.
    /// Call this function only if you are sure that the buffer won't need to resize anytime soon.
    /// </summary>

    void Trim()
    {
        if (size > 0)
        {
            if (size < buffer.Count)
            {
                buffer.RemoveRange(size,buffer.Count - size);
            }
        }
        else buffer = null;
    }

    /// <summary>
    /// Clear the array by resetting its size to zero. Note that the memory is not actually released.
    /// </summary>
    public void Clear() 
    { 
        size = 0;
    }

    /// <summary>
    /// Clear the array and release the used memory.
    /// </summary>

    public void Release() { size = 0;
    if (buffer != null) buffer.Clear();
    //buffer = null;
    }

    /// <summary>
    /// Add the specified item to the end of the list.
    /// </summary>

    public void Add(T item)
    {
        if (size == buffer.Count)
        {
            buffer.Add(item);
            size++;
            //AllocateMore();
        }
        else
        {
            buffer[size++] = item;
        }
    }

    public void AddRange(CSBetterList<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Add(list[i]);
        }
    }

    public void AddRange(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Add(list[i]);
        }
    }

    public void Reverse()
    {
        int length = Count / 2;
        for (int i = 0; i < length; i++)
        {
            T item = buffer[i];
            buffer[i] = buffer[Count - i - 1];
            buffer[Count - i - 1] = item;
        }
    }

    /// <summary>
    /// Insert an item at the specified index, pushing the entries back.
    /// </summary>

    public void Insert(int index, T item)
    {
        if (size == buffer.Count)
        {
            buffer.Add(item);
        }

        if (index > -1 && index < size)
        {
            for (int i = size; i > index; --i) buffer[i] = buffer[i - 1];
            buffer[index] = item;
            ++size;
        }
        else Add(item);
    }

    public void Insert(int index, CSBetterList<T> list)
    {
        if (list == null) return;
        for (int i = list.Count - 1; i >= 0; i--)
        {
            Insert(index, list[i]);
        }
    }

    /// <summary>
    /// Returns 'true' if the specified item is within the list.
    /// </summary>

    public bool Contains(T item)
    {
        if (buffer == null) return false;
        for (int i = 0; i < size; ++i) if (buffer[i].Equals(item)) return true;
        return false;
    }

    /// <summary>
    /// Return the index of the specified item.
    /// </summary>

    public int IndexOf(T item)
    {
        if (buffer == null) return -1;
        for (int i = 0; i < size; ++i) if (buffer[i].Equals(item)) return i;
        return -1;
    }

    /// <summary>
    /// Remove the specified item from the list. Note that RemoveAt() is faster and is advisable if you already know the index.
    /// </summary>

    public bool Remove(T item)
    {
        if (buffer != null)
        {
            EqualityComparer<T> comp = EqualityComparer<T>.Default;

            for (int i = 0; i < size; ++i)
            {
                if (comp.Equals(buffer[i], item))
                {
                    --size;
                    buffer[i] = default(T);
                    for (int b = i; b < size; ++b) buffer[b] = buffer[b + 1];
                    buffer[size] = default(T);
                    return true;
                }
            }
        }
        return false;
    }

    /// <summary>
    /// Remove an item at the specified index.
    /// </summary>

    public void RemoveAt(int index)
    {
        if (buffer != null && index > -1 && index < size)
        {
            --size;
            buffer[index] = default(T);
            for (int b = index; b < size; ++b) buffer[b] = buffer[b + 1];
            buffer[size] = default(T);
        }
    }

	public void RemoveRange(int startIndex)//包括startIndex
	{
		if (startIndex <= -1) 
		{
			Clear();
			return;
		}
		if (startIndex >= size)return;
		size = startIndex;
		return;
	}

    /// <summary>
    /// Remove an item from the end.
    /// </summary>

    public T Pop()
    {
        if (buffer != null && size != 0)
        {
            T val = buffer[--size];
            buffer[size] = default(T);
            return val;
        }
        return default(T);
    }

    /// <summary>
    /// Mimic List's ToArray() functionality, except that in this case the list is resized to match the current size.
    /// </summary>

    //public T[] ToArray() { Trim(); return buffer; }

    //class Comparer : System.Collections.IComparer
    //{
    //    public System.Comparison<T> func;
    //    public int Compare (object x, object y) { return func((T)x, (T)y); }
    //}

    //Comparer mComp = new Comparer();

    /// <summary>
    /// List.Sort equivalent. Doing Array.Sort causes GC allocations.
    /// </summary>

    //public void Sort (System.Comparison<T> comparer)
    //{
    //    if (size > 0)
    //    {
    //        mComp.func = comparer;
    //        System.Array.Sort(buffer, 0, size, mComp);
    //    }
    //}

    /// <summary>
    /// List.Sort equivalent. Manual sorting causes no GC allocations.
    /// </summary>

    [DebuggerHidden]
    [DebuggerStepThrough]
    public void Sort(CompareFunc comparer)
    {
        int start = 0;
        int max = size - 1;
        bool changed = true;

        while (changed)
        {
            changed = false;

            for (int i = start; i < max; ++i)
            {
                // Compare the two values
                if (comparer(buffer[i], buffer[i + 1]) > 0)
                {
                    // Swap the values
                    T temp = buffer[i];
                    buffer[i] = buffer[i + 1];
                    buffer[i + 1] = temp;
                    changed = true;
                }
                else if (!changed)
                {
                    // Nothing has changed -- we can start here next time
                    start = (i == 0) ? 0 : i - 1;
                }
            }
        }
    }

    /// <summary>
    /// Comparison function should return -1 if left is less than right, 1 if left is greater than right, and 0 if they match.
    /// </summary>

    public delegate int CompareFunc(T left, T right);

    public delegate bool CompareFunc_2(T t, object obj);

    public void FindAll(Predicate<T> match, CSBetterList<T> list)
    {
        list.Clear();
        for (int i = 0; i < size; i++)
        {
            if (match(buffer[i]))
            {
                list.Add(buffer[i]);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="match"></param>
    /// <param name="obj">值类型装换成object有装箱操作，有一定的性能消耗，防止大量的装箱的出现</param>
    /// <param name="list"></param>
    public void FindAll(CompareFunc_2 match, object obj, CSBetterList<T> list)
    {
        list.Clear();
        for (int i = 0; i < size; i++)
        {
            if (match(buffer[i], obj))
            {
                list.Add(buffer[i]);
            }
        }
    }

    public T Find(Predicate<T> match)
    {
        for (int i = 0; i < size; i++)
        {
            if (match(buffer[i]))
            {
                return buffer[i];
            }
        }
        return default(T);
    }

    public T Find(CompareFunc_2 match, object obj)
    {
        for (int i = 0; i < size; i++)
        {
            if (match(buffer[i], obj))
            {
                return buffer[i];
            }
        }
        return default(T);
    }

    public CSBetterList<T> GetRange(int startIndex, int count, CSBetterList<T> list)
    {
        list.Clear();
        for (int i = startIndex; i < startIndex + count; i++)
        {
            if (i < 0) continue;
            if (i < size)
            {
                list.Add(buffer[i]);
            }
        }
        return list;
    }

    public T GetUselessData()
    {
        if (buffer.Count > size) return buffer[size];
        return default(T);
    }
}
