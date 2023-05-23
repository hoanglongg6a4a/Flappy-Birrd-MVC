using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBird : BirdController
{
    public override void skill()
    {
        Debug.Log("Xài Skill");
        /*        GameObject bullet = SpawnBullet.instance.GetBullet();
                bullet.transform.position = BirdController.instance.birdOjc.transform.position;
                bullet.SetActive(true);*/
    }
}