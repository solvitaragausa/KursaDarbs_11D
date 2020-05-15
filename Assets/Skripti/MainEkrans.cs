using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainEkrans : MonoBehaviour
{
    public Networking network;
    public TMPro.TMP_Text skaitli;
    public TMPro.TMP_Text speletajs_text;
    void LateUpdate()
    {
        if(Common_Vertibas.AllStats != "no_data")
        {
            string[] data = Common_Vertibas.AllStats.Split(' ');

            Common_Vertibas.IzspeletasSpeles = Int32.Parse(data[0]);
            Common_Vertibas.PunktiKopa = Int32.Parse(data[1]);
            Common_Vertibas.KopaIzspeletaisLaiks = float.Parse(data[2]);
            Common_Vertibas.AugstakaisPunktuSkaits = Int32.Parse(data[3]);
            Common_Vertibas.AugstakaisPunktiMinute = float.Parse(data[4]);

            if (Common_Vertibas.KopaIzspeletaisLaiks != 0) Common_Vertibas.PunktiMinute = (Common_Vertibas.PunktiKopa / Common_Vertibas.KopaIzspeletaisLaiks) * 60;
            else Common_Vertibas.PunktiMinute = 0;

            if (Common_Vertibas.IzspeletasSpeles != 0)
            {
                Common_Vertibas.VidejasisLaiksVienaSpele = Common_Vertibas.KopaIzspeletaisLaiks / Common_Vertibas.IzspeletasSpeles;
                Common_Vertibas.VidejaisPunktiVienaSpele = (float)Common_Vertibas.PunktiKopa / Common_Vertibas.IzspeletasSpeles;
            }
            else
            {
                Common_Vertibas.VidejaisPunktiVienaSpele = 0;
                Common_Vertibas.VidejasisLaiksVienaSpele = 0;
            }



            speletajs_text.text = Common_Vertibas.UserName;
            skaitli.text = "Izspēlētās spēles: " + Common_Vertibas.IzspeletasSpeles.ToString() + Environment.NewLine
                          + "Kopā sakrātie punkti: " + Common_Vertibas.PunktiKopa.ToString() + Environment.NewLine
                          + "Kopējais izspēlētais laiks: " + (Common_Vertibas.KopaIzspeletaisLaiks / 60 / 60).ToString("f2") + " stundas" + Environment.NewLine
                          + "Punkti Minūtē (vidējais): " + Common_Vertibas.PunktiMinute.ToString("f2") + Environment.NewLine
                          + "Vidēji vienā spēlē nopelnītie punkti: " + Common_Vertibas.VidejaisPunktiVienaSpele.ToString("f2") + Environment.NewLine
                          + "Vidējais viens spēles laiks: " + Common_Vertibas.VidejasisLaiksVienaSpele.ToString("f2") + " sekundes" + Environment.NewLine
                          + "Augstākais punktu skaits vienā spēlē: " + Common_Vertibas.AugstakaisPunktuSkaits.ToString() + Environment.NewLine
                          + "Augstākais punkti minūtē vienā spēlē: " + Common_Vertibas.AugstakaisPunktiMinute.ToString("f2") + Environment.NewLine;



        }
        else
        {
            network.Pazinot("Savienojums ar serveri pazuda");
            network.LOGIN.SetActive(true);
            network.MAIN.SetActive(false);

        }


    }


    public void LoadGame()
    {
        SceneManager.LoadScene(1);
        Common_Vertibas.AtlautsSpelet = true;
    }
    public void Exit()
    {
        
        Application.Quit();
    }




}
