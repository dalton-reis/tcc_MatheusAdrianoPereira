using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ConfigAnimationUtil
{
    private static string PATH_SIGNAL(string id) => $"Assets/MyProject/Signals/{id}.anim";
    private static string PATH_CFG_SIGNAL(string id) => $"Assets/MyProject/Signals/{id}.cfg";

    public static ConfigAnimation LoadOrBlankConfigAnimation(string idAnimation)
    {
        ConfigAnimation config;
        if (File.Exists(PATH_CFG_SIGNAL(idAnimation)))
        {
            config = ConfigSignalsUtils.LoadCfg<ConfigAnimation>(idAnimation);
        }
        else
        {
            var clip = AssetDatabase.LoadAssetAtPath<AnimationClip>(PATH_SIGNAL(idAnimation));
            config = new ConfigAnimation();
            config.EndTime = clip.length;
            config.EndMarker.TimeMarker = clip.length;
        }

        return config;
    }
}
