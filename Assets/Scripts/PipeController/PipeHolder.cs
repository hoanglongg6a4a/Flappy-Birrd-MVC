using UnityEngine;
public class PipeHolder : MonoBehaviour
{
    private float speed;
    private float minX,maxX;
    void Start()
    {
        Vector3 screenMinPoint = new Vector3(0f,0f,0f);
        Vector3 worldMinPoint = Camera.main.ScreenToWorldPoint(screenMinPoint);
        minX = worldMinPoint.x;
        Vector3 screenMaxPoint = new Vector3(Screen.width, Screen.height,0f);
        Vector3 worldMaxPoint = Camera.main.ScreenToWorldPoint(screenMaxPoint);
        maxX = worldMaxPoint.x;
    }   
    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
    // Update is called once per frame
    private void Update()
    {
        PipeMovement();
    } 
    private void PipeMovement()
    {
        Vector3 temp = transform.position;
        temp.x -= speed * Time.deltaTime;
        transform.position = temp;
        if (transform.position.x < minX - 5f)
        {
            transform.position = new Vector2(maxX, Random.Range(-1.8f, 1.8f));
        }
    }
}