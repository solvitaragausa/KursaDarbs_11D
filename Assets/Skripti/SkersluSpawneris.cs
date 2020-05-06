using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkersluSpawneris : MonoBehaviour
{
    public GameObject PuseSkersla;
    public Transform VisiSkersli;
    int SkerslaNumurs = 0;
    public float MaxRobeza = Common_Vertibas.MaxSkerslaRobeza;
    int FreeSpace = Common_Vertibas.FreeSpace + 1;
    float PunchExtra = 15.0f;
    float RandomGajiens = 1.0f;

    void OnEnable()
    {
        MaxRobeza = Common_Vertibas.MaxSkerslaRobeza; 
    }

    void Start()
    {
       
    }


    void Update()
    {
        if(Common_Vertibas.Progress + Common_Vertibas.ObjektiPrieksa > SkerslaNumurs) IzveidotSkersli(4, (Helperi.SkerslaRezims)(int)Helperi.GetRandomFloat(-1, 8), 5);
        Common_Vertibas.debug_texts[4] = "Pašreizējais šķēršļu daudzums: " + VisiSkersli.childCount;
    }
    
    void IzveidotSkersli(float VidusPlatums, Helperi.SkerslaRezims Rezims, float speed)
    {
        Helperi.Log("Notiek skersla veidošana!");
        SkerslaNumurs++;

        float SkerslaRobeza =(-VidusPlatums / 2) + MaxRobeza;

        GameObject Skerslis = new GameObject();
        Skerslis.SetActive(false);
        Skerslis.transform.SetParent(VisiSkersli);       
        Skerslis.name = SkerslaNumurs.ToString();

        float ObstacleScaleX = PuseSkersla.transform.localScale.x;

        Vector3 KreisaPozicija = new Vector3((-ObstacleScaleX/2)-(VidusPlatums/2), 0, 0);
        Vector3 LabaPozicija = new Vector3((ObstacleScaleX / 2) + (VidusPlatums / 2),0 ,0);
        Skerslis.transform.position = new Vector3(Helperi.GetRandomFloat(-SkerslaRobeza, SkerslaRobeza), SkerslaNumurs * FreeSpace, 0);
        Vector3[] pozicijas = { KreisaPozicija, LabaPozicija };


        //TODO Uzlabot sūda sistēmu
        switch (Rezims)
        {
           
            case Helperi.SkerslaRezims.GoLeftAndRight:
                SpawnotSkersli(Helperi.SkersluVeids.Abi, Skerslis, pozicijas);
                GoLeftAndRight Skripts = Skerslis.AddComponent<GoLeftAndRight>();
                Skripts.SetVertibas(VidusPlatums, speed, SkerslaNumurs);
                break;
            case Helperi.SkerslaRezims.GoLeft:
                SpawnotSkersli(Helperi.SkersluVeids.Abi, Skerslis, pozicijas);
                GoLeft Skripts1 = Skerslis.AddComponent<GoLeft>();
                Skripts1.SetVertibas(VidusPlatums, speed, SkerslaNumurs);
                break;
            case Helperi.SkerslaRezims.GoRight:
                SpawnotSkersli(Helperi.SkersluVeids.Abi, Skerslis, pozicijas);
                GoRight Skripts2 = Skerslis.AddComponent<GoRight>();
                Skripts2.SetVertibas(VidusPlatums, speed, SkerslaNumurs);
                break;
            case Helperi.SkerslaRezims.PunchLeftAndRight:
                Vector3[] PosPunchLeftAndRight = { new Vector3(0, 0, 0), new Vector3(0, 0, 0) };
                SpawnotSkersli(Helperi.SkersluVeids.Kreisais, Skerslis, PosPunchLeftAndRight);
                PunchLeftAndRight Skripts3 = Skerslis.AddComponent<PunchLeftAndRight>();
                Skripts3.SetVertibas(VidusPlatums, speed, SkerslaNumurs, PunchExtra);
                break;
            case Helperi.SkerslaRezims.PunchLeft:
                SpawnotSkersli(Helperi.SkersluVeids.Labais, Skerslis, pozicijas);
                PunchLeft Skripts4 = Skerslis.AddComponent<PunchLeft>();
                Skripts4.SetVertibas(VidusPlatums, speed, SkerslaNumurs,PunchExtra);
                break;
            case Helperi.SkerslaRezims.PunchRight:
                SpawnotSkersli(Helperi.SkersluVeids.Kreisais, Skerslis, pozicijas);
                PunchRight Skripts5 = Skerslis.AddComponent<PunchRight>();
                Skripts5.SetVertibas(VidusPlatums, speed, SkerslaNumurs, PunchExtra);
                break;
            case Helperi.SkerslaRezims.RandomMovement:
                SpawnotSkersli(Helperi.SkersluVeids.Abi, Skerslis, pozicijas);
                RandomMovement Skripts6 = Skerslis.AddComponent<RandomMovement>();
                Skripts6.SetVertibas(VidusPlatums, speed/1.5f, SkerslaNumurs,RandomGajiens);
                break;
            case Helperi.SkerslaRezims.None:
                SpawnotSkersli(Helperi.SkersluVeids.Abi, Skerslis, pozicijas);
                None Skripts7 = Skerslis.AddComponent<None>();
                Skripts7.SetVertibas(VidusPlatums, SkerslaNumurs);
                break;
            case Helperi.SkerslaRezims.Empty:
                // Šķēršļa nav 
            break;

            default:

                SpawnotSkersli(Helperi.SkersluVeids.Abi, Skerslis, pozicijas);
                Helperi.Log("Nezināms režīms piešķirts šķērslim! Liekam nekādu režīmu.");
                None Skripts7_1 = Skerslis.AddComponent<None>();
                Skripts7_1.SetVertibas(VidusPlatums, SkerslaNumurs);
                break;

        }

        
        Skerslis.SetActive(true);
    }

    void SpawnotSkersli(Helperi.SkersluVeids veids, GameObject Skerslis, Vector3[] pozicija)
    {
        if (veids == Helperi.SkersluVeids.Kreisais)
        {
            Instantiate(PuseSkersla, pozicija[0]+Skerslis.transform.position, Skerslis.transform.rotation, Skerslis.transform);
        }
        else if (veids == Helperi.SkersluVeids.Labais)
        {
            Instantiate(PuseSkersla, pozicija[1] + Skerslis.transform.position, Skerslis.transform.rotation, Skerslis.transform);
        }
        else //abi
        {
            Instantiate(PuseSkersla, pozicija[0] + Skerslis.transform.position, Skerslis.transform.rotation, Skerslis.transform);
            Instantiate(PuseSkersla, pozicija[1] + Skerslis.transform.position, Skerslis.transform.rotation, Skerslis.transform);
        }
    }
}
