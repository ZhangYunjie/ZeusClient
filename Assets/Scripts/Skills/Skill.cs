using UnityEngine;
using System.Collections;

public class Skill{
    protected GameObject player;
    protected PlayerController playerController;
    public bool haveLaunched
    {
        get;
        set;
    }

    public Skill()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        haveLaunched = false;
    }

    public virtual void launch(){
        if (haveLaunched)
            return;
        haveLaunched = true;
    }
}
