using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamera : MonoBehaviour
{
    // Start is called before the first frame update
    Camera cam;
    public float SkerslaMaxRobeza = 0;
    void Start()
    {
        cam = gameObject.GetComponent<Camera>();
        SkerslaMaxRobeza = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)).x;
       
        SkersluSpawneris spawneris = FindObjectOfType<SkersluSpawneris>();
        spawneris.MaxRobeza = SkerslaMaxRobeza;
        spawneris.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
