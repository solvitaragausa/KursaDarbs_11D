using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoRight : MonoBehaviour
{
    public int ObjectNum = 0;
    public float MaxRobeza = 0.0f;
    public float speed = 10.0f;
    public Vector3 target;
    public float FreeSpace;

    void Start()
    {

        target = new Vector3(MaxRobeza, FreeSpace * ObjectNum, 0);
    }


    // Update is called once per frame
    void Update()
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
