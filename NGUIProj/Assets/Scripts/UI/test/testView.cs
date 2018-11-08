using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class testView : MonoBehaviour {
    testLogic m_logic = null;
    // Use this for initialization
    void Start () {
        if (m_logic == null)
            m_logic = new testLogic();

        m_logic.Initialize(this);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void UpdateCount(int count)
    {
        Debug.Log("############ " + count);
    }
}
