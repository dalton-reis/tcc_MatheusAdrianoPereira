using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ConfigAnimation
{
    public float StartTime = 0;
    public MarkerAnimation StartMarker = new MarkerAnimation();

    public float EndTime = 0;
    public MarkerAnimation EndMarker = new MarkerAnimation();

    public List<MarkerAnimation> Markers = new List<MarkerAnimation>();
}
