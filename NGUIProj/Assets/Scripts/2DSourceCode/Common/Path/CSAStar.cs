
//-----------------------------------------
//A星
//author lizongfu
//time 2016.3.11
//-----------------------------------------

using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node : IComparable
{
    public float nodeTotalCost;
    public float estimatedCost;
    public int Depth = 0;//用来排序用，相同数据排序问题
    public bool bObstacle = false;
    public ISfCell cell;
    public Node parent;
    public SFMisc.Dot2 coord;
    public Vector3 position;
    public bool isInOpenList = false;
    public bool isInClonseList = false;

    private long npcID = 0;
    public bool isCanCrossNpc
    {
        get
        {
            return npcID == 0;
        }
    }

    public bool isProtect
    {
        get
        {
            if (cell == null) cell = SFOut.IScene.getiMesh.getCellByISfCell(coord.x, coord.y);
            if (cell.isAttributesByISFCell((int)SFCellType.Protect)) return true;
            return false;
        }
    }
    /// <summary>
    /// 玩家数量
    /// </summary>
    public byte avatarNum
    {
        get { return UpdateAvatarNum(); }
    }
    private CSBetterList<long> mAvatarIDList = null;
    public CSBetterList<long> AvatarIDList
    {
        get
        {
            if (mAvatarIDList == null) mAvatarIDList = new CSBetterList<long>();
            return mAvatarIDList;
        }
    }

    private CSBetterList<long> mNotLoadAvatarIDList = null;
    public CSBetterList<long> NotLoadAvatarIDList
    {
        get
        {
            if (mNotLoadAvatarIDList == null) mNotLoadAvatarIDList = new CSBetterList<long>();
            return mNotLoadAvatarIDList;
        }
    }

    public Node(Vector3 pos, SFMisc.Dot2 coord)
    {
        this.estimatedCost = 0.0f;
        this.nodeTotalCost = 1.0f;
        this.parent = null;
        this.position = pos;
        this.coord = coord;
    }

    public void AddAvatarID(ISFAvater avatar)
    {
        if (avatar == null || avatar.ID == 0) return;

        if (avatar.AvatarType == EAvatarType.NPC)
            npcID = avatar.ID;

        if (avatar.isLoad)
        {
            if (!AvatarIDList.Contains(avatar.ID))
            {
                AvatarIDList.Add(avatar.ID);
            }
        }
        else
        {
            if (!NotLoadAvatarIDList.Contains(avatar.ID))
            {
                NotLoadAvatarIDList.Add(avatar.ID);
            }
        }
    }

    byte UpdateAvatarNum()
    {
        if (!SFOut.IGame.IsLanuchMainPlayer) return 0;
        if (AvatarIDList == null) return 0;
        byte num = 0;
        for (int i = 0; i < AvatarIDList.Count; i++)
        {
            ISFAvater a = SFOut.IScene.getAvatarByISFAvatar(AvatarIDList[i]);
            if (a != null && a.AvatarType == EAvatarType.Player)
            {
                num++;
            }
        }
        return num;
    }

    public void AddItemID(ISFItem avatar)
    {
        if (avatar == null || avatar.itemId == 0) return;
        if (!AvatarIDList.Contains(avatar.itemId))
        {
            AvatarIDList.Add(avatar.itemId);
        }
    }

    public void RemoveAvatarID(ISFAvater avatar)
    {
        if (avatar == null || avatar.ID == 0) return;
        if (avatar.AvatarType == EAvatarType.NPC && avatar.ID == npcID)
            npcID = 0;
        if (AvatarIDList.Contains(avatar.ID)/*&&!avatar.isLoad*/)//玩家死亡时，只是倒地，isLoad==true
        {
            AvatarIDList.Remove(avatar.ID);
        }
        //UpdateAvatarNum();        if (NotLoadAvatarIDList.Contains(avatar.ID))//玩家死亡时，只是倒地，isLoad==true
        {
            NotLoadAvatarIDList.Remove(avatar.ID);
        }
    }

    public void RemoveItemID(ISFItem avatar)
    {
        if (avatar == null || avatar.itemId == 0) return;
        AvatarIDList.Remove(avatar.itemId);
    }

    //Debug.Log("Remove = " + Coord.x + " " + Coord.y + "= " + avatar.BaseInfo.ID + " " + AvatarIDList.Count);
    public void MarkAsObstacle(bool b = true)
    {
        this.bObstacle = b;
    }

    public int CompareTo(object obj)
    {
        Node node = (Node)obj;

        if (this.estimatedCost < node.estimatedCost)
            return -1;
        if (this.estimatedCost > node.estimatedCost)
            return 1;

        if (this.Depth < node.Depth)
            return -1;
        if (this.Depth > node.Depth)
            return 1;
        return 0;
    }
}

