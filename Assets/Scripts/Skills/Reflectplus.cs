using UnityEngine;
using System.Collections;

public class Reflectplus : Skill {
    public Reflectplus()
    {
        canLaunchBefore = true;
        canLaunchDuringAction = false;
    }
    public void deactive(){
        if (!haveLaunched)
            return;
        Debug.Log("reflectplus deactive");
        haveLaunched = false;
        
        playerController.skillStatus.resetRefelctStatus();
    }
    public override void launch(){ 
        Debug.Log(haveLaunched);
        if (enabled && !haveLaunched)
            active();
        else if (enabled && haveLaunched)
            deactive();
    }
    public void active()
    {
        if (haveLaunched)
            return;
        
        Debug.Log("reflectplus");
        haveLaunched = true;
        
        playerController.skillStatus.gainRefelctStatus();
    }
}
