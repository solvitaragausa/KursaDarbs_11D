﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Speles_serveris
{
    public partial class Form1 : Form
    {
        public static TcpListener ServerListener;
        public static List<TcpClient> Clients = new List<TcpClient>();

        public readonly static int Ports = 20782;
        public static int Timeouts = 10;
        public static bool TimeLogging = true;
        public static bool LoggingEnabled = true;
        public static int Connected = 0;

        public static string LogFile = "";
        public static string UsersFile = "Users.txt";
        public static string UsersData = "UsersData.txt";


        //Tikai top 10 saglabajam
        public static string PunktiKopa_L = "AllPoints_L.txt";
        public static string AugstakaisPunktiMinute_L = "PointsPerMinute_L.txt";
        public static string AugstakaisPunktiSpele_L = "HighestPointsInOneGame_L.txt";
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //Kontroļu sakārtošana
            this.Controls.Add(listBox1);
            this.Controls.Add(textBox1);
            this.Controls.Add(panel1);

            if (LoggingEnabled)
            {
                if (!Directory.Exists("Logs"))
                {
                    Directory.CreateDirectory("Logs");
                }

                LogFile = string.Format(@"Logs\Serveris_{0:yyyy-MM-dd_hh-mm-ss}.log", DateTime.Now);
            }


            Helperi.CreateNewThread(ServeraInitializaacija); //Jauns threads lai ui nenokārtos
        }

        private void ServeraInitializaacija()
        {

            ListBox_Add("Notiek servera veidošana...");

           // string def = "EMPTY 0";

            //for (int i = 0; i < 20; i++) def = def + Environment.NewLine + "EMPTY 0";


            //Pārbaudām vai faili eksistē
            if (!File.Exists(UsersFile)) File.AppendAllText(UsersFile, "");
            if (!File.Exists(UsersData)) File.AppendAllText(UsersData, "");
            if (!File.Exists(PunktiKopa_L)) File.AppendAllText(PunktiKopa_L, "");
            if (!File.Exists(AugstakaisPunktiMinute_L)) File.AppendAllText(AugstakaisPunktiMinute_L, "");
            if (!File.Exists(AugstakaisPunktiSpele_L)) File.AppendAllText(AugstakaisPunktiSpele_L, "");




            Helperi.CreateNewThread(UI_Updater);
            ServerListener = new TcpListener(IPAddress.Any, Ports); //Klausās no jebkurienes
            ListBox_Add("Serveris izveidots. Startējam serveri uz porta " + Ports + "!");
            try
            {
                ServerListener.Start();
            }
            catch
            {
                ListBox_Add("Kritiska Kļūda: Ports " + Ports + " jau ir aizņemts! Varbūt jums šī programma jau ir atvērta?");
                Thread.Sleep(5000);
                Environment.Exit(0);
            }
            ListBox_Add("Serveris tika veiksmīgi startēts uz porta " + Ports + "!");
            Klausule();
        }
        public void Klausule()
        {
            ListBox_Add("Sākam klausīšanos!");
            int ConnectionId = 0;
            while (true)
            {
                TcpClient newClient;
                try
                {
                    newClient = ServerListener.AcceptTcpClient(); //Gaida kamēr kāds pievienosies
                }
                catch (Exception e)
                {

                    ListBox_Add(e.Message);
                    Thread.Sleep(5000);
                    continue;

                }
                Clients.Add(newClient);
                ConnectionId++;
                Connected++;
                // Izveidojam threadu tikai priekš šī clienta, lai varētu veikt komunikāciju ar to.
                ListBox_Add("Jauns savienojums pieņemts [" + newClient.Client.RemoteEndPoint + "]");
                Helperi.CreateNewThread(() => MainClientHandler(newClient, ConnectionId));
            }
        }
        public void MainClientHandler(TcpClient client, int id)
        {
            // sets two streams
            StreamWriter Writer = new StreamWriter(client.GetStream(), Encoding.ASCII);
            //Writer.AutoFlush = true; // Lai automatiski aizsutitu datus, nevajadzes izmantot Writer.Flush();
            StreamReader Reader = new StreamReader(client.GetStream(), Encoding.ASCII);



            string ClientData = null;
            string[] Users;
            string[] UsersStats;

            //Spēlētāja dati
            string UserName = null;
            int IzspeletasSpeles = 0;
            int PunktiKopa = 0;
            float KopaIzspeletaisLaiks = 0;
            int AugstakaisPunktuSkaits = 0;
            float AugstakaisPunktiMinute = 0;

            DateTime KomunikacijasBridis = DateTime.Now;
            while ((DateTime.Now - KomunikacijasBridis).Seconds < Timeouts)
            {

                try
                {
                    ClientData = null;
                    if (client.GetStream().DataAvailable) ClientData = Reader.ReadLine();
                }
                catch
                {
                    break;
                }


                if (ClientData != null)
                {
                    KomunikacijasBridis = DateTime.Now;
                    //ListBox_Add(ClientData);
                    string[] Dati = ClientData.Split(' ');
                    string response = " "; 



                    if (Dati[0] == "1") //Reģistrācija
                    {
                        //Dati[1] - Lietotājvārds
                        //Dati[2] - Parole
                        Users = File.ReadAllLines(UsersFile);
                        bool UserAlreadyExists = false;
                        foreach (string User in Users)
                        {
                            if (User.StartsWith(Dati[1]))
                            {
                                UserAlreadyExists = true;
                                break;
                            }
                        }

                        if (UserAlreadyExists)
                        {
                            response = "BAD_USERNAME";
                            ListBox_Add("Kāds centās reģistrēties ar nikneimu " + Dati[1] + ", bet tāds jau eksistēja");
                        }
                        else
                        {
                            string NewUserConfig = Dati[1] + " " + Dati[2];
                            File.AppendAllText(UsersFile, NewUserConfig + Environment.NewLine);
                            File.AppendAllText(UsersData, Dati[1] + " 0 0 0 0 0" + Environment.NewLine);
                            File.AppendAllText(PunktiKopa_L, Dati[1]+ " 0" + Environment.NewLine);
                            File.AppendAllText(AugstakaisPunktiMinute_L, Dati[1] + " 0" + Environment.NewLine);
                            File.AppendAllText(AugstakaisPunktiSpele_L, Dati[1] + " 0" + Environment.NewLine);
                            UserName = Dati[1];
                            response = "REG_SUCCESS";
                            ListBox_Add("Spēlētājs " + Dati[1] + " tikko reģistrējās");
                        }
                        Writer.WriteLine(response);
                        Writer.Flush();
                    }
                    else if (Dati[0] == "2") //Ielogošanās
                    {
                        //Dati[1] - Lietotājvārds
                        //Dati[2] - Parole
                        Users = File.ReadAllLines(UsersFile);
                        bool LoggedIn = false;
                        foreach (string User in Users)
                        {
                            if (User == Dati[1] + " " + Dati[2]) //Ja sakrīt lietotājvārds un parole
                            {
                                LoggedIn = true;
                                break;
                            }
                        }

                        if (LoggedIn)
                        {
                            ListBox_Add("Spēlētājs " + Dati[1] + " tikko iegāja savā profilā");

                            UserName = Dati[1];
                            UsersStats = File.ReadAllLines(UsersData);

                            foreach (string UserStats in UsersStats)
                            {
                                if (UserStats.StartsWith(UserName))
                                {
                                    string[] userstatsData = UserStats.Split(' ');

                                    IzspeletasSpeles = Int32.Parse(userstatsData[1]);
                                    PunktiKopa = Int32.Parse(userstatsData[2]);
                                    KopaIzspeletaisLaiks = float.Parse(userstatsData[3]);
                                    AugstakaisPunktuSkaits = Int32.Parse(userstatsData[4]);
                                    AugstakaisPunktiMinute = float.Parse(userstatsData[5]);


                                }
                            }
                            response = "LOGIN_SUCCESS";

                        }
                        else
                        {
                            ListBox_Add("Kāds centās pierakstīties, bet viņam bija nepareizs nikneims vai parole.");
                            response = "BAD_AUTH";
                        }

                        Writer.WriteLine(response);
                        Writer.Flush();
                    }
                    else if (Dati[0] == "3") //Pēcspēles datu nodošana
                    {
                        int SpelesPunkti = Int32.Parse(Dati[1]);
                        float SpelesLaiks = float.Parse(Dati[2]);
                        ListBox_Add("Saņēmām spēles datus no " + UserName+": "+SpelesPunkti+" punkti, "+SpelesLaiks+" sekundes");
                        //Dati[1] = Punkti
                        //Dati[2] = Laiks
                        
                        float PunktiMinute = float.Parse(((SpelesPunkti / SpelesLaiks) * 60).ToString("f2")); //Lai butu tikai 2 cipari aiz komata.

                        IzspeletasSpeles++;
                        PunktiKopa += SpelesPunkti;
                        KopaIzspeletaisLaiks += SpelesLaiks;
                        if (SpelesPunkti > AugstakaisPunktuSkaits )
                        {
                            ListBox_Add("Spēlētājam "+UserName + " jauns augstākais punktu skaits vienā spēlē");
                            AugstakaisPunktuSkaits = SpelesPunkti;
                        }
                        if (PunktiMinute > AugstakaisPunktiMinute && SpelesPunkti >= 25)
                        {
                            ListBox_Add("Spēlētājam " + UserName + " jauns augstākais punktu skaits vienā minūtē");
                            AugstakaisPunktiMinute = PunktiMinute;
                        }

                        //Nevajag responsu

                        #region Leaderboard saglabāšana
                        //Saglabājam leaderboardos ja nepieciešams
                        List<string> PunktiKopaFails = new List<string>(File.ReadAllLines(PunktiKopa_L));
                        List<string> AugstakaisPunktiMinuteFails = new List<string>(File.ReadAllLines(AugstakaisPunktiMinute_L));
                        List<string> AugstakaisPunktiSpeleFails = new List<string>(File.ReadAllLines(AugstakaisPunktiSpele_L));

                        #region Punkti Kopa Leaderboard
                        for (int i = 0; i < PunktiKopaFails.Count; i++)
                        {
                            string[] dati = PunktiKopaFails[i].Split(' ');
                            if (dati.Length == 2)
                            {
                                if (dati[0] == UserName)
                                {
                                    if (Int32.Parse(dati[1]) > PunktiKopa) break;
                                    else
                                    {
                                        PunktiKopaFails.RemoveAt(i);
                                        string temp = UserName + " " + PunktiKopa;
                                        PunktiKopaFails.Add(temp);
                                        PunktiKopaFails = PunktiKopaFails.OrderByDescending(s => Int32.Parse(s.Split(' ')[1]))
                                            .ThenBy(s => s).ToList();

                                    }
                                }
                           }
                        }
                        File.WriteAllLines(PunktiKopa_L, PunktiKopaFails);
                        #endregion

                        #region Augstakais Punkti Minute Leaderboard
                        for (int i = 0; i < AugstakaisPunktiMinuteFails.Count; i++)
                        {
                            string[] dati = AugstakaisPunktiMinuteFails[i].Split(' ');
                            if (dati.Length == 2)
                            {
                                if (dati[0] == UserName)
                                {
                                    if (float.Parse(dati[1]) > AugstakaisPunktiMinute) break;
                                    else
                                    {
                                        AugstakaisPunktiMinuteFails.RemoveAt(i);
                                        string temp = UserName + " " + AugstakaisPunktiMinute;
                                        AugstakaisPunktiMinuteFails.Add(temp);

                                        AugstakaisPunktiMinuteFails = AugstakaisPunktiMinuteFails.OrderByDescending(s => float.Parse(s.Split(' ')[1]))
                                            .ThenBy(s => s).ToList();

                                    }
                                }
                            }
                        }
                        File.WriteAllLines(AugstakaisPunktiMinute_L, AugstakaisPunktiMinuteFails);
                        #endregion

                        #region Augstakais Punkti Spele Leaderboard
                        for (int i = 0; i < AugstakaisPunktiSpeleFails.Count; i++)
                        {
                            string[] dati = AugstakaisPunktiSpeleFails[i].Split(' ');
                            if (dati.Length == 2)
                            {
                                if (dati[0] == UserName)
                                {
                                    if (Int32.Parse(dati[1]) > AugstakaisPunktuSkaits) break;
                                    else
                                    {
                                        AugstakaisPunktiSpeleFails.RemoveAt(i);
                                        string temp = UserName + " " + AugstakaisPunktuSkaits;
                                        AugstakaisPunktiSpeleFails.Add(temp);
                                        AugstakaisPunktiSpeleFails = AugstakaisPunktiSpeleFails.OrderByDescending(s => Int32.Parse(s.Split(' ')[1]))
                                         .ThenBy(s => s).ToList();

                                    }
                                }
                            }
                        }
                        File.WriteAllLines(AugstakaisPunktiSpele_L, AugstakaisPunktiSpeleFails);
                        #endregion

                        #endregion

                    }
                    else if (Dati[0] == "4") //Spēles datu pieprasīšana
                    {
                        ListBox_Add("Sūtam "+UserName + " viņa statistiku");
                        response = IzspeletasSpeles.ToString() + " " + PunktiKopa.ToString() + " " + KopaIzspeletaisLaiks.ToString() + " " + AugstakaisPunktuSkaits.ToString() + " " + AugstakaisPunktiMinute.ToString();
                        //ListBox_Add(response);
                        Writer.WriteLine(response);
                        Writer.Flush();
                    }
                    else if (Dati[0] == "5") //LeaderBoards
                    {
                        ListBox_Add("Sūtam " + UserName + " informāciju par top spēlētājiem");
                        List<string> PunktiKopaFails = new List<string>(File.ReadAllLines(PunktiKopa_L));
                        List<string> AugstakaisPunktiMinuteFails = new List<string>(File.ReadAllLines(AugstakaisPunktiMinute_L));
                        List<string> AugstakaisPunktiSpeleFails = new List<string>(File.ReadAllLines(AugstakaisPunktiSpele_L));


                        //Vajadzīgi 3 responsi, katrs responss atdalīts ar jaunu līniju
                        response = "";
                        for (int i = 0; i < PunktiKopaFails.Count; i++)
                            response = response + " " + PunktiKopaFails[i];
                        response = response + "[NEXT]";
                        for (int i = 0; i < AugstakaisPunktiMinuteFails.Count; i++)
                            response = response + " " + AugstakaisPunktiMinuteFails[i];
                        response = response + "[NEXT]";
                        for (int i = 0; i < AugstakaisPunktiSpeleFails.Count; i++)
                            response = response +" "+ AugstakaisPunktiSpeleFails[i];
                        Writer.WriteLine(response);
                        Writer.Flush();

                        ListBox_Add("Veiksmīgi aizsūtīts!");

                    }
                    else if (Dati[0] == "6") //Izrakstīšanās
                    {
                        break;
                    }
                }

                Thread.Sleep(100);
            }



            if (UserName != null)
            {
                ListBox_Add(UserName + " tikko atvienojās no servera, saglabājam viņa spēles datus.");
                //Jāsaglabā spēlētāju datus, kad viņs beidz spēlēt
                UsersStats = File.ReadAllLines(UsersData);
                for (int i = 0; i < UsersStats.Length; i++)
                {
                    if (UsersStats[i].StartsWith(UserName))
                    {
                        UsersStats[i] = UserName + " " + IzspeletasSpeles.ToString() + " " + PunktiKopa.ToString() + " " + KopaIzspeletaisLaiks.ToString() + " " + AugstakaisPunktuSkaits.ToString() + " " + AugstakaisPunktiMinute.ToString();
                        File.WriteAllLines(UsersData, UsersStats);
                        break;
                    }
                }

            }
            else ListBox_Add("Kāds atvienojās no servera");
            client.Close();
            Connected--;



        }

        private void UI_Updater()
        {
            DateTime Starts = DateTime.Now;
            //Vienu reizi tik vajag
            this.Invoke(new MethodInvoker(delegate ()
            {
                this.Text = "Serveris - Ports " + Ports;
            }));
            while (true)
            {
                TimeSpan UPTIME = DateTime.Now - Starts;
                this.Invoke(new MethodInvoker(delegate ()
                {
                    this.UI_Clients_Connected.Text = "Savienojušies: " + Connected;
                    this.UI_Uptime.Text = "Servera Laiks: " + Helperi.ServeraLaiks(UPTIME);
                }));
                Thread.Sleep(1000);
            }
        }



        public void ListBox_Add(string text)
        {
            string a = "";
            if (TimeLogging)
            {
                a = "[" + DateTime.Now.ToString() + "] ";
            }

            this.Invoke(new MethodInvoker(delegate () //Atrodas citā threadā
            {
                string b = a + text;
                listBox1.Items.Add(b);
                if (LoggingEnabled) File.AppendAllText(LogFile, b + Environment.NewLine);

            }));
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ServerListener.Stop();
            Environment.Exit(0); //Iznīcina visus threadus, savādākā gadījumā programma aizvērsies, bet threadi vienalga strādās.
        }
    }



    

}
