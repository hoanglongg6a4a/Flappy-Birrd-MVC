using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpawnerPipe : MonoBehaviour
{
    [SerializeField] private GameObject pipeHolderObj;
    public List<GameObject> poolPipe;
    public static SpawnerPipe instance;
    private int poolSize;
    private float speed;
    private int index;
    private float maxX;
    // Start is called before the first frame update
    public void GetPipeStatus(int poolSize , float speed )
    {
        this.poolSize = poolSize;
        this.speed = speed;
    }
    public float GetSpeed()
    {
        return speed;
    }    
    public List<GameObject> GetListPipe()
    {
        return poolPipe;
    }
    void Start()
    {
        index= 0;
        Vector3 screenMaxPoint = new Vector3(Screen.width, Screen.height, 0);
        Vector3 worldMaxPoint = Camera.main.ScreenToWorldPoint(screenMaxPoint);
        maxX = worldMaxPoint.x;
        Spawner();
    }
    public void SetSpeedPipe(float speed)
    {
        for (int i = 0; i < poolPipe.Count; i++)
        {
            GameObject pipe = poolPipe[i]; 
            pipe.GetComponent<PipeHolder>().SetSpeed(speed);
        }
    }    
    public GameObject getPipe(int index)
    {
        return poolPipe[index];
    }
    void Spawner()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject pipe = Instantiate(pipeHolderObj, new Vector3(maxX,Random.Range(-1.8f, 1.8f), 0f), Quaternion.identity);
            pipe.GetComponent<PipeHolder>().SetSpeed(this.speed);
            maxX += 5f;
            poolPipe.Add(pipe);
        }
    }
    public GameObject GetPooledPipe()
    {
        foreach (GameObject pipe in poolPipe)
        {
            if (!pipe.activeInHierarchy)
            {
                pipe.GetComponent<PipeHolder>().SetSpeed(this.speed);
                pipe.SetActive(true);
                return pipe;
            }
        }
        GameObject newPipe = Instantiate(pipeHolderObj);
        poolPipe.Add(newPipe);
        return newPipe;
    }
}


