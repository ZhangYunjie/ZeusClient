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
        kBackspin,
        kBurning,
        kSpeeding,
        kLeftturn,
        kRightturn,
        kReflectplus,
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
        skills = new Skill[8]{ 
            new Jump(), 
            new Topspin(),
            new Backspin(),
            new Burning(),
            new Speeding(),
            new Leftturn(),
            new Rightturn(),
            new Reflectplus()
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
        Debug.Log("exec:" + skillName);
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
            case "backspin":
                currentSkill = "backspin";
                skillNum = (int)SkillName.kBackspin;
                skills[skillNum].launch();
                break;
            case "burning":
                currentSkill = "burning";
                skillNum = (int)SkillName.kBurning;
                skills[skillNum].launch();
                break;
            case "speeding":
                currentSkill = "speeding";
                skillNum = (int)SkillName.kSpeeding;
                skills[skillNum].launch();
                break;
            case "leftturn":
                currentSkill = "leftturn";
                skillNum = (int)SkillName.kLeftturn;
                skills[skillNum].launch();
                break;
            case "rightturn":
                currentSkill = "rightturn";
                skillNum = (int)SkillName.kRightturn;
                skills[skillNum].launch();
                break;
            case "reflectplus":
                currentSkill = "reflectplus";
                skillNum = (int)SkillName.kReflectplus;
                skills[skillNum].launch();
                break;
            default:
                break;
        }
        mode = SkillMode.kModeLaunch;
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
            case "backspin":
                endAction();
                break;
            case "burning":
                endAction();
                break;
            case "speeding":
                endAction();
                break;
            case "leftturn":
                endAction();
                break;
            case "rightturn":
                endAction();
                break;
            case "reflectplus":
                endAction();
                break;
            default:
                break;
        }
    }
    
    private void launch(){
        mode = SkillMode.kModeAction;
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
