using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SFModelLoadBase
{
    public bool isSetEmpty = false;
    //TODO 临时等让小金修改
    public CSResource TriggerRes = null;
    public class ModelLoadData
    {
        public GameObject go;
        public SFAtlas atlas;
        public ResourceType type;
        public ResourceAssistType assistType;
    }
    /// <summary>
    /// key:modelID*10000+motion*100+direction 如果是技能modelID*10000+direction
    /// </summary>
    protected static Dictionary<long, ModelLoadData> mLoadedDic = new Dictionary<long, ModelLoadData>();
    public static Dictionary<long, ModelLoadData> LoadedDic
    {
        get { return mLoadedDic; }
        set { mLoadedDic = value; }
    }

    protected static Dictionary<uint, Dictionary<uint, int>> mAtlasNumDic = new Dictionary<uint, Dictionary<uint, int>>();

    /// <summary>
    /// 所有key加载完成后才进行回调
    /// </summary>
    protected CSBetterList<long> mObjectList = new CSBetterList<long>();
    protected CSBetterList<bool> mObjectFinishStateList = new CSBetterList<bool>();
    protected List<string> mHasUseModelResList = new List<string>();
    protected long[] mStructures = new long[10];
    protected CSBetterList<uint> mStructtrueSkill = new CSBetterList<uint>();//如果有技能要下载，也需要等待技能加载完才回调



    public void Release()
    {
        mObjectList.Release();
        mObjectFinishStateList.Release();
        mStructtrueSkill.Release();
    }

    public static long GetKey(int modelID,int motion,int direction)
    {
        CSDirection d = (CSDirection)direction;
        if (motion == (uint)CSMotion.Dead)
            d = CSDirection.Up;
        if (d == CSDirection.Left)
            d = CSDirection.Right;
        else if (d == CSDirection.Left_Up)
            d = CSDirection.Right_Up;
        else if (d == CSDirection.Left_Down)
            d = CSDirection.Right_Down;
        return (long)modelID * 10000 + motion * 100 + (int)d;
    }

    public void SplitKey(long key,ref int modelID,ref int motion,ref int direction)
    {
        modelID = (int)(key / 10000);
        motion = (int)(key % 10000 /100);
        direction = (int)(key % 100);
    }

    public string GetCombineModel(uint model, uint avaterMotion, uint avaterDirection)
    {
        if (avaterMotion == 0 && avaterDirection == 0) return model.ToString();
        uint motion = (uint)avaterMotion;
        uint direction = (uint)avaterDirection;
        CSDirection d = (CSDirection)direction;
        if (avaterMotion == (uint)CSMotion.Dead)
            d = CSDirection.Up;
        if (d == CSDirection.Left)
            d = CSDirection.Right;
        else if (d == CSDirection.Left_Up)
            d = CSDirection.Right_Up;
        else if (d == CSDirection.Left_Down)
            d = CSDirection.Right_Down;
        direction = (uint)d;
        CSStringBuilder.Clear();
        CSStringBuilder.Append(model.ToString(), "_", SFMisc.stringMotionDic.ContainsKey((int)motion) ? SFMisc.stringMotionDic[(int)motion] : "", "_", direction.ToString());
        return CSStringBuilder.ToString();
    }

    public void BeginLoad()
    {
        PreBeginLoad();
        mObjectList.Clear();
        mObjectFinishStateList.Clear();
        for (int i = 0; i < mStructures.Length; i++)
        {
            mStructures[i] = 0;
        }
        //onFinishAll = callBack;
    }

    public virtual void PreBeginLoad()
    {
        
    }

    public void GetWaitModelRes()
    {
        for (int i = 0; i < mObjectList.Count; i++)
        {
            long key = mObjectList[i];
            if (!mLoadedDic.ContainsKey(key)) continue;
            int modelID = 0, motion = 0, direction = 0;
            SplitKey(key, ref modelID, ref motion, ref direction);
            ModelLoadData data = mLoadedDic[key];
            string path = SFOut.IResourceManager.GetKeyPath(key);
            if (SFOut.IResourceManager == null) return;
            if (!string.IsNullOrEmpty(path))
            {
                CSResource res = SFOut.IResourceManager.GetRes(path);
                if (res != null && res.IsCanBeDelete)
                {
                    SFOut.IResourceManager.RemoveWaitingQueueDic(res.Path);
                }
            }
            else
            {
                string Model = GetCombineModel((uint)modelID, (uint)motion, (uint)direction);
                path = CSResource.GetPath(Model, data.type, false);
                CSResource res = SFOut.IResourceManager.GetRes(path);
                if (res != null && res.IsCanBeDelete)
                {
                    SFOut.IResourceManager.RemoveWaitingQueueDic(res.Path);
                }
            }
        }
    }


    public void Load(uint modelID, uint motion, uint direction, uint structure, ResourceType type, ResourceAssistType assistType)
    {
        //Debug.Log(modelID + " " + motion + " " + direction);
        if (SFOut.IResourceManager == null) return;
        long key = GetKey((int)modelID,(int)motion,(int)direction);
        //if (avater != null && avater.getAvatarType() == EAvatarType.MainPlayer)
        //{
        //    Debug.Log("motion = " + (CSMotion)motion);
        //}
        if (mLoadedDic.ContainsKey(key))
        {
            ModelLoadData data = mLoadedDic[key];
            if (data != null && data.atlas != null && !data.atlas.HasBeenDestroy)
            {
                mStructures[structure] = key;
                string path = SFOut.IResourceManager.GetKeyPath(key);
                CSResource res = SFOut.IResourceManager.GetRes(path);
                if (res != null) BeginLoadModelResCallBack(res);
                return;
            }
        }
        else
        {
            ModelLoadData data = new ModelLoadData();
            mLoadedDic.Add(key, data);
            data.type = type;
            data.assistType = assistType;
        }
        mStructures[structure] = key;
        mObjectList.Add(key);
        mObjectFinishStateList.Add(false);
    }

    protected virtual void BeginLoadRes()
    {
        for (int i = 0; i < mObjectList.Count; i++)
        {
            long key = mObjectList[i];
            ModelLoadData data = mLoadedDic[key];
            string path = SFOut.IResourceManager.GetKeyPath(key);
            if (!string.IsNullOrEmpty(path))
            {
                AddHasUseModelResList(path);
                CSResource res = SFOut.IResourceManager.AddQueue(path, data.type, Loaded, data.assistType, true, key);
                BeginLoadModelResCallBack(res);
            }
            else
            {
                int modelID=0, motion=0, direction=0;
                SplitKey(key, ref modelID, ref motion, ref direction);
                string Model = GetCombineModel((uint)modelID, (uint)motion, (uint)direction);
                path = CSResource.GetPath(Model, data.type, false);
                AddHasUseModelResList(path);
                SFOut.IResourceManager.AddKeyPath(key, path);
                //Debug.Log(Model + " " + type+" TimeCount = "+Time.frameCount);
                CSResource res = SFOut.IResourceManager.AddQueue(path, data.type, Loaded, data.assistType, true,key);
                BeginLoadModelResCallBack(res);
            }
        }
    }


    public virtual void BeginLoadModelResCallBack(CSResource res)
    {

    }

    protected virtual void LoadedOne(CSResource res)
    {

    }

    void Loaded(CSResource res)
    {
        if (!mLoadedDic.ContainsKey(res.Key)) return;
        ModelLoadData data = mLoadedDic[res.Key];
        GameObject go = res.MirrorObj as GameObject;
        if (go == null)
        {
            LoadedOne(res);
            return;
        }
        data.atlas = go.GetComponent<SFAtlas>();
        if (data.atlas != null)
        {
            data.atlas.ResPath = res.Path;
        }
        AtlasPoolItemDeal(data.atlas);
        if (FlagFinish(res.Key)&&IsAllFinish())//如果FlagFinish成功，表示是最新的，如果由于动作切换太快，后面的动作比前面动作加载快，那么Loaded都会调用，但是onFinishAll之后后面的动作到了之后才会调用
        {
            CallOnFinishAll();
            //else
            //{
            //    Debug.Log("onFinishAll is Null = ");
            //}
        }
        else
        {
            LoadedOne(res);
        }
    }

    public virtual void CallOnFinishAll()
    {

    }

    public virtual void CallOnExternalFinishCallBack()
    {

    }

    public SFAtlas GetAtlas(int structure)
    {
        long key = mStructures[structure];
        if (mLoadedDic.ContainsKey(key))
            return mLoadedDic[key].atlas;
        return null;
    }

    protected bool FlagFinish(long key)
    {
        int index = mObjectList.IndexOf(key);
        if(index != -1)
        {
            mObjectFinishStateList[index] = true;
            return true;
        }
        return false;
    }

    protected bool IsAllFinish()
    {
        isSetEmpty = false;
        if (mStructtrueSkill.Count != 0) return false;
        for (int i = 0; i < mObjectFinishStateList.Count; i++)
        {
            if (!mObjectFinishStateList[i]) return false;
        }
        return true;
    }

    public bool IsCanLoad(ResourceType type,int modelID)
    {
        uint t = (uint)type;
        if (mAtlasNumDic.ContainsKey(t))
        {
            if (mAtlasNumDic[t].ContainsKey((uint)modelID)) return true;
            int maxNum = 0;
            if (type == ResourceType.PlayerAtlas)
            {
                maxNum = SFOut.IResourceManager.MaxPlayerAtlasNum;
            }
            else if (type == ResourceType.WeaponAtlas)
            {
                maxNum = SFOut.IResourceManager.MaxWeaponAtlasNum;
            }
            else if (type == ResourceType.WingAtlas)
            {
                maxNum = SFOut.IResourceManager.MaxWingAtlasNum;
            }
            if (mAtlasNumDic[t].Count >= maxNum) return false;
        }
        return true;
    }

    public void AddAtlasNum(ResourceType type, uint modelID)
    {
        uint t = (uint)type;
        if (!mAtlasNumDic.ContainsKey(t))
        {
            mAtlasNumDic.Add(t,new Dictionary<uint,int>());
        }
        if (!mAtlasNumDic[t].ContainsKey(modelID))
        {
            mAtlasNumDic[t].Add(modelID, 0);
        }
        mAtlasNumDic[t][modelID] = mAtlasNumDic[t][modelID]+ 1;
    }

    public void RemoveAtlasNum(ResourceType type, uint modelID)
    {
        uint t = (uint)type;
        if (mAtlasNumDic.ContainsKey(t)&&mAtlasNumDic[t].ContainsKey(modelID))
        {
            mAtlasNumDic[t][modelID] = mAtlasNumDic[t][modelID] - 1;
            if (mAtlasNumDic[t][modelID] < 0) mAtlasNumDic[t][modelID] = 0;

            if (mAtlasNumDic[t][modelID] == 0)
            {
                mAtlasNumDic[t].Remove(modelID);
            }
        }
    }

    public virtual void LoadSkillRes()
    {
        
    }

    public virtual void AtlasPoolItemDeal(SFAtlas atlas)
    {
        
    }

    public static void Clear()
    {
        mLoadedDic.Clear();
        mAtlasNumDic.Clear();
    }

    public virtual void Destroy()
    {
        ClearWaitModelRes();
    }

    void AddHasUseModelResList(string path)
    {
        if (!mHasUseModelResList.Contains(path))
            mHasUseModelResList.Add(path);
    }

    void ClearWaitModelRes()
    {
        for (int i = 0; i < mHasUseModelResList.Count; i++)
        {
            if (SFOut.IResourceManager == null) continue;
            CSResource res = SFOut.IResourceManager.GetRes(mHasUseModelResList[i]);
            if (res != null)
            {
                res.onLoaded -= Loaded;
                SFOut.IResourceManager.RemoveWaitingQueueDic(res.Path);
            }
        }
        mHasUseModelResList.Clear();
    }
}
