using UnityEngine;
using System.Collections;

public class Jump : Skill
{
    public float jumpPower = 200;
    public override void launch(){ 
        Debug.Log(haveLaunched);
        if (haveLaunched)
            return;

        if (playerController.IsGrounded())
        { 
            Debug.Log("launch");
            haveLaunched = true;
            player.rigidbody.AddForce(Vector3.up * jumpPower);
        }
    }
}
