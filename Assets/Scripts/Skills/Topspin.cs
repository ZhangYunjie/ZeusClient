using UnityEngine;
using System.Collections;

public class Topspin : Skill {
    private int topDirection = 1;
    public float rotatePower = 100;
    public Topspin()
    {
        canLaunchBefore = false;
        canLaunchDuringAction = true;
    }
    public override void launch(){ 
        Debug.Log(haveLaunched);
        if (haveLaunched)
            return;
        

        Debug.Log("Top Spin");
        haveLaunched = true;

        player.rigidbody.AddTorque(calcRotationDirection() * rotatePower * topDirection);
    }

    Vector3 calcRotationDirection()
    {
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
