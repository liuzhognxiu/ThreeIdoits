using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

public class BuildAssetBundleWindow : EditorWindow {

    static BuildAssetBundleWindow _window;
    Vector3 scrollPos = Vector2.zero;
    List<string> atlasPaths = new List<string>();
    List<string> audioPaths = new List<string>();
    List<string> uiPrefabPaths = new List<string>();
    List<string> effectPaths = new List<string>();
    List<string> spinePaths = new List<string>();
    List<string> sharePaths = new List<string>();

    public static string ATLASPATH = "ATLASPATH";
    string m_atlasPaths = string.Empty;
    public static string AUDIOPATH = "AUDIOPATH";
    string m_audioPaths = string.Empty;
    public static string UIPREFABPATH = "UIPREFABPATH";
    string m_UIPrefabPaths = string.Empty;
    public static string EFFECTPATH = "EFFECTPATH";
    string m_effectPaths = string.Empty;
    public static string SPINEPATH = "SPINEPATH";
    string m_spinePaths = string.Empty;
    public static string SHAREPATH = "SHAREPATH";
    string m_sharePaths = string.Empty;

    string projectPath = string.Empty;

    [UnityEditor.MenuItem("LuaFramework/Setting AssetBundle")]
    static void Init()
    {
        _window = BuildAssetBundleWindow.GetWindow(typeof(BuildAssetBundleWindow), false, "Setting AssetBundle", true) as BuildAssetBundleWindow;
        _window.position = new Rect(100, 100, 800, 600);
        //Debug.logger.Log("Application.persistentDataPath: " + UnityEngine.Application.persistentDataPath);
        _window.Show(true);
    }

