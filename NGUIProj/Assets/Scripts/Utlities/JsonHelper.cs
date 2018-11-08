using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using LitJson;

public class JsonHelper
{

    public static T DeSerialize<T>(string jsonString)
    {
        return DeSerializeByLitjson<T>(jsonString);
    }

    public static string Serialize(object jsonObject)
    {
        return SerializeByLitjson(jsonObject);
    }

    public static bool WriteToFile(object jsonObject, string fileName)
    {
        if (string.IsNullOrEmpty(fileName))
        {
            throw new ArgumentNullException("File name should not empty!");
        }

        if (null == jsonObject)
            return false;

        string jsonString = Serialize(jsonObject);
        System.Diagnostics.Trace.WriteLine("[WriteToFile]" + fileName);
        try
        {
            using (StreamWriter file = System.IO.File.CreateText(fileName))
            {
                file.Write(jsonString);
                file.Close();
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Trace.WriteLine("[WriteToFile] ex" + ex);
        }

        return true;
    }

    public static T ReadFromFile<T>(string fileName)
    {
        if (string.IsNullOrEmpty(fileName))
        {
            throw new ArgumentNullException("File name should not empty!");
        }

        using (StreamReader file = File.OpenText(fileName))
        {
            string jsonString = file.ReadToEnd();
            return (T)DeSerialize<T>(jsonString);
        }
    }

    private static string SerializeByLitjson(object jsonObject)
    {
        try
        {
            return JsonMapper.ToJson(jsonObject);
        }
        catch (Exception ex)
        {
            Trace.WriteLine(ex);
        }
        return string.Empty;
    }

    private static T DeSerializeByLitjson<T>(string jsonString)
    {
        return JsonMapper.ToObject<T>(jsonString);
    }

}
