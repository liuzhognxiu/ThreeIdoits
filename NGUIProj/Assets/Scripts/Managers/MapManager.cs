using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MapManager {
    private static MapManager s_instance = null;
    public static MapManager Instance
    {
        get
        {
            if (s_instance == null)
                s_instance = new MapManager();

            return s_instance;
        }
    }

    public byte[] ReadMapData(string filename)
    {
        FileStream fs = GetMapDataFileStream(filename);
        if (null != fs)
        {
            byte[] bytes = new byte[(int)fs.Length];
            fs.Read(bytes, 0, (int)fs.Length);
            fs.Close();
            return bytes;
        }
        return null;
    }

    private FileStream GetMapDataFileStream(string fileName)
    {
        string filePath = GetMapDataPath(fileName);
        if (File.Exists(filePath))
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(filePath, FileMode.Open);
            }
            catch (System.Exception ex)
            {
                Debug.Log("@@@@@@@@@@######### " + ex.ToString());
            }
            return fs;
        }
        return null;
    }

    public LuaInterface.LuaByteBuffer LuaReadDataConfig(string filename)
    {
        FileStream fs = GetMapDataFileStream(filename);

        if (null != fs)
        {
            byte[] bytes = new byte[(int)fs.Length];
            fs.Read(bytes, 0, (int)fs.Length);
            fs.Close();
            return new LuaInterface.LuaByteBuffer(bytes);
        }

        return null;
    }

    private string GetMapDataPath(string fileName)
    {
        return Application.dataPath + "/../../data/map/" + fileName;
    }
}
