using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testTween : MonoBehaviour {
    public GameObject target;

	// Use this for initialization
	void Start () {
        if (target != null)
        {
            TweenColor tc = target.AddComponent<TweenColor>();
            tc.from = Color.white;
            tc.to = Color.red;
            tc.duration = 2f;
            tc.style = UITweener.Style.Once;
            //tc.enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ClickButton(GameObject go)
    {
        TweenColor tc = target.GetComponent<TweenColor>();
        if (target != null && tc != null)
        {
            tc.enabled = true;
            tc.ResetToBeginning();
            tc.PlayForward();
        }
    }
}
