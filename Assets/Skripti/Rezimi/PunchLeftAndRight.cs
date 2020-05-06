using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchLeftAndRight : MonoBehaviour
{
    public int ObjectNum = 0;
    public float speed;
    public Vector3 target;
    public float PunchExtra;
    public float VidPlatums;


    void Start()
    {
        transform.GetChild(0).transform.localScale = (new Vector3(0.5F, 1, 1));
    }

    void Update()
    {
        if (Common_Vertibas.Progress >= ObjectNum + Common_Vertibas.ObjektiAizmuguree) GameObject.Destroy(gameObject);

        if (Common_Vertibas.AtlautsSpelet)
        {
            float step = speed * Time.deltaTime*(PunchExtra/1.5f);
            transform.position = Vector3.MoveTowards(transform.position, target, step);
            if (Vector3.Distance(transform.position, target) < 0.001f)
            {
                target.x = -target.x;
            }
        }
    }

    public void SetVertibas(float VidusPlatums, float Skersla_speed, int SkerslaNumurs, float ExtraPunchForce)
    {
        VidPlatums = VidusPlatums;
        speed = Skersla_speed;
        ObjectNum = SkerslaNumurs;
        PunchExtra = ExtraPunchForce;


        if (Helperi.GetRandomBool()) target = new Vector3(Common_Vertibas.MaxSkerslaRobeza, transform.position.y, 0);
        else target = new Vector3(-Common_Vertibas.MaxSkerslaRobeza, transform.position.y, 0);
    }
}
