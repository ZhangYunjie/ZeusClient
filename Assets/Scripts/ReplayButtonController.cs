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
        Debug.Log("replay");
        Application.LoadLevel ("Map1");
    }
   
}
