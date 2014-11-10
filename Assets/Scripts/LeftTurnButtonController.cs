using UnityEngine;
using System.Collections;

public class LeftTurnButtonController : SkillButtonController {

    // Use this for initialization
    void Start ()
    {
        base.Start();
        skillNum = (int)SkillsController.SkillName.kLeftturn;
        skillName = "leftturn";
    }

}
