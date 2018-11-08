
//-------------------------------------------------------------------------
//Game Sate
//Author LiZongFu
//Time 2015.12.15
//-------------------------------------------------------------------------

using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;

public interface ICBGame
{
    bool IsLoadLocalRes { get; set; }
    Material getShareMaterial(UnityEngine.Object obj, EShareMatType type = EShareMatType.Normal, string shaderName = "", bool isTest = true);
    bool IsLanuchMainPlayer { get; }
    bool isCanCrossScene { get; }
    bool IsCanMoveFromSafeArea(Node f, Node s);
    bool IsNotCrossToAnthor(SFMisc.Dot2 dot, SFMisc.Dot2 dot1);
    int VerticalCount { get; }
    int HorizontalCount { get; }
    string StripSymbols(string text);
}

public class SFGame : MonoBehaviour
{

}