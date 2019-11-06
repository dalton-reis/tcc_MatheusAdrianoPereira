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
    public List<ValuePartHand> Compare = new List<ValuePartHand>();
    public string Guidance;



    public static bool TestTime(float time1, float time2)
    {
        return time2 < (time1 + 0.1) && time2 > (time1 - 0.1);
    }
}

[Serializable]
public class ValuePartHand : IComparable<string>
{
    public string part;
    public float value;

    public int CompareTo(string other)
    {
        return part.CompareTo(other);
    }
}