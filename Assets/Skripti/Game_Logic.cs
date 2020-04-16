using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Logic : MonoBehaviour
{
    // Start is called before the first frame update
    public TMPro.TMP_Text Punkti_text;
    float Laiks = 0;
    public GameObject ClickEfekts;
    public int ObjectNum = 0;
    public float MaxRobeza = 0;
    public float speed = 10.0f;
    public float FreeSpace;
    public int MaxPunkti = 0;
    int Punkti = 0;
    Vector3 target;
    public debug_text debug;

    // Update is called once per frame

    void Start()
    {
    }
    void Update()
    {
        
        Laiks += Time.deltaTime;
        float step = speed * Time.deltaTime;
        //  transform.position = Vector3.MoveTowards(transform.position, target, step);
        

        if(Input.GetMouseButtonDown(0)) //Kreisais klikšķis 
        {

            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = 0;
            ClickEfekts.transform.position = target;
            
            
            ClickEfekts.GetComponent<ParticleSystem>().Play();
            

            Vector2 direction = (target - (Vector3)transform.position).normalized;
            transform.up = direction;
           // transform.LookAt(worldPos);
        }

        target.z = -0.4f; //Lai kubs paliek aizmugurē, jo tā skaistāk
        transform.position = Vector3.MoveTowards(transform.position, target, step);
        Punkti = (int)((transform.position.y - 0.5f) / FreeSpace);
        if (Punkti > MaxPunkti) MaxPunkti = Punkti;
        Punkti_text.text = "Punkti: " + MaxPunkti;

        debug.texts[1] = "Laiks: " + (Laiks % 60).ToString("f1");
        debug.texts[2] = "Punkti Minūtē: " + (MaxPunkti/(Laiks % 60)).ToString("f2");

        if (Vector3.Distance(transform.position, target) < 0.5f)
        {
            if (ClickEfekts.GetComponent<ParticleSystem>().isPlaying) ClickEfekts.GetComponent<ParticleSystem>().Stop();
        }
        /*
        Vector2 direction = (worldPos - (Vector2)transform.position).normalized;
        transform.up = direction;
        transform.LookAt(worldPos);

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");




        // transform.position = new Vector2(transform.position.x + (h * step), transform.position.y + (v * step));
        transform.Translate(new Vector2(h*step,v*step));
        */
    }

}
