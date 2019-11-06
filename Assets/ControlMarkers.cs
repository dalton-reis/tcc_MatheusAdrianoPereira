using System;
using UnityEngine;

public class ControlMarkers
{
    private static MarkerAnimation marker;
    public static Material DefaultMaterial;
    public static Mesh SphereMesh;
    private static bool incrementedTime;

    internal static MarkerAnimation RefreshEffects(float time, ConfigAnimation cfgAnimation)
    {
        return marker = FindMarker(cfgAnimation, time);
    }

    private static MarkerAnimation FindMarker(ConfigAnimation cfgAnimation, float time)
    {
        if (MarkerAnimation.TestTime(cfgAnimation.EndTime, time))
        {
            return cfgAnimation.EndMarker;
        }

        float maxTime = cfgAnimation.StartTime;
        MarkerAnimation result = cfgAnimation.StartMarker;
        foreach (var marker in cfgAnimation.Markers)
        {
            if (marker.TimeMarker > maxTime && marker.TimeMarker < time)
            {
                maxTime = marker.TimeMarker;
                result = marker;
            }
        }

        return result;
    }

    internal static void DrawEffects(GameObject gameObject, string position)
    {
        if(marker != null)
        {
            var componentEffect = gameObject.GetComponent<EffectPoint>();
            if (componentEffect == null)
            {
                componentEffect = gameObject.AddComponent<EffectPoint>();
                componentEffect.DefaultMaterial = DefaultMaterial;
                componentEffect.SphereMesh = SphereMesh;
            }

            if (incrementedTime) componentEffect.ActiveTrail = marker.Trail.Contains(position);
            else componentEffect.ActiveTrail = false;
            componentEffect.ActiveFlag = marker.Flag.Contains(position);
        }
    }

    public static void CleanMarker()
    {
        marker = null;
    }

    internal static void incremetTime(bool incrementTime)
    {
        incrementedTime = incrementTime;
    }
}
