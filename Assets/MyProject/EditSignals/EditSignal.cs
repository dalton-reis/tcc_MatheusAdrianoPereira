using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System;

public class EditSignal : MonoBehaviour
{
    // fields usage just to Input data on scene
    public static string Operation = "I";
    public static string Id = "";

    // Objects Controls
    public InputField InputFieldId;
    public InputField InputFieldName;
    public InputField InputFieldDescription;
    public Toggle ToggleRightHand;
    public Toggle ToggleLeftHand;

    // Start is called before the first frame update
    void Start()
    {
        if (Operation == "E")
        {
            InputFieldId.interactable = false;
            Signal atualSignal = Signals.FindSignal(Id);
            ConsistSignal(atualSignal);

            InputFieldId.text = atualSignal.Id;
            InputFieldName.text = atualSignal.Name;
            InputFieldDescription.text = atualSignal.Description;
            ToggleRightHand.isOn = atualSignal.UseRightHand;
            ToggleLeftHand.isOn = atualSignal.UseLeftHand;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Close()
    {
        StartCoroutine(MainMenuScript.LoadScene("ChoseSignalScene"));
    }

    public void EditAnimation()
    {
        if (ValidateSignalId() && EditorUtility.DisplayDialog("Editar Animação", "Ao continuar será gravado as configurações atuais. Deseja continuar?", "Continuar", "Cancelar"))
        {
            SaveSignal();
            EditorAnim.SignalId = InputFieldId.text;
            StartCoroutine(MainMenuScript.LoadScene("EditAnimationSignalsScene"));
        }
    }

    public void Save()
    {
        if (ValidateSignalId())
        {
            SaveSignal();
            Close();
        }
    }

    public bool ValidateSignalId()
    {
        if (Operation != "E" && (InputFieldId.text.ToUpper() == "GERAL" || Signals.FindSignal(InputFieldId.text) != null))
        {
            EditorUtility.DisplayDialog("Sinal", "Esse ID já existe. Use outro.", "Continuar");
            return false;
        }
        return true;
    }

    private void SaveSignal()
    {
        Signals signals = Signals.GetSignals();
        Signal signal;
        if (Operation == "E")
        {
            signal = Signals.FindSignal(InputFieldId.text, signals);
            ConsistSignal(signal);
        }
        else
        {
            signal = new Signal();
            signals.List.Add(signal);
        }
        signal.Id = InputFieldId.text;
        signal.Name = InputFieldName.text;
        signal.Description = InputFieldDescription.text;
        signal.UseRightHand = ToggleRightHand.isOn;
        signal.UseLeftHand = ToggleLeftHand.isOn;

        Signals.Save(signals);
    }

    private void ConsistSignal(Signal atualSignal)
    {
        if (atualSignal == null)
        {
            EditorUtility.DisplayDialog("ERRO", "Sinal não encontrado", "Retornar a escolha de Sinais");
            Close();
        }
    }
}
