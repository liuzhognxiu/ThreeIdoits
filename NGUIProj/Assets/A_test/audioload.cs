using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LuaFramework;

public class audioload : MonoBehaviour
{

    public AudioClip a;
    private AudioSource b;
    // Use this for initialization
    void Start()
    {
        Invoke("test", 1f);
    }

    void test()
    {
        a = ResMgr.Instance.LoadAudioClipAssetFromResource("Audio/LoginView");
        b = this.GetComponent<AudioSource>();
        b.clip = a;
        b.Play();
        b.mute = false;

        Instantiate<GameObject>(ResMgr.Instance.LoadAssetFromResource("Prefabs/LoginView") as GameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
