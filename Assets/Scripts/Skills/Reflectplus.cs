using UnityEngine;
using System.Collections;

public class Reflectplus : Skill {
    public override void launch(){ 
        Debug.Log(haveLaunched);
        if (haveLaunched)
            return;
        
        Debug.Log("reflectplus");
        haveLaunched = true;

        playerController.skillStatus.gainRefelctStatus();
    }
}
