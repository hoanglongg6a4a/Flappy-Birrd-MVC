using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpawnerPipe : MonoBehaviour
{
   // [SerializeField] private PipeHolder pipeHolder;
    [SerializeField] private GameObject pipeHolderObj;
    public List<GameObject> poolPipe;
    public static SpawnerPipe instance;
    private int poolSize;
    private float speed;
    private int index=0;
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
        for (int i = 0; i < poolSize; i++)
        {
            GameObject pipe = Instantiate(pipeHolderObj);
            poolPipe.Add(pipe);
        }
        StartCoroutine(Spawner());
    }
    public void SetSpeedPipe(float speed)
    {
        for (int i = 0; i < poolPipe.Count; i++)
        {
            GameObject pipe = poolPipe[i]; // Lấy đối tượng ống tại chỉ mục i

            pipe.GetComponent<PipeHolder>().SetSpeed(speed);
        }
    }    

    public GameObject getPipe()
    {
        return poolPipe[index];
    }

    IEnumerator Spawner()
    {
        Vector3 screenMaxPoint = new Vector3(Screen.width, Screen.height, 0);
        Vector3 worldMaxPoint = Camera.main.ScreenToWorldPoint(screenMaxPoint);
        float maxX = worldMaxPoint.x;
        yield return new WaitForSeconds(1.5f);
        GameObject pipe = poolPipe[index];
        Vector3 temp = pipe.transform.position;
        temp.y = Random.Range(-1.8f, 1.8f);
        temp.x = maxX;
        pipe.transform.position = temp;
        
        if (index >= poolPipe.Count)
        { index = 0; }
        setPositionPipe(pipe, temp);
        StartCoroutine(Spawner());
    }
    private void setPositionPipe(GameObject pipe , Vector3 temp)
    {
        Vector3 screenMinPoint = new Vector3(0, 0, 0);
        Vector3 worldMinPoint = Camera.main.ScreenToWorldPoint(screenMinPoint);
        float minX = worldMinPoint.x;
        if (pipe.transform.position.x < minX - 0.5f)
        {
            index++;
            pipe.transform.position = temp;
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


