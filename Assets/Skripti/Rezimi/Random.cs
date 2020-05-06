using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public int ObjectNum;
    public float MaxRobeza;
    public float speed;
    public float RandomGajiens;
    public Vector3 target;
    float VidPlatums;

    void Update()
    {
        if (Common_Vertibas.Progress >= ObjectNum + Common_Vertibas.ObjektiAizmuguree) GameObject.Destroy(gameObject);

        if (Common_Vertibas.AtlautsSpelet)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, step);

            if (Vector3.Distance(transform.position, target) < 0.001f)
            {
                target = GetRandomTarget(target);
            }
        }
    }

    Vector3 GetRandomTarget(Vector3 CurrentTarget)
    {
        float MaxRandKreisi;
        float MaxRandLabi;

        float DistancePaLabi = (Common_Vertibas.MaxSkerslaRobeza - (VidPlatums / 2)) - transform.position.x;
        float DistancePaKreisi = (2*(Common_Vertibas.MaxSkerslaRobeza - (VidPlatums / 2))) - DistancePaLabi;

        if (DistancePaKreisi < RandomGajiens) MaxRandKreisi = DistancePaKreisi;
        else MaxRandKreisi = RandomGajiens;

        if (DistancePaLabi < RandomGajiens) MaxRandLabi = DistancePaLabi;
        else MaxRandLabi = RandomGajiens;


        CurrentTarget.x += Helperi.GetRandomFloat(-MaxRandKreisi, MaxRandLabi);
        return CurrentTarget;
    }

    public void SetVertibas(float VidusPlatums, float Skersla_speed, int SkerslaNumurs, float RandGajiens)
    {
        VidPlatums = VidusPlatums;
        speed = Skersla_speed;
        ObjectNum = SkerslaNumurs;
        RandomGajiens = RandGajiens;
        target = new Vector3(transform.position.x, transform.position.y, 0);
    }
}
