using UnityEngine;
using System.Collections;

public class ReflectPlusButtonController : SkillButtonController {
    void Start ()
    {
        base.Start();
        skillNum = (int)SkillsController.SkillName.kReflectplus;
        skillName = "reflectplus";
    }
}
