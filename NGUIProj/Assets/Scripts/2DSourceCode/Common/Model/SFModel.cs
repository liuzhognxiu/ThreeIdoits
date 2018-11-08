using UnityEngine;
using System.Collections;

public interface ISFModel
{
    void SetFPS();
    ISFAction getAction { get; }
}
