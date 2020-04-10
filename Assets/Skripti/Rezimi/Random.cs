using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random : MonoBehaviour
{
    // Start is called before the first frame update
    public int ObjectNum = 0;
    public float MaxRobeza = 0;
    public float speed = 10.0f;
    public float FreeSpace = 0;
    public float RandomGajiens = 1.0f;
    public Vector3 target;

    void Start()
    {
        target.y = FreeSpace * ObjectNum;
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, step);

        if (Vector3.Distance(transform.position, target) < 0.001f)
        {
            target = GetRandomTarget(target);
        }
    }

    Vector3 GetRandomTarget(Vector3 CurrentTarget)
    {
        float MaxRandKreisi;
        float MaxRandLabi;

        float DistancePaLabi = MaxRobeza - transform.position.x;
        float DistancePaKreisi = (2*MaxRobeza) - DistancePaLabi;
         //Debug.Log("DKreisi: " + DistancePaKreisi);
         //Debug.Log("DLabi: " + DistancePaLabi);

        if (DistancePaKreisi < RandomGajiens) MaxRandKreisi = DistancePaKreisi;
        else MaxRandKreisi = RandomGajiens;

        if (DistancePaLabi < RandomGajiens) MaxRandLabi = DistancePaLabi;
        else MaxRandLabi = RandomGajiens;


        CurrentTarget.x += Helperi.GetRandomFloat(-MaxRandKreisi, MaxRandLabi);
        return CurrentTarget;
    }
}
