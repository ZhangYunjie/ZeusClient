using UnityEngine;
using System.Collections.Generic;
using System;

public class SkillsController : MonoBehaviour {
    public Skill[] skills;
    private string currentSkill = "";
    protected GameObject player;
    protected PlayerController playerController;

    public enum SkillMode
    {
        kModeWait = 0,
        kModeReady,
        kModeLaunch,
        kModeAction,
        kModeEnd,
        NULL,
    };

    public enum SkillName
    {
        kJump = 0,
        kTopspin,
        NULL,
    };
    
    public SkillMode mode{
        get;
        set;
    }

    public bool enabled
    {
        get;
        set;
    }
    
    // Use this for initialization
    void Start () {
        mode = SkillMode.kModeWait;
        skills = new Skill[2]{ 
            new Jump(), 
            new Topspin() 
        };

        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
    }
    
    // Update is called once per frame
    void Update () {
        switch (mode)
        {
            case SkillMode.kModeWait:
                coolDownAll();
                enable(false);
                break;
            case SkillMode.kModeReady:
                enable(true);
                break;
            case SkillMode.kModeLaunch:
                enable(false);
                launch();
                break;
            case SkillMode.kModeAction:
                handleAction();
                break;
            case SkillMode.kModeEnd:
                handleEnd();
                break;
            default:
                break;
        }
    }

    public void exec(string skillName){
        if (!enabled)
            return;
        int skillNum = 0;
        switch (skillName)
        {
            case "jump":
                currentSkill = "jump";
                skillNum = (int)SkillName.kJump;
                skills[skillNum].launch();
                break;
            case "topspin":
                currentSkill = "topspin";
                skillNum = (int)SkillName.kTopspin;
                skills[skillNum].launch();
                break;
            default:
                break;
        }
        mode = SkillMode.kModeLaunch;
    }
    
    private void launch(){
        mode = SkillMode.kModeAction;
    }

    private void handleAction()
    {
        switch (currentSkill)
        {
            case "jump":
                if(playerController.IsGrounded()) endAction();
                break;
            case "topspin":
                endAction();
                break;
            default:
                break;
        }
    }

    private void endAction()
    {
        currentSkill = "";
        mode = SkillMode.kModeEnd;
    }

    private void handleEnd()
    {
        mode = SkillMode.kModeReady;
    }
    
    private void enable(bool will_enabled)
    {
        if(enabled != will_enabled) enabled = will_enabled;
    }
    
    private void coolDownAll()
    {
        foreach (Skill skill in skills)
        {
            skill.haveLaunched = false;
        }
    }
}
