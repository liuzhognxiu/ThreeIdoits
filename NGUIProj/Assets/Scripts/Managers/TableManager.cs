using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TableManager {
    private static TableManager s_instance = null;
    public static TableManager Instance
    {
        get
        {
            if (s_instance == null)
                s_instance = new TableManager();

            return s_instance;
        }
    }

    public byte[] ReadDataConfig(string filename)
    {
        FileStream fs = GetDataFileStream(filename);
        if (null != fs)
        {
            byte[] bytes = new byte[(int)fs.Length];
            fs.Read(bytes, 0, (int)fs.Length);
            fs.Close();
            return bytes;
        }
        return null;
    }

    public LuaInterface.LuaByteBuffer LuaReadDataConfig(string filename)
    {
        FileStream fs = GetDataFileStream(filename);

        if (null != fs)
        {
            byte[] bytes = new byte[(int)fs.Length];
            fs.Read(bytes, 0, (int)fs.Length);
            fs.Close();
            return new LuaInterface.LuaByteBuffer(bytes);
        }

        return null;
    }

    private FileStream GetDataFileStream(string fileName)
    {
        string filePath = GetDataConfigPath(fileName);
        if (File.Exists(filePath))
        {
            FileStream fs = null ;
            try
            {
                fs = new FileStream(filePath, FileMode.Open);
            }
            catch(System.Exception ex)
            {
                Debug.Log("@@@@@@@@@@######### " + ex.ToString());
            }
            return fs;
        }
        return null;
    }

    private string GetDataConfigPath(string fileName)
    {
        return Application.dataPath + "/../../data/table/" + fileName;
    }
    
}
