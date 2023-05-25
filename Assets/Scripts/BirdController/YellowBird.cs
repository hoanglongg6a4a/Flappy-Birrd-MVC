using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBird : BirdController
{
   
    private bool canPressButoon = true;
    public override void Skill()
    {
        if (canPressButoon)
        {
            base.SetTime();
            canPressButoon &= false;
            StartCoroutine(SkillCoolDown());
        }
    }
    public void Update()
    {
        if(!canPressButoon)
        {
            base.GetTime();
        }
    }
    IEnumerator SkillCoolDown()
    {
        base.setSpeed(9f);
        base.setCurrentSpeed(15f);
        yield return new WaitForSeconds(0.5f);
        base.setSpeed(5f);
        base.setCurrentSpeed(5f);
        int countdownValue = 5;
        while (countdownValue >= 0)
        {
            base.SetSkillCoolDown(countdownValue);
            countdownValue--;
            yield return new WaitForSeconds(1f);
        }
        //countDownText.text = "Go";
        canPressButoon = true;
    }
}