    void OnGUI()
    {
        projectPath = Environment.CurrentDirectory.Replace(@"\", "/");
        scrollPos = GUI.BeginScrollView(new Rect(0, 0, position.width, position.height), scrollPos, new Rect(0, 0, 1000, 1000));
        GUILayout.Space(10);

        //share
        UpdateSharePath();
        GUILayout.Space(10);

        //atlas
        UpdateAtlasPath();
        GUILayout.Space(10);

        //音效
        UpdateAudioPath();
        GUILayout.Space(10);

        UpdateUIPrefabPath();
        GUILayout.Space(10);

        UpdateEffectPath();
        GUILayout.Space(10);

        UpdateSpinePath();
        GUILayout.Space(10);

        UpdateButtons();

        GUI.EndScrollView();


    }

    void UpdateButtons()
    {
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Save", GUILayout.Width(200)))
        {
            AssetBundlePath assetBundleAsset = ScriptableObject.CreateInstance<AssetBundlePath>();
            if (assetBundleAsset == null)
            {
                UnityEngine.Debug.LogError("create asset bundle asset failed!");
                return;
            }

            assetBundleAsset.SharePaths = sharePaths;
            assetBundleAsset.AtlasPaths = atlasPaths;
            assetBundleAsset.AudioPaths = audioPaths;
            assetBundleAsset.UIPrefabPaths = uiPrefabPaths;
            assetBundleAsset.ModelAndEffectPaths = effectPaths;
            assetBundleAsset.SpinePaths = spinePaths;
            
            AssetDatabase.CreateAsset(assetBundleAsset, "Assets/assetbundlepath.asset");
        }

        if (GUILayout.Button("Load", GUILayout.Width(200)))
        {
            AssetBundlePath assetBundlePath = AssetDatabase.LoadAssetAtPath<AssetBundlePath>("Assets/assetbundlepath.asset");
            if (assetBundlePath != null)
            {
                sharePaths = assetBundlePath.SharePaths;
                atlasPaths = assetBundlePath.AtlasPaths;
                audioPaths = assetBundlePath.AudioPaths;
                uiPrefabPaths = assetBundlePath.UIPrefabPaths;
                effectPaths = assetBundlePath.ModelAndEffectPaths;
                spinePaths = assetBundlePath.SpinePaths;
            }

            Repaint();
        }

        EditorGUILayout.EndHorizontal();
    }

    #region UI Prefab
    void UpdateUIPrefabPath()
    {
        string retPath = string.Empty;
        GetUIPrefabsPaths();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("设置UI Prefab", GUILayout.Width(100));
        if (GUILayout.Button("增加目录", GUILayout.Width(100)))
        {
            //FolderBrowserDialog fb = new FolderBrowserDialog();
            //fb.Description = "选择文件夹";
            //fb.RootFolder = System.Environment.SpecialFolder.MyComputer;
            //fb.ShowNewFolderButton = false;

            string selectedPath = EditorUtility.OpenFolderPanel("增加目录", System.IO.Directory.GetCurrentDirectory(), "");

            //if (fb.ShowDialog() == DialogResult.OK)
            {
                retPath = selectedPath;
                retPath = retPath.Replace(@"\", "/");
                if (retPath.Contains(projectPath))
                {
                    retPath = retPath.Replace(projectPath + "/", "");
                    if (!uiPrefabPaths.Contains(retPath))
                        uiPrefabPaths.Add(retPath);
                }
            }
        }

        EditorGUILayout.EndHorizontal();
        List<string> tmp = new List<string>();
        foreach (string path in uiPrefabPaths)
        {
            tmp.Add(path);
        }

        foreach (string path in tmp)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.TextField(path, GUILayout.Width(200));
            if (GUILayout.Button("-", GUILayout.Width(30)))
            {
                uiPrefabPaths.Remove(path);
            }
            EditorGUILayout.EndHorizontal();
        }
        SaveUIPrefabsPath();
    }

    void GetUIPrefabsPaths()
    {
        string uiprefabPath = EditorPrefs.GetString(UIPREFABPATH);
        string[] paths = uiprefabPath.Split('|');
        //Debug.logger.Log("atlasPath " + paths.Length);
        foreach (string path in paths)
        {
            if (!uiPrefabPaths.Contains(path))
            {
                if (path != string.Empty)
                    uiPrefabPaths.Add(path);
            }
        }
    }

    void SaveUIPrefabsPath()
    {
        string paths = string.Empty;

        if (uiPrefabPaths.Count > 0)
            if (uiPrefabPaths[0] != string.Empty)
                paths = uiPrefabPaths[0];
        for (int i = 1; i < uiPrefabPaths.Count; i++)
        {
            paths += "|";
            paths += uiPrefabPaths[i];
        }

        EditorPrefs.SetString(UIPREFABPATH, paths);
    }
    #endregion

    #region 音效
    void UpdateAudioPath()
    {
        string retPath = string.Empty;
        GetAudioPaths();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("设置音效", GUILayout.Width(100));
        if (GUILayout.Button("增加目录", GUILayout.Width(100)))
        {
            //FolderBrowserDialog fb = new FolderBrowserDialog();
            //fb.Description = "选择文件夹";
            //fb.RootFolder = System.Environment.SpecialFolder.MyComputer;
            //fb.ShowNewFolderButton = false;

            string selectedPath = EditorUtility.OpenFolderPanel("增加目录", System.IO.Directory.GetCurrentDirectory(), "");
            //if (fb.ShowDialog() == DialogResult.OK)
            {
                retPath = selectedPath;
                retPath = retPath.Replace(@"\", "/");
                if (retPath.Contains(projectPath))
                {
                    retPath = retPath.Replace(projectPath + "/", "");
                    if (!audioPaths.Contains(retPath))
                        audioPaths.Add(retPath);
                }
            }
        }

        EditorGUILayout.EndHorizontal();
        List<string> tmp = new List<string>();
        foreach (string path in audioPaths)
        {
            tmp.Add(path);
        }

        foreach (string path in tmp)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.TextField(path, GUILayout.Width(200));
            if (GUILayout.Button("-", GUILayout.Width(30)))
            {
                audioPaths.Remove(path);
            }
            EditorGUILayout.EndHorizontal();
        }
        SaveAudioPath();
    }

    void GetAudioPaths()
    {
        string audioPath = EditorPrefs.GetString(AUDIOPATH);
        string[] paths = audioPath.Split('|');
        //Debug.logger.Log("atlasPath " + paths.Length);
        foreach (string path in paths)
        {
            if (!audioPaths.Contains(path))
            {
                if (path != string.Empty)
                    audioPaths.Add(path);
            }
        }
    }

    void SaveAudioPath()
    {
        string paths = string.Empty;

        if (audioPaths.Count > 0)
            if (audioPaths[0] != string.Empty)
                paths = audioPaths[0];
        for (int i = 1; i < audioPaths.Count; i++)
        {
            paths += "|";
            paths += audioPaths[i];
        }

        EditorPrefs.SetString(AUDIOPATH, paths);
    }
    #endregion

    #region 图集
    void UpdateAtlasPath()
    {
        string retPath = string.Empty;
        GetAtlasPaths();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("设置图集资源", GUILayout.Width(100));
        if (GUILayout.Button("增加目录", GUILayout.Width(100)))
        {

            //FolderBrowserDialog fb = new FolderBrowserDialog();
            //fb.Description = "选择文件夹";
            //fb.RootFolder = System.Environment.SpecialFolder.MyComputer;
            //fb.ShowNewFolderButton = false;

            string selectedPath = EditorUtility.OpenFolderPanel("增加目录", System.IO.Directory.GetCurrentDirectory(), "");
            //if (fb.ShowDialog() == DialogResult.OK)
            {
                retPath = selectedPath;
                retPath = retPath.Replace(@"\", "/");
                
                if (retPath.Contains(projectPath))
                {
                    retPath = retPath.Replace(projectPath + "/", "");
                    if (!atlasPaths.Contains(retPath))
                        atlasPaths.Add(retPath);
                }
            }
        }
        EditorGUILayout.EndHorizontal();
        List<string> tmp = new List<string>();
        foreach (string path in atlasPaths)
        {
            tmp.Add(path);
        }

        foreach (string path in tmp)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.TextField(path, GUILayout.Width(200));
            if (GUILayout.Button("-", GUILayout.Width(30)))
            {
                atlasPaths.Remove(path);
            }
            EditorGUILayout.EndHorizontal();
        }
        SaveAtlasPath();
    }

    void GetAtlasPaths()
    {
        string atlasPath = EditorPrefs.GetString(ATLASPATH);
        string[] paths = atlasPath.Split('|');
        //Debug.logger.Log("atlasPath " + paths.Length);
        foreach(string path in paths)
        {
            if (!atlasPaths.Contains(path))
            {
                if (path != string.Empty)
                    atlasPaths.Add(path);
            }
        }
    }

    void SaveAtlasPath()
    {
        string paths = string.Empty;

        if (atlasPaths.Count > 0 
            && atlasPaths[0] != string.Empty)
            paths = atlasPaths[0];
        for (int i = 1; i < atlasPaths.Count; i++)
        {
            paths += "|";
            paths += atlasPaths[i];
        }

        EditorPrefs.SetString(ATLASPATH, paths);
    }
    #endregion

    #region 特效模型
    void UpdateEffectPath()
    {
        string retPath = string.Empty;
        GetEffectPaths();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("设置特效&模型资源", GUILayout.Width(100));
        if (GUILayout.Button("增加目录", GUILayout.Width(100)))
        {

            //FolderBrowserDialog fb = new FolderBrowserDialog();
            //fb.Description = "选择文件夹";
            //fb.RootFolder = System.Environment.SpecialFolder.MyComputer;
            //fb.ShowNewFolderButton = false;

            string selectedPath = EditorUtility.OpenFolderPanel("增加目录", System.IO.Directory.GetCurrentDirectory(), "");
            //if (fb.ShowDialog() == DialogResult.OK)
            {
                retPath = selectedPath;
                retPath = retPath.Replace(@"\", "/");

                if (retPath.Contains(projectPath))
                {
                    retPath = retPath.Replace(projectPath + "/", "");
                    if (!effectPaths.Contains(retPath))
                        effectPaths.Add(retPath);
                }
            }
        }
        EditorGUILayout.EndHorizontal();
        List<string> tmp = new List<string>();
        foreach (string path in effectPaths)
        {
            tmp.Add(path);
        }

        foreach (string path in tmp)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.TextField(path, GUILayout.Width(200));
            if (GUILayout.Button("-", GUILayout.Width(30)))
            {
                effectPaths.Remove(path);
            }
            EditorGUILayout.EndHorizontal();
        }
        SaveEffectPath();
    }

    void GetEffectPaths()
    {
        string effectPath = EditorPrefs.GetString(EFFECTPATH);
        string[] paths = effectPath.Split('|');
        //Debug.logger.Log("atlasPath " + paths.Length);
        foreach (string path in paths)
        {
            if (!effectPaths.Contains(path))
            {
                if (path != string.Empty)
                    effectPaths.Add(path);
            }
        }
    }

    void SaveEffectPath()
    {
        string paths = string.Empty;

        if (effectPaths.Count > 0
            && effectPaths[0] != string.Empty)
            paths = effectPaths[0];
        for (int i = 1; i < effectPaths.Count; i++)
        {
            paths += "|";
            paths += effectPaths[i];
        }

        EditorPrefs.SetString(EFFECTPATH, paths);
    }
    #endregion

    #region SPINES
    void UpdateSpinePath()
    {
        string retPath = string.Empty;
        GetSpinePaths();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("设置SPINE资源", GUILayout.Width(100));
        if (GUILayout.Button("增加目录", GUILayout.Width(100)))
        {

            //FolderBrowserDialog fb = new FolderBrowserDialog();
            //fb.Description = "选择文件夹";
            //fb.RootFolder = System.Environment.SpecialFolder.MyComputer;
            //fb.ShowNewFolderButton = false;

            string selectedPath = EditorUtility.OpenFolderPanel("增加目录", System.IO.Directory.GetCurrentDirectory(), "");
            //if (fb.ShowDialog() == DialogResult.OK)
            {
                retPath = selectedPath;
                retPath = retPath.Replace(@"\", "/");

                if (retPath.Contains(projectPath))
                {
                    retPath = retPath.Replace(projectPath + "/", "");
                    if (!spinePaths.Contains(retPath))
                        spinePaths.Add(retPath);
                }
            }
        }
        EditorGUILayout.EndHorizontal();
        List<string> tmp = new List<string>();
        foreach (string path in spinePaths)
        {
            tmp.Add(path);
        }

        foreach (string path in tmp)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.TextField(path, GUILayout.Width(200));
            if (GUILayout.Button("-", GUILayout.Width(30)))
            {
                spinePaths.Remove(path);
            }
            EditorGUILayout.EndHorizontal();
        }
        SaveSpinePath();
    }

    void GetSpinePaths()
    {
        string spinePath = EditorPrefs.GetString(SPINEPATH);
        string[] paths = spinePath.Split('|');
        //Debug.logger.Log("atlasPath " + paths.Length);
        foreach (string path in paths)
        {
            if (!spinePaths.Contains(path))
            {
                if (path != string.Empty)
                    spinePaths.Add(path);
            }
        }
    }

    void SaveSpinePath()
    {
        string paths = string.Empty;

        if (spinePaths.Count > 0
            && spinePaths[0] != string.Empty)
            paths = spinePaths[0];
        for (int i = 1; i < spinePaths.Count; i++)
        {
            paths += "|";
            paths += spinePaths[i];
        }

        EditorPrefs.SetString(SPINEPATH, paths);
    }
    #endregion

    #region Share
    void UpdateSharePath()
    {
        string retPath = string.Empty;
        GetSharePaths();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("设置Share资源", GUILayout.Width(100));
        if (GUILayout.Button("增加目录", GUILayout.Width(100)))
        {

            //FolderBrowserDialog fb = new FolderBrowserDialog();
            //fb.Description = "选择文件夹";
            //fb.RootFolder = System.Environment.SpecialFolder.MyComputer;
            //fb.ShowNewFolderButton = false;

            string selectedPath = EditorUtility.OpenFolderPanel("增加目录", System.IO.Directory.GetCurrentDirectory(), "");

            //if (fb.ShowDialog() == DialogResult.OK)
            {
                retPath = selectedPath;
                retPath = retPath.Replace(@"\", "/");

                if (retPath.Contains(projectPath))
                {
                    retPath = retPath.Replace(projectPath + "/", "");
                    if (!sharePaths.Contains(retPath))
                        sharePaths.Add(retPath);
                }
            }
        }
        EditorGUILayout.EndHorizontal();
        List<string> tmp = new List<string>();
        foreach (string path in sharePaths)
        {
            tmp.Add(path);
        }

        foreach (string path in tmp)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.TextField(path, GUILayout.Width(200));
            if (GUILayout.Button("-", GUILayout.Width(30)))
            {
                sharePaths.Remove(path);
            }
            EditorGUILayout.EndHorizontal();
        }
        SaveSharePath();
    }

    void GetSharePaths()
    {
        string sharePath = EditorPrefs.GetString(SHAREPATH);
        string[] paths = sharePath.Split('|');
        //Debug.logger.Log("atlasPath " + paths.Length);
        foreach (string path in paths)
        {
            if (!sharePaths.Contains(path))
            {
                if (path != string.Empty)
                    sharePaths.Add(path);
            }
        }
    }

    void SaveSharePath()
    {
        string paths = string.Empty;

        if (sharePaths.Count > 0
            && sharePaths[0] != string.Empty)
            paths = sharePaths[0];
        for (int i = 1; i < sharePaths.Count; i++)
        {
            paths += "|";
            paths += sharePaths[i];
        }

        EditorPrefs.SetString(SHAREPATH, paths);
    }
    #endregion
}
