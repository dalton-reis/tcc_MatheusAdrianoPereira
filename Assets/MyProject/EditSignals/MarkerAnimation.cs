using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MarkerAnimation 
{
    public float TimeMarker = 0;
    public List<string> Trail = new List<string>();
    public List<string> Flag = new List<string>();
    public List<string> Compare = new List<string>();
    public string Guidance;



    public static bool TestTime(float time1, float time2)
    {
        return time2 < (time1 + 0.1) && time2 > (time1 - 0.1);
    }
}