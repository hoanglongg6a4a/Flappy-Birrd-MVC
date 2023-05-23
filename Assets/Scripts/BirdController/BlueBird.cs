using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;

public class BlueBird : BirdController
{
    public override void skill()
    {
        StartCoroutine(SkillCoroutine());
    }

    private IEnumerator SkillCoroutine()
    {
        GameObject[] pipes = GameObject.FindGameObjectsWithTag("PipeHolder");
        if(pipes.Length == 1 )
        {
            pipes[0].GetComponent<PipeHolder>().speed = 1;
        }
        else if(pipes.Length == 2 )
        {
            pipes[0].GetComponent<PipeHolder>().speed = 1;
            pipes[1].GetComponent<PipeHolder>().speed = 1;
        }    
        PipeHolder.instance.SetSpeed(1f); // 
        yield return new WaitForSeconds(0.5f); //                                             
        PipeHolder.instance.SetSpeed(5f);
        if (pipes.Length == 1)
        {
            pipes[0].GetComponent<PipeHolder>().speed = 5;
        }
        else if (pipes.Length == 2)
        {
            pipes[0].GetComponent<PipeHolder>().speed = 5;
            pipes[1].GetComponent<PipeHolder>().speed = 5;
        }
    }
}