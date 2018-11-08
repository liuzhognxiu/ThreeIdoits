using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using System.Collections.Generic;


namespace AssetBundles
{
    public class LoadedAssetBundle
    {
        public AssetBundle m_AssetBundle;
        public int m_ReferencedCount;
        public Object asset = null;

        public LoadedAssetBundle(AssetBundle assetBundle,Object obj)
        {
            m_AssetBundle = assetBundle;
            m_ReferencedCount = 1;
            asset = obj;
        }
        public LoadedAssetBundle(AssetBundle assetBundle)
        {
            m_AssetBundle = assetBundle;
            m_ReferencedCount = 1;
        }
    }

    
    public class AssetBundleManager : MonoBehaviour
    {
        public enum LogMode { All, JustErrors };
        public enum LogType { Info, Warning, Error };

        static LogMode m_LogMode = LogMode.All;
        static string m_BaseDownloadingURL = "";
      public  static AssetBundleManifest m_AssetBundleManifest = null;
//#if UNITY_EDITOR
        static int m_SimulateAssetBundleInEditor = -1;
        const string kSimulateAssetBundles = "SimulateAssetBundles";
//#endif

        static Dictionary<string, LoadedAssetBundle> m_LoadedAssetBundles = new Dictionary<string, LoadedAssetBundle>();
        static Dictionary<string, WWW> m_DownloadingWWWs = new Dictionary<string, WWW>();
        static Dictionary<string, string> m_DownloadingErrors = new Dictionary<string, string>();
        static List<AssetBundleLoadOperation> m_InProgressOperations = new List<AssetBundleLoadOperation>();
        static Dictionary<string, string[]> m_Dependencies = new Dictionary<string, string[]>();
        static List<string> keysToRemove = new List<string>();

        public static LogMode logMode
        {
            get { return m_LogMode; }
            set { m_LogMode = value; }
        }

        // The base downloading url which is used to generate the full downloading url with the assetBundle names.
        public static string BaseDownloadingURL
        {
            get { return m_BaseDownloadingURL; }
            set { m_BaseDownloadingURL = value; }
        }

        // AssetBundleManifest object which can be used to load the dependecies and check suitable assetBundle variants.
        public static AssetBundleManifest AssetBundleManifestObject
        {
            set { m_AssetBundleManifest = value; }
        }

        private static void Log(LogType logType, string text)
        {
            if (logType == LogType.Error)
            { if (Debug.developerConsoleVisible)Debug.LogError("[AssetBundleManager] " + text); }
            else if (m_LogMode == LogMode.All)
            { if (Debug.developerConsoleVisible)Debug.Log("[AssetBundleManager] " + text); }
        }


        // Flag to indicate if we want to simulate assetBundles in Editor without building them actually.
        public static bool SimulateAssetBundleInEditor
        {
            get
            {
#if UNITY_EDITOR
                if (m_SimulateAssetBundleInEditor == -1)
                    m_SimulateAssetBundleInEditor = EditorPrefs.GetBool(kSimulateAssetBundles, true) ? 1 : 0;
                return m_SimulateAssetBundleInEditor != 0;
#endif
                return false;
            }
            set
            {
                #if UNITY_EDITOR
                int newValue = value ? 1 : 0;
                if (newValue != m_SimulateAssetBundleInEditor)
                {
                    m_SimulateAssetBundleInEditor = newValue;
                    EditorPrefs.SetBool(kSimulateAssetBundles, value);
                }
                #endif
            }
        }
        private static string GetStreamingAssetsPath()
        {
            //if (Application.isMobilePlatform || Application.isConsolePlatform)
            //    return Application.streamingAssetsPath+"/";
            //else // For standalone player.
            return /*"file://" +*/ Application.streamingAssetsPath + "/";

            //return "file://" + Application.persistentDataPath + "/";
        }

