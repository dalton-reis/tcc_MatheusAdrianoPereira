using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSelectPointsHands : MonoBehaviour
{
    public Text Title;
    public GameObject[] ButtonsCheck;

    private List<string> ActivePointsHand;
    private MarkerAnimation markerActive;

    void Start()
    {
    }

    //public void MenuSelectHandsComparable(string name, MarkerAnimation marker)
    //{
    //    UpdateListnerInputOrientation();
    //    MenuSelectHands(name, marker.Compare);
    //    inputFieldOrientation.SetTextWithoutNotify(marker.Guidance);
    //    inputOrientation.SetActive(true);
    //    markerActive = marker;
    //}

    public void MenuSelectHands(string name, List<string> pointsHand)
    {
        Title.text = name;
        ActivePointsHand = pointsHand;
        gameObject.SetActive(!gameObject.activeInHierarchy);
        if (gameObject.activeInHierarchy)
        {
            ControlSelectPointsHands.UpdateColorPointsHands(ButtonsCheck, ActivePointsHand);
        }
    }

    public void ClickCheck(string pointName)
    {
        if (ActivePointsHand.Contains(pointName))
        {
            ActivePointsHand.Remove(pointName);
            SetToggleActive(pointName, false);
        }
        else
        {
            ActivePointsHand.Add(pointName);
            SetToggleActive(pointName, true);
        }
    }

    private void SetToggleActive(string namePoint, bool active)
    {
        var nameBtn = $"Btn_{namePoint}";
        foreach (var btn in ButtonsCheck)
        {
            if (btn.name == nameBtn)
            {
                ControlSelectPointsHands.SetToggleActive(btn, active);
                return;
            }
        }
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}


