using UnityEngine;
using System.Collections;

public static class StringExtension
{

    public static string PathNormalize(this string path)
    {
        return path.Replace('\\', '/');
    }

    public static string NoExtension(this string path)
    {
        if (path.LastIndexOf('.') < 0)
            return path;
        return path.Substring(0, path.LastIndexOf('.'));
    }

    public static string AssetFileName(this string path)
    {
        return path.Substring(path.LastIndexOf('/') + 1);
    }
}
