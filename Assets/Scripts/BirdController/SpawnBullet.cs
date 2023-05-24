using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpawnBullet : MonoBehaviour
{
    public GameObject bulletPrefab;
    public int poolSize ;
    public List<GameObject> bulletPool;
    private float speed;
    private Vector3 currentBirdPos;
    public void GetBulletStatus(int poolSize , float speed)
    {
        this.poolSize = poolSize;
        this.speed = speed;
        
    }   
    public void GetBirdPos(Vector2 currentBirdPos)
    {
        this.currentBirdPos = currentBirdPos;
    }    
    public float GetSpeed()
    {
        return speed;
    }    
    private void Start()
    {
        bulletPool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bulletPool.Add(bullet);
        }
    }
    private void Update()
    {
       
        Vector3 screenMaxPoint = new Vector3(Screen.width, Screen.height, 0);
        Vector3 worldMaxPoint = Camera.main.ScreenToWorldPoint(screenMaxPoint);
        float maxX = worldMaxPoint.x;
        foreach (GameObject bullet in bulletPool)
        {
            if (bullet.transform.position.x > maxX)
            {
                bullet.SetActive(false);
            }
        }
    }
    public void GetBullet()
    {
        foreach (GameObject bullet in bulletPool)
        {
            if (!bullet.activeInHierarchy)
            {
                bullet.SetActive(true);
                bullet.GetComponent<Bullet>().GetSpeed(speed);
                bullet.transform.position = currentBirdPos;
                break;
                //return bullet;
            }
        }
  /*      GameObject newBullet = Instantiate(bulletPrefab);
        bulletPool.Add(newBullet);
        return newBullet;*/
    }
}
