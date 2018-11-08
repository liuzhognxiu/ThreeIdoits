using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MogoEngine.Utils;
using System;
using LuaFramework;

public class UIPool : Singleton<UIPool>, IInit, IDispose
{
    UITipsView tipsTemplate;
    List<UITipsView> tipsList = new List<UITipsView>();
    string name = "UI Root";
    //主Root
    GameObject MainRoot;
    GameObject UItips;
    public UITipsView PopUITipPanel()
    {
        if (tipsTemplate == null)
        {
            //这里获取主面板
            GameObject panel = CreateUITips();
            tipsTemplate = panel.GetComponent<UITipsView>();
            tipsTemplate.gameObject.SetActive(false);
            //  UILayerMgr.Instance.SetLayer(panel, UILayerType.TopWindow);
            tipsTemplate.InitDepth(panel.GetComponent<UIPanel>().depth);
        }
        UITipsView tip = null;
        if (tipsList.Count > 0)
        {
            tip = tipsList[tipsList.Count - 1];
            tipsList.RemoveAt(tipsList.Count - 1);
        }
        else
        {
            tip = GameObject.Instantiate(tipsTemplate) as UITipsView;
        }
        tip.TipsLb.text = "";
        tip.TipsGame.text = "";
        tip.TipsHonor.SetActive(false);
        tip.Panel.alpha = 1;
        tip.CacheTrans.parent = MainRoot.transform;
        tip.CacheTrans.localPosition = Vector3.zero;
        tip.CacheTrans.localScale = Vector3.one;

        return tip;
    }

    public void PushUITipPanel(UITipsView tip)
    {
        if (tip == null) return;
        tip.gameObject.SetActive(false);
        tipsList.Add(tip);
    }

    public void Dispose()
    {

    }

    public void Init()
    {
        MainRoot = GameObject.Find(name).transform.GetChild(0).gameObject;
    }


    /// <summary>
    /// 这里之后按照资源管理读取
    /// </summary>
    /// <returns></returns>
    public GameObject CreateUITips()
    {
        GameObject UItipsGO = GameObject.Instantiate(Resources.Load<GameObject>("UITips")) as GameObject;
        NGUITools.SetParent(MainRoot.transform, UItipsGO);
        return UItipsGO;
    }
}
