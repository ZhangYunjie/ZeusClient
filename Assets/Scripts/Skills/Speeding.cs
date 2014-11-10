using UnityEngine;
using System.Collections;

public class Speeding : Skill {
    public Speeding()
    {
        canLaunchBefore = true;
        canLaunchDuringAction = false;
    }

    public void active()
    {
        if (haveLaunched)
            return;
        
        Debug.Log("Speed*2");
        haveLaunched = true;

        playerController.skillStatus.gainSpeedingStatus();
    }

    public void deactive(){
        if (!haveLaunched)
            return;
        Debug.Log("Speeding deactive");
        haveLaunched = false;
        
        playerController.skillStatus.resetSpeedingStatus();
    }

    public override void launch(){ 
        Debug.Log(haveLaunched);
        if (enabled && !haveLaunched)
            active();
        else if (enabled && haveLaunched)
            deactive();
    }
}
