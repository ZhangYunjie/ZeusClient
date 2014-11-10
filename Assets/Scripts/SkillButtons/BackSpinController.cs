using UnityEngine;
using System.Collections;

public class BackSpinController : SkillButtonController {
    void Start ()
    {
        base.Start();
        skillNum = (int)SkillsController.SkillName.kBackspin;
        skillName = "backspin";
    }
}
