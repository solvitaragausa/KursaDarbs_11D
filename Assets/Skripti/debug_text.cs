using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class debug_text : MonoBehaviour
{
    public string[] texts = new string[50];
    public TMPro.TMP_Text debug;
    // Start is called before the first frame update
    void Start()
    {
        texts[0] = "debug true" + Environment.NewLine;
    }

    // Update is called once per frame
    void Update()
    {
        debug.text = "";
        if (Settings.debug)
        {
            foreach(string s in texts)
            {
                if (s != null)
                {
                    debug.text += s + Environment.NewLine;
                }
            }
        }
        
    }
}
