using System.Collections.Generic;
using Components.Struct;

namespace Components.AStar
{
    interface IPathFinder
    {
        #region 事件
        event PathFinderDebugHandler PathFinderDebug;
        #endregion

        #region 属性
        bool Stopped
        {
            get;
        }

        HeuristicFormula Formula
        {
            get;
            set;
        }

        bool Diagonals
        {
            get;
            set;
        }

        bool HeavyDiagonals
        {
            get;
            set;
        }

        int HeuristicEstimate
        {
            get;
            set;
        }

        bool PunishChangeDirection
        {
            get;
            set;
        }

        bool TieBreaker
        {
            get;
            set;
        }

        int SearchLimit
        {
            get;
            set;
        }

        double CompletedTime
        {
            get;
            set;
        }

        bool DebugProgress
        {
            get;
            set;
        }

        bool DebugFoundPath
        {
            get;
            set;
        }
        #endregion

        #region 方法
        void FindPathStop();
        List<PathFinderNode> FindPath(Point2D start, Point2D end);
        #endregion

    }
}
