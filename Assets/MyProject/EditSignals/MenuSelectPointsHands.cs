using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSelectPointsHands : MonoBehaviour
{
    public Text Title;
    public Sprite SpriteToggleActive;
    public Sprite SpriteToggleInactive;
    public GameObject inputOrientation;
    public GameObject[] ButtonsCheck;

    private List<string> ActivePointsHand;
    private MarkerAnimation markerActive;
    private InputField inputFieldOrientation;

    void Start()
    {
        UpdateListnerInputOrientation();
    }

    private void UpdateListnerInputOrientation()
    {
        inputFieldOrientation = inputOrientation.GetComponent<InputField>();
        inputFieldOrientation.onValueChanged.RemoveAllListeners();
        inputFieldOrientation.onValueChanged.AddListener((m) =>
        {
            if (markerActive != null)
            {
                markerActive.Guidance = m;
            }
        });
    }

    public void MenuSelectHandsComparable(string name, MarkerAnimation marker)
    {
        UpdateListnerInputOrientation();
        MenuSelectHands(name, marker.Compare);
        inputFieldOrientation.SetTextWithoutNotify(marker.Guidance);
        inputOrientation.SetActive(true);
        markerActive = marker;
    }

    public void MenuSelectHands(string name, List<string> pointsHand)
    {
        inputOrientation.SetActive(false);
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
        inputOrientation.SetActive(false);
        gameObject.SetActive(false);
    }
}


