﻿using UnityEngine;
using System.Collections;

public class Burning : Skill {
    public override void launch(){ 
        Debug.Log(haveLaunched);
        if (haveLaunched)
            return;
        
        Debug.Log("Burning");
        haveLaunched = true;
        player.rigidbody.velocity = player.rigidbody.velocity * 2.0f;
        // reflect*2
        // penetrate
    }
}
