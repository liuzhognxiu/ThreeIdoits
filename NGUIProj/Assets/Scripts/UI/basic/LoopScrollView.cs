using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class LoopScrollView : MonoBehaviour
{
    public enum Arrangement
    {
        Horizontal,
        Vertical,
    }
    /// <summary>
    /// 视图中最多显示item的个数
    /// </summary>
    [HideInInspector]
    public int MaxShowCount;
    public bool EnableMaxPerline = false;
    private int RealShowCount;
    /// <summary>
    /// 视图中每个方向缓存的个数
    /// </summary>
    [HideInInspector]
    public int InvisibleCache = 4;

    public Arrangement arrangement = Arrangement.Horizontal;
    public UIWidget.Pivot pivot = UIWidget.Pivot.TopLeft;
    public float cellWidth = 200f;
    public float cellHeight = 200f;
    /// <summary>
    /// 排列方式
    /// </summary>
    public enum ArrangeDirection
    {
        Left_to_Right,
        Right_to_Left,
        Up_to_Down,
        Down_to_Up,
    }
    /// <summary>  
    /// items的排列方式默认从上到下
    /// </summary>  
    [HideInInspector]
    public ArrangeDirection arrangeDirection = ArrangeDirection.Up_to_Down;

    /// <summary>  
    /// 列表单项模板，注意不要放在itemParent目录下  
    /// </summary>  
    public GameObject itemPrefab;

    /// <summary>  
    /// The items list.  
    /// </summary>  
    [HideInInspector]
    public List<LoopItemObject> itemsList;
    /// <summary>  
    /// The datas list.  
    /// </summary>  
    public List<LoopItemData> datasList;

    /// <summary>  
    /// 列表脚本  
    /// </summary>  
    public UIScrollView scrollView;

    public GameObject itemParent;

    public int maxPerLine = 0;

    /// <summary>  
    /// itemsList的第一个元素  
    /// </summary>  
    [HideInInspector]
    private LoopItemObject _firstItem = null;
    public LoopItemObject firstItem
    {
        get
        {
            return _firstItem;
        }
        set
        {
            if (_firstItem != null)
            {
                _firstItem.widget.color = Color.white;
            }
            _firstItem = value;
        }
    }

    /// <summary>  
    /// itemsList的最后一个元素  
    /// </summary>  
    private LoopItemObject _lastItem = null;
    LoopItemObject lastItem
    {
        get
        {
            return _lastItem;
        }
        set
        {
            _lastItem = value;
            //if (_lastItem != null)
            //    UnityEngine.Debug.Log("################### _lastItem.dataIndex: " + _lastItem.dataIndex);
        }
    }


    public delegate void DelegateHandler(LoopItemObject item, LoopItemData data);
    /// <summary>  
    /// 响应  
    /// </summary>  
    public DelegateHandler OnItemInit;

    /// <summary>  
    /// 第一item的起始位置  
    /// </summary>  
    [HideInInspector]
    public Vector3 itemStartPos = Vector3.zero;
    /// <summary>  
    /// 菜单项间隙  
    /// </summary>  
    [HideInInspector]
    public float gapDis = 0f;
    /// <summary>  
    /// 开始拖动的位置 
    /// </summary>  
    private Vector3 startDragPos;
    /// <summary>  
    /// 结束拖动的位置 
    /// </summary>  
    private Vector3 endDragPos;


    // 对象池  
    // 再次优化，频繁的创建与销毁  
    Queue<LoopItemObject> itemLoop = new Queue<LoopItemObject>();

    void Awake()
    {
        if (itemPrefab == null || scrollView == null || itemParent == null)
        {
            if (Debug.developerConsoleVisible) Debug.LogError("LoopScrollView.Awake() 有属性没有在inspector中赋值");
        }

        // 设置scrollview的movement  
        if (arrangeDirection == ArrangeDirection.Up_to_Down ||
           arrangeDirection == ArrangeDirection.Down_to_Up)
        {
            scrollView.movement = UIScrollView.Movement.Vertical;
        }
        else
        {
            scrollView.movement = UIScrollView.Movement.Horizontal;
        }
        scrollView.onDragStarted += () =>
        {
            startDragPos = scrollView.transform.localPosition;
        };
        scrollView.onDragFinished += () =>
        {
            endDragPos = scrollView.transform.localPosition;
        };
        scrollView.onStoppedMoving += () =>
        {
            scrollView.UpdatePosition();
        };

        //这是一个偷懒的方式，防止从零开始时由上到下翻页的时候将需要显示的数据隐藏掉
        //数据隐藏掉会导致scrollView的Center改变（变小），导致springPanel-MomentumAndSpring回弹到变小之后的Center上
        if (arrangeDirection == ArrangeDirection.Up_to_Down)
        {
            InvisibleCache = MaxShowCount;
        }
    }



    // Update is called once per frame  
    void Update()
    {
        //if(scrollView.isDragging)  
        {
            Validate();
        }
    }

    protected void ResetPosition(List<LoopItemObject> list)
    {
        if (!EnableMaxPerline) return;
        int x = 0;
        int y = 0;
        int maxX = 0;
        int maxY = 0;
        Transform myTrans = transform;

        for (int i = 0, imax = list.Count; i < imax; ++i)
        {
            Transform t = list[i].widget.transform;

            float depth = t.localPosition.z;
            x = (list[i].dataIndex) % maxPerLine;
            y = (list[i].dataIndex) / maxPerLine;
            Vector3 pos = (arrangement == Arrangement.Horizontal) ?
                new Vector3(cellWidth * x, -cellHeight * y, depth) :
                new Vector3(cellWidth * y, -cellHeight * x, depth);

            t.localPosition = pos;

            maxX = Mathf.Max(maxX, x);
            maxY = Mathf.Max(maxY, y);

            //x = (list[i].dataIndex + 1) % maxPerLine;
            //y = (list[i].dataIndex + 1)/ maxPerLine;

            //if (++x >= maxPerLine && maxPerLine > 0)
            //{
            //    x = 0;
            //    ++y;
            //}
        }

        // Apply the origin offset
        if (pivot != UIWidget.Pivot.TopLeft)
        {
            Vector2 po = NGUIMath.GetPivotOffset(pivot);

            float fx, fy;

            if (arrangement == Arrangement.Horizontal)
            {
                fx = Mathf.Lerp(0f, maxX * cellWidth, po.x);
                fy = Mathf.Lerp(-maxY * cellHeight, 0f, po.y);
            }
            else
            {
                fx = Mathf.Lerp(0f, maxY * cellWidth, po.x);
                fy = Mathf.Lerp(-maxX * cellHeight, 0f, po.y);
            }

            for (int i = 0; i < list.Count; ++i)
            {
                Transform t = list[i].widget.transform;
                SpringPosition sp = t.GetComponent<SpringPosition>();

                if (sp != null)
                {
                    sp.target.x -= fx;
                    sp.target.y -= fy;
                }
                else
                {
                    Vector3 pos = t.localPosition;
                    pos.x -= fx;
                    pos.y -= fy;
                    t.localPosition = pos;
                }
            }
        }
    }

    LoopItemObject GetSmallestDataIndexItem()
    {
        LoopItemObject smallestItem = null;
        int smallestIndex = int.MaxValue;
        if (itemsList != null)
        {
            foreach (LoopItemObject item in itemsList)
            {
                if (item.dataIndex < smallestIndex)
                {
                    smallestIndex = item.dataIndex;
                    smallestItem = item;
                }
            }
        }
        return smallestItem;
    }

    LoopItemObject GetBiggestDataIndexItem()
    {
        LoopItemObject biggestItem = null;
        int biggestIndex = int.MinValue;
        if (itemsList != null)
        {
            foreach (LoopItemObject item in itemsList)
            {
                if (item.dataIndex > biggestIndex)
                {
                    biggestIndex = item.dataIndex;
                    biggestItem = item;
                }
            }
        }
        return biggestItem;
    }

    /// <summary>  
    /// 检验items的两端是否要补上或删除  
    /// </summary>  
    void Validate()
    {
        if (datasList == null || datasList.Count == 0)
        {
            return;
        }

        // 如果itemsList还不存在  
        if (itemsList == null || itemsList.Count == 0)
        {
            itemsList = new List<LoopItemObject>();

            LoopItemObject item = GetItemFromLoop();
            if (tempFirstItem == -1)
            {
                InitItem(item, 0, datasList[0]);
            }
            else
            {
                InitItemDontReset(item, tempFirstItem, datasList[tempFirstItem]);
            }
            firstItem = lastItem = item;
            itemsList.Add(item);
            //Validate();  
        }

        //   
        bool all_invisible = true;
        for (int i = 0; i < itemsList.Count; i++)
        {
            if (itemsList[i].widget.isVisible == true)
            {
                all_invisible = false;
                tempFirstItem = -1;
            }
        }
        if (all_invisible == true)
        {
            if (tempFirstItem == -1)
            {
                if (endDragPos.y > startDragPos.y)
                {
                    if (lastItem.dataIndex < datasList.Count - 1)
                    {
                        if (itemsList.Count > 1)
                        {
                            itemsList.Remove(firstItem);
                            //UnityEngine.Debug.Log("(((((((((((((((((((((( itemList.count is: " + itemsList.Count);
                            PutItemToLoop(firstItem);

                            firstItem = GetSmallestDataIndexItem();//itemsList[0];
                        }

                        LoopItemObject item = GetItemFromLoop();
                        int index = lastItem.dataIndex + 1;
                        AddToBack(lastItem, item, index, datasList[index]);
                        lastItem = item;
                        itemsList.Add(item);
                    }
                }
                if (endDragPos.y < startDragPos.y)
                {
                    if (firstItem.dataIndex > 0)
                    {
                        LoopItemObject item = GetItemFromLoop();

                        int index = firstItem.dataIndex - 1;
                        AddToFront(firstItem, item, index, datasList[index]);
                        firstItem = item;
                        itemsList.Insert(0, item);

                        itemsList.Remove(lastItem);
                        //UnityEngine.Debug.Log(")))))))))))))))))))))) itemList.count is: " + itemsList.Count);
                        PutItemToLoop(lastItem);
                        lastItem = GetBiggestDataIndexItem();//itemsList[itemsList.Count - 1];
                    }
                }
                ResetPosition(itemsList);
                return;
            }
            else
            {
                if (lastItem.dataIndex < datasList.Count - 1)
                {
                    LoopItemObject item = GetItemFromLoop();
                    int index = lastItem.dataIndex + 1;
                    AddToBack(lastItem, item, index, datasList[index]);
                    lastItem = item;
                    itemsList.Add(item);
                }
                ResetPosition(itemsList);
                return;
            }
        }

        // 先判断前端是否要增减  
        if (firstItem.widget.isVisible)
        {
            // 判断要不要在它的前面补充一个item  
            if (firstItem.dataIndex > 0)
            {
                LoopItemObject item = GetItemFromLoop();

                // 初化：数据索引、大小、位置、显示  
                int index = firstItem.dataIndex - 1;
                //InitItem(item, index, datasList[index]);  
                AddToFront(firstItem, item, index, datasList[index]);
                firstItem = item;
                itemsList.Insert(0, item);


                //Validate();  
            }
        }
        else
        {
            // 判断要不要将它移除  
            // 条件：自身是不可见的；且它后一个item也是不可见的（或被被裁剪过半的）.  
            //      这有个隐含条件是itemsList.Count>=2.  
            if (itemsList.Count >= InvisibleCache)
            {
                bool isRemove = true;
                for (int i = 0; i < InvisibleCache; i++)
                {
                    if (itemsList[i].widget.isVisible)
                    {
                        isRemove = false;
                    }
                }



                if (isRemove)
                {
                    itemsList.Remove(firstItem);
                    //UnityEngine.Debug.Log("!!!!!!!!!!!!!!!!!! itemList.count is: " + itemsList.Count);
                    PutItemToLoop(firstItem);
                    firstItem = GetSmallestDataIndexItem();//itemsList[0];


                }

                //Validate();  
            }
        }

        // 再判断后端是否要增减  
        if (lastItem.widget.isVisible)
        {
            // 判断要不要在它的后面补充一个item  
            //UnityEngine.Debug.Log("11111111111111 lastItem.dataIndex " + lastItem.dataIndex + " datasList.Count " + datasList.Count + " itemlist count is: " + itemsList.Count);
            if (lastItem.dataIndex < datasList.Count - 1)
            {
                //UnityEngine.Debug.Log("2222222222222222 lastItem.dataIndex " + lastItem.dataIndex + " datasList.Count " + datasList.Count);

                LoopItemObject item = GetItemFromLoop();

                // 初化：数据索引、大小、位置、显示  
                int index = lastItem.dataIndex + 1;
                AddToBack(lastItem, item, index, datasList[index]);
                lastItem = item;
                itemsList.Add(item);

                //Validate();  
            }
        }
        else
        {
            // 判断要不要将它移除  
            // 条件：自身是不可见的；且它前一个item也是不可见的（或被被裁剪过半的）.  
            //      这有个隐含条件是itemsList.Count>=2.  
            //UnityEngine.Debug.Log("33333333333333333 lastItem.dataIndex " + lastItem.dataIndex + " datasList.Count " + datasList.Count);
            if (itemsList.Count >= InvisibleCache)
            {
                bool isRemove = true;
                for (int i = 1; i <= InvisibleCache; i++)
                {
                    if (itemsList[itemsList.Count - i].widget.isVisible)
                    {
                        isRemove = false;
                    }
                }
                if (isRemove)
                {
                    //UnityEngine.Debug.Log("444444444444444444444 lastItem.dataIndex " + lastItem.dataIndex + " datasList.Count " + datasList.Count);
                    itemsList.Remove(lastItem);
                    //UnityEngine.Debug.Log("*********************** itemList.count is: " + itemsList.Count);
                    PutItemToLoop(lastItem);
                    lastItem = GetBiggestDataIndexItem();//itemsList[itemsList.Count - 1];
                }

                //Validate();  
            }
        }

        //UnityEngine.Debug.Log("55555555555555555555555 itemsList.count " + itemsList.Count);
        ResetPosition(itemsList);

    }

    public void Test(int[] a)
    {
        for (int i = 0; i < a.Length; i++)
        {
            UnityEngine.Debug.Log(a[i]);
        }
    }

    /// <summary>  
    /// Init the specified datas.  
    /// </summary>  
    /// <param name="datas">Datas.</param>  
    public void Init(LoopItemData[] datas, DelegateHandler onItemInitCallback)
    {
        RealShowCount = MaxShowCount < datas.Length ? MaxShowCount : datas.Length;
        datasList = datas.ToList();
        // itemParent.GetComponent<UIWidget>().height = datas.Count * itemPrefab.GetComponent<UIWidget>().height;
        this.OnItemInit = onItemInitCallback;
        Validate();
    }

    /// <summary>  
    /// 构造一个 item 对象  
    /// </summary>  
    /// <returns>The item.</returns>  
    LoopItemObject CreateItem()
    {
        GameObject go = NGUITools.AddChild(itemParent, itemPrefab);
        UIWidget widget = go.GetComponent<UIWidget>();
        LoopItemObject item = new LoopItemObject();
        item.widget = widget;
        go.SetActive(true);
        return item;
    }

    /// <summary>  
    /// 用数据列表来初始化scrollview  
    /// </summary>  
    /// <param name="item">Item.</param>  
    /// <param name="indexData">Index data.</param>  
    /// <param name="data">Data.</param>  
    void InitItem(LoopItemObject item, int dataIndex, LoopItemData data)
    {
        if (item == null) return;
        //UnityEngine.Debug.Log("aaaaaaaaaaaaaaaa " + dataIndex);
        item.dataIndex = dataIndex;

        if (OnItemInit != null)
        {
            OnItemInit(item, data);
        }
        if (item.widget != null)
            item.widget.transform.localPosition = itemStartPos;
    }

    void InitItemDontReset(LoopItemObject item, int dataIndex, LoopItemData data)
    {
        //UnityEngine.Debug.Log("bbbbbbbbbbbbbbbbbb " + dataIndex);
        item.dataIndex = dataIndex;
        if (OnItemInit != null)
        {
            OnItemInit(item, data);
        }
        if (scrollView.movement == UIScrollView.Movement.Vertical)
        {
            float offsetY = (item.widget.height + gapDis) * dataIndex;
            if (arrangeDirection == ArrangeDirection.Down_to_Up) offsetY *= -1f;
            item.widget.transform.localPosition = itemStartPos - new Vector3(0, offsetY, 0);
        }
        else
        {
            float offsetX = (item.widget.width + gapDis) * dataIndex;
            if (arrangeDirection == ArrangeDirection.Right_to_Left) offsetX *= -1f;
            item.widget.transform.localPosition = itemStartPos + new Vector3(offsetX, 0, 0);
        }
    }

    /// <summary>  
    /// 在itemsList前面补上一个item  
    /// </summary>  
    void AddToFront(LoopItemObject priorItem, LoopItemObject newItem, int newIndex, LoopItemData newData)
    {
        InitItem(newItem, newIndex, newData);
        // 计算新item的位置  
        if (scrollView.movement == UIScrollView.Movement.Vertical)
        {
            float offsetY = priorItem.widget.height * 0.5f + gapDis + newItem.widget.height * 0.5f;
            if (arrangeDirection == ArrangeDirection.Down_to_Up) offsetY *= -1f;
            newItem.widget.transform.localPosition = priorItem.widget.cachedTransform.localPosition + new Vector3(0f, offsetY, 0f);
        }
        else
        {
            float offsetX = priorItem.widget.width * 0.5f + gapDis + newItem.widget.width * 0.5f;
            if (arrangeDirection == ArrangeDirection.Right_to_Left) offsetX *= -1f;
            newItem.widget.transform.localPosition = priorItem.widget.cachedTransform.localPosition - new Vector3(offsetX, 0f, 0f);
        }
    }

    /// <summary>  
    /// 在itemsList后面补上一个item  
    /// </summary>  
    void AddToBack(LoopItemObject backItem, LoopItemObject newItem, int newIndex, LoopItemData newData)
    {
        InitItem(newItem, newIndex, newData);
        // 计算新item的位置  
        if (scrollView.movement == UIScrollView.Movement.Vertical)
        {
            float offsetY = backItem.widget.height * 0.5f + gapDis + newItem.widget.height * 0.5f;
            if (arrangeDirection == ArrangeDirection.Down_to_Up) offsetY *= -1f;
            newItem.widget.transform.localPosition = backItem.widget.cachedTransform.localPosition - new Vector3(0f, offsetY, 0f);
        }
        else
        {
            float offsetX = backItem.widget.width * 0.5f + gapDis + newItem.widget.width * 0.5f;
            if (arrangeDirection == ArrangeDirection.Right_to_Left) offsetX *= -1f;
            newItem.widget.transform.localPosition = backItem.widget.cachedTransform.localPosition + new Vector3(offsetX, 0f, 0f);
        }
    }

    int tempFirstItem = -1;
    /// <summary>
    /// 设回初始状态
    /// </summary>
    /// <param name="isResetPos">是否重置位置</param>
    public void ResetToBegining(bool isResetPos = true)
    {
        //itemLoop = new Queue<LoopItemObject>();
        if (itemsList != null)
        {
            for (int i = 0; i < itemsList.Count; i++)
            {
                if (!itemLoop.Contains(itemsList[i]))
                {
                    itemsList[i].widget.gameObject.name = "0";
                    itemsList[i].widget.gameObject.SetActive(false);
                    PutItemToLoop(itemsList[i]);
                }
            }
        }
        if (firstItem != null)
        {
            tempFirstItem = isResetPos ? -1 : firstItem.dataIndex;
        }
        lastItem = null;
        firstItem = null;
        itemsList = null;
        if (isResetPos)
        {
            scrollView.ResetPosition();
        }
    }


    #region 对象池性能相关  
    /// <summary>  
    /// 从对象池中取行一个item  
    /// </summary>  
    /// <returns>The item from loop.</returns>  
    LoopItemObject GetItemFromLoop()
    {
        LoopItemObject item;
        if (itemLoop == null || itemLoop.Count <= 0)
        {
            item = CreateItem();
        }
        else
        {
            item = itemLoop.Dequeue();
            item.widget.gameObject.SetActive(true);
        }
        if (item != null && item.widget != null)
        {
            item.widget.gameObject.SetActive(true);
        }
        return item;
    }
    /// <summary>  
    /// 将要移除的item放入对象池中  
    /// --这个里我保证这个对象池中存在的对象不超过RealShowCount个  
    /// </summary>  
    /// <param name="item">Item</param>  
    void PutItemToLoop(LoopItemObject item)
    {
        if (itemLoop.Count >= RealShowCount)
        {

            Destroy(item.widget.gameObject);
            return;
        }
        item.dataIndex = -1;
        item.widget.gameObject.SetActive(false);
        if (item.widget.GetComponent<UIToggle>() != null)
        {
            item.widget.GetComponent<UIToggle>().Set(false);
        }
        itemLoop.Enqueue(item);
    }
    #endregion

}

[System.Serializable]
public class LoopItemObject
{
    /// <summary>  
    /// The widget.  
    /// </summary>  
    public UIWidget widget;

    /// <summary>  
    /// 本item，在实际整个scrollview中的索引位置，  
    /// 即对就数据，在数据列表中的索引  
    /// </summary>  
    public int dataIndex = -1;

}


