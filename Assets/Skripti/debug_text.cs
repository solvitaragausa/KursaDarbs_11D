using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class debug_text : MonoBehaviour
{
    
    public TMPro.TMP_Text debug;
    // Start is called before the first frame update
    void Start()
    {
        
        Settings.debug_texts[0] = "debug true" + Environment.NewLine;
    }

    // Update is called once per frame
    void Update()
    {
        debug.text = "";
        if (Settings.debug)
        {
            foreach(string s in Settings.debug_texts)
            {
                if (s != null)
                {
                    debug.text += s + Environment.NewLine;
                }
            }
        }
        
    }
}
