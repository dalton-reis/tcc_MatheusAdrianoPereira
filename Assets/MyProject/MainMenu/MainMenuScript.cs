using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            BackButton();
        }
    }

    public void BackButton()
    {
        Application.Quit();
    }

    public void ConfigButton()
    {
        StartCoroutine(LoadScene("ChoseSignalScene"));
    }

    public void ConfigComplexitySignals()
    {
        StartCoroutine(LoadScene("LearnComplexitySignalsScene"));
    }

    public void LoadGameARScene()
    {
        StartCoroutine(LoadScene("GameARScene"));
        //SceneManager.LoadScene("GameAR");
    }

    public void LoadLearnSignalsScene()
    {
        StartCoroutine(LoadScene("LearnSignalsScene"));
        //SceneManager.LoadScene("LearnSignalsScene");
    }

    public void LoadGameVRScene()
    {
        StartCoroutine(LoadScene("GameVRScene"));
        //SceneManager.LoadScene("GameVRScene");
    }

    public static IEnumerator LoadScene(string scene)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);

        while (!asyncLoad.isDone)
        {
            //Debug.Log(asyncLoad.progress);
            yield return null;
        }
    }
}
