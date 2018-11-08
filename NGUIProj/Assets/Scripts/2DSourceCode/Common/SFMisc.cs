
//------------------------------------------------------
//算法，公式，集合
//author LiZongFu
//time 2015.12.29
//------------------------------------------------------

using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
using System.Net.NetworkInformation;
using System.Net.Sockets;
#endif
public class SFMisc : MonoBehaviour
{
    public static float Zero = float.Epsilon;
    public static Color color = new Color(1, 1, 1, 1);
    public static Color blackColor = new Color(0f, 0f, 0f, 0.5f);
    public static Color greyColor = new Color(1, 1, 1, 1);
    public static Vector3[] MeshVertices = new Vector3[4];
    public static Vector2[] uvs = new Vector2[4];
    public static int[] array2 = new int[4 * 3];
    public static Color[] meshColorList = new Color[4] { Color.white, Color.white, Color.white, Color.white };
    public static Dictionary<int, Dot2> dirMove = new Dictionary<int, Dot2>()
    {
     {(int)CSDirection.Right, new Dot2(1,0)},
     {(int)CSDirection.Right_Up, new Dot2(1,-1)},
     {(int)CSDirection.Up, new Dot2(0,-1)},

     {(int)CSDirection.Left_Up, new Dot2(-1,-1)},
     {(int)CSDirection.Left, new Dot2(-1,0)},
     {(int)CSDirection.Left_Down, new Dot2(-1,1)},

     {(int)CSDirection.Down, new Dot2(0,1)},
     {(int)CSDirection.Right_Down, new Dot2(1,1)},
    };


    public static List<Dot2> dirList = new List<Dot2>()
    {
        new Dot2(1,0),
        new Dot2(1,-1),
        new Dot2(0,-1),

        new Dot2(-1,-1),
        new Dot2(-1,0),
        new Dot2(-1,1),

        new Dot2(0,1),
        new Dot2(1,1),
    };

    public static Dictionary<byte, byte> dirDic = new Dictionary<byte, byte>()
    {
        {21,(byte)CSDirection.Right},
        {20,(byte)CSDirection.Right_Down},
        {10,(byte)CSDirection.Down},
        {0,(byte)CSDirection.Left_Down},
        {1,(byte)CSDirection.Left},
        {2,(byte)CSDirection.Left_Up},
        {12,(byte)CSDirection.Up},
        {22,(byte)CSDirection.Right_Up},
    };

    public static CSDirection GetDirection(SFMisc.Dot2 dot)
    {
        dot = dot.Normal();
        byte b = (byte)((dot.x + 1) * 10 + (dot.y + 1));
        if (dirDic.ContainsKey(b)) return (CSDirection)dirDic[b];
        return CSDirection.None;
    }

    public static Dictionary<int, int> effectRotaion = new Dictionary<int, int>()
    {
     {(int)CSDirection.Right, 0},
     {(int)CSDirection.Right_Up, 45},
     {(int)CSDirection.Up, 90},

     {(int)CSDirection.Left_Up, 135},
     {(int)CSDirection.Left, 180},
     {(int)CSDirection.Left_Down, 225},

     {(int)CSDirection.Down, 270},
     {(int)CSDirection.Right_Down, 315},
    };

    public static Dictionary<int, int> effectDepth = new Dictionary<int, int>()
    {
     {(int)CSDirection.Right, -100},
     {(int)CSDirection.Right_Up, -100},
     {(int)CSDirection.Up, -100},

     {(int)CSDirection.Left_Up, -100},
     {(int)CSDirection.Left, -100},
     {(int)CSDirection.Left_Down, -100},

     {(int)CSDirection.Down, -100},
     {(int)CSDirection.Right_Down, -100},
    };

    public static Dictionary<int, Vector3> yeManChongZhuangStartEffectPosDelta = new Dictionary<int, Vector3>()
    {
     {(int)CSDirection.Right, new Vector3(0,0,0)},
     {(int)CSDirection.Right_Up, new Vector3(0,2,0)},
     {(int)CSDirection.Up, new Vector3(-8,0,0)},

     {(int)CSDirection.Left_Up, new Vector3(0,-10,0)},
     {(int)CSDirection.Left, new Vector3(-20,-20,0)},
     {(int)CSDirection.Left_Down, new Vector3(0,-35,0)},

     {(int)CSDirection.Down, new Vector3(11,-20,0)},
     {(int)CSDirection.Right_Down, new Vector3(0,0,0)},
    };

