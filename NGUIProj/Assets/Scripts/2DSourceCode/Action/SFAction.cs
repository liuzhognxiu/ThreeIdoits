

//-------------------------------------------------------------------------
//动作
//Author LiZongFu
//Time 2015.12.15
//-------------------------------------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface ISFAction
{
    CSDirection Direction { get; }
    CSMotion Motion { get; }
}
