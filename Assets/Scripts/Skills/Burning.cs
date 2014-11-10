using UnityEngine;
using System.Collections;

public class Burning : Skill {

    public float burningForcePower = 200;
    public Burning()
    {
        canLaunchBefore = false;
        canLaunchDuringAction = true;
    }
    public override void launch(){ 
        Debug.Log(haveLaunched);
        if (haveLaunched)
            return;
        
        Debug.Log("Burning");
        haveLaunched = true;
        player.rigidbody.AddForce(player.rigidbody.velocity.normalized * burningForcePower);
        // reflect*2
        // penetrate
    }
}
