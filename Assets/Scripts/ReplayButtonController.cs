﻿using UnityEngine;
using System.Collections;

public class ReplayButtonController : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
    
    }
    
    // Update is called once per frame
    void Update()
    {
    
    }
   
    void OnClick()
    {
        if (FingerGestures.Instance)
        {
            Destroy(FingerGestures.Instance);
        }
        Application.LoadLevel ("Map1");
    }
   
}
