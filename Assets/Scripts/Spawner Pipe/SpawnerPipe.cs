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
    private int speed;
    // Start is called before the first frame update
    public void GetPipeStatus(int poolSize , int speed )
    {
        this.poolSize = poolSize;
        this.speed = speed;
    }
    public List<GameObject> GetListPipe()
    {
        return poolPipe;
    }
    void Start()
    {
        MakeInstance();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject pipe = Instantiate(pipeHolderObj);
            pipe.SetActive(false);
            poolPipe.Add(pipe);
        }
        StartCoroutine(Spawner());
    }
    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    IEnumerator Spawner()
    {
        Vector3 screenMaxPoint = new Vector3(Screen.width, Screen.height, 0);
        Vector3 worldMaxPoint = Camera.main.ScreenToWorldPoint(screenMaxPoint);
        float maxX = worldMaxPoint.x;
        yield return new WaitForSeconds(1.5f);
            GameObject pipe = GetPooledPipe();
           Vector3 temp = pipe.transform.position;
            temp.y = Random.Range(-1.8f, 1.8f);
            temp.x = maxX;
            pipe.transform.position = temp;
        StartCoroutine(Spawner());
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
        // Nếu không có viên đạn khả dụng trong pool, tạo thêm và trả về viên đạn mới
        GameObject newPipe = Instantiate(pipeHolderObj);
        poolPipe.Add(newPipe);
        return newPipe;
    }
}