    public static Dictionary<int, string> avatarTypeToName = new Dictionary<int, string>()
    {
        {(int)EAvatarType.Item,"Item"},
        {(int)EAvatarType.MainPlayer,"MainPlayer"},
        {(int)EAvatarType.Monster,"Monster"},
        {(int)EAvatarType.None,"None"},
        {(int)EAvatarType.NPC,"NPC"},
        {(int)EAvatarType.Pet,"Pet"},
        {(int)EAvatarType.Player,"Player"},
        {(int)EAvatarType.Guard,"Guard"},
    };

    public static Dictionary<int, string> stringMotionDic = new Dictionary<int, string>()
    {
        {(int)CSMotion.Static,"Static"},
        {(int)CSMotion.Stand,"Stand"},
        {(int)CSMotion.Walk,"Walk"},
        {(int)CSMotion.Attack,"Attack"},
        {(int)CSMotion.Attack2,"Attack2"},
        {(int)CSMotion.BeAttack,"BeAttack"},
        {(int)CSMotion.Dead,"Dead"},
        {(int)CSMotion.Mining,"Mining"},
        {(int)CSMotion.ShowStand,"ShowStand"},
        {(int)CSMotion.Run,"Run"},
        {(int)CSMotion.WaKuang,"WaKuang"},
        {(int)CSMotion.GuWu,"GuWu"},
        {(int)CSMotion.RunOverDoSmoething,"RunOverDoSmoething"},
    };

    public static Dictionary<int, int> motionNamsCount = new Dictionary<int, int>()
    {
        {(int)CSMotion.Stand,5},
        {(int)CSMotion.Run,8},
        {(int)CSMotion.Attack,8},
        {(int)CSMotion.Attack2,8},
        {(int)CSMotion.Dead,5},
    };

    public static Dictionary<int, Dictionary<int, int>> partsFPS = new Dictionary<int, Dictionary<int, int>>()
    {
        {(int)EAvatarType.MainPlayer,new Dictionary<int,int>()},
        {(int)EAvatarType.Player,new Dictionary<int,int>()},
        {(int)EAvatarType.Monster,new Dictionary<int,int>()},
        {(int)EAvatarType.Guard,new Dictionary<int,int>()},
        {(int)EAvatarType.Pet,new Dictionary<int,int>()},
        {(int)EAvatarType.NPC,new Dictionary<int,int>()},
        {(int)EAvatarType.Hero,new Dictionary<int,int>()},
    };

    //深度变换原理--模型的左右-右手
    //----------------//
    // 后   后  前
    //   ↖ ↑ ↗  
    //后←中心点→ 前
    //   ↙ ↓ ↘  
    //  后  后  前 
    public static Dictionary<int, int> DephtRight = new Dictionary<int, int>()
   {
     {(int)CSDirection.Down, -1},          //后面
     {(int)CSDirection.Up, 1},            //正的

     {(int)CSDirection.Left_Down, -1},     //后面
     {(int)CSDirection.Left_Up, -1},       //后面
     {(int)CSDirection.Left, -1},          //后面

     {(int)CSDirection.Right, -1},         //正的
     {(int)CSDirection.Right_Down, -1},    //正的
     {(int)CSDirection.Right_Up, -1},      //正的
   };

    public static Dictionary<int, int> BackRight = new Dictionary<int, int>()
   {
     {(int)CSDirection.Down, 2},           //后面
     {(int)CSDirection.Up, -2},            //正的

     {(int)CSDirection.Left_Down, 2},      //后面
     {(int)CSDirection.Left_Up, -2},       //后面
     {(int)CSDirection.Left, -2},          //后面

     {(int)CSDirection.Right, -2},         //正的
     {(int)CSDirection.Right_Down, 2},     //正的
     {(int)CSDirection.Right_Up, -2},      //正的
   };

    // 部位结构深度
    public static Dictionary<int, float> ModelDepht = new Dictionary<int, float>()
    {
        {(int)ModelStructure.Shadow, 3},
        {(int)ModelStructure.Body, 0},
        {(int)ModelStructure.Weapon, 1},
        {(int)ModelStructure.Effect, 2},
        {(int)ModelStructure.Bottom, -1},
    };

