using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class CoinUI : MonoBehaviour
{
    [SerializeField] private TMP_Text coinText;


    private void OnEnable()
    {
        PlayerObserverManager.OnPlayerCoinschanged += UpdateCoinText;
    }

    private void OnDisable()
    {
        PlayerObserverManager.OnPlayerCoinschanged -= UpdateCoinText;
    }

    private void UpdateCoinText(int coins)
    {
        coinText.text = coins.ToString();
    }

}
