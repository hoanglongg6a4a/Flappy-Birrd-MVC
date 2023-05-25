using System.Collections.Generic;
using UnityEngine;
public class SpawnBullet : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;
    private int poolSize ;
    private List<GameObject> bulletPool;
    private float speed;
    private Vector2 currentBirdPos;
    public void SetBulletStatus(int poolSize , float speed)
    {
        this.poolSize = poolSize;
        this.speed = speed;     
    }   
    public void SetBirdPos(Vector2 currentBirdPos)
    {
        this.currentBirdPos = currentBirdPos;
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
            }
        }
    }
}
