using UnityEngine;
using System.Collections;

public class UITipsCountDown : MonoBehaviour {
    public static bool IsShow = false;
    public UILabel lab;
    private int coundDown;
    public delegate void OnCountDownCallBack(UITipsCountDown tip,int curCount);
    public event OnCountDownCallBack onCallBack;
    public void Show(int coundDown,OnCountDownCallBack callBack,Color color)
    {
        if(!gameObject.activeSelf)gameObject.SetActive(true);
        this.onCallBack = callBack;
        this.coundDown = coundDown;
        lab.color = color;
        StartCoroutine(CountDown());
    }

    IEnumerator CountDown()
    {
        while (coundDown>=0)
        {
            if(onCallBack != null)
                onCallBack(this,coundDown);
            yield return new WaitForSeconds(1.0f);
            coundDown--;
        }
    }

    public void FillInfo(string info)
    {
        lab.text = info;
    }

    void OnEnable()
    {
        IsShow = true;
    }

    void OnDisable()
    {
        IsShow = false;
    }
}
