using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class PathSolver<TPathNode, TUserContext> : SettlersEngine.SpatialAStar<TPathNode,
    TUserContext> where TPathNode : SettlersEngine.IPathNode<TUserContext>
{
    protected override Double Heuristic(PathNode inStart, PathNode inEnd)
    {
        //int formula = GameManager.distance;
        int formula = 3;
        int dx = Math.Abs(inStart.X - inEnd.X);
        int dy = Math.Abs(inStart.Y - inEnd.Y);

        if (formula == 0)
            return Math.Sqrt(dx * dx + dy * dy); //Euclidean distance

        else if (formula == 1)
            return (dx * dx + dy * dy); //Euclidean distance squared

        else if (formula == 2)
            return Math.Min(dx, dy); //Diagonal distance

        else if (formula == 3)
            return (dx * dy) + (dx + dy); //Manhatten distance



        else
            return Math.Abs(inStart.X - inEnd.X) + Math.Abs(inStart.Y - inEnd.Y);

        //return 1*(Math.Abs(inStart.X - inEnd.X) + Math.Abs(inStart.Y - inEnd.Y) - 1); //optimized tile based Manhatten
        //return ((dx * dx) + (dy * dy)); //Khawaja distance
    }

    protected override Double NeighborDistance(PathNode inStart, PathNode inEnd)
    {
        return Heuristic(inStart, inEnd);
    }

    public PathSolver(TPathNode[,] inGrid)
        : base(inGrid)
    {
    }
}

