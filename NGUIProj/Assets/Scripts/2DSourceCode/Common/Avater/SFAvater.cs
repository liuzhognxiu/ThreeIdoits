
//-------------------------------------------------------------------------
//Avatera
//Author LiZongFu
//Time 2015.12.15
//-------------------------------------------------------------------------

using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public enum EAvatarType
{
    None,
    MainPlayer,
    Player,
    Hero,
    Monster,
    NPC,
    Pet,
    Item,
    Guard,
    Trigger,
    Grave,
}

public enum EMoveState
{
    initiative,  // 主动
    YeManChongZhuang, // 被动
}

public interface ISFAvater
{
    Transform CacheTransform { get; set; }
    bool isDead { get;}
    SFMisc.Dot2 getOldCellPos { get; }
    Vector3 getRealPosition2();
    long ID { get; }
    EAvatarType AvatarType { get; }
    bool isLoad { get; set; }
    EMoveState MoveState { get; set; }
    bool IsDataSplit { get; }
    ISFModel getIModel { get; }
    void IOnOldCellChange();
    void OnChangeOneFrame(int frame);
    BoxCollider getBox { get; set; }
}