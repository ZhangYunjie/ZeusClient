using UnityEngine;
using System.Collections.Generic;
using System;

public class SkillsController : MonoBehaviour {
    public Skill[] skills;

    public enum SkillMode
    {
        kModeWait=0,
        kModeReady,
        kModeLaunch,
        kModeAction,
        kModeEnd,
        NULL,
    };

    public enum SkillName
    {
        kJump=0,
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
        skills = new Skill[1]{ new Jump() };
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
                break;
            default:
                break;
        }
    }

    public void exec(string skillName){
        if (!enabled)
            return;
        mode = SkillMode.kModeLaunch;
        switch (skillName)
        {
            case "jump":
                int skillNum = (int)Convert.ChangeType(SkillName.kJump, typeof(int));
                skills[skillNum].launch();
                break;
            default:
                break;
        }
    }
    
    private void launch(){
        mode = SkillMode.kModeAction;
    }

    private void handleAction()
    {
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
