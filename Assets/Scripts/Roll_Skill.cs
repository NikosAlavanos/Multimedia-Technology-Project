using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roll_Skill : Skill
{


    public override void UseSkill()
    {
        base.UseSkill();

        Debug.Log("Did something amazing when Rolling");
    }
}
