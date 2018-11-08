using UnityEngine;
using System.Collections.Generic;
using System;

/// <summary>
/// UI Atlas contains a collection of sprites inside one large texture atlas.
/// </summary>

public interface ISFAtlas
{
    void InitShaderMiss();
    bool HasBeenDestroy { get; set; }
    string ResPath { get; set; }
}

public class SFAtlas : MonoBehaviour, ISFAtlas
{
    public virtual void InitShaderMiss() { }
    public virtual bool HasBeenDestroy { get { return false; } set { } }
    public virtual string ResPath { get { return string.Empty; } set { } }
    public virtual Material spriteMaterial { get { return null; } set { } }
}