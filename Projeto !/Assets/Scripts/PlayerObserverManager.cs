using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerObserverManager
{
    public static Action<int> OnPlayerCoinschanged;

    public static void playerCoinsChanged(int value)
    {
        OnPlayerCoinschanged?.Invoke(value);
    }
    
    public static Action<int> OnPlayerColetavelchanged;
    public static void playerColetavelChanged(int value)
    {
        OnPlayerColetavelchanged?.Invoke(value);
    }
    
}



