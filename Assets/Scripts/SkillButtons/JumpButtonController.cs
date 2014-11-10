using UnityEngine;
using System.Collections;

public class JumpButtonController : SkillButtonController {
    void Start ()
    {
        base.Start();
        skillNum = (int)SkillsController.SkillName.kJump;
        skillName = "jump";
    }
}
