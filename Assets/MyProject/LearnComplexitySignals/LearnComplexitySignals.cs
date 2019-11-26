using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LearnComplexitySignals : MonoBehaviour
{
    public Text Title;
    public Material DefaultMaterial;
    public Mesh SphereMesh;

    public GameObject HandRecordered;
    public GameObject HandTrack;
    public GameObject Congraturation;
    public GameObject Guidance;
    public GameObject GuidanceLeftHand;
    public GameObject GuidanceRightHand;
    public GameObject[] ButonsHandsGuidance;


    private LinkedList<Signal> signals;
    private LinkedListNode<Signal> Node;
    private AnimationClip Clip;
    private ConfigAnimation CfgAnimation;
    private bool IsPlayed = false;
    private float TimeSignal = 0;

    private static string PATH_SIGNAL(string id) => $"Assets/MyProject/Signals/{id}.anim";
    // Start is called before the first frame update
    void Start()
    {
        signals = new LinkedList<Signal>();
        foreach (var signal in Signals.GetSignals().List)
        {
            signals.AddLast(signal);
        }
        Node = signals.First;
        LoadSignal();
        ControlMarkers.DefaultMaterial = DefaultMaterial;
        ControlMarkers.SphereMesh = SphereMesh;
    }

    private void LoadSignal()
    {
        Congraturation.SetActive(false);
        var signal = Signals.FindSignal(Node.Value.Id);
        var dhRecordered = HandRecordered.GetComponent<DrawHands>();
        dhRecordered.DrawRight = signal.UseRightHand;
        dhRecordered.DrawLeft = signal.UseLeftHand;
        dhRecordered.ClearReferencePoint();
        var dhTrack = HandTrack.GetComponent<DrawHands>();
        dhTrack.DrawRight = signal.UseRightHand;
        dhTrack.DrawLeft = signal.UseLeftHand;
        dhTrack.ClearReferencePoint();


        Clip = AssetDatabase.LoadAssetAtPath<AnimationClip>(PATH_SIGNAL(Node.Value.Id));
        Title.text = Node.Value.Name;
        CfgAnimation = ConfigAnimationUtil.LoadOrBlankConfigAnimation(Node.Value.Id);
        TimeSignal = CfgAnimation.StartTime;
        IsPlayed = true;
    }

    float LastDeltaTime = 0;

    // Update is called once per frame
    void Update()
    {
        if (IsPlayed)
        {
            Congraturation.SetActive(false);
            var marker = ControlMarkers.RefreshEffects(TimeSignal, CfgAnimation);
            Clip.SampleAnimation(HandRecordered, TimeSignal);
            DisableGuidance();

            var incrementTime = true;
            if (marker.TimeMarker < TimeSignal && marker.TimeMarker >= TimeSignal - LastDeltaTime) {
                if (marker.Compare.Count > 0)
                {
                    var DHRecordered = HandRecordered.GetComponent<DrawHands>();
                    var DHTracked = HandTrack.GetComponent<DrawHands>();
                    var acceptComparable = ComparerUtil.Compare(marker, DHRecordered, DHTracked);
                    if (!acceptComparable)
                    {
                        incrementTime = false;
                        EnableGuidance(marker.Guidance, DHRecordered);
                        ControlSelectPointsHands.UpdateColorPointsHands(ButonsHandsGuidance, marker.Compare);
                    }
                    else
                    {
                        if (MarkerWithPalm(marker))
                        {
                            DHRecordered.SaveMtrixReference();
                            DHTracked.SaveMtrixReference();
                        }
                    }
                }
            }

            ControlMarkers.incremetTime(incrementTime);

            if (incrementTime)
            {
                TimeSignal += Time.deltaTime;
                LastDeltaTime = Time.deltaTime;

                if (TimeSignal > CfgAnimation.EndTime)
                {
                    TimeSignal = CfgAnimation.EndTime;
                    IsPlayed = false;
                    Congraturation.SetActive(true);
                }
            }
        }
    }

    private bool MarkerWithPalm(MarkerAnimation marker)
    {
        foreach (var part in marker.Compare)
        {
            if (part.part.Substring(2) == "FIST")
                return true;
        }
        return false;
    }

    private void DisableGuidance()
    {
        Guidance.SetActive(false);
    }

    private void EnableGuidance(string guidanceDescription, DrawHands drawHands)
    {
        GuidanceLeftHand.SetActive(drawHands.DrawLeft);
        GuidanceRightHand.SetActive(drawHands.DrawRight);
        Guidance.SetActive(true);
        var textGuidance = Guidance.transform.Find("Description").GetComponent<Text>();
        textGuidance.text = guidanceDescription;
    }

    public void ClickPlay()
    {
        if (IsPlayed) IsPlayed = false;
        else
        {
            IsPlayed = true;
            if (TimeSignal >= CfgAnimation.EndTime)
            {
                TimeSignal = CfgAnimation.StartTime;
                HandRecordered.GetComponent<DrawHands>().ClearReferencePoint();
                HandTrack.GetComponent<DrawHands>().ClearReferencePoint();
            }
        }
    }

    public void RestartAnimation()
    {
        IsPlayed = true;
        DisableGuidance();
        TimeSignal = CfgAnimation.StartTime;
        var marker = ControlMarkers.RefreshEffects(TimeSignal, CfgAnimation);
        Clip.SampleAnimation(HandRecordered, TimeSignal);
        HandRecordered.GetComponent<DrawHands>().ClearReferencePoint();
        HandTrack.GetComponent<DrawHands>().ClearReferencePoint();
    }

    public void ClickNext()
    {
        if (Node.Next != null)
        {
            Node = Node.Next;
            LoadSignal();
        }
    }

    public void ClickPrevious()
    {
        if (Node.Previous != null)
        {
            Node = Node.Previous;
            LoadSignal();
        }
    }

    public void ClickClose()
    {
        StartCoroutine(MainMenuScript.LoadScene("MainMenuScene"));
    }


}
