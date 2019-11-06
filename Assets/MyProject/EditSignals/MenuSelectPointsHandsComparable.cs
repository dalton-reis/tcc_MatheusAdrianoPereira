using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSelectPointsHandsComparable : MonoBehaviour
{
    public Text Title;
    public InputField inputFieldOrientation;
    public GameObject[] ButtonsCheck;
    public GameObject PanelSlider;
    public Toggle ToggleSlider;
    public Slider Slider;
    public Text ValueText;
    public SphereFloatDraw Sphere; ///TODO REMOVE
    public GameObject Indicator;

    private List<ValuePartHand> ActivePointsHand;
    private string ActivePartName;
    private ValuePartHand ActivePart;
    private MarkerAnimation markerActive;
    private GameObject ActiveBtnPart;

    void Start()
    {
        UpdateListnerInputOrientation();
        foreach(var gameObjectBtn in ButtonsCheck)
        {
            var btn = gameObjectBtn.GetComponent<Button>();
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() => {
                ClickCheck(gameObjectBtn.name.Substring(4));
            });
        }
    }

    private void UpdateListnerInputOrientation()
    {
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
        markerActive = marker;
        ActivePart = null;
        PanelSlider.SetActive(false);
    }

    private void MenuSelectHands(string name, List<ValuePartHand> pointsHand)
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
        PanelSlider.SetActive(true);
        ActivePartName = pointName;
        var part = ActivePointsHand.Find(p => p.part == pointName);
        ActivePart = part;
        if (ActivePart == null)
        {
            ToggleSlider.SetIsOnWithoutNotify(false);
            Slider.SetValueWithoutNotify(0);
            Slider.gameObject.SetActive(false);
            ValueText.text = 0f.ToString("0.0000");
        }
        else
        {
            ToggleSlider.SetIsOnWithoutNotify(true);
            Slider.SetValueWithoutNotify(ActivePart.value);
            Slider.gameObject.SetActive(true);
            ValueText.text = ActivePart.value.ToString("0.0000");
        }
        ControlSelectPointsHands.UpdateColorPointsHands(ButtonsCheck, ActivePointsHand);
        SetToggleActive(pointName, Color.blue);
    }

    private void SetToggleActive(string namePoint, Color color)
    {
        var nameBtn = $"Btn_{namePoint}";
        foreach (var btn in ButtonsCheck)
        {
            if (btn.name == nameBtn)
            {
                ActiveBtnPart = btn;
                ControlSelectPointsHands.SetToggleActive(btn, color);
                return;
            }
        }
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void ClickActivePart()
    {
        if(ActivePart == null)
        {
            ActivePart = new ValuePartHand() { part = ActivePartName, value = 0 };
            ActivePointsHand.Add(ActivePart);
            Slider.SetValueWithoutNotify(ActivePart.value);
            Slider.gameObject.SetActive(true);
        }
        else
        {
            ActivePointsHand.Remove(ActivePart);
            ActivePart = null;
            ToggleSlider.SetIsOnWithoutNotify(false);
            Slider.SetValueWithoutNotify(0);
            Slider.gameObject.SetActive(false);
        }
    }

    public void RefreshValuePart()
    {
        if(ActivePart != null)
        {
            ActivePart.value = Slider.value;
            ValueText.text = ActivePart.value.ToString("0.0000");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ActivePart != null)
        {
            if (Indicator.transform.parent != ActiveBtnPart.transform)
            {
                Indicator.transform.parent = ActiveBtnPart.transform;
            }
            Indicator.SetActive(true);
            var transformRect = Indicator.GetComponent<RectTransform>();
            transformRect.sizeDelta = new Vector2(ActivePart.value*80, ActivePart.value * 80);

            transform.lossyScale.Set(1, 1, 1);
            transformRect.anchoredPosition3D = new Vector3(0, 0, 0);
            transformRect.localRotation = new Quaternion(0, 0, 0, 0);
        }
        else
        {
            Indicator.SetActive(false);
        }
    }
}