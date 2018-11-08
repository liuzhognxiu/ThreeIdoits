using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class TablePacker {

    private static string tableClientPath = Application.dataPath + "/ClientProtobuf/table/";
    private static string luaClientPath = Application.dataPath + "/LuaFramework/Lua/3rd/pblua/";
    private static string TABLEPATH
    {
        get
        {
            string ret = Application.dataPath + "/../../table";

            return ret;
        }
    }


    private static string EXCELTABLEPATH
    {
        get
        {
            string ret = TABLEPATH + "/workbook";
            return ret;
        }
    }

    private static string TABLEBYTESPATH
    {
        get
        {
            string ret = Application.dataPath + "/../../data/table";
            return ret;
        }
    }
    #region 20 CSharp
    [MenuItem("Tools/Generator Selected Table(CS FILE, PROTO2)")]
    static void GeneratorSelectedTable20()
    {
        string byteName = BuildDataAndProtoFromTable20();
        ProcessTableProtoToCS20(byteName);
    }
    static bool ProcessTableProtoToCS20(string name)
    {
        bool ret = false;
        string projectDirectory = Directory.GetCurrentDirectory();
        try
        {
            Directory.SetCurrentDirectory(TABLEBYTESPATH);
            string param = string.Format("-i:{0}.proto -o:{0}.cs -p:detectMissing", name);
            //环境变量里已经设置了protogen, 所以可以直接运行
            if (Utility.CallProcess("protogen.exe", param))
            {
                File.Copy(@".\" + name + ".cs", tableClientPath + name + ".cs", true);
                File.Delete(@".\" + name + ".cs");
                ret = true;
            }
            Directory.SetCurrentDirectory(projectDirectory);
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
            Directory.SetCurrentDirectory(projectDirectory);
            return false;
        }


        return false;
    }

    static string BuildDataAndProtoFromTable20()
    {
        string excelPath = EditorUtility.OpenFilePanel("选择EXCEL文件", EXCELTABLEPATH, "xls");

        if (string.IsNullOrEmpty(excelPath))
        {
            Debug.LogError("Please select one xls file!");
            return null;
        }

        string excelDirectory = Path.GetDirectoryName(excelPath);
        string projectDirectory = Directory.GetCurrentDirectory();
        string excelName = Path.GetFileNameWithoutExtension(excelPath);
        string bytesName = excelName.ToLower();

        try
        {
            Directory.SetCurrentDirectory(TABLEPATH);
            if (Utility.CallProcess("python", string.Format("xls_deploy_tool_v2.py {0} workbook/{1}.xls", excelName.ToUpper(), excelName)))
            {
                File.Copy(@".\" + bytesName + ".data", TABLEBYTESPATH + "/" + bytesName + ".data", true);
                File.Copy(@".\" + bytesName + ".proto", TABLEBYTESPATH + "/" + bytesName + ".proto", true);
                File.Delete(@".\" + bytesName + ".data");
                File.Delete(@".\" + bytesName + ".proto");
                File.Delete(@".\" + bytesName + ".txt");
                File.Delete(@".\" + bytesName + "_pb2.py");
                File.Delete(@".\" + bytesName + "_pb2.pyc");
            }
            Directory.SetCurrentDirectory(projectDirectory);
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
            Directory.SetCurrentDirectory(projectDirectory);
            return null;
        }

        return bytesName;
    }
    #endregion

    #region 30 CSharp
    [MenuItem("Tools/Generator Selected Table(CS FILE, PROTO3)")]
    static void GeneratorSelectedTable30()
    {
        string byteName = BuildDataAndProtoFromTable30();
        ProcessTableProtoToCS30(byteName);
    }

    static bool ProcessTableProtoToCS30(string name)
    {
        bool ret = false;
        string projectDirectory = Directory.GetCurrentDirectory();
        try
        {
            Directory.SetCurrentDirectory(TABLEBYTESPATH);
            string param = string.Format("--csharp_out=. {0}.proto", name);
            //环境变量里已经设置了protogen, 所以可以直接运行
            if (Utility.CallProcess("protoc.exe", param))
            {
                name = name.Replace("_", " ");
                name = System.Text.RegularExpressions.Regex.Replace(name, @"(^\w)|(\s\w)", m => m.Value.ToUpper());
                name = name.Replace(" ", "");
                File.Copy(@".\" + name + ".cs", tableClientPath + name + ".cs", true);
                File.Delete(@".\" + name + ".cs");
                ret = true;
            }
            Directory.SetCurrentDirectory(projectDirectory);
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
            Directory.SetCurrentDirectory(projectDirectory);
            return false;
        }


        return false;
    }

    static string BuildDataAndProtoFromTable30()
    {
        string excelPath = EditorUtility.OpenFilePanel("选择EXCEL文件", EXCELTABLEPATH, "xls");

        if (string.IsNullOrEmpty(excelPath))
        {
            Debug.LogError("Please select one xls file!");
            return null;
        }

        string excelDirectory = Path.GetDirectoryName(excelPath);
        string projectDirectory = Directory.GetCurrentDirectory();
        string excelName = Path.GetFileNameWithoutExtension(excelPath);
        string bytesName = excelName.ToLower();

        try
        {
            Directory.SetCurrentDirectory(TABLEPATH);
            if (Utility.CallProcess("python", string.Format("xls_deploy_tool_v3.py {0} workbook/{1}.xls", excelName, excelName)))
            {
                File.Copy(@".\" + bytesName + ".data", TABLEBYTESPATH + "/" + bytesName + ".data", true);
                File.Copy(@".\" + bytesName + ".proto", TABLEBYTESPATH + "/" + bytesName + ".proto", true);
                File.Delete(@".\" + bytesName + ".data");
                File.Delete(@".\" + bytesName + ".proto");
                File.Delete(@".\" + bytesName + ".txt");
                File.Delete(@".\" + bytesName + "_pb2.py");
                File.Delete(@".\" + bytesName + "_pb2.pyc");
            }
            Directory.SetCurrentDirectory(projectDirectory);
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
            Directory.SetCurrentDirectory(projectDirectory);
            return null;
        }

        return bytesName;
    }
    #endregion

    #region PROTO3 LUA
    [MenuItem("Tools/Generator Selected Table(LUA FILE, PROTO3)")]
    static void GeneratorSelectedTable30Lua()
    {
        string byteName = BuildDataAndProtoFromTableLua30();
        ProcessTableProtoToLua30(byteName);
    }

    static string BuildDataAndProtoFromTableLua30()
    {
        string excelPath = EditorUtility.OpenFilePanel("选择EXCEL文件", EXCELTABLEPATH, "xls");

        if (string.IsNullOrEmpty(excelPath))
        {
            Debug.LogError("Please select one xls file!");
            return null;
        }

        string excelDirectory = Path.GetDirectoryName(excelPath);
        string projectDirectory = Directory.GetCurrentDirectory();
        string excelName = Path.GetFileNameWithoutExtension(excelPath);
        string bytesName = excelName.ToLower();

        try
        {
            Directory.SetCurrentDirectory(TABLEPATH);
            if (Utility.CallProcess("python", string.Format("xls_deploy_tool_v3.py {0} workbook/{1}.xls", excelName, excelName)))
            {
                File.Copy(@".\" + bytesName + ".data", TABLEBYTESPATH + "/" + bytesName + ".data", true);
                File.Copy(@".\" + bytesName + ".proto", TABLEBYTESPATH + "/" + bytesName + ".proto", true);
                File.Delete(@".\" + bytesName + ".data");
                File.Delete(@".\" + bytesName + ".proto");
                File.Delete(@".\" + bytesName + ".txt");
                File.Delete(@".\" + bytesName + "_pb2.py");
                File.Delete(@".\" + bytesName + "_pb2.pyc");
            }
            Directory.SetCurrentDirectory(projectDirectory);
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
            Directory.SetCurrentDirectory(projectDirectory);
            return null;
        }

        return bytesName;
    }

    static bool ProcessTableProtoToLua30(string name)
    {
        bool ret = false;
        string projectDirectory = Directory.GetCurrentDirectory();
        try
        {
            Directory.SetCurrentDirectory(TABLEBYTESPATH);
            string protoc_gen_dir = "\"d:/protoc-gen-lua/plugin/protoc-gen-lua.bat\"";

            string param = string.Format("--lua_out=./ --plugin=protoc-gen-lua={0} {1}.proto", protoc_gen_dir, name);
            //环境变量里已经设置了protoc, 所以可以直接运行
            if (Utility.CallProcess("protoc", param))
            {
                File.Copy(@".\" + name + ".proto", luaClientPath + name + ".proto", true);
                File.Copy(@".\" + name + "_pb.lua", luaClientPath + name + "_pb.lua", true);
                File.Delete(@".\" + name + ".proto");
                File.Delete(@".\" + name + "_pb.lua");
                ret = true;
            }
            Directory.SetCurrentDirectory(projectDirectory);
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
            Directory.SetCurrentDirectory(projectDirectory);
            return false;
        }


        return false;
    }

    #endregion


}