        // Get loaded AssetBundle, only return vaild object when all the dependencies are downloaded successfully.
        static public LoadedAssetBundle GetLoadedAssetBundle(string assetBundleName, out string error)
        {
            if (m_DownloadingErrors.TryGetValue(assetBundleName, out error))
                return null;

            LoadedAssetBundle bundle = null;
            m_LoadedAssetBundles.TryGetValue(assetBundleName, out bundle);
            if (bundle == null)
                return null;

            // No dependencies are recorded, only the bundle itself is required.
            string[] dependencies = null;
            if (!m_Dependencies.TryGetValue(assetBundleName, out dependencies))
                return bundle;

            // Make sure all dependencies are loaded
            foreach (var dependency in dependencies)
            {
                if (m_DownloadingErrors.TryGetValue(assetBundleName, out error))
                    return bundle;

                // Wait all the dependent assetBundles being loaded.
                LoadedAssetBundle dependentBundle;
                m_LoadedAssetBundles.TryGetValue(dependency, out dependentBundle);
                if (dependentBundle == null)
                    return null;
            }

            return bundle;
        }

        static public AssetBundleLoadManifestOperation Initialize()
        {
            m_BaseDownloadingURL = GetStreamingAssetsPath() + SFMisc.GetPlatformName()+"/";
            if (Debug.developerConsoleVisible) Debug.Log(m_BaseDownloadingURL);
            return Initialize(SFMisc.GetPlatformName());
        }

        static public void InitializeMaifest()
        {
           
#if UNITY_EDITOR
            m_BaseDownloadingURL = Application.streamingAssetsPath + "/" + SFMisc.GetPlatformName() + "/";
#else
            m_BaseDownloadingURL =  Application.persistentDataPath +"/"+ SFMisc.GetPlatformName() + "/";
            //m_BaseDownloadingURL = Application.persistentDataPath + "/Android/";
#endif

            string path = m_BaseDownloadingURL + SFMisc.GetPlatformName();

            //Debug.LogError("--------------------------------------------------------------   "+path);

            AssetBundle ab = AssetBundle.LoadFromFile(path);

            if (ab != null)
            {
                AssetBundleManifestObject = ab.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
            }
            else
            {
                if (Debug.developerConsoleVisible) Debug.LogError("--------------AssetBundleManifest -----------");
            }
        }

        // Load AssetBundleManifest.
        static public AssetBundleLoadManifestOperation Initialize(string manifestAssetBundleName)
        {
#if UNITY_EDITOR
            Log(LogType.Info, "Simulation Mode: " + (SimulateAssetBundleInEditor ? "Enabled" : "Disabled"));
#endif

            var go = new GameObject("AssetBundleManager", typeof(AssetBundleManager));
            DontDestroyOnLoad(go);
            LoadAssetBundle(manifestAssetBundleName, true);
            var operation = new AssetBundleLoadManifestOperation(manifestAssetBundleName, "AssetBundleManifest", typeof(AssetBundleManifest));
            m_InProgressOperations.Add(operation);
            return operation;
        }

        void Update()
        {
            return;

            // Collect all the finished WWWs.
            keysToRemove.Clear();

            foreach (var keyValue in m_DownloadingWWWs)
            {
                WWW download = keyValue.Value;

                // If downloading fails.
                if (download.error != null)
                {
                    m_DownloadingErrors.Add(keyValue.Key, string.Format("Failed downloading bundle {0} from {1}: {2}", keyValue.Key, download.url, download.error));
                    keysToRemove.Add(keyValue.Key);
                    continue;
                }

                // If downloading succeeds.
                if (download.isDone)
                {
                    AssetBundle bundle = download.assetBundle;
                    if (bundle == null)
                    {
                        m_DownloadingErrors.Add(keyValue.Key, string.Format("{0} is not a valid asset bundle.", keyValue.Key));
                        keysToRemove.Add(keyValue.Key);
                        continue;
                    }
                    m_LoadedAssetBundles.Add(keyValue.Key, new LoadedAssetBundle(download.assetBundle));
                    keysToRemove.Add(keyValue.Key);
                }
            }

            // Remove the finished WWWs.
            foreach (var key in keysToRemove)
            {
                WWW download = m_DownloadingWWWs[key];
                m_DownloadingWWWs.Remove(key);
                download.Dispose();
            }

            // Update all in progress operations
            for (int i = 0; i < m_InProgressOperations.Count;)
            {
                if (!m_InProgressOperations[i].Update())
                {
                    m_InProgressOperations.RemoveAt(i);
                }
                else
                    i++;
            }
        }


