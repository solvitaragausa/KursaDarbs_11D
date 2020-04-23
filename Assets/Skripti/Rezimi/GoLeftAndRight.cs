using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoLeftAndRight : MonoBehaviour
{
    public int ObjectNum;
    public float VidPlatums;
    public float speed;
    public Vector3 target;


    void Update()
    {
        if (Common_Vertibas.AtlautsSpelet)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, step);
            if (Vector3.Distance(transform.position, target) < 0.001f)
            {
                target.x = -target.x;
            }
        }
    }

    public void SetVertibas(float VidusPlatums, float Skersla_speed, int SkerslaNumurs)
    {
        VidPlatums = VidusPlatums;
        speed = Skersla_speed;
        ObjectNum = SkerslaNumurs;

        if (Helperi.GetRandomBool()) target = new Vector3(Common_Vertibas.MaxSkerslaRobeza - (VidPlatums / 2), transform.position.y, 0); 
        else target = new Vector3(-(Common_Vertibas.MaxSkerslaRobeza - (VidPlatums / 2)), transform.position.y, 0);

    }

}
