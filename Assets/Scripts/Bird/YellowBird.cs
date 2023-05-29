using System.Collections;
using UnityEngine;
public class YellowBird : Bird
{
    private bool canPressButoon = true;
    public override void Skill()
    {
        if (canPressButoon)
        {
            base.ResetCoolDown.Invoke();
            canPressButoon = false;
            StartCoroutine(SkillCoolDown());
        }
    }
    private void Update()
    {
        if(!canPressButoon)
        {
            base.StartCoolDown.Invoke();
        }
    }
    IEnumerator SkillCoolDown()
    {
        base.setSpeed.Invoke(9f);
        base.setCurrentSpeed(9f);
        yield return new WaitForSeconds(0.5f);
        base.setSpeed.Invoke(5f);
        base.setCurrentSpeed(5f);
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