        #region 卸载AssetBundle
        // Unload assetbundle and its dependencies.
        static public void UnloadAssetBundle(string assetBundleName)
        {
            //#if UNITY_EDITOR
            //            if (SimulateAssetBundleInEditor) return;
            //#endif

            string uiName = "ui/" + (assetBundleName.Replace("(Clone)", "")).ToLower();
            UnloadAssetBundleInternal(uiName);
            UnloadDependencies(uiName);
        }

        static protected void UnloadDependencies(string assetBundleName)
        {
            string[] dependencies = null;
            if (!m_Dependencies.TryGetValue(assetBundleName, out dependencies))
                return;

            // Loop dependencies.
            foreach (var dependency in dependencies)
            {
                UnloadAssetBundleInternal(dependency);
            }

            m_Dependencies.Remove(assetBundleName);
        }

        static protected void UnloadAssetBundleInternal(string assetBundleName)
        {
            string error;
            LoadedAssetBundle bundle = GetLoadedAssetBundle(assetBundleName, out error);
            if (bundle == null)
                return;

            if (--bundle.m_ReferencedCount == 0)
            {
                bundle.m_AssetBundle.Unload(true);
                m_LoadedAssetBundles.Remove(assetBundleName);

                if (Debug.developerConsoleVisible) Log(LogType.Info, assetBundleName + " has been unloaded successfully");
            }
        }
#endregion

        #region  异步加载 AssetBundle
        /// <summary>
        /// 下载UI --assetBundle
        /// </summary>
        /// <param name="assetBundleName"></param>
        /// <param name="assetName"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        static public AssetBundleLoadAssetOperation LoadAssetAsync(string assetBundleName, string assetName, System.Type type)
        {
            if (Debug.developerConsoleVisible) Log(LogType.Info, "Loading " + assetName + " from " + assetBundleName + " bundle");

            AssetBundleLoadAssetOperation operation = null;
            //#if UNITY_EDITOR
            //            if (SimulateAssetBundleInEditor)
            //            {
            //                string[] assetPaths = AssetDatabase.GetAssetPathsFromAssetBundleAndAssetName(assetBundleName, assetName);
            //                if (assetPaths.Length == 0)
            //                {
            //                    Debug.LogError("There is no asset with name \"" + assetName + "\" in " + assetBundleName);
            //                    return null;
            //                }

            //                // @TODO: Now we only get the main object from the first asset. Should consider type also.
            //                Object target = AssetDatabase.LoadMainAssetAtPath(assetPaths[0]);
            //                operation = new AssetBundleLoadAssetOperationSimulation(target);
            //            }
            //            else
            //#endif
            {
                LoadAssetBundle(assetBundleName);
                operation = new AssetBundleLoadAssetOperationFull(assetBundleName, assetName, type);

                m_InProgressOperations.Add(operation);
            }

            return operation;
        }

        static protected void LoadAssetBundle(string assetBundleName, bool isLoadingAssetBundleManifest = false)
        {
            if(Debug.developerConsoleVisible)Log(LogType.Info, "Loading Asset Bundle " + (isLoadingAssetBundleManifest ? "Manifest: " : ": ") + assetBundleName);

            //#if UNITY_EDITOR
            //            // If we're in Editor simulation mode, we don't have to really load the assetBundle and its dependencies.
            //            if (SimulateAssetBundleInEditor) return;
            //#endif
            if (!isLoadingAssetBundleManifest)
            {
                if (m_AssetBundleManifest == null)
                {
                    if (Debug.developerConsoleVisible) Debug.LogError("Please initialize AssetBundleManifest by calling AssetBundleManager.Initialize()");
                    return;
                }
            }

            // Check if the assetBundle has already been processed.
            bool isAlreadyProcessed = LoadAssetBundleInternal(assetBundleName, isLoadingAssetBundleManifest);

            // Load dependencies.
            if (!isAlreadyProcessed && !isLoadingAssetBundleManifest)
                LoadDependencies(assetBundleName);
        }

