﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LearnSignalsScript : MonoBehaviour
{

    public Text uppercaseLetterText;
    public Text lowerLetterText;
    public Text numberText;
    public GameObject signalsObject;
    public GameObject menuObject;
    public GameObject loadingObject;
    public UnityEngine.UI.Image correctImage;

    Transform transformHand;
    Animator animatorHand;

    string[] letters = {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J",
                        "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T",
                        "U", "V", "W", "X", "Y", "Z"};

    string[] numbers = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

    string[] currentSignals;

    int positionCurrentSignal = -1;

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackButton();
        }

        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                transformHand.Rotate(0f, touch.deltaPosition.x * 0.3f, 0f);
            }
        }

        if (animatorHand != null && animatorHand.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animatorHand.IsInTransition(0))
        {
            loadingObject.SetActive(false);
            correctImage.gameObject.SetActive(true);
        } else {
            loadingObject.SetActive(true);
            correctImage.gameObject.SetActive(false);
        }
    }

    void Init()
    {
        menuObject.SetActive(false);
        signalsObject.SetActive(true);

        GameObject hand = GameObject.Find("Hand");
        transformHand = hand.GetComponent<Transform>();
        animatorHand = GameObject.Find("Hand").GetComponentInChildren<Animator>();

        positionCurrentSignal = -1;
        NextSignalButton();
    }

    public void BackButton()
    {
        SceneManager.LoadSceneAsync("MainMenuScene");
    }

    public void MenuButton()
    {
        animatorHand.Rebind();
        menuObject.SetActive(true);
        signalsObject.SetActive(false);
        numberText.enabled = false;
        uppercaseLetterText.enabled = false;
        lowerLetterText.enabled = false;
    }

    public void LearnNumbersButton()
    {
        currentSignals = numbers;
        numberText.enabled = true;
        Init();
    }

    public void LearnLettersButton()
    {
        currentSignals = letters;
        uppercaseLetterText.enabled = true;
        lowerLetterText.enabled = true;
        Init();
    }

    public void NextSignalButton()
    {
        if (positionCurrentSignal != currentSignals.Length - 1)
        {
            positionCurrentSignal++;
            PlayAnimationSignal();
        }
    }

    public void PreviusSignalButton()
    {
        if (positionCurrentSignal != 0)
        {
            positionCurrentSignal--;
            PlayAnimationSignal();
        }
    }

    void PlayAnimationSignal()
    {
        transformHand.rotation = Quaternion.Euler(0, -80, 0);

        string signal = currentSignals[positionCurrentSignal];
        uppercaseLetterText.text = signal;
        lowerLetterText.text = signal.ToLower();
        numberText.text = signal;
        animatorHand.Play("Signal" + signal);
    }

    public void ReloadSignalButton()
    {
        transformHand.rotation = Quaternion.Euler(0, -80, 0);
        animatorHand.Play("Initial");
        Invoke("PlayAnimationSignal", 0.1f);
    }
}