public class PriorityQueue
{
    private CSBetterList<Node> nodes = new CSBetterList<Node>();

    public int Length
    {
        get { return this.nodes.Count; }
    }

    public bool Contains(Node node)
    {
        return this.nodes.Contains(node);
    }

    public Node First()
    {
        if (this.nodes.Count > 0)
        {
            float temp_estimatedCost = float.MaxValue;
            float temp_depth = float.MaxValue;
            int index = -1;
            for (int i = 0; i < nodes.Count; i++)
            {
                Node node = nodes[i];
                if (node.estimatedCost < temp_estimatedCost)
                {
                    index = i;
                    temp_estimatedCost = node.estimatedCost;
                    continue;
                }
                if (node.Depth < temp_depth)
                {
                    index = 0;
                    temp_depth = node.Depth;
                    continue;
                }
            }
            if (index != -1)
            {
                Node node = nodes[index];
                nodes.RemoveAt(index);
                return node;
            }
        }
        return null;
    }

    public void Push(Node node, bool isSort = true)
    {
        node.Depth = nodes.Count;
        this.nodes.Add(node);
        //if (isSort)
        //{
        //    this.nodes.Sort();
        //}
    }

    public void Remove(Node node)
    {
        this.nodes.Remove(node);

        //this.nodes.Sort();
    }

    public void Reset()
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            Node node = nodes[i];
            if (node != null)
            {
                node.isInOpenList = false;
                node.isInClonseList = false;
            }
        }
        nodes.Clear();
    }
}

public class CSAStar
{
    public static PriorityQueue openList = new PriorityQueue();
    public static PriorityQueue closedList = new PriorityQueue();

    private static float HeuristicCloselyCost(Node curNode, Node goalNode)
    {
        Vector2 dir = Vector2.zero;
        dir = Direction(curNode, goalNode, ref dir);
        dir.x = Mathf.Abs(dir.x);
        dir.y = Mathf.Abs(dir.y);
        if (dir.x == 1 && dir.y == 1) return 1.4f;
        return 1;
    }

    private static float HeuristicEstimateCost(Node curNode, Node goalNode)
    {
        Vector2 dir = Vector2.zero;
        dir = Direction(curNode, goalNode, ref dir);
        dir.x = Mathf.Abs(dir.x);
        dir.y = Mathf.Abs(dir.y);
        if (dir.x == 1 && dir.y == 1) return 1.4f;
        return dir.x + dir.y;
    }

    private static Vector2 Direction(Node curNode, Node goalNode, ref Vector2 dir)
    {
        dir.x = goalNode.coord.x - curNode.coord.x;
        dir.y = goalNode.coord.y - curNode.coord.y;
        return dir;
    }