        /// <summary>
        /// 下载每一个assetBundle
        /// </summary>
        /// <param name="assetBundleName"></param>
        /// <param name="isLoadingAssetBundleManifest"></param>
        /// <returns></returns>
        static protected bool LoadAssetBundleInternal(string assetBundleName, bool isLoadingAssetBundleManifest)
        {
            // Already loaded.
            LoadedAssetBundle bundle = null;

            m_LoadedAssetBundles.TryGetValue(assetBundleName, out bundle);
            if (bundle != null)
            {
                bundle.m_ReferencedCount++;
                return true;
            }

            // @TODO: Do we need to consider the referenced count of WWWs?
            // In the demo, we never have duplicate WWWs as we wait LoadAssetAsync()/LoadLevelAsync() to be finished before calling another LoadAssetAsync()/LoadLevelAsync().
            // But in the real case, users can call LoadAssetAsync()/LoadLevelAsync() several times then wait them to be finished which might have duplicate WWWs.
            if (m_DownloadingWWWs.ContainsKey(assetBundleName))
                return true;

            if (Debug.developerConsoleVisible) Debug.LogError("assetBundleName == " + assetBundleName);

            WWW download = null;
            string url = m_BaseDownloadingURL + assetBundleName;

            // For manifest assetbundle, always download it as we don't have hash for it.
            if (isLoadingAssetBundleManifest)
                download = new WWW(url);
            else
                download = WWW.LoadFromCacheOrDownload(url, m_AssetBundleManifest.GetAssetBundleHash(assetBundleName), 0);

            m_DownloadingWWWs.Add(assetBundleName, download);

            return false;
        }

        /// <summary>
        /// 下载UI-依赖
        /// </summary>
        /// <param name="assetBundleName"></param>
        static protected void LoadDependencies(string assetBundleName)
        {
            if (m_AssetBundleManifest == null)
            {
                if (Debug.developerConsoleVisible) Debug.LogError("Please initialize AssetBundleManifest by calling AssetBundleManager.Initialize()");
                return;
            }

            // Get dependecies from the AssetBundleManifest object..
            string[] dependencies = m_AssetBundleManifest.GetAllDependencies(assetBundleName);

            if (dependencies.Length == 0) return;

            // Record and load all dependencies.
            m_Dependencies.Add(assetBundleName, dependencies);

            for (int i = 0; i < dependencies.Length; i++)
                LoadAssetBundleInternal(dependencies[i], false);
        }
#endregion

        #region 同步加载AssetBundle
        static public LoadedAssetBundle LoadUIAssetAsync(string assetBundleName)
        {
            if (m_AssetBundleManifest == null)
            {
                if (Debug.developerConsoleVisible) Debug.LogError("Please initialize AssetBundleManifest by calling AssetBundleManager.Initialize()");
                return null;
            }

            LoadUIAssetBundleInternal(assetBundleName);

            // Get dependecies from the AssetBundleManifest object..
            string[] dependencies = m_AssetBundleManifest.GetAllDependencies(assetBundleName);

            if (!m_Dependencies.ContainsKey(assetBundleName))
                m_Dependencies.Add(assetBundleName, dependencies);

            for (int i = 0; i < dependencies.Length; i++)
            {
                LoadUIAssetBundleInternal(dependencies[i]);
            }

            LoadedAssetBundle bundle = null;

            m_LoadedAssetBundles.TryGetValue(assetBundleName, out bundle);

            return bundle;
        }
        
