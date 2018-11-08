
//-------------------------------------------------------------------------
//Resource load
//Author LiZongFu
//Time 2015.12.15
//-------------------------------------------------------------------------

//using System.Diagnostics;
using System.IO;
using UnityEngine;
using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;



public interface ISFResourceManager
{
    bool IsUIRes(CSResource res);
    void wwwLoaded(CSResource res);
    string GetKeyPath(long key);
    CSResource GetRes(string path);
    void RemoveWaitingQueueDic(string path);
    CSResource AddQueue(string name, ResourceType type, CSEventDelegate<CSResource>.OnLoaded onLoadCallBack,
        ResourceAssistType assistType, bool isPath = false, long key = 0);
    void AddKeyPath(long key, string path);
    int MaxPlayerAtlasNum { get; set; }
    int MaxWeaponAtlasNum { get; set; }
    int MaxWingAtlasNum { get; set; }
    int MaxPlayerNum { get; set; }
    bool DestroyResource(string path, bool isUnLoadUnUsedAssets = false, bool isRemoveFromDic = true, bool isRemoveCallBack = false);
}
