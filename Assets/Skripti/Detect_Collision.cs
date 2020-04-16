using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detect_Collision : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {
        Common_Vertibas.debug_texts[3] = "Dzīvs";
    }

    void OnCollisionEnter(Collision collision)
    {
        //Spēlētājs saskārārās ar citu objektu
        Common_Vertibas.debug_texts[3] = "Miris :/";
    }
}
