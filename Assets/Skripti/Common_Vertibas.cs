using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using UnityEngine;

public static class Common_Vertibas
{
    public static string IpAdrese = "127.0.0.1";
    public static int Ports = 20782;
    public static TcpClient client = new TcpClient();
    public static StreamWriter Writer;
    public static StreamReader Reader;
    public static bool Connected = false;
    public static bool LoggedIn = false;


    public static float MaxSkerslaRobeza = 5; //Drošības pēc
    public static int FreeSpace = 6; //Default
    public static bool debug = false;
    public static bool AtlautsSpelet = true;
    public static string[] debug_texts = new string[50];


    //TODO iebūvēt
    public static int ObjektiPirms = 10;
    public static int ObjektiPec = 20;
}
