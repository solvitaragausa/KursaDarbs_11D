using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PostGame : MonoBehaviour
{
    public Game_Logic GameLogic;
    public GameObject Pazinojums;
    public TMPro.TMP_Text PazinojumsText;
    void Start()
    {
        if(Common_Vertibas.LoggedIn)
        {
            try
            {
                Common_Vertibas.Writer.WriteLine("3 " + GameLogic.MaxPunkti.ToString() + " " + GameLogic.Laiks.ToString("f2"));
                Common_Vertibas.Writer.Flush();
            }
            catch
            {
                PazinojumsText.text = "Savienojums ar serveri pazuda. Šis rezultāts un turpmākie rezultāti netiks ieskaitīti jūsu statistikā.";
                Pazinojums.SetActive(true);
                Common_Vertibas.Connected = false;
                Common_Vertibas.LoggedIn = false;
                Common_Vertibas.client.Close();
                Common_Vertibas.client.Dispose();
            }
        }

    }
    public void ReloadLevel()
    {
        SceneManager.LoadScene(1);
        Common_Vertibas.AtlautsSpelet = true;
        Common_Vertibas.Zaudejis = false;
        Common_Vertibas.Progress = 0;
    }

    public void LoadMainLevel()
    {
        SceneManager.LoadScene(0);
        Common_Vertibas.AtlautsSpelet = true;
        Common_Vertibas.Zaudejis = false;
        Common_Vertibas.Progress = 0;
    }
}
