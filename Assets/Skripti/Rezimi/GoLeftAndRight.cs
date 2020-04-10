using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoLeftAndRight : MonoBehaviour
{
    // Start is called before the first frame update
    public int ObjectNum = 0;
    public float MaxRobeza = 0;
    public float speed = 10.0f;
    public float FreeSpace = 0;
    public Vector3 target;

    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, step);
        if (Vector3.Distance(transform.position,target) <0.001f)
        {
            target.x = -target.x;
        }    
    }


    void OnEnable()
    {
        target = new Vector3(MaxRobeza, FreeSpace * ObjectNum, 0);
        this.enabled = true;
    }
}
