using UnityEngine;
using System.Collections;

public class Backspin : Skill {
    private int backDirection = -1;
    public float rotatePower = 100;
    public override void launch(){ 
        Debug.Log(haveLaunched);
        if (haveLaunched)
            return;
        

        Debug.Log("Back Spin");
        haveLaunched = true;

        Vector2 speedDirection = new Vector2(player.rigidbody.velocity.x, player.rigidbody.velocity.z);
        Vector3 rotateDirection = new Vector3(speedDirection.y, 0, - speedDirection.x);

        player.rigidbody.AddTorque(rotateDirection.normalized * rotatePower * backDirection);
    }
}
