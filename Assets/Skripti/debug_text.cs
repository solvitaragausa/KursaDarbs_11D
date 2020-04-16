using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class debug_text : MonoBehaviour
{
    
    public TMPro.TMP_Text debug;

    void Start()
    {
        
        Common_Vertibas.debug_texts[0] = "debug true" + Environment.NewLine;
    }


    void Update()
    {
        debug.text = "";
        if (Common_Vertibas.debug)
        {
            foreach(string s in Common_Vertibas.debug_texts)
            {
                if (s != null)
                {
                    debug.text += s + Environment.NewLine;
                }
            }
        }
        
    }
}
