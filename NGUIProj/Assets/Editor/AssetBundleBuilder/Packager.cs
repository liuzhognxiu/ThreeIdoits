using UnityEditor;
using UnityEngine;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using LuaFramework;

public class Packager {
    public static string platform = string.Empty;
    static List<string> paths = new List<string>();
    static List<string> files = new List<string>();

    static string spinePath = "Spines/";
    static string sharePath = "Share/";
    static string atlasPath = "UIAtlas/";
    static string prePath = "AudioClips/";
    static string prefabPath = "UIPrefab/";
    static string effectAndModelPath = "Model/";

    ///-----------------------------------------------------------
    static string[] exts = { ".txt", ".xml", ".lua", ".assetbundle", ".json" };
    static bool CanCopy(string ext) {   //能不能复制
        foreach (string e in exts) {
            if (ext.Equals(e)) return true;
        }
        return false;
    }

    /// <summary>
    /// 载入素材
    /// </summary>
    static UnityEngine.Object LoadAsset(string file) {
        if (file.EndsWith(".lua")) file += ".txt";
        return AssetDatabase.LoadMainAssetAtPath("Assets/LuaFramework/Examples/Builds/" + file);
    }

    [MenuItem("LuaFramework/Build iPhone Resource", false, 100)]
    public static void BuildiPhoneResource() {
        BuildTarget target;
#if UNITY_5
        target = BuildTarget.iOS;
#else
        target = BuildTarget.iPhone;
#endif
        BuildAssetResource(target);
    }

    [MenuItem("LuaFramework/Build Android Resource", false, 101)]
    public static void BuildAndroidResource() {
        BuildAssetResource(BuildTarget.Android);
    }

    [MenuItem("LuaFramework/Build Windows Resource", false, 102)]
    public static void BuildWindowsResource() {
        BuildAssetResource(BuildTarget.StandaloneWindows);
    }

    /// <summary>
    /// 生成绑定素材
    /// </summary>
    public static void BuildAssetResource(BuildTarget target) {
        if (Directory.Exists(Util.DataPath) && !AppConst.Test)
        {   
            Directory.Delete(Util.DataPath, true);
        }
        string streamPath = Application.streamingAssetsPath;
        if (Directory.Exists(streamPath)) {
            Directory.Delete(streamPath, true);
        }
        AssetDatabase.Refresh();

        if (AppConst.BuildAssetBundle) {
            HandleAssetBundle();//HandleExampleBundle(target);
        }
        if (AppConst.LuaBundleMode) {
            HandleBundle();
        } else {
            HandleLuaFile();
        }
        BuildShareIndex();
        BuildFileIndex();
        AssetDatabase.Refresh();
    }

    static void HandleBundle() {
        BuildLuaBundles();
        string luaPath = AppDataPath + "/StreamingAssets/lua/";
        string[] luaPaths = { AppDataPath + "/LuaFramework/lua/", 
                              AppDataPath + "/LuaFramework/Tolua/Lua/" };

        for (int i = 0; i < luaPaths.Length; i++) {
            paths.Clear(); files.Clear();
            string luaDataPath = luaPaths[i].ToLower();
            Recursive(luaDataPath);
            foreach (string f in files) {
                if (f.EndsWith(".meta") || f.EndsWith(".lua")) continue;
                string newfile = f.Replace(luaDataPath, "");
                string path = Path.GetDirectoryName(luaPath + newfile);
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                string destfile = path + "/" + Path.GetFileName(f);
                File.Copy(f, destfile, true);
            }
        }
    }

    static void ClearAllLuaFiles() {
        string osPath = Application.streamingAssetsPath + "/" + LuaConst.osDir;

        if (Directory.Exists(osPath)) {
            string[] files = Directory.GetFiles(osPath, "Lua*.unity3d");

            for (int i = 0; i < files.Length; i++) {
                File.Delete(files[i]);
            }
        }

        string path = osPath + "/Lua";

        if (Directory.Exists(path)) {
            Directory.Delete(path, true);
        }

        path = Application.dataPath + "/Resources/Lua";

        if (Directory.Exists(path)) {
            Directory.Delete(path, true);
        }

        path = Application.persistentDataPath + "/" + LuaConst.osDir + "/Lua";

        if (Directory.Exists(path)) {
            Directory.Delete(path, true);
        }
    }

    static void CreateStreamDir(string dir) {
        dir = Application.streamingAssetsPath + "/" + dir;

        if (!File.Exists(dir)) {
            Directory.CreateDirectory(dir);
        }
    }

    static void CopyLuaBytesFiles(string sourceDir, string destDir, bool appendext = true) {
        if (!Directory.Exists(sourceDir)) {
            return;
        }

        string[] files = Directory.GetFiles(sourceDir, "*.lua", SearchOption.AllDirectories);
        int len = sourceDir.Length;

        if (sourceDir[len - 1] == '/' || sourceDir[len - 1] == '\\') {
            --len;
        }

        for (int i = 0; i < files.Length; i++) {
            string str = files[i].Remove(0, len);
            string dest = destDir + str;
            if (appendext) dest += ".bytes";
            string dir = Path.GetDirectoryName(dest);
            Directory.CreateDirectory(dir);

            if (AppConst.LuaByteMode) {
                Packager.EncodeLuaFile(files[i], dest);
            } else {
                File.Copy(files[i], dest, true);
            }
        }
    }

