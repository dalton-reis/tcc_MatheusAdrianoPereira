using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlSelectPointsHands
{

    public static void UpdateColorPointsHands(GameObject[] buttons, List<string> pointsHands)
    {
        foreach (var btn in buttons)
        {
            var namePoint = btn.name.Substring(4);
            SetToggleActive(btn, pointsHands.Contains(namePoint));
        }
    }

    public static void UpdateColorPointsHands(GameObject[] buttons, List<ValuePartHand> pointsHands)
    {
        foreach (var btn in buttons)
        {
            var namePoint = btn.name.Substring(4);
            SetToggleActive(btn, pointsHands.Exists(p => p.part == namePoint));
        }
    }

    public static void SetToggleActive(GameObject btn, bool active)
    {
        var bone = btn.transform.Find("bone");
        var sprite = bone.GetComponent<Image>();
        if (active) sprite.color = Color.green;
        else sprite.color = Color.white;
    }

    public static void SetToggleActive(GameObject btn, Color color)
    {
        var bone = btn.transform.Find("bone");
        var sprite = bone.GetComponent<Image>();
        sprite.color = color;
    }
}
