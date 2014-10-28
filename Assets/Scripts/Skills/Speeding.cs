using UnityEngine;
using System.Collections;

public class Speeding : Skill {
    public override void launch(){ 
        Debug.Log(haveLaunched);
        if (haveLaunched)
            return;
        
        Debug.Log("Speed*2");
        haveLaunched = true;
        player.rigidbody.velocity = player.rigidbody.velocity * 2.0f;
    }
}
