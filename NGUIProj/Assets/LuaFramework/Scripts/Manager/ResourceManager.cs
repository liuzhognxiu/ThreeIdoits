using UnityEngine;
using System.Collections;
using System.IO;
using System;

namespace LuaFramework
{
    public class ResourceManager : Manager
    {
        private AssetBundle shared;
        public void initialize(Action func)
        {

            if (func != null) func();
        }

        public AssetBundle LoadBundle(string name)
        {
            string uri = Util.DataPath + name.ToLower() + AppConst.ExtName;
            AssetBundle bundle = AssetBundle.LoadFromFile(uri);
            return bundle;
        }

        public GameObject LoadPrefab(string name)
        {
            if (Helper.SimulateAssetBundleInEditor)
                return Resources.Load<GameObject>(name);
            else
            {
                //Debug.logger.Log("[ASSETBUNDLE]new assetbundle " + name);
                return ResMgr.Instance.LoadAssetFromResource(name) as GameObject;
            }
        }

        public GameObject LoadPrefab(string name, GameObject parent)
        {
            GameObject prefab;

            if (Helper.SimulateAssetBundleInEditor)
                prefab= Resources.Load<GameObject>(name);
            else
            {
                Debug.logger.Log("[ASSETBUNDLE]new assetbundle " + name);
                prefab= ResMgr.Instance.LoadAssetFromResource(name) as GameObject;
            }

            GameObject go = Instantiate(prefab) as GameObject;
            if (parent != null)
            {
                go.transform.parent = parent.transform;
                go.transform.localPosition = Vector3.zero;
                go.transform.localScale = Vector3.one;
            }

            var ns = name.Split('/');
            go.name = ns[ns.Length - 1];
            go.AddComponent<LuaBehaviour>();

            return go;
        }

        public Material LoadMaterial(string name)
        {
            return Resources.Load<Material>(name);
        }

        //载入音效资源
        public AudioClip LoadAudioClip(string name)
        {
            //shared.LoadAsset<AudioClip>(abName, new string[] { assetName }, func);

            if (Helper.SimulateAssetBundleInEditor)
                return Resources.Load<AudioClip>(name);
            else
            {
                //Debug.logger.Log("[ASSETBUNDLE]new assetbundle " + name);
                return ResMgr.Instance.LoadAudioClipAssetFromResource(name) as AudioClip;
            }
        }

        //public Spine.Unity.SkeletonDataAsset LoadSkeletonDataAsset(string name)
        //{
        //    if (Helper.SimulateAssetBundleInEditor)
        //        return Resources.Load(name) as Spine.Unity.SkeletonDataAsset;
        //    else
        //        return ResMgr.Instance.LoadAssetFromResource(name) as Spine.Unity.SkeletonDataAsset;
        //}

        public Texture LoadTexture(string name)
        {
            if (Helper.SimulateAssetBundleInEditor)
                return Resources.Load(name) as Texture;
            else
                return ResMgr.Instance.LoadAssetFromResource(name) as Texture;
        }

        public string LoadText(string name)
        {
            if (Helper.SimulateAssetBundleInEditor)
                return Resources.Load(name).ToString();
            else
                return ResMgr.Instance.LoadAssetText(name);
        }

        void OnDestroy()
        {
            if (shared != null) shared.Unload(true);
            Debug.Log("~ResourceManager was destroy!");
        }
    }
}