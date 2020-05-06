using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class Networking : MonoBehaviour
{
    public GameObject LOGIN;
    public GameObject REG;
    public GameObject MAIN;

    public TMPro.TMP_Text PazinojumsText;
    public GameObject Pazinojums;

    public TMPro.TMP_InputField LogIn_Nikneims;
    public TMPro.TMP_InputField LogIn_Parole;

    public TMPro.TMP_InputField Reg_Nikneims;
    public TMPro.TMP_InputField Reg_Parole;
    public TMPro.TMP_InputField Reg_Parole2;

    string response;


    void Start()

    { 
        if(Common_Vertibas.LoggedIn) //Tātad izgāja no spēles, jo iepriekšējo reizi jau bija pierakstījies
        {
            LOGIN.SetActive(false);
            MAIN.SetActive(true);
            //Atjauninām statistiku
            SendToServer("4");
            response = GetResponse();
            Common_Vertibas.AllStats = response;
        }
        InvokeRepeating("KeepConnectionAlive", 0f, 5.0f);
    }
    
    private static void Connect()
    {
        try
        {
            Common_Vertibas.client = new TcpClient();
            Common_Vertibas.client.Connect(Common_Vertibas.IpAdrese, Common_Vertibas.Ports);
            Common_Vertibas.Writer = new StreamWriter(Common_Vertibas.client.GetStream(), Encoding.ASCII);
            Common_Vertibas.Reader = new StreamReader(Common_Vertibas.client.GetStream(), Encoding.ASCII);
            Common_Vertibas.Connected = true;
        }
        catch
        {
            Common_Vertibas.Connected = false;
        }
        
    }
    public void LogIn()
    {

        if (!Common_Vertibas.Connected) Connect();

        if (!Common_Vertibas.Connected) Pazinot("Nevar savienoties ar serveri!");
        else
        if (!Common_Vertibas.LoggedIn)
        {
            bool Abort = false;
            if (LogIn_Nikneims.text.Length == 0 || LogIn_Parole.text.Length == 0)
            {
                Pazinot("Aizpildiet visus tukšos laukumus");
                Abort = true;
            }
            else if (LogIn_Nikneims.text.Contains(" ") || LogIn_Parole.text.Contains(" "))
            {
                Pazinot("Spelētāja vardā vai parolē nedrīkst atrasties atstarpei.");
                Abort = true;
            }


            if (!Abort)
            {
                SendToServer("2 " + LogIn_Nikneims.text + " " + LogIn_Parole.text);
                response = GetResponse();

                if (response == "LOGIN_SUCCESS")
                {
                    Common_Vertibas.UserName = LogIn_Nikneims.text;
                    Common_Vertibas.LoggedIn = true;
                    Helperi.Log("Veiksmigi ielogojies");
                    SendToServer("4");
                    response = GetResponse();
                    Common_Vertibas.AllStats = response;
                    LOGIN.SetActive(false);
                    MAIN.SetActive(true);
                }
                else
                {
                    Pazinot("Spelētaja vārds vai parole nav pareiza");
                }
            }
        }
    }

    public void Register()
    {
        if (!Common_Vertibas.Connected) Connect();
        if (!Common_Vertibas.Connected) Pazinot("Nevar savienoties ar serveri");
        else
        if (!Common_Vertibas.LoggedIn)
        {
            bool Abort = false;
            if (Reg_Nikneims.text.Length == 0 || Reg_Parole.text.Length == 0 || Reg_Parole2.text.Length == 0)
            {
                Pazinot("Aizpildiet visus tukšos laukumus");
                Abort = true;
            }
            else if (Reg_Nikneims.text.Contains(" ") || Reg_Parole.text.Contains(" ") || Reg_Parole2.text.Contains(" "))
            {
                Pazinot("Spelētāja vardā vai parolē nedrīkst atrasties atstarpei.");
                Abort = true;
            }
            else if (Reg_Parole.text != Reg_Parole2.text)
            {
                Pazinot("Paroles nesakrīt");
                Abort = true;
            }

            if (!Abort)
            {
                SendToServer("1 " + Reg_Nikneims.text + " " + Reg_Parole.text);
                response = GetResponse();

                if (response == "REG_SUCCESS")
                {
                    Common_Vertibas.UserName = Reg_Nikneims.text;
                    Common_Vertibas.LoggedIn = true;
                    Helperi.Log("Veiksmigi piereģistrējies.");
                    SendToServer("4");
                    response = GetResponse();
                    Common_Vertibas.AllStats = response;
                    REG.SetActive(false);
                    MAIN.SetActive(true);

                }
                else
                {
                    Pazinot("Spēlētaja vārds jau ir aizņemts");
                }
            }

        }



    }

    private void KeepConnectionAlive()
    {
        if (Common_Vertibas.Connected)
        {
            try
            {
                Common_Vertibas.Writer.WriteLine();
                Common_Vertibas.Writer.Flush();
            }
            catch
            {
                if(MAIN.activeSelf)
                {
                    MAIN.SetActive(false);
                    LOGIN.SetActive(true);
                    Pazinot("Pazuda savienojums ar serveri");
                }
           

                Common_Vertibas.Connected = false;
                Common_Vertibas.LoggedIn = false;
                Common_Vertibas.client.Close();
                Common_Vertibas.client.Dispose();
            }
        }
    }

    private void SendToServer(string msg)
    {
        if(Common_Vertibas.Connected)
        {
            try
            {
                Common_Vertibas.Writer.WriteLine(msg);
                Common_Vertibas.Writer.Flush();
            }
            catch
            {
                Common_Vertibas.Connected = false;
                Common_Vertibas.LoggedIn = false;
                Common_Vertibas.client.Close();
                Common_Vertibas.client.Dispose();
            }
        }


    }
    public string GetResponse()
    {
        DateTime Starts = DateTime.Now;
        string Data = null;
        while ((DateTime.Now - Starts).Seconds < Common_Vertibas.Timeouts)
        {
          try
            {
                if (Common_Vertibas.client.GetStream().DataAvailable)
                 Data = Common_Vertibas.Reader.ReadLine();

                if (Data != null)
                {
                    return Data;
                }
            }
            catch
            {
                Common_Vertibas.Connected = false;
                Common_Vertibas.LoggedIn = false;
                Common_Vertibas.client.Close();
                Common_Vertibas.client.Dispose();
                return "no_data";
            }

        }
        return "no_data";
    }

    public void Pazinot(string msg)
    {
        PazinojumsText.text = msg;
        Pazinojums.SetActive(true);
    }
}
