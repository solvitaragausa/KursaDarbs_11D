using System.IO;
using System.Net.Sockets;

public static class Common_Vertibas
{
    public static string IpAdrese = "77.219.8.4";//"127.0.0.1";
    public static int Ports = 20782;
    public static TcpClient client = new TcpClient();
    public static StreamWriter Writer;
    public static StreamReader Reader;
    public static bool Connected = false;
    public static bool LoggedIn = false;
    public static int Timeouts = 5;
    public static int Progress = 0;
    public static string UserName = null;

    public static string AllStats;
    public static string LeaderBoards1;
    public static string LeaderBoards2;
    public static string LeaderBoards3;


    public static int IzspeletasSpeles = 0;
    public static int PunktiKopa = 0;
    public static float PunktiMinute = 0;
    public static float VidejaisPunktiVienaSpele = 0;
    public static float VidejasisLaiksVienaSpele = 0;
    public static float KopaIzspeletaisLaiks = 0;
    public static int AugstakaisPunktuSkaits = 0;
    public static float AugstakaisPunktiMinute = 0;


    public static float MaxSkerslaRobeza = 5; //Drošības pēc
    public static int FreeSpace = 6; //Default
    public static bool debug = false;
    public static bool AtlautsSpelet = true;
    public static bool Zaudejis = false;
    public static string[] debug_texts = new string[50];

    public static int ObjektiPrieksa = 10;
    public static int ObjektiAizmuguree = 5;
}
