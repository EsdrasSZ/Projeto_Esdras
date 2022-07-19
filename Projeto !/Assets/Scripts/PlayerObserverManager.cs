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
}