    static void BuildLuaBundles() {
        ClearAllLuaFiles();
        CreateStreamDir("lua/");
        CreateStreamDir(AppConst.LuaTempDir);

        string dir = Application.persistentDataPath;
        if (!File.Exists(dir)) {
            Directory.CreateDirectory(dir);
        }

        string streamDir = Application.dataPath + "/" + AppConst.LuaTempDir;
        CopyLuaBytesFiles(CustomSettings.luaDir, streamDir);
        CopyLuaBytesFiles(CustomSettings.FrameworkPath + "/ToLua/Lua", streamDir);

        AssetDatabase.Refresh();
        string[] dirs = Directory.GetDirectories(streamDir, "*", SearchOption.AllDirectories);

        for (int i = 0; i < dirs.Length; i++) {
            string str = dirs[i].Remove(0, streamDir.Length);
            BuildLuaBundle(str);
        }

        BuildLuaBundle(null);
        Directory.Delete(streamDir, true);
        AssetDatabase.Refresh();
    }

    static void BuildLuaBundle(string dir) {
        BuildAssetBundleOptions options = BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets |
                                          BuildAssetBundleOptions.DeterministicAssetBundle | BuildAssetBundleOptions.UncompressedAssetBundle;
        string path = "Assets/" + AppConst.LuaTempDir + dir;
        string[] files = Directory.GetFiles(path, "*.lua.bytes");
        List<Object> list = new List<Object>();
        string bundleName = "lua.unity3d";
        if (dir != null) {
            dir = dir.Replace('\\', '_').Replace('/', '_');
            bundleName = "lua_" + dir.ToLower() + AppConst.ExtName;
        }
        for (int i = 0; i < files.Length; i++) {
            Object obj = AssetDatabase.LoadMainAssetAtPath(files[i]);
            list.Add(obj);
        }

        if (files.Length > 0) {
            string output = Application.streamingAssetsPath + "/lua/" + bundleName;
            if (File.Exists(output)) {
                File.Delete(output);
            }
            BuildPipeline.BuildAssetBundle(null, list.ToArray(), output, options, EditorUserBuildSettings.activeBuildTarget);
            AssetDatabase.Refresh();
        }
    }

    static void HandleAssetBundle()
    {
        Object mainAsset = null;        //主素材名，单个
        Object[] addis = null;     //附加素材名，多个
        string assetfile = string.Empty;  //素材文件名

        BuildAssetBundleOptions options = BuildAssetBundleOptions.UncompressedAssetBundle |
                                          BuildAssetBundleOptions.CollectDependencies |
                                          BuildAssetBundleOptions.DeterministicAssetBundle;
        string dataPath = Util.DataPath;
        if (Directory.Exists(dataPath) && !AppConst.Test)
        {
            Directory.Delete(dataPath, true);
        }
        string assetPath = AppDataPath + "/StreamingAssets/";
        if (Directory.Exists(dataPath))
        {
            Directory.Delete(assetPath, true);
        }
        if (!Directory.Exists(assetPath)) Directory.CreateDirectory(assetPath);

        BuildShare();
        //图集
        //string[] atlasPaths = EditorPrefs.GetString(BuildAssetBundleWindow.ATLASPATH).Split('|');

        //string[] atlasAssets = AssetDatabase.FindAssets("t:Prefab", atlasPaths);

        //BuildPipeline.PushAssetDependencies();
        BuildAtals();

        //在这里需要打包UI的prefab 关联起来
        //BuildUIPrefabs();
        //BuildPipeline.PopAssetDependencies();

        //音效
        BuildAudio();

        BuildSpines();

        BuildEffectsAndModels();
    }

