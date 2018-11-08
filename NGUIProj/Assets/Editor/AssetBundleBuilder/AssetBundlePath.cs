using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AssetBundlePath : ScriptableObject {
    public List<string> SharePaths;
    public List<string> AtlasPaths;
    public List<string> AudioPaths;
    public List<string> UIPrefabPaths;
    public List<string> ModelAndEffectPaths;
    public List<string> SpinePaths;
}
