using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchRight : MonoBehaviour
{
    public int ObjectNum = 0;
    public float speed;
    public Vector3 target;
    public float PunchExtra;
    public float VidPlatums;

    //Ātrs sitiens pa kreisi un tad lēnam atpakaļ
    bool a = false;
    float step;

    void Start()
    {
        //Pievieojam vizuālo identifikatoru
        Material sitiens = Resources.Load("Materials/PunchRight", typeof(Material)) as Material;
        transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = sitiens;
    }
    void Update()
    {
        if (Common_Vertibas.Progress >= ObjectNum + Common_Vertibas.ObjektiAizmuguree) GameObject.Destroy(gameObject);

        if (Common_Vertibas.AtlautsSpelet)
        {
            if (!a)
            {
                step = PunchExtra * speed * Time.deltaTime;
            }
            else
            {
                step = speed * Time.deltaTime;
            }
            transform.position = Vector3.MoveTowards(transform.position, target, step);

            if (Vector3.Distance(transform.position, target) < 0.001f)
            {
                if (!a) target.x = -(Common_Vertibas.MaxSkerslaRobeza - (VidPlatums / 2));
                else target.x = Common_Vertibas.MaxSkerslaRobeza + (VidPlatums / 2);

                a = !a;
            }
        }
    }

    public void SetVertibas(float VidusPlatums, float Skersla_speed, int SkerslaNumurs, float ExtraPunchForce)
    {
        VidPlatums = VidusPlatums;
        speed = Skersla_speed;
        ObjectNum = SkerslaNumurs;
        PunchExtra = ExtraPunchForce;
        target = new Vector3(Common_Vertibas.MaxSkerslaRobeza - (VidPlatums / 2), transform.position.y, 0);
    }
}
