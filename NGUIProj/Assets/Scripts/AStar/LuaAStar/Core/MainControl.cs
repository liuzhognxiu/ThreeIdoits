using System;using UnityEngine;using System.Collections;using System.Collections.Generic;using Components.AStar;using Components.Struct;


public class MainControl : MonoBehaviour
{    private byte[,] Matrix = new byte[64, 64]; //寻路用二维矩阵
    private float GridSize = 0.2f; //单位格子大小
    List<Rect> lst = new List<Rect>();    List<Rect> lst2 = new List<Rect>();    List<Rect> lst3 = new List<Rect>();    List<Rect> lst4 = new List<Rect>();    private IPathFinder PathFinder = null;

    private Point2D Start; //移动起点坐标
    private Point2D End;  //移动终点坐标
                          // Use this for initialization
    void Awake()
    {        ResetMatrix(); //初始化二维矩阵
        //Observable.EveryUpdate().Where(_ => Input.GetMouseButtonDown(0)).Subscribe(LeftMouseClick);    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LeftMouseClick(0);
        }
    }

    private void LeftMouseClick(long l)    {        lst2.Clear();        Rect rect = new Rect();        rect.width = GridSize;        rect.height = GridSize;        rect.x = GridSize;        rect.y = GridSize;        lst2.Add(rect);

        Start = new Point2D(1, 1);

        //获得屏幕坐标
        Vector3 p = Input.mousePosition;        int x = ((int)p.x / (int)(GridSize * 100));
        int y = ((int)p.y / (int)(GridSize * 100));        End = new Point2D(x, y);  //计算终点坐标
        Debug.Log("mouse:" + p + " x/GridSize:" + x + " y/GridSize:"+y);

        lst4.Clear();        lst4.Add(IndexConvertToRect(x, y));

        PathFinder = new PathFinderFast(Matrix);        PathFinder.Formula = HeuristicFormula.Manhattan; //使用我个人觉得最快的曼哈顿A*算法
        PathFinder.SearchLimit = 2000; //即移动经过方块(20*20)不大于2000个(简单理解就是步数)

        List<PathFinderNode> path = PathFinder.FindPath(Start, End); //开始寻径

        if (path == null)        {            Debug.Log("路径不存在！");
        }        else        {            string output = string.Empty;            for (int i = path.Count - 1; i >= 0; i--)            {                output = string.Format(output                    + "{0}"                    + path[i].X.ToString()                    + "{1}"                    + path[i].Y.ToString()                    + "{2}",                    "(", ",", ") ");

                lst2.Add(IndexConvertToRect(path[i].X, path[i].Y));            }            Debug.Log("路径坐标分别为:" + output);

        }    }

    private void ResetMatrix()    {        Debug.Log("Matrix.GetUpperBound(1) " + Matrix.GetUpperBound(1));        Debug.Log("Matrix.GetUpperBound(0) " + Matrix.GetUpperBound(0));        for (int y = 0; y < Matrix.GetUpperBound(1); y++)        {            for (int x = 0; x < Matrix.GetUpperBound(0); x++)            {
                //默认值可以通过在矩阵中用1表示
                Matrix[x, y] = 1;

                Rect rectx = new Rect();                rectx.width = GridSize;                rectx.height = GridSize;                rectx.x = x * GridSize;                rectx.y = y * GridSize;                lst3.Add(rectx);            }        }

        Rect rect = new Rect();

        //构建障碍物
        for (int i = 0; i < 18; i++)        {
            //障碍物在矩阵中用0表示
            Matrix[i, 12] = 0;


            rect = new Rect();            rect.width = GridSize;            rect.height = GridSize;            rect.x = i * GridSize;            rect.y = 12 * GridSize;            lst.Add(rect);        }        for (int i = 13; i < 17; i++)        {            Matrix[17, i] = 0;
            rect = new Rect();            rect.width = GridSize;            rect.height = GridSize;

            rect.x = 17 * GridSize;            rect.y = i * GridSize;            lst.Add(rect);        }        for (int i = 3; i < 18; i++)        {            Matrix[i, 16] = 0;
            rect = new Rect();            rect.width = GridSize;            rect.height = GridSize;

            rect.x = i * GridSize;            rect.y = 16 * GridSize;            lst.Add(rect);        }

    }    void OnDrawGizmos()    {
        //lst3.foreach ((r) =>
        // {
        //     othertest.drawrect(r, color.green);
        // }) ;

        lst.ForEach((r) =>        {            otherTest.DrawRect(r, Color.yellow);        });

        lst2.ForEach((r) =>        {            otherTest.DrawRect(r, Color.red);        });

        lst4.ForEach((r) =>        {            otherTest.DrawRect(r, Color.white);        });    }

    public Rect IndexConvertToRect(int x, int y)    {        Rect rect2 = new Rect();

        rect2.x = Convert.ToSingle(x * GridSize);        rect2.y = Convert.ToSingle(y * GridSize);        rect2.width = GridSize;        rect2.height = GridSize;

        return rect2;    }}
