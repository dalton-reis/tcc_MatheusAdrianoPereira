using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class RecorderAnimationSignal : MonoBehaviour
{
    public static string Id = "CASA_SINAL";

    public UnityAnimationRecorder Recorder;
    public Text txtBtnRecorder;
    public Text txtTittle;

    private string PATH_LAST = @"Assets/MyProject/Signals/lastRecordered.anim";
    private string PATH_SIGNAL = $"Assets/MyProject/Signals/{Id}.anim";


    private bool IsRecorder = false;
    private bool IsSavedRecorder = true;

    // Start is called before the first frame update
    void Start()
    {

        var dh = Recorder.gameObject.GetComponent<DrawHands>();
        var signal = Signals.FindSignal(Id);
        dh.DrawRight = signal.UseRightHand;
        dh.DrawLeft = signal.UseLeftHand;

        ControlMarkers.CleanMarker();
        txtTittle.text = Id;
        if (System.IO.File.Exists(PATH_LAST)) AssetDatabase.DeleteAsset(PATH_LAST);

        IsSavedRecorder = System.IO.File.Exists(PATH_SIGNAL);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Close()
    {
        if (IsSavedRecorder || EditorUtility.DisplayDialog($"Sair da gravação", "A gravação não foi salva. Deseja continuar?", "Continuar", "Cancelar"))
        {
            if (System.IO.File.Exists(PATH_SIGNAL)) StartCoroutine(MainMenuScript.LoadScene("EditAnimationSignalsScene"));
            else StartCoroutine(MainMenuScript.LoadScene("EditSignalScene"));
        }
    }

    public void Salvar()
    {
        if (!IsRecorder)
        {
            Debug.Log("Save Recorder!");
            IsSavedRecorder = true;
            if (System.IO.File.Exists(PATH_SIGNAL)) AssetDatabase.DeleteAsset(PATH_SIGNAL);
            AssetDatabase.MoveAsset(PATH_LAST, PATH_SIGNAL);
        }
        else
        {
            EditorUtility.DisplayDialog($"Salvar Gravação", "A gravação não pode ser salva pois ainda não foi finalizada.", "Continuar");
        }
    }

    public void ClickRecorder()
    {
        if (!IsRecorder)
        {
            Debug.Log("Start Recorder!");
            if (System.IO.File.Exists(PATH_LAST)) AssetDatabase.DeleteAsset(PATH_LAST);
            Recorder.StartRecording();
            IsRecorder = true;
            IsSavedRecorder = false;
            txtBtnRecorder.text = "FINALIZAR";
        }
        else
        {
            Debug.Log("End Recorder!");
            txtBtnRecorder.text = "GRAVAR";
            Recorder.StopRecording();
            IsSavedRecorder = false;
            IsRecorder = false;
        }
    }
}
