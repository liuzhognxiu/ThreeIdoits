using UnityEngine;
using System.Collections;
public enum CSMotion // 怪物死亡和角色展示 是一个动画
{
    Static,         // 无
    Stand,          // 待机
    Walk,           // 走路
    Attack,         // 攻击
    Attack2,        // 法攻击
    BeAttack,       // 被击
    Dead,           // 死亡
    Mining,         // 挖矿
    ShowStand,      // 展示
    Run,            // 跑步
    WaKuang, //挖矿
    GuWu,//鼓舞
    RunOverDoSmoething,//跑过去做一些东西
}
// 编程者自己左右上下
public enum CSDirection //移动方向
{
    Up,                 // 上    0
    Right_Up,           // 右上   1
    Right,              // 右    2
    Right_Down,         // 右下   3
    Down,               // 下     4
    Left_Down,          // 左下   5
    Left,               // 左    6
    Left_Up,            // 左上   7
    None,
}

public enum SFCellType
{

    Normal = 0,

    Lucency = 1,

    Resistance = 2,

    Protect = 3,

    Born = 4,

    Stallge = 5,

    Diggable = 6,

    Transmit = 7,

    Resurgence = 8,

    SafeEffectPoint = 9,

    Separate = 10,

    Special_1 = 11,

    Special_2 = 12,

    Special_3 = 13,

    Special_4 = 14,

    Special_5 = 15,

    Special_6 = 16,

    Special_7 = 17,

    Special_8 = 18
}
public enum ModelBearing
{
    Head,
    Body,
    UnderFoot,
    Hand,
    HandLeft,
    HandRight,
    Foot,
    FootLeft,
    FootRight,
    Bottom,
    Front,
    Around,
    Back,
}
/// <summary>
/// 结构
/// </summary>
public enum ModelStructure
{
    Structure,
    Shadow,
    Body,
    Weapon,
    Bottom,
    Effect,
    Wing,
}

public enum EActionStopFrameType
{
    None,
    First,
    End,
    LastFrame,
}