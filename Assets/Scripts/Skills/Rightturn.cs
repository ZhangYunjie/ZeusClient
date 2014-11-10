using UnityEngine;
using System.Collections;

public class Rightturn : Skill {
    public float turnPower = 300;
    public Rightturn()
    {
        canLaunchBefore = false;
        canLaunchDuringAction = true;
    }
    public override void launch(){ 
        Debug.Log(haveLaunched);
        if (haveLaunched)
            return;
        
        Debug.Log("Rightturn");
        haveLaunched = true;
        
        Vector2 speedDirection = new Vector2(player.rigidbody.velocity.x, player.rigidbody.velocity.z);
        
        player.rigidbody.AddTorque(player.rigidbody.velocity.normalized * turnPower * -1);
    }
}
