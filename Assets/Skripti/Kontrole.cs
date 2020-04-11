using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kontrole : MonoBehaviour
{
    // Start is called before the first frame update
    public int ObjectNum = 0;
    public float MaxRobeza = 0;
    public float speed = 10.0f;
    Vector2 target;


    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        //  transform.position = Vector3.MoveTowards(transform.position, target, step);
        

        if(Input.GetMouseButtonDown(0))
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //target.z = 0;

            Vector2 direction = (target - (Vector2)transform.position).normalized;
            transform.up = direction;
           // transform.LookAt(worldPos);
        }

        transform.position = Vector2.MoveTowards(transform.position, target, step);


       // if (Vector3.Distance(transform.position, target) < 0.001f)
       // {
      //      target.x = -target.x;
       // }
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
