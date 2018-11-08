

//-------------------------------------------------------------------------
//Avater行为
//Author LiZongFu
//Time 2015.12.15
//-------------------------------------------------------------------------

using UnityEngine;
using System.Collections;

public abstract class BehaivorProvider
{
    public abstract bool InitializeFSM(FSMState fsm);

    public abstract void Reset();
}
