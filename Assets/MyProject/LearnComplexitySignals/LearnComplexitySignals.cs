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
    public GameObject Guidance;
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
        var signal = Signals.FindSignal(Node.Value.Id);
        var dhRecordered = HandRecordered.GetComponent<DrawHands>();
        dhRecordered.DrawRight = signal.UseRightHand;
        dhRecordered.DrawLeft = signal.UseLeftHand;
        var dhTrack = HandTrack.GetComponent<DrawHands>();
        dhTrack.DrawRight = signal.UseRightHand;
        dhTrack.DrawLeft = signal.UseLeftHand;


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
            var marker = ControlMarkers.RefreshEffects(TimeSignal, CfgAnimation);
            Clip.SampleAnimation(HandRecordered, TimeSignal);
            DisableGuidance();

            var incrementTime = true;
            if (marker.TimeMarker < TimeSignal && marker.TimeMarker >= TimeSignal - LastDeltaTime) {
                if (marker.Compare.Count > 0)
                {
                    var acceptComparable = ComparerUtil.Compare(marker, HandRecordered.GetComponent<DrawHands>(), HandTrack.GetComponent<DrawHands>());
                    if (!acceptComparable)
                    {
                        incrementTime = false;
                        EnableGuidance(marker.Guidance);
                        ControlSelectPointsHands.UpdateColorPointsHands(ButonsHandsGuidance, marker.Compare);
                    }
                }
            }

            if (incrementTime)
            {
                TimeSignal += Time.deltaTime;
                LastDeltaTime = Time.deltaTime;

                if (TimeSignal > CfgAnimation.EndTime)
                {
                    TimeSignal = CfgAnimation.EndTime;
                    IsPlayed = false;
                }
            }
        }
    }

    private void DisableGuidance()
    {
        Guidance.SetActive(false);
    }

    private void EnableGuidance(string guidanceDescription)
    {
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
            }
        }
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
