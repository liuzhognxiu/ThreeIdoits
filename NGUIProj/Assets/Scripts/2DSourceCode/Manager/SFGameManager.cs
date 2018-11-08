using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net;

public class SFGameManager:MonoBehaviour
{
    public delegate void OnTextureRebuildCallback();
    public virtual void RegFontRebuild(OnTextureRebuildCallback callback) { }
    public virtual void UnRegFontRebuild(OnTextureRebuildCallback callback) { }
    public virtual UnityEngine.Object GetStaticObj(string name) { return null; }
}
