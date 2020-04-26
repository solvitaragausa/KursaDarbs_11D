using System;
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
        public static int Timeouts = 10000;
        public static bool TimeLogging = true;
        public static bool LoggingEnabled = true;
        public static int Connected = 0;

        public static string LogFile = "";
        public static string UsersFile = "Users.txt";
        public static string UsersData = "UsersData.txt";
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

                LogFile = string.Format(@"Logs\Serveris_Log_{0:yyyy-MM-dd_hh-mm-ss}.log", DateTime.Now);
            }

            Helperi.CreateNewThread(ServeraInitializaacija); //Jauns threads lai ui nenokārtos
        }

        private void ServeraInitializaacija()
        {

            ListBox_Add("Notiek servera veidošana...");
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
                ListBox_Add("Jauns savienojums ar id " + ConnectionId + " pieņemts [" + newClient.Client.RemoteEndPoint + "]");
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
            string UserName;
            int PointRecord;
            float PointsPerMinuteRecord;
            
            while (true)
            {
                try
                {
                    ClientData = Reader.ReadLine();
                }
                catch
                {
                    break;
                }
                

                if (ClientData != null)
                {
                    ListBox_Add(ClientData);
                    string[] Dati = ClientData.Split(' ');
                    string response = " ";


                    if (Dati[0]=="1") //Reģistrācija
                    {
                        //Dati[1] - Lietotājvārds
                        //Dati[2] - Parole
                        Users = File.ReadAllLines(UsersFile);
                        bool UserAlreadyExists = false;
                        foreach(string User in Users)
                        {
                            if (User == Dati[1])
                            {
                                UserAlreadyExists = true;
                                break;
                            }
                        }

                        if(UserAlreadyExists)
                        {
                            response = "BAD_USERNAME";
                            ListBox_Add("Kāds centās reģistrēties ar nikneimu " + Dati[2] + ", bet tāds jau eksistēja");
                        }
                        else
                        {
                            string NewUserConfig = Dati[1] + " " + Dati[2];
                            File.AppendAllText(UsersFile, NewUserConfig + Environment.NewLine);
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
                                    PointRecord = Int32.Parse(userstatsData[1]);
                                    PointsPerMinuteRecord = float.Parse(userstatsData[2]);
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

                    }
                    else if (Dati[0] == "4") //Spēles datu pieprasīšana
                    {

                    }
                    else if (Dati[0] == "4") //TODO:LeaderBoards
                    {

                    }
                }
            }
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
