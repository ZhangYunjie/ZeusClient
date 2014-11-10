using UnityEngine;
using System.Collections;

public class SkillButtonController : MonoBehaviour {
    protected GameObject player;
    protected SkillsController skillsController;
    protected int skillNum;
    protected string skillName;
    
    // Use this for initialization
    public void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        skillsController = GameObject.FindGameObjectWithTag("Skills").GetComponent<SkillsController>();
    }
    
    // Update is called once per frame
    void Update () {
        bool will_enabled = skillsController.skills[skillNum].getEnabled();
        UIButton button = GetComponent<UIButton>();
        if (button.isEnabled != will_enabled)
        {
            Debug.Log("enable: " + skillsController.skills[skillNum].enabled);
            button.isEnabled = will_enabled;
        }
    }

    
    void OnClick(){
        skillsController.exec(skillName);
    }
}
