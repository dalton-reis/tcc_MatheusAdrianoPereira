using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChoseSignals : MonoBehaviour
{
    public GameObject PanelExample;
    public GameObject Grid;

    // Start is called before the first frame update
    void Start()
    {
        Signals signals = Signals.GetSignals();
        foreach (var signal in signals.List)
        {
            var game = Instantiate(PanelExample, Grid.transform);
            game.name = signal.Id;
            game.SetActive(true);
            var signalName = game.transform.Find("Signal");
            var textSignalName = signalName.GetComponent<Text>();
            textSignalName.text = signal.Name;
            var signalDescription = game.transform.Find("Description");
            var textSignalDescription = signalDescription.GetComponent<Text>();
            textSignalDescription.text = signal.Description;

            var actionGO = game.transform.Find("Action");
            var excludeGO = actionGO.transform.Find("BtnExclude");
            var btnExclude = excludeGO.GetComponent<Button>();
            btnExclude.onClick.AddListener(() => ClickExclude(signal.Id));

            var editGO = actionGO.transform.Find("BtnEdit");
            var btnEdit = editGO.GetComponent<Button>();
            btnEdit.onClick.AddListener(() => ClickEdit(signal.Id));

        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ClickExclude(string id)
    {
        if (EditorUtility.DisplayDialog($"Excluir Sinal {id}", "A exclusão é irreversivel. Deseja continuar?", "Continuar", "Cancelar"))
        {
            var signals = Signals.GetSignals();
            Signal signal = Signals.FindSignal(id, signals);
            signals.List.Remove(signal);
            Signals.Save(signals);
            if (File.Exists($"Assets/MyProject/Signals/{id}.anim")) AssetDatabase.DeleteAsset($"Assets/MyProject/Signals/{id}.anim");
            if (File.Exists($"Assets/MyProject/Signals/{id}.cfg")) File.Delete($"Assets/MyProject/Signals/{id}.cfg");
            StartCoroutine(MainMenuScript.LoadScene("ChoseSignalScene"));
        }

    }

    private void ClickEdit(string id)
    {
        EditSignal.Id = id;
        EditSignal.Operation = "E";
        StartCoroutine(MainMenuScript.LoadScene("EditSignalScene"));
    }

    public void ClickNew()
    {
        EditSignal.Id = "";
        EditSignal.Operation = "I";
        StartCoroutine(MainMenuScript.LoadScene("EditSignalScene"));
    }

    public void Close()
    {
        StartCoroutine(MainMenuScript.LoadScene("MainMenuScene"));
    }
}
