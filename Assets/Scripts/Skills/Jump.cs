using UnityEngine;
using System.Collections;

public class Jump : Skill
{
    public float jumpPower = 300;

    public Jump()
    {
        canLaunchBefore = false;
        canLaunchDuringAction = true;
    }

    public override void launch(){ 
        Debug.Log(haveLaunched);
        if (haveLaunched)
            return;

        if (playerController.IsGrounded())
        { 
            Debug.Log("Jump");
            haveLaunched = true;
            player.rigidbody.AddForce(Vector3.up * jumpPower);
        }
    }
}
