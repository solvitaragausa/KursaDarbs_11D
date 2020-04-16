using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detect_Collision : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {
        Settings.debug_texts[3] = "Dzīvs";
    }

    void OnCollisionEnter(Collision collision)
    {
        Settings.debug_texts[3] = "Miris :/";
       // transform.parent = null;
       // Debug.Log("Herp Derp esmu miris");
    }
}
