

//-------------------------------------------------------------------------
//Avater行为
//Author LiZongFu
//Time 2015.12.15
//-------------------------------------------------------------------------

using UnityEngine;
using System.Collections;

public abstract class BehaviourProvider
{
    public abstract bool InitializeFSM(FSMState fsm);

    public abstract void Reset();

    public delegate void OnRunOverDoSmoethingStart(ISFAvater avatar);

    public OnRunOverDoSmoethingStart onRunOverDoSmoethingStart;

    public delegate void OnRunOverDoSmoethingEnd(ISFAvater avatar);

    public OnRunOverDoSmoethingEnd onRunOverDoSmoethingEnd;
}
