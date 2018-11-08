using System.Collections.Generic;
using Components.Struct;

namespace Components.AStar
{
    interface IPathFinder
    {
        #region �¼�
        event PathFinderDebugHandler PathFinderDebug;
        #endregion

        #region ����
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

        #region ����
        void FindPathStop();
        List<PathFinderNode> FindPath(Point2D start, Point2D end);
        #endregion

    }
}
