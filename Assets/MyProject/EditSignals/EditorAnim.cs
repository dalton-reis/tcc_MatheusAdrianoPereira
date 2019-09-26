using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class EditorAnim : MonoBehaviour
{
    public static string SignalId = "UVA_SOLETRADO";

    public GameObject PanelToMarker;
    public UnityEngine.UI.Slider Slider;
    public GameObject[] ButtonsInterfaceMarker;
    public MenuSelectPointsHands MenuHands;
    public Material DefaultMaterial;
    public Mesh SphereMesh;

    [Range(0, 100)] public float PercentAnimation;

    public bool IsPlayed = false;

    private AnimationClip clip;

    private Dictionary<float, GameObject> markerObjects = new Dictionary<float, GameObject>();
    private ConfigAnimation CfgAnimation;




    //private List<MarkerAnimation> markers = new List<MarkerAnimation>();
    private MarkerAnimation MarkerActivated = null;
    private string PATH_SIGNAL = $"Assets/MyProject/Signals/{SignalId}.anim";

    void Start()
    {
        ControlMarkers.CleanMarker();
        //var name = "teste9";
        //Controller.AddParameter(name, AnimatorControllerParameterType.Trigger);
        //AnimatorStateMachine rootStateMachine = Controller.layers[0].stateMachine;
        //AnimatorState stateA1 = rootStateMachine.AddState(name);
        //var transitionAnimation = rootStateMachine.AddAnyStateTransition(stateA1);
        //transitionAnimation.AddCondition(AnimatorConditionMode.If, 0, name);
        //
        //
        //var returnIdle = stateA1.AddTransition(rootStateMachine.defaultState);
        //returnIdle.hasExitTime = true;
        //returnIdle.exitTime = 1;
        //AnimationClip clip = AssetDatabase.LoadAssetAtPath<AnimationClip>($"Assets/Teste/{name}.anim");
        //stateA1.motion = clip;
        //
        if (!File.Exists(PATH_SIGNAL))
        {
            Recorder();
        }
        else
        {
            var dh = gameObject.GetComponent<DrawHands>();
            var signal = Signals.FindSignal(SignalId);
            dh.DrawRight = signal.UseRightHand;
            dh.DrawLeft = signal.UseLeftHand;

            clip = AssetDatabase.LoadAssetAtPath<AnimationClip>(PATH_SIGNAL);
            var titleObject = PanelToMarker.transform.Find("Title");
            titleObject.GetComponent<Text>().text = SignalId;
            CfgAnimation = ConfigAnimationUtil.LoadOrBlankConfigAnimation(SignalId);

            MarkerActivated = CfgAnimation.StartMarker;
            PercentAnimation = TimeToPercent(CfgAnimation.StartTime);
            Slider.SetValueWithoutNotify(PercentAnimation);
            ChangeEnableButtons(true);
            CreateGameObjectMarker(CfgAnimation.StartMarker, Color.green);
            CreateGameObjectMarker(CfgAnimation.EndMarker, Color.black);
            foreach (var marker in CfgAnimation.Markers)
            {
                CreateGameObjectMarker(marker, Color.blue);
            }
        }

        ControlMarkers.DefaultMaterial = DefaultMaterial;
        ControlMarkers.SphereMesh = SphereMesh;
        //AssetDatabase.CreateAsset(clip, $"Assets/Signal/cfg/{SignalName}.cfg");
    }

    void Update()
    {
        if (clip != null)
        {
            if (IsPlayed) RefreshPlayer();
            ControlMarkers.RefreshEffects(PercentToTime(PercentAnimation), CfgAnimation);
            clip.SampleAnimation(gameObject, PercentToTime(PercentAnimation));
        }
    }

    private void RefreshPlayer()
    {
        var newPercentAnimation = TimeToPercent(Time.deltaTime/2) + PercentAnimation;
        if (PercentToTime(newPercentAnimation) > clip.length)
        {
            IsPlayed = false;
            Slider.SetValueWithoutNotify(100);
        }
        else Slider.SetValueWithoutNotify(newPercentAnimation);
        PercentAnimation = Slider.value;
    }

    public void CreateMarker()
    {
        float timeMarker = PercentToTime(Slider.value);
        if (FindMarker(timeMarker) == null)
        {
            MarkerAnimation mk = new MarkerAnimation();
            mk.TimeMarker = timeMarker;
            CfgAnimation.Markers.Add(mk);
            CreateGameObjectMarker(mk, Color.blue);
            MarkerActivated = mk;
            ChangeEnableButtons(!IsPlayed);
        }
        else EditorUtility.DisplayDialog($"Novo marcador", "Já possui outro marcador proximo, não é possivel usar esse momento.", "Continuar");
    }

    public void ExcludeMarker()
    {
        if(MarkerActivated == CfgAnimation.StartMarker || MarkerActivated == CfgAnimation.EndMarker)
        {
            EditorUtility.DisplayDialog($"Excluir marcador", "Marcador é de início ou fim, não é possivel excluir.", "Continuar");
        }
        else
        {
            CfgAnimation.Markers.Remove(MarkerActivated);
            Destroy(markerObjects[MarkerActivated.TimeMarker]);
            MarkerActivated = null;
            ChangeEnableButtons(false);
        }
    }

    public void SetStart()
    {
        float timeMarker = PercentToTime(Slider.value);
        var temporaryMarker = FindMarker(timeMarker);
        if (temporaryMarker == null || temporaryMarker == CfgAnimation.StartMarker)
        {
            var gameObjectStartMarker = markerObjects[CfgAnimation.StartTime];
            markerObjects.Remove(CfgAnimation.StartTime);
            markerObjects.Add(timeMarker, gameObjectStartMarker);
            CfgAnimation.StartTime = timeMarker;
            CfgAnimation.StartMarker.TimeMarker = timeMarker;
            RectTransform rect = gameObjectStartMarker.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector3(CalcXLocationObjectInTime(timeMarker), rect.anchoredPosition.y);
            var btn = gameObjectStartMarker.GetComponent<Button>();
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() => ClickMarker(timeMarker));

            MarkerActivated = CfgAnimation.StartMarker;
            ChangeEnableButtons(!IsPlayed);
        }
        else EditorUtility.DisplayDialog($"Novo marcador", "Já possui outro marcador proximo, não é possivel usar esse momento.", "Continuar");
    }

    public void SetEnd()
    {
        float timeMarker = PercentToTime(Slider.value);
        var temporaryMarker = FindMarker(timeMarker);
        if (temporaryMarker == null || temporaryMarker == CfgAnimation.EndMarker)
        {
            var gameObjectEndMarker = markerObjects[CfgAnimation.EndTime];
            markerObjects.Remove(CfgAnimation.EndTime);
            markerObjects.Add(timeMarker, gameObjectEndMarker);
            CfgAnimation.EndTime = timeMarker;
            CfgAnimation.EndMarker.TimeMarker = timeMarker;
            RectTransform rect = gameObjectEndMarker.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector3(CalcXLocationObjectInTime(timeMarker), rect.anchoredPosition.y);
            var btn = gameObjectEndMarker.GetComponent<Button>();
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() => ClickMarker(timeMarker));

            MarkerActivated = CfgAnimation.EndMarker;
            ChangeEnableButtons(!IsPlayed);
        }
        else EditorUtility.DisplayDialog($"Novo marcador", "Já possui outro marcador proximo, não é possivel usar esse momento.", "Continuar");
    }

    private void CreateGameObjectMarker(MarkerAnimation mk, Color color)
    {
        var game = new GameObject($"Marker_{mk.TimeMarker}");
        game.transform.parent = PanelToMarker.transform;
        game.transform.localPosition = new Vector3(0, 0, 0);

        markerObjects.Add(mk.TimeMarker, game);

        float xLocationInfo = CalcXLocationObjectInTime(mk.TimeMarker);

        RectTransform rect = game.AddComponent<RectTransform>();
        rect.anchorMax = new Vector2(0.5f, 0f);
        rect.anchorMin = new Vector2(0.5f, 0f);
        rect.sizeDelta = new Vector2(10, 50);
        rect.transform.localScale = new Vector3(1, 1, 1);
        rect.anchoredPosition = new Vector3(xLocationInfo, 154, 0);
        var image = game.AddComponent<UnityEngine.UI.Image>();
        image.color = color;
        var btn = game.AddComponent<UnityEngine.UI.Button>();
        btn.onClick.AddListener(() => ClickMarker(mk.TimeMarker));
    }

    private float CalcXLocationObjectInTime(float time)
    {
        var relativeSliderWidth = (Slider.GetComponent<RectTransform>().sizeDelta.x - 20);
        var widthSlider = relativeSliderWidth * 2;
        var xLocationInfo = ((widthSlider * TimeToPercent(time)) / 100) - relativeSliderWidth;
        return xLocationInfo;
    }

    public void UpdateTime(float percent)
    {
        PercentAnimation = percent;
        CalculateButtonsAnimation();
        IsPlayed = false;
    }

    public void PlayAnimation()
    {
        if (IsPlayed)
        {
            IsPlayed = false;
            CalculateButtonsAnimation();
        }
        else
        {
            IsPlayed = true;
            ChangeEnableButtons(false);
        }
    }

    private void CalculateButtonsAnimation()
    {
        var time = PercentToTime(PercentAnimation);
        MarkerActivated = FindMarker(time);
        bool enableButtons = MarkerActivated != null;
        ChangeEnableButtons(enableButtons);
    }

    private MarkerAnimation FindMarker(float time)
    {
        if (MarkerAnimation.TestTime(time, CfgAnimation.StartTime))
            return CfgAnimation.StartMarker;

        if (MarkerAnimation.TestTime(time, CfgAnimation.EndTime))
            return CfgAnimation.EndMarker;

        foreach (var marker in CfgAnimation.Markers)
        {
            if (MarkerAnimation.TestTime(time, marker.TimeMarker))
            {
                return marker;
            }
        }
        return null;
    }

    private void ChangeEnableButtons(bool enableButtons)
    {
        foreach (var button in ButtonsInterfaceMarker)
        {
            button.SetActive(enableButtons);
        }
    }

    public void ClickTrailAnimation()
    {
        MenuHands.MenuSelectHands("Rastros", MarkerActivated.Trail);
    }

    public void ClickFlagAnimation()
    {
        MenuHands.MenuSelectHands("Ponto de Atenção", MarkerActivated.Flag);
    }
    public void ClickCompareAnimation()
    {
        MenuHands.MenuSelectHandsComparable("Ponto de Comparação", MarkerActivated);
    }

    private void ClickMarker(float time)
    {
        Slider.value = TimeToPercent(time);
        CalculateButtonsAnimation();
        IsPlayed = false;
    }

    private float TimeToPercent(float time)
    {
        return (time * 100) / clip.length;
    }

    private float PercentToTime(float percent)
    {
        return (clip.length / 100) * percent;
    }

    public void Close()
    {
        if (EditorUtility.DisplayDialog($"Sair da edição", "Se a edição ainda não foi salva as alterações serão perdidas. Deseja continuar?", "Continuar", "Cancelar"))
        {
            StartCoroutine(MainMenuScript.LoadScene("ChoseSignalScene"));
        }
    }

    public void Recorder()
    {
        RecorderAnimationSignal.Id = SignalId;
        StartCoroutine(MainMenuScript.LoadScene("RecorderAnimationSignalScene"));
    }

    public void Save()
    {
        ConfigSignalsUtils.SaveCfg(CfgAnimation, SignalId);
    }
}
