using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class ColetavelUI : MonoBehaviour
{
    [SerializeField] private TMP_Text ColetavelText;
    
   
        private void OnEnable()
        {
            PlayerObserverManager.OnPlayerColetavelchanged += UpdateColetavelText;
        }
        private void OnDisable()
        {
            PlayerObserverManager.OnPlayerColetavelchanged -= UpdateColetavelText;
        }


        private void UpdateColetavelText(int coletaveis)
    {
        ColetavelText.text = coletaveis.ToString();
    }
    

}