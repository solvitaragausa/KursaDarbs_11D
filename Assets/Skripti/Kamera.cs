using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamera : MonoBehaviour
{
    public GameObject Speletajs;
    public Vector3 offset = new Vector3(0, 5, -10);
    void Start()
    {
        SkersluSpawneris spawneris = FindObjectOfType<SkersluSpawneris>();
        Common_Vertibas.MaxSkerslaRobeza = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0)).x;
        Helperi.Log(Common_Vertibas.MaxSkerslaRobeza.ToString());
        spawneris.enabled = true;
    }

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
