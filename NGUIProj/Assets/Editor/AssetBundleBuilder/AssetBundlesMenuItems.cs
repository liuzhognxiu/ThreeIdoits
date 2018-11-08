using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AssetBundlesMenuItems {
    const string kSimulationMode = "LuaFramework/Simulation Mode";

#if UNITY_EDITOR

    [UnityEditor.MenuItem(kSimulationMode)]
    public static void ToggleSimulationMode()
    {
        Helper.SimulateAssetBundleInEditor = !Helper.SimulateAssetBundleInEditor;
    }

    [UnityEditor.MenuItem(kSimulationMode, true)]
    public static bool ToggleSimulationModeValidate()
    {
        Menu.SetChecked(kSimulationMode, Helper.SimulateAssetBundleInEditor);
        return true;
    }
#endif
}
