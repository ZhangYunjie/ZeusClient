using UnityEngine;
using System.Collections;

public class SkillButtonController : MonoBehaviour {
    protected GameObject player;
    protected SkillsController skillsController;
    protected int skillNum;
    protected string skillName;
    
    // Use this for initialization
    protected void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        skillsController = GameObject.FindGameObjectWithTag("Skills").GetComponent<SkillsController>();
    }
    
    // Update is called once per frame
    protected void Update () {
        Skill currentSkill = skillsController.skills [skillNum];
        bool will_enabled = currentSkill.getEnabled();
        UIButton button = GetComponent<UIButton>();

        if (button.isEnabled != will_enabled)
        {
            button.isEnabled = will_enabled;
        }

        if (currentSkill.canLaunchBefore && currentSkill.haveLaunched)
            button.SendMessage("OnHover", true);
    }

    
    void OnClick(){
        skillsController.exec(skillName);
    }
}
