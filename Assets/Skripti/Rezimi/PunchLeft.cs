using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchLeft : MonoBehaviour
{
    public int ObjectNum = 0;
    public float MaxRobeza = 0.0f;
    public float speed = 10.0f;
    public Vector3 target;
    public float FreeSpace;
    public float PunchExtra = 15;
    void Start()
    {
      
       // GameObject.Destroy(transform.GetChild(0).gameObject);
        target = new Vector3(-MaxRobeza, FreeSpace * ObjectNum, 0);
    }


    //Ātrs sitiens pa kreisi un tad lēnam atpakaļ
    bool a = false;
    float step;
    void Update()
    {
        if (!a)
        {
            step = PunchExtra * speed * Time.deltaTime;
        }
        else { 
            step = speed * Time.deltaTime; 
        }
        transform.position = Vector3.MoveTowards(transform.position, target, step);
        if (Vector3.Distance(transform.position, target) < 0.001f)
        {
            target.x = -target.x;
            a = !a;
        }
    }
}
