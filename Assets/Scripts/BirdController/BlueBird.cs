using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;

public class BlueBird : BirdController
{
    private bool canPressButoon = true;    
    public override void Skill()
    {
        if (canPressButoon)
        {
            canPressButoon&= false;
            StartCoroutine(SkillCoolDown());
        }  
    }
    IEnumerator SkillCoolDown()
    {
        base.setSpeed(1f);
        yield return new WaitForSeconds(0.5f);
        base.setSpeed(5f);
        int countdownValue = 5;
        while (countdownValue >= 0)
        {
            base.SetSkillCoolDown(countdownValue);
            countdownValue--;
            yield return new WaitForSeconds(1f);
        }
        canPressButoon = true;
    }
}