    /// <summary>
    /// 隔断区域技能寻路问题
    /// </summary>
    /// <param name="start"></param>
    /// <param name="goal"></param>
    /// <param name="isAssist"></param>
    /// <param name="isMainPlayer"></param>
    /// <param name="assistList"></param>
    /// <returns></returns>
    public static CSBetterList<Node> FindPathBySeparate(Node start, Node goal, bool isAssist = false, bool isMainPlayer = false, CSBetterList<Node> assistList = null)
    {
        //SFMisc.Dot2 startCoord = new SFMisc.Dot2(41, 67);
        //SFMisc.Dot2 targetCoord = new SFMisc.Dot2(45, 65);

        //start = CSScene.Sington.Mesh.getNode(startCoord);
        //goal = CSScene.Sington.Mesh.getNode(targetCoord);
        bool isCanCrossScene = SFOut.IGame.isCanCrossScene;
        //isCanCrossScene = false;
        CSBetterList<Node> list = isAssist ? assistList : normalList;
        if (assistList != null) list = assistList;
        if (goal == null || start == null || goal.bObstacle || start.bObstacle || goal.coord.Equal(start.coord))
        {
            //Debug.Log("开始或目的点有阻挡点");
            if (list != null) list.Clear();
            return list;
        }
        if (start.cell == null) start.cell = SFOut.IScene.getiMesh.getCellByISfCell(start.coord.x, start.coord.y);
        if (goal.cell == null) goal.cell = SFOut.IScene.getiMesh.getCellByISfCell(goal.coord.x, goal.coord.y);

        //Debug.Log("start.position=" + start.coord.x + " " + start.coord.y+" goal.position = " + goal.coord.x + " " + goal.coord.y);
        start.parent = null;
        openList.Push(start);

        start.nodeTotalCost = 0.0f;
        start.estimatedCost = HeuristicEstimateCost(start, goal);
        Node node = null;
        CSBetterList<Node> neighbours = new CSBetterList<Node>();
        while (openList.Length != 0)
        {
            node = openList.First();
            node.isInOpenList = false;
            ISfCell cell = SFOut.IScene.getiMesh.getCellByISfCell(node.coord);
            if (cell != null && cell.isAttributesByISFCell((int)SFCellType.Separate))
            {
                string s = "";
            }
            if (node.coord.Equal(goal.coord))
            {
                Reset();
                return CalculatePath(node);
            }
            neighbours.Clear();
            SFOut.IScene.getiMesh.GetNeighbours(node, neighbours);
            for (int i = 0; i < neighbours.Count; i++)
            {
                Node neighbourNode = (Node)neighbours[i];
                //if (isMainPlayer && isAssist)
                //{
                //    if (!neighbourNode.coord.Equal(goal.coord))
                //    {
                //        if (/*isAssist && */!neighbourNode.isCanCrossNpc) continue;
                //        //if (neighbourNode.avatarNum >= 2) continue;
                //        if (!isCanCrossScene && !neighbourNode.isProtect && neighbourNode.avatarNum > 0 && CSScene.IsLanuchMainPlayer && CSScene.Sington.MainPlayer.MoveState != EMoveState.YeManChongZhuang) continue;
                //    }
                //}
                //if (neighbourNode.bObstacle)
                //{
                //    CSCell cell = CSScene.Sington.Mesh.getCell(neighbourNode.coord);
                //    if (cell != null && cell.isAttributes(MapEditor.CellType.Separate))
                //    {
                //        string s = "";
                //    }
                //}

                if (!neighbourNode.isInClonseList && !neighbourNode.bObstacle)
                {
                    float cost = HeuristicCloselyCost(node, neighbourNode);
                    float totalCost = node.nodeTotalCost + cost;
                    float neighbourNodeEstCost = HeuristicEstimateCost(neighbourNode, goal);

                    if (!neighbourNode.isInOpenList || totalCost < neighbourNode.nodeTotalCost)
                    {
                        neighbourNode.nodeTotalCost = totalCost;
                        neighbourNode.parent = node;
                        neighbourNode.estimatedCost = totalCost + neighbourNodeEstCost;
                    }
                    if (!neighbourNode.isInOpenList)
                    {
                        neighbourNode.isInOpenList = true;
                        openList.Push(neighbourNode);
                    }
                }
            }
            node.isInClonseList = true;
            closedList.Push(node, false);
            //node.isInOpenList = false;
            //openList.Remove(node);
        }
        Reset();
        if (node.position != goal.position)
        {
            // Debug.LogError("Goal Not Found = " + start.coord.x + "," + start.coord.y + "|" + goal.coord.x + "," + goal.coord.y);
            if (list != null) list.Clear();
            return list;
        }
        return CalculatePath(node, isAssist);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="start"></param>
    /// <param name="goal"></param>
    /// <param name="isAssist">true:使用辅助列表，不然会把已有的列表数据冲掉</param>
    /// <returns></returns>
    public static CSBetterList<Node> FindPath(Node start, Node goal, bool isAssist = false, bool isMainPlayer = false, CSBetterList<Node> assistList = null)
    {
        //SFMisc.Dot2 startCoord = new SFMisc.Dot2(41, 67);
        //SFMisc.Dot2 targetCoord = new SFMisc.Dot2(45, 65);

        //start = CSScene.Sington.Mesh.getNode(startCoord);
        //goal = CSScene.Sington.Mesh.getNode(targetCoord);
        bool isCanCrossScene = SFOut.IGame.isCanCrossScene;
        //isCanCrossScene = false;
        CSBetterList<Node> list = isAssist ? assistList : normalList;
        if (assistList != null) list = assistList;
        if (goal == null || start == null || goal.bObstacle || start.bObstacle || goal.coord.Equal(start.coord))
        {
            //Debug.Log("开始或目的点有阻挡点");
            if (list != null) list.Clear();
            return list;
        }
        if (start.cell == null) start.cell = SFOut.IScene.getiMesh.getCellByISfCell(start.coord.x, start.coord.y);
        if (goal.cell == null) goal.cell = SFOut.IScene.getiMesh.getCellByISfCell(goal.coord.x, goal.coord.y);
        if (start != null && goal != null)
        {
            if (goal.cell.isAttributesByISFCell((int)SFCellType.Separate) && start.cell.isAttributesByISFCell((int)SFCellType.Normal))
            {
                if (list != null) list.Clear();
                return list;
            }
            if (goal.cell.isAttributesByISFCell((int)SFCellType.Normal) && start.cell.isAttributesByISFCell((int)SFCellType.Separate))
            {
                if (list != null) list.Clear();
                return list;
            }
        }


        //Debug.Log("start.position=" + start.coord.x + " " + start.coord.y+" goal.position = " + goal.coord.x + " " + goal.coord.y);
        start.parent = null;
        openList.Push(start);

        start.nodeTotalCost = 0.0f;
        start.estimatedCost = HeuristicEstimateCost(start, goal);
        Node node = null;
        CSBetterList<Node> neighbours = new CSBetterList<Node>();
        while (openList.Length != 0)
        {
            node = openList.First();
            node.isInOpenList = false;
            if (node.coord.Equal(goal.coord))
            {
                Reset();
                return CalculatePath(node);
            }
            neighbours.Clear();
            SFOut.IScene.getiMesh.GetNeighbours(node, neighbours);
            for (int i = 0; i < neighbours.Count; i++)
            {
                Node neighbourNode = (Node)neighbours[i];
                if (isMainPlayer && isAssist)
                {
                    if (!neighbourNode.coord.Equal(goal.coord))
                    {
                        if (/*isAssist && */!neighbourNode.isCanCrossNpc) continue;
                        //if (neighbourNode.avatarNum >= 2) continue;
                        if (!isCanCrossScene && !neighbourNode.isProtect && neighbourNode.avatarNum > 0 && SFOut.IGame.IsLanuchMainPlayer && SFOut.IScene.getMainPlayer.getMoveStateBySF != EMoveState.YeManChongZhuang) continue;
                    }
                }

                if (!neighbourNode.isInClonseList && !neighbourNode.bObstacle)
                {
                    float cost = HeuristicCloselyCost(node, neighbourNode);
                    float totalCost = node.nodeTotalCost + cost;
                    float neighbourNodeEstCost = HeuristicEstimateCost(neighbourNode, goal);

                    if (!neighbourNode.isInOpenList || totalCost < neighbourNode.nodeTotalCost)
                    {
                        neighbourNode.nodeTotalCost = totalCost;
                        neighbourNode.parent = node;
                        neighbourNode.estimatedCost = totalCost + neighbourNodeEstCost;
                    }
                    if (!neighbourNode.isInOpenList)
                    {
                        neighbourNode.isInOpenList = true;
                        openList.Push(neighbourNode);
                    }
                }
            }
            node.isInClonseList = true;
            closedList.Push(node, false);
            //node.isInOpenList = false;
            //openList.Remove(node);
        }
        Reset();
        if (node.position != goal.position)
        {
            // Debug.LogError("Goal Not Found = " + start.coord.x + "," + start.coord.y + "|" + goal.coord.x + "," + goal.coord.y);
            if (list != null) list.Clear();
            return list;
        }
        return CalculatePath(node, isAssist);
    }

    public static CSBetterList<Node> FindPathInGrassScene(Node start, Node goal, bool isAssist = false, bool isMainPlayer = false, CSBetterList<Node> assistList = null)
    {
        //SFMisc.Dot2 startCoord = new SFMisc.Dot2(41, 67);
        //SFMisc.Dot2 targetCoord = new SFMisc.Dot2(45, 65);

        //start = CSScene.Sington.Mesh.getNode(startCoord);
        //goal = CSScene.Sington.Mesh.getNode(targetCoord);
        bool isCanCrossScene = SFOut.IGame.isCanCrossScene;
        //isCanCrossScene = false;
        CSBetterList<Node> list = isAssist ? assistList : normalList;
        if (assistList != null) list = assistList;
        bool isIgnoreResistance = false;
        if (goal == null || start == null || goal.bObstacle
            || start.bObstacle || goal.coord.Equal(start.coord))
        {
#if UNITY_EDITOR
            UnityEngine.Debug.LogError("开始或目的点有阻挡点");
#endif
            if (list != null) list.Clear();
            return list;
        }
        if (start.cell == null) start.cell = SFOut.IScene.getiMesh.getCellByISfCell(start.coord.x, start.coord.y);
        if (goal.cell == null) goal.cell = SFOut.IScene.getiMesh.getCellByISfCell(goal.coord.x, goal.coord.y);
        if (start != null && goal != null)
        {
            if (goal.cell.isAttributesByISFCell((int)SFCellType.Separate) && start.cell.isAttributesByISFCell((int)SFCellType.Normal))
            {
                if (list != null) list.Clear();
                return list;
            }
            if (goal.cell.isAttributesByISFCell((int)SFCellType.Normal) && start.cell.isAttributesByISFCell((int)SFCellType.Separate))
            {
                if (list != null) list.Clear();
                return list;
            }
        }


        //Debug.Log("start.position=" + start.coord.x + " " + start.coord.y+" goal.position = " + goal.coord.x + " " + goal.coord.y);
        start.parent = null;
        openList.Push(start);

        start.nodeTotalCost = 0.0f;
        start.estimatedCost = HeuristicEstimateCost(start, goal);
        Node node = null;
        CSBetterList<Node> neighbours = new CSBetterList<Node>();
        while (openList.Length != 0)
        {
            node = openList.First();
            node.isInOpenList = false;
            if (node.coord.Equal(goal.coord))
            {
                Reset();
                return CalculatePath(node);
            }
            neighbours.Clear();
            SFOut.IScene.getiMesh.GetNeighbours(node, neighbours);
            for (int i = 0; i < neighbours.Count; i++)
            {
                Node neighbourNode = (Node)neighbours[i];
                if (isMainPlayer && isAssist)
                {
                    if (!neighbourNode.coord.Equal(goal.coord))
                    {
                        if (/*isAssist && */!neighbourNode.isCanCrossNpc) continue;
                        //if (neighbourNode.avatarNum >= 2) continue;
                        if (!isCanCrossScene && !neighbourNode.isProtect && neighbourNode.avatarNum > 0 && SFOut.IGame.IsLanuchMainPlayer && SFOut.IScene.getMainPlayer.getMoveStateBySF != EMoveState.YeManChongZhuang) continue;
                    }
                }
                if (SFOut.IGame.IsNotCrossToAnthor(node.coord, neighbourNode.coord)) continue;

                if (!neighbourNode.isInClonseList && !neighbourNode.bObstacle && SFOut.IGame.IsCanMoveFromSafeArea(node, neighbourNode))
                {
                    float cost = HeuristicCloselyCost(node, neighbourNode);
                    float totalCost = node.nodeTotalCost + cost;
                    float neighbourNodeEstCost = HeuristicEstimateCost(neighbourNode, goal);

                    if (!neighbourNode.isInOpenList || totalCost < neighbourNode.nodeTotalCost)
                    {
                        neighbourNode.nodeTotalCost = totalCost;
                        neighbourNode.parent = node;
                        neighbourNode.estimatedCost = totalCost + neighbourNodeEstCost;
                    }
                    if (!neighbourNode.isInOpenList)
                    {
                        neighbourNode.isInOpenList = true;
                        openList.Push(neighbourNode);
                    }
                }
            }
            node.isInClonseList = true;
            closedList.Push(node, false);
            //node.isInOpenList = false;
            //openList.Remove(node);
        }
        Reset();
        if (node.position != goal.position)
        {
            // Debug.LogError("Goal Not Found = " + start.coord.x + "," + start.coord.y + "|" + goal.coord.x + "," + goal.coord.y);
            if (list != null) list.Clear();
            return list;
        }
        return CalculatePath(node, isAssist);
    }

    static void Reset()
    {
        if (openList != null)
        {
            openList.Reset();
        }
        if (closedList != null)
        {
            closedList.Reset();
        }

    }


    public static CSBetterList<Node> normalList = new CSBetterList<Node>();
    public static CSBetterList<Node> assistList = new CSBetterList<Node>();
    private static CSBetterList<Node> CalculatePath(Node node, bool isAssist = false)
    {
        CSBetterList<Node> data = isAssist ? assistList : normalList;
        data.Clear();
        while (node != null)
        {
            data.Add(node);
            node = node.parent;

            if (data.Count > 1000)
            {
                return null;
            }
        }
        data.Reverse();
        return data;
    }
}
