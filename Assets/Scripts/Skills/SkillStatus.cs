using UnityEngine;
using System.Collections;

public class SkillStatus{
    public bool reflect_plus;

    public SkillStatus(){
        reflect_plus = false;
    }

    public void reset()
    {
        reflect_plus = false;
    }
}
