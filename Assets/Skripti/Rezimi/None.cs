﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class None : MonoBehaviour
{

    public int ObjectNum;
    public float VidPlatums;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Common_Vertibas.Progress >= ObjectNum + Common_Vertibas.ObjektiAizmuguree) GameObject.Destroy(gameObject);
    }

    public void SetVertibas(float VidusPlatums, int SkerslaNumurs)
    {
        VidPlatums = VidusPlatums;
        ObjectNum = SkerslaNumurs;
    }
}
