using UnityEngine;
using System.Collections;

public class Skill{
    protected GameObject player;
    protected PlayerController playerController;
    public bool enabled
    {
        get;
        set;
    }
    public bool canLaunchBefore
    {
        get;
        set;
    }
    public bool canLaunchDuringAction
    {
        get;
        set;
    }
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
        enabled = true;
    }

    public virtual void launch(){
        if (haveLaunched)
            return;
        haveLaunched = true;
    }

    public bool getEnabled()
    {
        if (canLaunchBefore)
            return enabled;
       return enabled && !haveLaunched;
    }
}
