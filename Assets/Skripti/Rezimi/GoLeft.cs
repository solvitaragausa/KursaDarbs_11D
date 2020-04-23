using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoLeft : MonoBehaviour
{
    public int ObjectNum;
    public float speed;
    public float VidPlatums;
    public Vector3 target;

    void Start()
    {
        //Pievieojam vizuālo identifikatoru
        Material sitiens = Resources.Load("Materials/GoLeft", typeof(Material)) as Material;
        transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = sitiens;
    }

    void Update()
    {
        if(Common_Vertibas.AtlautsSpelet)
        { 
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, step);
            if (Vector3.Distance(transform.position, target) < 0.001f)
            {
                target.x = -target.x;
                transform.position = target;
                target.x = -target.x;
            }
        }
    }

    public void SetVertibas(float VidusPlatums, float Skersla_speed, int SkerslaNumurs)
    {
        VidPlatums = VidusPlatums;
        speed = Skersla_speed;
        ObjectNum = SkerslaNumurs;
        target = new Vector3(-(Common_Vertibas.MaxSkerslaRobeza + (VidPlatums / 2)), transform.position.y, 0);
    }
}