    public static byte[] Reverse(byte[] array)
    {
        int length = array.Length / 2;
        byte temp = 0;
        for (int i = 0; i < length; i++)
        {
            temp = array[i];
            array[i] = array[array.Length - i - 1];
            array[array.Length - i - 1] = temp;
        }
        return array;
    }

    public static bool inView(Vector3 positon)
    {
        float left_x = Camera.main.transform.position.x - (Screen.width / 2);
        float right_x = Camera.main.transform.position.x + (Screen.width / 2);
        float down_y = Camera.main.transform.position.y - (Screen.height / 2);
        float up_y = Camera.main.transform.position.y + (Screen.height / 2);

        if ((left_x < positon.x && positon.x < right_x) && (down_y < positon.y && positon.y < up_y))
        {
            return true;
        }
        return false;
    }

    public static bool outView(Vector3 positon)
    {
        float left_x = Camera.main.transform.position.x - (Screen.width / 2);
        float right_x = Camera.main.transform.position.x + (Screen.width / 2);
        float down_y = Camera.main.transform.position.y - (Screen.height / 2);
        float up_y = Camera.main.transform.position.y + (Screen.height / 2);

        if ((left_x < positon.x && positon.x < right_x) && (down_y < positon.y && positon.y < up_y))
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list">it must have been sorted before</param>
    /// <param name="de">Predicate: ArrayList must be sorted by Predicate<T> rule</param>
    /// <returns></returns>
    public static int FindInsertIndex(ArrayList list, object t, IComparer comp)
    {
        if (comp == null || list == null) return -1;

        int begin = 0;
        int end = list.Count - 1;
        int middle = (begin + end) / 2;

        while (begin != end)
        {
            if (list[begin] == null || list[middle] == null) return -1;

            if (comp.Compare(list[begin], t) >= 0 && comp.Compare(list[middle], t) <= 0)
            {
                end = middle;
            }
            else
            {
                begin = middle;
            }
            middle = (begin + end) / 2;
        }
        return middle;
    }

    public static long GUID
    {
        get
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            return BitConverter.ToInt64(buffer, 0);
        }
    }

    public struct Dot2
    {
        public int x, y;
        private static Dot2 mZero = new Dot2(0, 0);
        public static Dot2 Zero
        {
            get
            {
                return mZero;
            }
        }

        public Dot2(int _x, int _y)
        {
            x = _x;
            y = _y;
        }

        public void Clear()
        {
            x = y = 0;
        }

        public bool Equal(Dot2 dot)
        {
            return (dot.x == x && dot.y == y);
        }

        public bool Equal(int xx, int yy)
        {
            return (x == xx && y == yy);
        }

        public Dot2 Abs()
        {
            Dot2 d = new Dot2();
            d.x = (x > 0 ? x : -x);
            d.y = (y > 0 ? y : -y);
            return d;
        }

        public Dot2 Normal()
        {
            Dot2 d = new Dot2();
            d.x = NormalX();
            d.y = NormalY();
            return d;
        }

        public int NormalX()
        {
            return NomalInternal(x);
        }

        public int NormalY()
        {
            return NomalInternal(y);
        }

        int NomalInternal(int value)
        {
            if (value > 0) return 1;
            else if (value < 0) return -1;
            return 0;
        }

        public static Dot2 operator +(Dot2 f, Dot2 s)
        {
            Dot2 dot = new Dot2();
            dot.x = f.x + s.x;
            dot.y = f.y + s.y;
            return dot;
        }


        public static Dot2 operator -(Dot2 f, Dot2 s)
        {
            Dot2 dot = new Dot2();
            dot.x = f.x - s.x;
            dot.y = f.y - s.y;
            return dot;
        }

        public static Dot2 operator *(Dot2 f, int i)
        {
            Dot2 dot = new Dot2();
            dot.x = f.x * i;
            dot.y = f.y * i;
            return dot;
        }

        public static int operator *(Dot2 f, Dot2 s)
        {
            return f.x * s.x + f.y * s.y;
        }

        public int Pow2()
        {
            return x * x + y * y;
        }

        public static int DistancePow2(SFMisc.Dot2 dot0, SFMisc.Dot2 dot1)
        {
            SFMisc.Dot2 d = dot0 - dot1;
            return d.x * d.x + d.y * d.y;
        }
    }

    public struct Dot3
    {
        public int x, y, z;

        public Dot3(int _x, int _y, int _z)
        {
            x = _x;
            y = _y;
            z = _z;
        }

