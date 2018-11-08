

//-------------------------------------------------------------------------
//scene manager
//Author LiZongFu
//Time 2015.12.15
//-------------------------------------------------------------------------

using System;
using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface ISFScene
{
    ISFAvater getAvatarByISFAvatar(long id);
    Transform CahceWorldTrans { get; }
    ISFMesh getiMesh { get; set; }
    ISFCharactor getMainPlayer { get; }
}