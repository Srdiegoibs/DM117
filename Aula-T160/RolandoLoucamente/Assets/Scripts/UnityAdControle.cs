﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_ADS
using UnityEngine.Advertisements;
#endif

/// <summary>
/// Classe que controla ads
/// </summary>
public class UnityAdControle : MonoBehaviour {
    /// <summary>
    /// Variavel de controle se devemos ou nao mostrar ads
    /// </summary>
    public static bool showAds = true;

    //Tipo que pode ser null
    public static DateTime? proxTempoReward = null;

    // Referencia para o obstaculo.
    public static ObsComp obstaculo;

    /// <summary>
    /// Metodo para invocar anuncios
    /// </summary>
    public static void ShowAd() {

#if UNITY_ADS

        //Opcoes para o ad
        ShowOptions opcoes = new ShowOptions();
        opcoes.resultCallback = Unpause;

        if (Advertisement.IsReady()) {
            //Mostra o anuncio
            Advertisement.Show(opcoes);
            //Pausar o jogo enquanto
            //o ad esta sendo mostrad
            MenuPauseComp.pausado = true;
            Time.timeScale = 0;
        }
        
#endif
    }
    /// <summary>
    /// Metodo para mostrar ad com recompensa
    /// </summary>
    public static void ShowRewardAd() {
#if UNITY_ADS
        
        proxTempoReward = DateTime.Now.AddSeconds(15);
        if (Advertisement.IsReady()) {
            // Pausar o jogo
            MenuPauseComp.pausado = true;
            Time.timeScale = 0f;
            //Outra forma de criar a 
            //instancia do ShowOptions e setar o callback
            var opcoes = new ShowOptions {
                resultCallback = TratarMostrarResultado
            };
            
            Advertisement.Show(opcoes);
        }
#endif
    }

#if UNITY_ADS
    public static void Unpause(ShowResult result) {
        //Quando o anuncio acabar 
        //sai do modo pausado
        MenuPauseComp.pausado = false;
        Time.timeScale = 1f;
    }
#endif

    /// <summary>
    /// Metodo para tratar o resultado com reward/recompensa
    /// </summary>
    /// <param name="result"></param>
#if UNITY_ADS
    public static void TratarMostrarResultado(ShowResult result) {

        switch (result) {
            case ShowResult.Finished:
                // Anuncio mostrado. Continue o jogo
                obstaculo.Continue();
                break;
            case ShowResult.Skipped:
                Debug.Log("Ad pulado. Faz nada");
                break;
            case ShowResult.Failed:
                Debug.LogError("Erro no ad. Faz nada");
                break;
        }

    // Saia do modo pausado
        MenuPauseComp.pausado = false;
        Time.timeScale = 1f;
    }
#endif

}
