using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed;
    void Start()
    {
       
    }
    public void GetSpeed(float speed)
    {
        this.speed = speed;
    }    

    void Update()
    {
      /*  speed = gameObject.GetComponent<SpawnBullet>().GetSpeed();*/
        BulletMovement();
    }
    void BulletMovement()
    {
        
        Vector3 temp = transform.position;
        temp.x += speed * Time.deltaTime;
        transform.position = temp;    
    }
}
