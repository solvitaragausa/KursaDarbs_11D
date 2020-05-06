using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameNetworking : MonoBehaviour
{
    public GameObject Pazinojums;
    public TMPro.TMP_Text PazinojumsText;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("KeepConnectionAlive", 0f, 5.0f);
    }

    private void KeepConnectionAlive()
    {
        if (Common_Vertibas.Connected)
        {

            Common_Vertibas.debug_texts[5] = "Savienots ar serveri";
            if (Common_Vertibas.LoggedIn) Common_Vertibas.debug_texts[6] = "Ir ielogojies serverī";
            else Common_Vertibas.debug_texts[6] = "Nav ielogojies serverī";

            try
            {
                Common_Vertibas.Writer.WriteLine();
                Common_Vertibas.Writer.Flush();
            }
            catch
            {
                if (Common_Vertibas.LoggedIn)
                {
                    if (Common_Vertibas.Zaudejis)
                    {
                        PazinojumsText.text = "Savienojums ar serveri pazuda. Turpmākie rezultāti netiks ieskaitīti jūsu statistikā.";
                    }
                    else
                    {
                        PazinojumsText.text = "Savienojums ar serveri pazuda. Šis rezultāts un turpmākie rezultāti netiks ieskaitīti jūsu statistikā.";
                        Common_Vertibas.AtlautsSpelet = false;
                    }
                    Pazinojums.SetActive(true);
                }

                Common_Vertibas.Connected = false;
                Common_Vertibas.LoggedIn = false;
                Common_Vertibas.debug_texts[5] = "Nav savienots ar serveri";
                Common_Vertibas.debug_texts[6] = "Nav ielogojies serverī";
                Common_Vertibas.client.Close();
                Common_Vertibas.client.Dispose();
            }
        }
        else
        {
            Common_Vertibas.debug_texts[5] = "Nav savienots ar serveri";
            Common_Vertibas.debug_texts[6] = "Nav ielogojies serverī";
        }
    }

    public void ResumeGame()
    {
        if(!Common_Vertibas.Zaudejis) Common_Vertibas.AtlautsSpelet = true;
    }


}
