using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class Signals
{
    [SerializeField]
    public List<Signal> List = new List<Signal>();

    internal static Signals GetSignals()
    {
        return ConfigSignalsUtils.LoadCfg<Signals>("Geral");
    }

    internal static void Save(Signals siganls)
    {
        ConfigSignalsUtils.SaveCfg(siganls, "Geral");
    }

    internal static Signal FindSignal(string id)
    {
        return FindSignal(id, Signals.GetSignals());
    }

    internal static Signal FindSignal(string id, Signals signals)
    {
        Signal atualSignal = null;
        foreach (var signal in signals.List)
        {
            if (signal.Id == id)
            {
                atualSignal = signal;
            }
        }

        return atualSignal;
    }
}

[Serializable]
public class Signal
{
    public string Id;
    public string Name;
    public string Description;
    public bool UseRightHand = true;
    public bool UseLeftHand = true;
}