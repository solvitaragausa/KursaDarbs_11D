using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Logic : MonoBehaviour
{
    public GameObject InGame;
    public GameObject PostGame;
    public TMPro.TMP_Text PostGame_Punkti;
    public TMPro.TMP_Text PostGame_Laiks;
    public TMPro.TMP_Text PostGame_PointsPerMinute;



    public TMPro.TMP_Text Punkti_text;
    public float Laiks = 0;
    public GameObject ClickEfekts;
    public float speed = 10.0f;
    public int MaxPunkti = 0;
    int Punkti = 0;
    Vector3 target;


    void Update()
    {   
        
        if(Common_Vertibas.AtlautsSpelet)
        {
            Laiks += Time.deltaTime;
            float step = speed * Time.deltaTime;

            if (Input.GetMouseButtonDown(0)) //Kreisais klikšķis 
            {

                target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                target.z = 0;
                ClickEfekts.transform.position = target;


                ClickEfekts.GetComponent<ParticleSystem>().Play();


                Vector2 direction = (target - (Vector3)transform.position).normalized;
                transform.up = direction;

            }
            transform.position = Vector3.MoveTowards(transform.position, target, step);
            Punkti = (int)((transform.position.y - 0.5f) / (Common_Vertibas.FreeSpace + 1));
            if (Punkti > MaxPunkti)
            {
                MaxPunkti = Punkti;
                Common_Vertibas.Progress = Punkti;
            }
            Punkti_text.text = "Punkti: " + MaxPunkti;

            Common_Vertibas.debug_texts[1] = "Laiks: " + Laiks.ToString("f1");
            Common_Vertibas.debug_texts[2] = "Punkti Minūtē: " + (MaxPunkti / Laiks * 60).ToString("f2");

            if (Vector3.Distance(transform.position, target) < 0.5f)
            {
                if (ClickEfekts.GetComponent<ParticleSystem>().isPlaying) ClickEfekts.GetComponent<ParticleSystem>().Stop();
            }
        }
        if(Common_Vertibas.Zaudejis)
        {
            if (ClickEfekts.GetComponent<ParticleSystem>().isPlaying) ClickEfekts.GetComponent<ParticleSystem>().Stop();

            InGame.SetActive(false);

            PostGame_Punkti.text = "Iegūtie Punkti: "+MaxPunkti.ToString();
            PostGame_Laiks.text = "Laiks: "+Laiks.ToString("f2") + " sekundes";
            if (MaxPunkti >= 25)
                PostGame_PointsPerMinute.text = "Punkti Minūtē: "+(MaxPunkti / Laiks * 60).ToString("f2") ;
            else
                PostGame_PointsPerMinute.text = "Punkti Minūtē: Iegūstiet vismaz 25 punktus, lai precīzi uzzinātu punktus minūtē";
            PostGame.SetActive(true);
        }

    }
    

}