        static public LoadedAssetBundle LoadUIAssetBundleInternal(string assetBundleName)
        {
            LoadedAssetBundle bundle = null;

            m_LoadedAssetBundles.TryGetValue(assetBundleName, out bundle);

            if (bundle != null)
            {
                bundle.m_ReferencedCount++;
                return bundle;
            }
            
            string url = m_BaseDownloadingURL + assetBundleName;

            AssetBundle ab = AssetBundle.LoadFromFile(url);

            if (ab != null)
            {
                bundle = new LoadedAssetBundle(ab);
                m_LoadedAssetBundles.Add(assetBundleName, bundle);
            }
            else
            {
               if(Debug.developerConsoleVisible) Debug.LogError(assetBundleName + " == null");
            }

            return bundle;
        }

        static public GameObject LoadUIAsset(string assetName, Transform parent)
        {
            LoadedAssetBundle assetBudle = LoadUIAssetAsync("ui/"+assetName.ToLower());

            GameObject instClone = null;

            if (assetBudle != null)
            {
                GameObject gp = assetBudle.m_AssetBundle.LoadAsset<GameObject>(assetName);
                if (gp != null) instClone = GameObject.Instantiate(gp);
                SFMisc.SetParent(parent, instClone);
                return instClone;
            }
            return instClone;
        }

        static public GameObject LoadUIAsset(string assetName)
        {
#if UNITY_EDITOR
            string[] assetPaths = AssetDatabase.GetAssetPathsFromAssetBundleAndAssetName("ui/"+assetName.ToLower(), assetName);
            if (assetPaths.Length == 0)
            {
               if(Debug.developerConsoleVisible) Debug.LogError("There is no asset with name \"" + assetName + "\" in " + assetName);
                return null;
            }
            //FileCommon.SaveUIResLoadPre("ui/" + assetName.ToLower());
            Object target = AssetDatabase.LoadMainAssetAtPath(assetPaths[0]);

            return target as GameObject;
#else
             LoadedAssetBundle assetBudle = LoadUIAssetAsync("ui/" + assetName.ToLower());

            if (assetBudle == null) return null ;

            return assetBudle.m_AssetBundle.LoadAsset<GameObject>(assetName);
#endif



        }
        #endregion

        public static UnityEngine.Object Prestrain()
        {
#if UNITY_EDITOR
            string[] assetPaths = AssetDatabase.GetAssetPathsFromAssetBundleAndAssetName("chart/icon", "icon");

            if (assetPaths.Length == 0)
            {
                if (Debug.developerConsoleVisible) Debug.LogError("There is no asset with name \"" + "icon" + "\" in " + "icon");
                return null;
            }
            Object target = AssetDatabase.LoadMainAssetAtPath(assetPaths[0]);
            //FileCommon.SaveUIResLoadPre("chart/icon");
            return target;
#else
            LoadedAssetBundle assetBudle = LoadUIAssetAsync("chart/icon");
            if (assetBudle == null) return null;

            Object objs = assetBudle.m_AssetBundle.LoadAsset<Object>("icon");

            return objs;

#endif
        }

        public static UnityEngine.Object PrestrainFont()
        {
#if UNITY_EDITOR
            string[] assetPaths = AssetDatabase.GetAssetPathsFromAssetBundleAndAssetName("font/msyh", "msyh");

            if (assetPaths.Length == 0)
            {
                if (Debug.developerConsoleVisible) Debug.LogError("There is no asset with name \"" + "icon" + "\" in " + "icon");
                return null;
            }
            Object target = AssetDatabase.LoadMainAssetAtPath(assetPaths[0]);
            //FileCommon.SaveUIResLoadPre("font/msyh");
            return target;
#else
            LoadedAssetBundle assetBudle = LoadUIAssetAsync("font/msyh");
            if (assetBudle == null) return null;

            Object objs = assetBudle.m_AssetBundle.LoadAsset<Object>("msyh");

            return objs;

#endif
        }

    }
}