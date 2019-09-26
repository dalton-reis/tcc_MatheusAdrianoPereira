using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ConfigSignalsUtils
{
    internal static T LoadCfg<T>(string nameCfg)
    {
        string json = null;
        using (StreamReader sr = new StreamReader($"Assets/MyProject/Signals/{nameCfg}.cfg"))
        {
            json = sr.ReadToEnd();
        }
        return JsonUtility.FromJson<T>(json);
    }

    internal static void SaveCfg<T>(T siganls, string nameCfg)
    {
        var json = JsonUtility.ToJson(siganls);
        using (StreamWriter sw = new StreamWriter($"Assets/MyProject/Signals/{nameCfg}.cfg"))
        {
            sw.Write(json);
        }
    }
}
