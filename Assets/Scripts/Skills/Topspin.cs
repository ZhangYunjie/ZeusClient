using UnityEngine;
using System.Collections;

public class Topspin : Skill {

    public float rotatePower = 100;
    public override void launch(){ 
        Debug.Log(haveLaunched);
        if (haveLaunched)
            return;
        
        if (playerController.IsGrounded())
        { 
            Debug.Log("Top Spin");
            haveLaunched = true;
            Debug.Log(player.rigidbody.velocity);
            Vector2 speedDirection = new Vector2(player.rigidbody.velocity.x, player.rigidbody.velocity.z);
            Vector3 rotateDirection = new Vector3(speedDirection.y / speedDirection.magnitude, 0, - speedDirection.x / speedDirection.magnitude);
            Debug.Log(rotateDirection);
            player.rigidbody.AddTorque(rotateDirection.normalized * rotatePower * 1);
        }
    }
}
