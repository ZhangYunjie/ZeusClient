using UnityEngine;
using System.Collections;

public class Backspin : Skill {
    private int backDirection = -1;
    public float rotatePower = 100;
    public Backspin()
    {
        canLaunchBefore = false;
        canLaunchDuringAction = true;
    }
    public override void launch(){ 
        Debug.Log(haveLaunched);
        if (haveLaunched)
            return;

        Debug.Log("Back Spin");
        haveLaunched = true;

        player.rigidbody.AddTorque(calcRotateDirection() * rotatePower * backDirection);
    }

    Vector3 calcRotateDirection(){
        Vector3 rotateDirection;
        if (playerController.ground_collid_point == null)
        {
            Vector2 speedDirection = new Vector2(player.rigidbody.velocity.x, player.rigidbody.velocity.z);
            rotateDirection = new Vector3(speedDirection.y, 0, - speedDirection.x);
        } else
        {
            Vector3 ground_plane = playerController.ground_collid_point - player.transform.position;
            rotateDirection =  Vector3.Cross(player.rigidbody.velocity, ground_plane);
        }
        return rotateDirection.normalized;
    }
}
