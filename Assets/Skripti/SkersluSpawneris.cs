using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkersluSpawneris : MonoBehaviour
{

    public GameObject PuseSkersla;
    public Transform VisiSkersli;
    public int SkerslaNumurs = 0;
    public int FreeSpace = 1;
    public float MaxRobeza = 0;
    
    void Start()
    {
        //temp 
        for(int i = 0; i<7;++i)
        IzveidotSkersli(4, (Helperi.SkerslaRezims) i,5);

        FreeSpace++;
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log(Helperi.GetRandomFloat(5.6f,8.2f));
    }
    
    void IzveidotSkersli(float VidusPlatums, Helperi.SkerslaRezims Rezims, float speed)
    {
        Helperi.Log("Notiek skersla veidošana!");
        SkerslaNumurs++;

        float SkerslaRobeza =(float)((-VidusPlatums / 2) + MaxRobeza);


        GameObject Skerslis = new GameObject();
        Skerslis.SetActive(false);
        Skerslis.transform.SetParent(VisiSkersli);       
        Skerslis.name = SkerslaNumurs.ToString();

        

        float ObstacleScaleX = PuseSkersla.transform.localScale.x;


        Vector3 KreisaPozicija = new Vector3((-ObstacleScaleX/2)-(VidusPlatums/2), SkerslaNumurs * FreeSpace , 0);
        Vector3 LabaPozicija = new Vector3((ObstacleScaleX / 2) + (VidusPlatums / 2), SkerslaNumurs * FreeSpace, 0);
        

        //TODO spawnot skerslus pec taa reziima 
        Instantiate(PuseSkersla,KreisaPozicija,Skerslis.transform.rotation, Skerslis.transform);
        Instantiate(PuseSkersla, LabaPozicija, Skerslis.transform.rotation, Skerslis.transform);

        Skerslis.transform.position = new Vector3(Helperi.GetRandomFloat(-SkerslaRobeza, SkerslaRobeza), SkerslaNumurs * FreeSpace, 0);

        //Debug.Log(Skerslis.transform.position);


        //TODO Uzlabot sūda sistēmu
        switch (Rezims)
        {
           
            case Helperi.SkerslaRezims.GoLeftAndRight:
                GoLeftAndRight Skripts = Skerslis.AddComponent<GoLeftAndRight>();
                Skripts.enabled = false;
                Skripts.MaxRobeza = SkerslaRobeza;  //Lai daiet da ekrana malaam un talaak nekru
                Skripts.ObjectNum = SkerslaNumurs;
                Skripts.speed = speed;
                Skripts.FreeSpace = FreeSpace;
                Skripts.enabled = true;
                break;

            case Helperi.SkerslaRezims.GoLeft:
                GoLeft Skripts1 = Skerslis.AddComponent<GoLeft>();
                Skripts1.MaxRobeza = SkerslaRobeza; 
                Skripts1.ObjectNum = SkerslaNumurs;
                Skripts1.speed = speed;
                Skripts1.FreeSpace = FreeSpace;
                break;
            case Helperi.SkerslaRezims.GoRight:
                GoRight Skripts2 =Skerslis.AddComponent<GoRight>();
                Skripts2.MaxRobeza = SkerslaRobeza;
                Skripts2.ObjectNum = SkerslaNumurs;
                Skripts2.speed = speed;
                Skripts2.FreeSpace = FreeSpace;
                break;
            case Helperi.SkerslaRezims.PunchLeftAndRight:
                PunchLeftAndRight Skripts3 = Skerslis.AddComponent<PunchLeftAndRight>();
                break;

                //TODO Prieks puncheriem izveidot labaaku sisteemu.
            case Helperi.SkerslaRezims.PunchLeft:
                PunchLeft Skripts4 = Skerslis.AddComponent<PunchLeft>();
                Skripts4.MaxRobeza = MaxRobeza+(VidusPlatums/2);
                Skripts4.ObjectNum = SkerslaNumurs;
                Skripts4.speed = speed;
                Skripts4.FreeSpace = FreeSpace;
                break;
            case Helperi.SkerslaRezims.PunchRight:
                PunchRight Skripts5 = Skerslis.AddComponent<PunchRight>();
                Skripts5.MaxRobeza = MaxRobeza + (VidusPlatums / 2);
                Skripts5.ObjectNum = SkerslaNumurs;
                Skripts5.speed = speed;
                Skripts5.FreeSpace = FreeSpace;
                break;
            case Helperi.SkerslaRezims.Random:
                Random Skripts6 = Skerslis.AddComponent<Random>();
                Skripts6.MaxRobeza = SkerslaRobeza;
                Skripts6.ObjectNum = SkerslaNumurs;
                Skripts6.speed = speed;
                Skripts6.FreeSpace = FreeSpace;
                break;
            case Helperi.SkerslaRezims.None:
                None Skripts7 = Skerslis.AddComponent<None>();
                break;
            default:
                Helperi.Log("Nezināms režīms piešķirts šķērslim! Liekam nekādu režīmu.");
                Skerslis.AddComponent<None>();
                break;

        }

        Skerslis.SetActive(true);
    }

     

}
