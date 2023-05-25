using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed;
    public void GetSpeed(float speed)
    {
        this.speed = speed;
    }    
    private void Update()
    {
        BulletMovement();
    }
    private void BulletMovement()
    {
        Vector3 temp = transform.position;
        temp.x += speed * Time.deltaTime;
        transform.position = temp;    
    }
}
