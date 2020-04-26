using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class Networking : MonoBehaviour
{
    public TMPro.TMP_InputField LogIn_Nikneims;
    public TMPro.TMP_InputField LogIn_Parole;

    public TMPro.TMP_InputField Reg_Nikneims;
    public TMPro.TMP_InputField Reg_Parole;
    public TMPro.TMP_InputField Reg_Parole2;

    string response;

    string Kludas;



    private static void Connect()
    {
        try
        {
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
        Kludas = "";

        if (!Common_Vertibas.Connected) Connect();

        if (!Common_Vertibas.Connected) AddError("Nevar savienoties ar serveri");
        else
        if (!Common_Vertibas.LoggedIn)
        {
            bool Abort = false;
            if (LogIn_Nikneims.text.Length == 0 || LogIn_Parole.text.Length == 0)
            {
                AddError("Aizpildiet visus tukšos laukumus");
                Abort = true;
            }
            else if (LogIn_Nikneims.text.Contains(" ") || LogIn_Parole.text.Contains(" "))
            {
                AddError("Spelētāja vardā vai parolē nedrīkst atrasties atstarpei.");
                Abort = true;
            }


            if (!Abort)
            {
                SendToServer("2 " + LogIn_Nikneims.text + " " + LogIn_Parole.text);
                response = GetResponse();

                if (response == "LOGIN_SUCCESS")
                {
                    Common_Vertibas.LoggedIn = true;
                    Helperi.Log("Veiksmigi ielogojies");
                }
                else
                {
                    AddError("Speletaja vards vai parole nav pareiza");
                }
            }
        }
        else
        {
            AddError("Tu jau esi iegājis iekšā.");
        }
        if (Kludas != "") Helperi.Log(Kludas); //Todo SHow it
    }

    public void Register()
    {
        Kludas = "";
        if (!Common_Vertibas.Connected) Connect();
        if (!Common_Vertibas.Connected) AddError("Nevar savienoties ar serveri");
        else
        if (!Common_Vertibas.LoggedIn)
        {
            bool Abort = false;
            if (Reg_Nikneims.text.Length == 0 || Reg_Parole.text.Length == 0 || Reg_Parole2.text.Length == 0)
            {
                AddError("Aizpildiet visus tukšos laukumus");
                Abort = true;
            }
            else if (Reg_Nikneims.text.Contains(" ") || Reg_Parole.text.Contains(" ") || Reg_Parole2.text.Contains(" "))
            {
                AddError("Spelētāja vardā vai parolē nedrīkst atrasties atstarpei.");
                Abort = true;
            }
            else if (Reg_Parole.text != Reg_Parole2.text)
            {
                AddError("Paroles nesakrīt");
                Abort = true;
            }

            if (!Abort)
            {
                SendToServer("1 " + Reg_Nikneims.text + " " + Reg_Parole.text);
                response = GetResponse();

                if (response == "REG_SUCCESS")
                {
                    Common_Vertibas.LoggedIn = true;
                    Helperi.Log("Veiksmigi piereģistrējies.");
                }
                else
                {
                    AddError("Speletaja vārds jau ir aizņemts");
                }
            }

        }
        else
        {
            AddError("Tu jau esi ielogojies iekšā, nevar reģistrēties");
        }

        if (Kludas != "") Helperi.Log(Kludas); //Todo SHow it


    }


    private void SendToServer(string msg)
    {
        Common_Vertibas.Writer.WriteLine(msg);
        Common_Vertibas.Writer.Flush();
    }
    private string GetResponse()
    {
        string Data = null;
        while (true)//emm..
        {
            Data = Common_Vertibas.Reader.ReadLine();

            if (Data != null)
            {
                return Data;
            }
        }
    }
    private void AddError(string error)
    {
        Kludas = Kludas + error + Environment.NewLine;
    }
    private void GetStats()
    {

    }
}