        public void Clear()
        {
            x = y = z = 0;
        }

        public bool Equal(Dot3 dot)
        {
            return (dot.x == x && dot.y == y && dot.z == z);
        }
    }

    static public void SetLayer(GameObject go, int layer)
    {
        go.layer = layer;

        Transform t = go.transform;

        for (int i = 0, imax = t.childCount; i < imax; ++i)
        {
            Transform child = t.GetChild(i);
            SetLayer(child.gameObject, layer);
        }
    }

     ///特殊深度 
    ///道具0 CSItem  public virtual Vector3 getPosition()
    ///火墙-10 CSRoundEffect
    ///阴影-20 CSSpriteAnimation SetShodowDepath
    /// <summary>
    /// 每个格子间距的深度距离是100（像素）
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="depthType">0:格子深度 1：格子下面 2：格子深度上面 3 最小格子深度 4：最大格子深度 100-110：最小格子深度下面(x-100)*100 
    /// 200-210：最大格子深度上面（x-200）*100</param>
    /// <returns></returns>
    public static int GetDepth(ISfCell cell, int depthType,EAvatarType avatarType = EAvatarType.None)
    {
        //数字越大，层次越低
        if (cell == null) return 0;
        int depth = 0;
        if (depthType == 0)
        {
            depth = (int)cell.LocalPosition2.y;
            if (avatarType == EAvatarType.MainPlayer)
            {
                depth -= 10;
            }
            else if (avatarType == EAvatarType.NPC)
            {
                depth -= 8;
            }
            else if (avatarType == EAvatarType.Player)
            {
                depth -= 6;
            }
        }
        else if (depthType == 1)
        {
            depth = (int)cell.LocalPosition2.y + 25;
        }
        else if (depthType == 2)
        {
            depth = (int)cell.LocalPosition2.y - 25;
        }
        else
        {
            ISfCell ss_cell = SFOut.IScene.getiMesh.getCellByISfCell(0, 0);
            ISfCell e_cell = SFOut.IScene.getiMesh.getCellByISfCell(0, SFOut.IGame.HorizontalCount - 1);
            int maxY = (int)Mathf.Min(ss_cell.LocalPosition2.y, e_cell.LocalPosition2.y);
            int minY = (int)Mathf.Max(ss_cell.LocalPosition2.y, e_cell.LocalPosition2.y);
            if (depthType == 3)
            {
                depth = minY + 25;
            }
            else if (depthType == 4)
            {
                depth = maxY - 25;
            }
            else if (depthType >= 100 && depthType <= 110)
            {
                depth = minY + (depthType - 100) * 100;
            }
            else if (depthType >= 200 && depthType <= 210)
            {
                depth = maxY - (depthType - 200) * 100;
            }
        }
        depth = depth - 10000;
        return depth;
    }

    public static void SetParent(Transform p, GameObject c)
    {
        c.transform.parent = p;
        c.transform.localScale = Vector3.one;
        c.transform.localPosition = Vector3.zero;
    }

    public static string GetPlatformName()
    {
#if UNITY_EDITOR
        return GetPlatformForAssetBundles(EditorUserBuildSettings.activeBuildTarget);
#else
            return GetPlatformForAssetBundles(Application.platform);
#endif
    }
#if UNITY_EDITOR
    private static string GetPlatformForAssetBundles(BuildTarget target)
    {

        switch (target)
        {
            case BuildTarget.Android:
                return "Android";
            case BuildTarget.iOS:
                return "iOS";
            case BuildTarget.WebGL:
                return "WebGL";
            case BuildTarget.StandaloneWindows:
            case BuildTarget.StandaloneWindows64:
                return "Windows";
            case BuildTarget.StandaloneOSXIntel:
            case BuildTarget.StandaloneOSXIntel64:
            case BuildTarget.StandaloneOSXUniversal:
                return "OSX";
            default: return null;
        }
    }

#endif
    private static string GetPlatformForAssetBundles(RuntimePlatform target)
    {
        switch (target)
        {
            case RuntimePlatform.Android:
                return "Android";
            case RuntimePlatform.IPhonePlayer:
                return "iOS";
            case RuntimePlatform.WebGLPlayer:
                return "WebGL";
            case RuntimePlatform.WindowsPlayer:
                return "Windows";
            default: return null;
        }
    }
}