using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBird : BirdController
{
    public override void skill()
    {
        StartCoroutine(SkillCoroutine());
    }
    private IEnumerator SkillCoroutine()
        {
            PipeHolder.instance.SetSpeed(15f);
            yield return new WaitForSeconds(0.5f);                                            
            PipeHolder.instance.SetSpeed(5f);
        }
 }



