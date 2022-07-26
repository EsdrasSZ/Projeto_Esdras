using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class coletavelUI : MonoBehaviour
{
    [SerializeField] private TMP_Text coletavelText;


    private void OnEnable()
    {
        PlayerObserverManager.OnPlayercoletavelchanged  UptdatecoletavelText;
    }

    private void OnDisable()
    {
        PlayerObserverManager.OnPlayercoletavelchanged  UptdatecoletavelText;
    }

    private void UptdatecoletavelText(int coletavel)
    {
        coletavelText.text = coletavel.ToString();
    }
    
}