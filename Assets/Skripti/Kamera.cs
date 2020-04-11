using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamera : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Speletajs;
    public Vector3 offset = new Vector3(0, 5, -10);
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

    float oldPos = 0;
    void LateUpdate()
    {
        if (oldPos <= Speletajs.transform.position.y)
        {
            transform.position = new Vector3(0, Speletajs.transform.position.y, 0) + offset;
            oldPos = Speletajs.transform.position.y;
        }
    }

}
