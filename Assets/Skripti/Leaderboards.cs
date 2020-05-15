using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Leaderboards : MonoBehaviour
{

    public Networking network;

    public TMPro.TMP_Text First;
    public TMPro.TMP_Text Second;
    public TMPro.TMP_Text Third;

    public TMPro.TMP_Text Vieta1;
    public TMPro.TMP_Text Vieta2; 
    public TMPro.TMP_Text Vieta3;
    void OnEnable()
    {
        if (Common_Vertibas.LeaderBoards1 != "no_data")//&& Common_Vertibas.LeaderBoards2 != "no_data" && Common_Vertibas.LeaderBoards3 != "no_data")
        {
            string[] data1 = Common_Vertibas.LeaderBoards1.Split(' ');
            for(int i = 1;i<data1.Length;i=i+2)
            {
                First.text = First.text + "  " + ((int)i / 2 + 1).ToString() + ". "+data1[i] + ": " + data1[i + 1] + Environment.NewLine;
                if (data1[i] == Common_Vertibas.UserName) Vieta1.text = Vieta1.text + ((int)i/2+1).ToString();
            }

        }
        if (Common_Vertibas.LeaderBoards2 != "no_data")//&& Common_Vertibas.LeaderBoards2 != "no_data" && Common_Vertibas.LeaderBoards3 != "no_data")
        {
            string[] data2 = Common_Vertibas.LeaderBoards2.Split(' ');

            for (int i = 1; i < data2.Length; i = i + 2)
            {
                Second.text = Second.text + "  " + ((int)i / 2 + 1).ToString() + ". " + data2[i] + ": " + data2[i + 1] + Environment.NewLine;
                if (data2[i] == Common_Vertibas.UserName) Vieta2.text = Vieta2.text + ((int)i / 2 + 1).ToString();
            }

        }
        if (Common_Vertibas.LeaderBoards3 != "no_data")//&& Common_Vertibas.LeaderBoards2 != "no_data" && Common_Vertibas.LeaderBoards3 != "no_data")
        {

            string[] data3 = Common_Vertibas.LeaderBoards3.Split(' ');

            for (int i = 1; i < data3.Length; i = i + 2)
            {
                Third.text = Third.text + "  " + ((int)i / 2 + 1).ToString() + ". " + data3[i] + ": " + data3[i + 1] + Environment.NewLine;
                if (data3[i] == Common_Vertibas.UserName) Vieta3.text = Vieta3.text + ((int)i / 2 + 1).ToString();
            }
        }
        /*
        else
        {
            network.Pazinot("Savienojums ar serveri pazuda");
            network.LOGIN.SetActive(true);
            gameObject.SetActive(false);
        }*/
    }

    public void CleanText()
    {
        First.text = "";
        Second.text = "";
        Third.text = "";
        Vieta1.text = "Vieta: ";
        Vieta2.text = "Vieta: ";
        Vieta3.text = "Vieta: ";
    }
}
