using UnityEngine;
using System.Collections;

public class Burning : Skill {

    public float additionalForcePower = 200;
    public override void launch(){ 
        Debug.Log(haveLaunched);
        if (haveLaunched)
            return;
        
        Debug.Log("Burning");
        haveLaunched = true;
        player.rigidbody.AddForce(player.rigidbody.velocity.normalized * additionalForcePower);
    }
}
