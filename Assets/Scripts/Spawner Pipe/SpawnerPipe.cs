using System.Collections.Generic;
using UnityEngine;
public class SpawnerPipe : MonoBehaviour
{
    [SerializeField] private PipeHolder pipeHolderObj;
    [SerializeField] private List<PipeHolder> poolPipe;
    private int poolSize;
    private float speed;
    private float maxX;
    // Start is called before the first frame update
    public void SetPipeStatus(int poolSize , float speed)
    {
        this.poolSize = poolSize;
        this.speed = speed;
    }
    public float GetSpeed()
    {
        return speed;
    }    
    private void Start()
    {
        Vector3 screenMaxPoint = new Vector3(Screen.width, Screen.height, 0f);
        Vector3 worldMaxPoint = Camera.main.ScreenToWorldPoint(screenMaxPoint);
        maxX = worldMaxPoint.x;
        Spawner();
    }
    public void SetSpeedPipe(float speed)
    {
        for (int i = 0; i < poolPipe.Count; i++)
        {
            PipeHolder pipe = poolPipe[i]; 
            pipe.SetSpeed(speed);
        }
    }    
    public PipeHolder GetPipe(int index)
    {
        return poolPipe[index];
    }
    private void Spawner()
    {
        for (int i = 0; i < poolSize; i++)
        {
            PipeHolder pipe = Instantiate<PipeHolder>(pipeHolderObj, new Vector3(maxX,Random.Range(-1.8f, 1.8f), 0f), Quaternion.identity);
            pipe.SetSpeed(this.speed);
            maxX += 5f;
            poolPipe.Add(pipe);
        }
    }
}


