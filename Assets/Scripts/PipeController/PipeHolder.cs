using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PipeHolder : MonoBehaviour
{
    public float speed;
    public static PipeHolder instance;
    void Start()
    {
        //speed = gameObject.GetComponent<SpawnerPipe>().GetSpeed();
    }
    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
    public float GetSpeed()
    {
        return speed;
    }
    // Update is called once per frame
    void Update()
    {
        PipeMovement();  
    } 
    void PipeMovement()
    {
       
        Vector3 temp = transform.position;
        temp.x -= speed * Time.deltaTime;
        transform.position = temp;
    }
}