    static void BuildShare()
    {
        Object mainAsset = null;        //主素材名，单个
        string assetfile = string.Empty;  //素材文件名
        string assetPath = AppDataPath + "/StreamingAssets/";
        
        if (Directory.Exists(assetPath + sharePath))
            Directory.Delete(assetPath + sharePath, true);

        if (!Directory.Exists(assetPath + sharePath)) Directory.CreateDirectory(assetPath + sharePath);

        BuildAssetBundleOptions options = BuildAssetBundleOptions.CompleteAssets |
                                          BuildAssetBundleOptions.CollectDependencies;

        string[] sharePaths = EditorPrefs.GetString(BuildAssetBundleWindow.SHAREPATH).Split('|');

        Dictionary<string, string> shareDic = new Dictionary<string, string>();

        for(int i = 0; i < sharePaths.Length; i++)
        {
            if (string.IsNullOrEmpty(sharePaths[i]))
                continue;

            Helper.GetObjectNameToArray(sharePaths[i].Replace("Assets/", ""), ref shareDic);
        }

        string projectPath = System.Environment.CurrentDirectory.Replace(@"\", "/");
        foreach (KeyValuePair<string, string> kv in shareDic)
        {
            BuildPipeline.PushAssetDependencies();
            assetfile = assetPath + sharePath + kv.Key.Split('.')[0] + "-" + kv.Key.Split('.')[1] + AppConst.ExtName;
            mainAsset = AssetDatabase.LoadMainAssetAtPath(kv.Value.Replace(projectPath + "/", ""));
            BuildPipeline.BuildAssetBundle(mainAsset, null, assetfile, options, EditorUserBuildSettings.activeBuildTarget);
            BuildPipeline.PopAssetDependencies();
        }
        //string[] shaderAssets = AssetDatabase.FindAssets("t:Shader", sharePaths);

        //foreach (string asset in shaderAssets)
        //{
        //    string path = AssetDatabase.GUIDToAssetPath(asset);
        //    int spalashIndex = path.LastIndexOf('/');
        //    string assetName = path.Substring(spalashIndex + 1).Replace(".shader", "");

        //    BuildPipeline.PushAssetDependencies();
        //    assetfile = assetPath + sharePath + assetName + AppConst.ExtName;
        //    mainAsset = AssetDatabase.LoadMainAssetAtPath(path);
        //    UnityEngine.Debug.Log("#################### " + path);
        //    BuildPipeline.BuildAssetBundle(mainAsset, null, assetfile, options, EditorUserBuildSettings.activeBuildTarget);
        //    BuildPipeline.PopAssetDependencies();

        //}
    }

    /// <summary>
    /// 打包图集
    /// </summary>
    static void BuildAtals()
    {
        Object mainAsset = null;        //主素材名，单个
        string assetfile = string.Empty;  //素材文件名
        string assetPath = AppDataPath + "/StreamingAssets/";
        
        if (Directory.Exists(assetPath + atlasPath))
            Directory.Delete(assetPath + atlasPath, true);

        if (!Helper.CheckDirectory(assetPath + atlasPath))
            return;
        

        BuildAssetBundleOptions options = BuildAssetBundleOptions.CompleteAssets |
                                          BuildAssetBundleOptions.CollectDependencies;

        string[] atlasPaths = EditorPrefs.GetString(BuildAssetBundleWindow.ATLASPATH).Split('|');
        
        //png:start
        BuildPipeline.PushAssetDependencies();

        string[] textureAssets = AssetDatabase.FindAssets("t:Texture", atlasPaths);
        foreach (string asset in textureAssets)
        {
            string path = AssetDatabase.GUIDToAssetPath(asset);
            int spalashIndex = path.LastIndexOf('/');
            string assetName = path.Substring(spalashIndex + 1).Split('.')[0];//.Replace(".png", "");

            assetfile = assetPath + sharePath + assetName + "-png" + AppConst.ExtName;
            mainAsset = AssetDatabase.LoadMainAssetAtPath(path);
            BuildPipeline.BuildAssetBundle(mainAsset, null, assetfile, options, EditorUserBuildSettings.activeBuildTarget);
        }

        //mat:start
        
        string[] materialAssets = AssetDatabase.FindAssets("t:Material", atlasPaths);
        foreach(string asset in materialAssets)
        {
            BuildPipeline.PushAssetDependencies();
            string path = AssetDatabase.GUIDToAssetPath(asset);
            int spalashIndex = path.LastIndexOf('/');
            string assetName = path.Substring(spalashIndex + 1).Replace(".mat", "");

            assetfile = assetPath + atlasPath + assetName + "-mat" + AppConst.ExtName;
            mainAsset = AssetDatabase.LoadMainAssetAtPath(path);
            BuildPipeline.BuildAssetBundle(mainAsset, null, assetfile, options, EditorUserBuildSettings.activeBuildTarget);
            BuildPipeline.PopAssetDependencies();

        }

        //atlas prefab: start
        string[] atlasAssets = AssetDatabase.FindAssets("t:Prefab", atlasPaths);
        foreach (string asset in atlasAssets)
        {
            BuildPipeline.PushAssetDependencies();
            string path = AssetDatabase.GUIDToAssetPath(asset);
            int spalashIndex = path.LastIndexOf('/');
            string assetName = path.Substring(spalashIndex + 1).Replace(".prefab", "");

            assetfile = assetPath + atlasPath + assetName + AppConst.ExtName;
            mainAsset = AssetDatabase.LoadMainAssetAtPath(path);
            BuildPipeline.BuildAssetBundle(mainAsset, null, assetfile, options, EditorUserBuildSettings.activeBuildTarget);
            BuildPipeline.PopAssetDependencies();
        }

        //[UI prefab]:start
        string[] UIPrefabPaths = EditorPrefs.GetString(BuildAssetBundleWindow.UIPREFABPATH).Split('|');
        

        string[] uiprefabAssets = AssetDatabase.FindAssets("t:Prefab", UIPrefabPaths);
        foreach (string asset in uiprefabAssets)
        {
            BuildPipeline.PushAssetDependencies();
            string path = AssetDatabase.GUIDToAssetPath(asset);
            
            int spalashIndex = path.LastIndexOf('/');
            string assetName = path.Substring(spalashIndex + 1).Replace(".prefab", "");

            string dirPath = path.Replace(assetName + ".prefab", "");
            dirPath = dirPath.Replace("Assets/Resources/", "");
            Helper.CheckDirectory(assetPath + dirPath);

            assetfile = assetPath + dirPath + assetName + AppConst.ExtName;
            mainAsset = AssetDatabase.LoadMainAssetAtPath(path);
            BuildPipeline.BuildAssetBundle(mainAsset, null, assetfile, options, EditorUserBuildSettings.activeBuildTarget);
            BuildPipeline.PopAssetDependencies();
        }
        //[UI prefab]:end
        

        //atlas prefab: end
        

        //mat:end
        
        //png:end
        BuildPipeline.PopAssetDependencies();
    } 

    /// <summary>
    /// 打包UI Prefab
    /// </summary>
    //static void BuildUIPrefabs()
    //{
    //    Object mainAsset = null;        //主素材名，单个
    //    string assetfile = string.Empty;  //素材文件名
    //    string assetPath = AppDataPath + "/StreamingAssets/";
        

    //    if (Directory.Exists(assetPath + prefabPath))
    //        Directory.Delete(assetPath + prefabPath, true);

    //    if (!Directory.Exists(assetPath + prefabPath)) Directory.CreateDirectory(assetPath + prefabPath);

    //    BuildAssetBundleOptions options = BuildAssetBundleOptions.CompleteAssets |
    //                                      BuildAssetBundleOptions.CollectDependencies;// |
    //                                      //BuildAssetBundleOptions.DeterministicAssetBundle;

    //    string[] uiPrefabPaths = EditorPrefs.GetString(BuildAssetBundleWindow.UIPREFABPATH).Split('|');

    //    string[] uiPrefabAssets = AssetDatabase.FindAssets("t:Prefab", uiPrefabPaths);
    //    foreach (string asset in uiPrefabAssets)
    //    {
    //        BuildPipeline.PushAssetDependencies();
    //        string path = AssetDatabase.GUIDToAssetPath(asset);
    //        int spalashIndex = path.LastIndexOf('/');
    //        string assetName = path.Substring(spalashIndex + 1).Replace(".prefab", "");

    //        assetfile = assetPath + prefabPath + assetName + AppConst.ExtName;
    //        mainAsset = AssetDatabase.LoadMainAssetAtPath(path);
    //        BuildPipeline.BuildAssetBundle(mainAsset, null, assetfile, options, EditorUserBuildSettings.activeBuildTarget);
    //        BuildPipeline.PopAssetDependencies();
    //    }
    //}

    /// <summary>
    /// 打包音效
    /// </summary>
    static void BuildAudio()
    {
        Object mainAsset = null;        //主素材名，单个
        string assetfile = string.Empty;  //素材文件名
        string assetPath = AppDataPath + "/StreamingAssets/";
        

        if (Directory.Exists(assetPath + prePath))
            Directory.Delete(assetPath + prePath, true);

        if (!Directory.Exists(assetPath + prePath)) Directory.CreateDirectory(assetPath + prePath);

        BuildAssetBundleOptions options = BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.CompleteAssets | BuildAssetBundleOptions.DeterministicAssetBundle;

        string[] audioPaths = EditorPrefs.GetString(BuildAssetBundleWindow.AUDIOPATH).Split('|');

        string[] audioAssets = AssetDatabase.FindAssets("t:AudioClip", audioPaths);
        foreach (string asset in audioAssets)
        {
            string path = AssetDatabase.GUIDToAssetPath(asset);
            FileInfo fi = new FileInfo(path);
            if (!Directory.Exists(assetPath + prePath + fi.Directory.Name)) Directory.CreateDirectory(assetPath + prePath + fi.Directory.Name);

            BuildPipeline.PushAssetDependencies();
            int spalashIndex = path.LastIndexOf('/');
            string assetName = path.Substring(spalashIndex + 1).Split('.')[0];

            string dirPath = string.Empty;
            if (path.Contains(assetName + ".mp3"))
                dirPath = path.Replace(assetName + ".mp3", "");
            else
                dirPath = path.Replace(assetName + ".ogg", "");

            dirPath = dirPath.Replace("Assets/Resources/", "");
            Helper.CheckDirectory(assetPath + dirPath);

            assetfile = assetPath + dirPath + assetName + AppConst.ExtName;
            mainAsset = AssetDatabase.LoadMainAssetAtPath(path);
            BuildPipeline.BuildAssetBundle(mainAsset, null, assetfile, options, EditorUserBuildSettings.activeBuildTarget);
            BuildPipeline.PopAssetDependencies();
        }
    }

    /// <summary>
    /// 打包特效模型
    /// </summary>
    static void BuildEffectsAndModels()
    {
        Object mainAsset = null;        //主素材名，单个
        string assetfile = string.Empty;  //素材文件名
        string assetPath = AppDataPath + "/StreamingAssets/";

        if (Directory.Exists(assetPath + effectAndModelPath))
            Directory.Delete(assetPath + effectAndModelPath, true);

        if (!Helper.CheckDirectory(assetPath + effectAndModelPath))
            return;


        BuildAssetBundleOptions options = BuildAssetBundleOptions.CompleteAssets |
                                          BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.DeterministicAssetBundle;

        string[] effectAndModelPaths = EditorPrefs.GetString(BuildAssetBundleWindow.EFFECTPATH).Split('|');

        //png:start
        BuildPipeline.PushAssetDependencies();

        string[] textureAssets = AssetDatabase.FindAssets("t:Texture", effectAndModelPaths);
        foreach (string asset in textureAssets)
        {
            string path = AssetDatabase.GUIDToAssetPath(asset);
            int spalashIndex = path.LastIndexOf('/');
            string assetName = path.Substring(spalashIndex + 1).Split('.')[0];//.Replace(".png", "");

            assetfile = assetPath + sharePath + assetName + "-png" + AppConst.ExtName;
            mainAsset = AssetDatabase.LoadMainAssetAtPath(path);
            BuildPipeline.BuildAssetBundle(mainAsset, null, assetfile, options, EditorUserBuildSettings.activeBuildTarget);
        }

        
        BuildPipeline.PushAssetDependencies();
        
        //mat:start
        string[] materialAssets = AssetDatabase.FindAssets("t:Material", effectAndModelPaths);
        foreach (string asset in materialAssets)
        {
            string path = AssetDatabase.GUIDToAssetPath(asset);
            int spalashIndex = path.LastIndexOf('/');
            string assetName = path.Substring(spalashIndex + 1).Replace(".mat", "");

            string dirPath = path.Replace(assetName + ".mat", "");
            dirPath = dirPath.Replace("Assets/Resources/", "");
            Helper.CheckDirectory(assetPath + dirPath);

            assetfile = assetPath + dirPath + assetName + "-mat" + AppConst.ExtName;
            mainAsset = AssetDatabase.LoadMainAssetAtPath(path);
            BuildPipeline.BuildAssetBundle(mainAsset, null, assetfile, options, EditorUserBuildSettings.activeBuildTarget);
        }

        //fbx:start
        string[] modelAssets = AssetDatabase.FindAssets("t:Model", effectAndModelPaths);
        foreach (string asset in modelAssets)
        {
            BuildPipeline.PushAssetDependencies();
            string path = AssetDatabase.GUIDToAssetPath(asset);
            int spalashIndex = path.LastIndexOf('/');
            path = path.ToLower();
            string assetName = path.Substring(spalashIndex + 1).Replace(".fbx", "");

            assetfile = assetPath + sharePath + assetName + "-fbx" + AppConst.ExtName;
            mainAsset = AssetDatabase.LoadMainAssetAtPath(path);
            BuildPipeline.BuildAssetBundle(mainAsset, null, assetfile, options, EditorUserBuildSettings.activeBuildTarget);
            BuildPipeline.PopAssetDependencies();

        }
        BuildPipeline.PopAssetDependencies();
        //model prefab: start
        string[] atlasAssets = AssetDatabase.FindAssets("t:Prefab", effectAndModelPaths);
        foreach (string asset in atlasAssets)
        {
            BuildPipeline.PushAssetDependencies();
            string path = AssetDatabase.GUIDToAssetPath(asset);
            int spalashIndex = path.LastIndexOf('/');
            string assetName = path.Substring(spalashIndex + 1).Replace(".prefab", "");

            string dirPath = path.Replace(assetName + ".prefab", "");
            dirPath = dirPath.Replace("Assets/Resources/", "");
            Helper.CheckDirectory(assetPath + dirPath);

            assetfile = assetPath + dirPath + assetName + AppConst.ExtName;
            mainAsset = AssetDatabase.LoadMainAssetAtPath(path);
            BuildPipeline.BuildAssetBundle(mainAsset, null, assetfile, options, EditorUserBuildSettings.activeBuildTarget);
            BuildPipeline.PopAssetDependencies();
        }
        //atlas prefab: end


        //mat:end
        
        //png:end
        BuildPipeline.PopAssetDependencies();
    }

    /// <summary>
    /// 打包Spine
    /// </summary>
    static void BuildSpines()
    {
        Object mainAsset = null;        //主素材名，单个
        string assetfile = string.Empty;  //素材文件名
        string assetPath = AppDataPath + "/StreamingAssets/";
        
        if (Directory.Exists(assetPath + spinePath))
            Directory.Delete(assetPath + spinePath, true);

        if (!Directory.Exists(assetPath + spinePath)) Directory.CreateDirectory(assetPath + spinePath);

        BuildAssetBundleOptions options = BuildAssetBundleOptions.CompleteAssets |
                                          BuildAssetBundleOptions.CollectDependencies | BuildAssetBundleOptions.DeterministicAssetBundle;

        string[] spinePaths = EditorPrefs.GetString(BuildAssetBundleWindow.SPINEPATH).Split('|');
        
        string[] spineAssets = AssetDatabase.FindAssets("t:Texture", spinePaths);
        BuildPipeline.PushAssetDependencies();
        //json:start
        Dictionary<string, string> jsonDic = new Dictionary<string, string>();
        string projectPath = System.Environment.CurrentDirectory.Replace(@"\", "/");
        for (int i = 0; i < spinePaths.Length; i++)
        {
            if (string.IsNullOrEmpty(spinePaths[i]))
                continue;

            Helper.GetObjectNamesByExtension(spinePaths[i].Replace("Assets/", ""), ".json", ref jsonDic);
        }

        foreach (KeyValuePair<string, string> kv in jsonDic)
        {
            assetfile = assetPath + sharePath + kv.Key.Split('.')[0] + "-" + kv.Key.Split('.')[1] + AppConst.ExtName;
            mainAsset = AssetDatabase.LoadMainAssetAtPath(kv.Value.Replace(projectPath + "/", ""));
            BuildPipeline.BuildAssetBundle(mainAsset, null, assetfile, options, EditorUserBuildSettings.activeBuildTarget);
        }
        //json:end

        //txt:start
        Dictionary<string, string> txtDic = new Dictionary<string, string>();
        for (int i = 0; i < spinePaths.Length; i++)
        {
            if (string.IsNullOrEmpty(spinePaths[i]))
                continue;

            Helper.GetObjectNamesByExtension(spinePaths[i].Replace("Assets/", ""), ".txt", ref txtDic);
        }

        foreach (KeyValuePair<string, string> kv in txtDic)
        {
            assetfile = assetPath + sharePath + kv.Key.Split('.')[0] + "-" + kv.Key.Split('.')[1] + AppConst.ExtName;
            mainAsset = AssetDatabase.LoadMainAssetAtPath(kv.Value.Replace(projectPath + "/", ""));
            BuildPipeline.BuildAssetBundle(mainAsset, null, assetfile, options, EditorUserBuildSettings.activeBuildTarget);
        }
        //txt:end

        //asset:start
        Dictionary<string, string> assetDic = new Dictionary<string, string>();
        for (int i = 0; i < spinePaths.Length; i++)
        {
            if (string.IsNullOrEmpty(spinePaths[i]))
                continue;

            Helper.GetObjectNamesByExtension(spinePaths[i].Replace("Assets/", ""), ".asset", ref assetDic);
        }

        string dirPath2 = string.Empty;
        foreach (KeyValuePair < string, string > kv in assetDic)
        {
            string path = kv.Value.Replace(projectPath + "/", "");

            dirPath2 = path.Replace(kv.Key, "");
            dirPath2 = dirPath2.Replace("Assets/Resources/", "");
            Helper.CheckDirectory(assetPath + dirPath2);
        }

        foreach (KeyValuePair<string, string> kv in assetDic)
        {
            BuildPipeline.PushAssetDependencies();
            string path = kv.Value.Replace(projectPath + "/", "");

            dirPath2 = path.Replace(kv.Key, "");
            dirPath2 = dirPath2.Replace("Assets/Resources/", "");
            assetfile = assetPath + dirPath2 + kv.Key.Split('.')[0]/* + "-" + kv.Key.Split('.')[1]*/ + AppConst.ExtName;
            mainAsset = AssetDatabase.LoadMainAssetAtPath(kv.Value.Replace(projectPath + "/", ""));
            BuildPipeline.BuildAssetBundle(mainAsset, null, assetfile, options, EditorUserBuildSettings.activeBuildTarget);
            BuildPipeline.PopAssetDependencies();
        }
        //asset:end

        //png:start
        foreach (string asset in spineAssets)
        {
            string path = AssetDatabase.GUIDToAssetPath(asset);
            int spalashIndex = path.LastIndexOf('/');
            string assetName = path.Substring(spalashIndex + 1).Split('.')[0];

            assetfile = assetPath + sharePath + assetName + "-png" + AppConst.ExtName;
            mainAsset = AssetDatabase.LoadMainAssetAtPath(path);
            BuildPipeline.BuildAssetBundle(mainAsset, null, assetfile, options, EditorUserBuildSettings.activeBuildTarget);   
        }
        //png:end

        string[] shaderAssets = AssetDatabase.FindAssets("t:Shader", spinePaths);
        foreach (string shaderAsset in shaderAssets)
        {
            string shaderPath = AssetDatabase.GUIDToAssetPath(shaderAsset);
            int spalashIndex2 = shaderPath.LastIndexOf('/');
            string assetShaderName = shaderPath.Substring(spalashIndex2 + 1).Replace(".shader", "");
            assetfile = assetPath + sharePath + assetShaderName + AppConst.ExtName;
            mainAsset = AssetDatabase.LoadMainAssetAtPath(shaderPath);
            BuildPipeline.BuildAssetBundle(mainAsset, null, assetfile, options, EditorUserBuildSettings.activeBuildTarget);
        }

        
        string[] matAssets = AssetDatabase.FindAssets("t:Material", spinePaths);
        foreach (string matAsset in matAssets)
        {
            BuildPipeline.PushAssetDependencies();
            string matPath = AssetDatabase.GUIDToAssetPath(matAsset);
            int spalashIndex2 = matPath.LastIndexOf('/');
            string assetMatName = matPath.Substring(spalashIndex2 + 1).Replace(".mat", "");

            string dirPath = matPath.Replace(assetMatName + ".mat", "");
            dirPath = dirPath.Replace("Assets/Resources/", "");
            Helper.CheckDirectory(assetPath + dirPath);

            assetfile = assetPath + dirPath + assetMatName + "-mat" + AppConst.ExtName;
            mainAsset = AssetDatabase.LoadMainAssetAtPath(matPath);
            BuildPipeline.BuildAssetBundle(mainAsset, null, assetfile, options, EditorUserBuildSettings.activeBuildTarget);
            BuildPipeline.PopAssetDependencies();
        }

        spineAssets = AssetDatabase.FindAssets("t:Prefab", spinePaths);
        foreach(string asset in spineAssets)
        {
            BuildPipeline.PushAssetDependencies();
            string path = AssetDatabase.GUIDToAssetPath(asset);
            int spalashIndex = path.LastIndexOf('/');
            string assetName = path.Substring(spalashIndex + 1).Replace(".prefab", "");

            string dirPath = path.Replace(assetName + ".prefab", "");
            dirPath = dirPath.Replace("Assets/Resources/", "");
            Helper.CheckDirectory(assetPath + dirPath);

            assetfile = assetPath + dirPath + assetName + AppConst.ExtName;
            mainAsset = AssetDatabase.LoadMainAssetAtPath(path);
            BuildPipeline.BuildAssetBundle(mainAsset, null, assetfile, options, EditorUserBuildSettings.activeBuildTarget);
            BuildPipeline.PopAssetDependencies();
        }
        BuildPipeline.PopAssetDependencies();
    }

    static void HandleExampleBundle(BuildTarget target) {
        Object mainAsset = null;        //主素材名，单个
        Object[] addis = null;     //附加素材名，多个
        string assetfile = string.Empty;  //素材文件名

        BuildAssetBundleOptions options = BuildAssetBundleOptions.UncompressedAssetBundle |
                                          BuildAssetBundleOptions.CollectDependencies |
                                          BuildAssetBundleOptions.DeterministicAssetBundle;
        string dataPath = Util.DataPath;
        if (Directory.Exists(dataPath) && !AppConst.Test) {
            Directory.Delete(dataPath, true);
        }
        string assetPath = AppDataPath + "/StreamingAssets/";
        if (Directory.Exists(dataPath)) {
            Directory.Delete(assetPath, true);
        }
        if (!Directory.Exists(assetPath)) Directory.CreateDirectory(assetPath);

        ///-----------------------------生成共享的关联性素材绑定-------------------------------------
        BuildPipeline.PushAssetDependencies();

        assetfile = assetPath + "shared" + AppConst.ExtName;
        mainAsset = LoadAsset("Shared/Atlas/Dialog.prefab");
        BuildPipeline.BuildAssetBundle(mainAsset, null, assetfile, options, target);

        ///------------------------------生成PromptPanel素材绑定-----------------------------------
        BuildPipeline.PushAssetDependencies();
        mainAsset = LoadAsset("Prompt/Prefabs/PromptPanel.prefab");
        addis = new Object[1];
        addis[0] = LoadAsset("Prompt/Prefabs/PromptItem.prefab");
        assetfile = assetPath + "prompt" + AppConst.ExtName;
        BuildPipeline.BuildAssetBundle(mainAsset, addis, assetfile, options, target);
        BuildPipeline.PopAssetDependencies();

        ///------------------------------生成MessagePanel素材绑定-----------------------------------
        BuildPipeline.PushAssetDependencies();
        mainAsset = LoadAsset("Message/Prefabs/MessagePanel.prefab");
        assetfile = assetPath + "message" + AppConst.ExtName;
        BuildPipeline.BuildAssetBundle(mainAsset, null, assetfile, options, target);
        BuildPipeline.PopAssetDependencies();

		///------------------------------生成HudPanel素材绑定---------------------------------------
		//BuildPipeline.PushAssetDependencies();
		//mainAsset = LoadAsset ("Hud/Prefabs/HudPanel.prefab");
		//assetfile = assetPath + "hud" + AppConst.ExtName;
		//BuildPipeline.BuildAssetBundle(mainAsset, null, assetfile, options, target);
		//BuildPipeline.PopAssetDependencies();

        ///-------------------------------刷新---------------------------------------
        BuildPipeline.PopAssetDependencies();
    }

    /// <summary>
    /// 处理Lua文件
    /// </summary>
    static void HandleLuaFile() {
        string luaPath = AppDataPath + "/StreamingAssets/lua/";

        //----------复制Lua文件----------------
        if (!Directory.Exists(luaPath)) {
            Directory.CreateDirectory(luaPath); 
        }
        string[] luaPaths = { AppDataPath + "/LuaFramework/lua/", 
                              AppDataPath + "/LuaFramework/Tolua/Lua/" };

        for (int i = 0; i < luaPaths.Length; i++) {
            paths.Clear(); files.Clear();
            string luaDataPath = luaPaths[i].ToLower();
            Recursive(luaDataPath);
            int n = 0;
            foreach (string f in files) {
                if (f.EndsWith(".meta") || f.EndsWith(".luaprj") || f.EndsWith(".buildpath") || f.EndsWith(".project")) continue;
                string newfile = f.Replace(luaDataPath, "");
                string newpath = luaPath + newfile;
                string path = Path.GetDirectoryName(newpath);
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                if (File.Exists(newpath)) {
                    File.Delete(newpath);
                }
                if (AppConst.LuaByteMode) {
                    EncodeLuaFile(f, newpath);
                } else {
                    File.Copy(f, newpath, true);
                }
                UpdateProgress(n++, files.Count, newpath);
            } 
        }
        EditorUtility.ClearProgressBar();
        AssetDatabase.Refresh();
    }

    static void BuildShareIndex()
    {
        string resPath = AppDataPath + "/StreamingAssets/Share/";
        ///----------------------创建文件列表-----------------------
        string newFilePath = resPath + "/Share.txt";
        if (File.Exists(newFilePath)) File.Delete(newFilePath);

        paths.Clear(); files.Clear();
        Recursive(resPath);

        FileStream fs = new FileStream(newFilePath, FileMode.CreateNew);
        StreamWriter sw = new StreamWriter(fs);
        for (int i = 0; i < files.Count; i++)
        {
            string file = files[i];
            string ext = Path.GetExtension(file);
            if (file.EndsWith(".meta") || file.Contains(".DS_Store")) continue;
            
            string value = file.Replace(resPath, string.Empty);
            value = value.Replace(".unity3d", "");
            sw.WriteLine(value);
        }
        sw.Close(); fs.Close();
    }

    static void BuildFileIndex() {
        string resPath = AppDataPath + "/StreamingAssets/";
        ///----------------------创建文件列表-----------------------
        string newFilePath = resPath + "/files.txt";
        if (File.Exists(newFilePath)) File.Delete(newFilePath);

        paths.Clear(); files.Clear();
        Recursive(resPath);

        FileStream fs = new FileStream(newFilePath, FileMode.CreateNew);
        StreamWriter sw = new StreamWriter(fs);

        for (int i = 0; i < files.Count; i++) {
            string file = files[i];
            string ext = Path.GetExtension(file);
            if (file.EndsWith(".meta") || file.Contains(".DS_Store")) continue;

            string md5 = Util.md5file(file);
            string value = file.Replace(resPath, string.Empty);
            float number = 0;
            UnityEngine.Debug.Log(file);
            foreach (string d in Directory.GetFileSystemEntries(AppDataPath+ "/StreamingAssets/" + value))
            {
                FileStream files = File.OpenRead(d);
                number += files.Length;
                files.Close();
            }

            sw.WriteLine(value + "|" + md5 + "|" + number);
        } 
        sw.Close(); fs.Close();
    }

    /// <summary>
    /// 数据目录
    /// </summary>
    static string AppDataPath {
        get { return Application.dataPath.ToLower(); }
    }

    /// <summary>
    /// 遍历目录及其子目录
    /// </summary>
    static void Recursive(string path) {
        string[] names = Directory.GetFiles(path);
        string[] dirs = Directory.GetDirectories(path);
        foreach (string filename in names) {
            string ext = Path.GetExtension(filename);
            if (ext.Equals(".meta")) continue;
            files.Add(filename.Replace('\\', '/'));
        }
        foreach (string dir in dirs) {
            paths.Add(dir.Replace('\\', '/'));
            Recursive(dir);
        }
    }

    static void UpdateProgress(int progress, int progressMax, string desc) {
        string title = "Processing...[" + progress + " - " + progressMax + "]";
        float value = (float)progress / (float)progressMax;
        EditorUtility.DisplayProgressBar(title, desc, value);
    }

    public static void EncodeLuaFile(string srcFile, string outFile) {
        if (!srcFile.ToLower().EndsWith(".lua")) {
            File.Copy(srcFile, outFile, true);
            return;
        }
        bool isWin = true; 
        string luaexe = string.Empty;
        string args = string.Empty;
        string exedir = string.Empty;
        string currDir = Directory.GetCurrentDirectory();
        if (Application.platform == RuntimePlatform.WindowsEditor) {
            isWin = true;
            luaexe = "luajit.exe";
            args = "-b " + srcFile + " " + outFile;
            exedir = AppDataPath.Replace("assets", "") + "LuaEncoder/luajit/";
        } else if (Application.platform == RuntimePlatform.OSXEditor) {
            isWin = false;
            luaexe = "./luac";
            args = "-o " + outFile + " " + srcFile;
            exedir = AppDataPath.Replace("assets", "") + "LuaEncoder/luavm/";
        }
        Directory.SetCurrentDirectory(exedir);
        ProcessStartInfo info = new ProcessStartInfo();
        info.FileName = luaexe;
        info.Arguments = args;
        info.WindowStyle = ProcessWindowStyle.Hidden;
        info.UseShellExecute = isWin;
        info.ErrorDialog = true;
        Util.Log(info.FileName + " " + info.Arguments);

        Process pro = Process.Start(info);
        pro.WaitForExit();
        Directory.SetCurrentDirectory(currDir);
    }

    [MenuItem("LuaFramework/Build Protobuf-lua-gen File")]
    public static void BuildProtobufFile() {
        if (!AppConst.ExampleMode) {
            UnityEngine.Debug.LogError("若使用编码Protobuf-lua-gen功能，需要自己配置外部环境！！");
            return;
        }
        string dir = AppDataPath + "/LuaFramework/Lua/3rd/pblua";
        paths.Clear(); files.Clear(); Recursive(dir);

        string protoc = "protoc";
        string protoc_gen_dir = "\"d:/protoc-gen-lua/plugin/protoc-gen-lua.bat\"";

        foreach (string f in files) {
            string name = Path.GetFileName(f);
            string ext = Path.GetExtension(f);
            if (!ext.Equals(".proto")) continue;

            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = protoc;
            info.Arguments = " --lua_out=./ --plugin=protoc-gen-lua=" + protoc_gen_dir + " " + name;
            info.WindowStyle = ProcessWindowStyle.Hidden;
            info.UseShellExecute = true;
            info.WorkingDirectory = dir;
            info.ErrorDialog = true;
            Util.Log(info.FileName + " " + info.Arguments);

            Process pro = Process.Start(info);
            pro.WaitForExit();
        }
        AssetDatabase.Refresh();
    }
}