using UnityEngine;
using System.Collections;

public class TopSpinButtonController : SkillButtonController {
    void Start ()
    {
        base.Start();
        skillNum = (int)SkillsController.SkillName.kTopspin;
        skillName = "topspin";
    }
}
