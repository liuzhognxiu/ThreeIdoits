using UnityEngine;
using System;
public static class SFOut
{
    #region Not Clear
    public static SFGame Game;
    public static ICBGame IGame;
    public static ISFResUpdateMgr IResUpdateMgr;
    public static ISFResourceManager IResourceManager;
    public static string URL_mCommonResURL = string.Empty;
    public static string URL_mServerResURL = string.Empty;
    public static string URL_mClientResPath = string.Empty;
    public static string URL_mClientResUrl = string.Empty;
    public static SFGameManager IGameManager;
    #endregion

    public static ISFScene IScene;

    public static void ClearISFScene()
    {
        IScene = null;
    }
}
