using UnityEngine;
using System.Collections;

public class SkillStatus{
    public bool reflect_plus;
    private float reflect_plus_rate;

    public SkillStatus(){
        reflect_plus = false;
        reflect_plus_rate = 1.0f;
    }

    public void reset()
    {
        reflect_plus = false;
        reflect_plus_rate = 1.0f;
    }

    public void gainRefelctStatus(){
        reflect_plus = true;
        reflect_plus_rate = 2.0f;
    }

    public float getReflectPlusRate(){
        if (reflect_plus == false)
            return 1.0f;

        float pre_reflect_plus_rate = reflect_plus_rate;
        reflect_plus_rate = reflect_plus_rate * 0.8f;
        if (reflect_plus_rate < 1.0f)
            reflect_plus_rate = 1.0f;

        return pre_reflect_plus_rate;
    }
}
