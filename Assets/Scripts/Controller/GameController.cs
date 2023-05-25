
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BirdType
{
    Yellow,
    Red,
    Blue
}
public class GameController : MonoBehaviour
{
    [System.Serializable]
    public class BirdInfo
    {
        public BirdType birdType;
        public GameObject birdPrefab;
    }
    [Header("MVC")]
    [SerializeField] GameModel model;
    [SerializeField] GameView view;
    [SerializeField] GameAudio audio;
    [Header("Preference")]
    [SerializeField] private SpawnBullet spawnBullet;
    [SerializeField] private SpawnerPipe spawnerPipe;
    [SerializeField] private PipeHolder pipeHolder;
    [SerializeField] private Button instuctionButton;
    [SerializeField] private SpawnBird spawnBird;
    [SerializeField] private List<BirdInfo> birdList;
    private Vector2 spawnPosition = new Vector2(-1.5f, 0f);
    private GameObject birdObj;
    private BirdController bird;
    private int index;
    private void Awake()
    {
        Time.timeScale = 0;
        spawnerPipe.GetPipeStatus(model.pipeNum,model.speed);
        spawnBullet.GetBulletStatus(model.bulletNum,model.bulletSpeed);
        birdObj = choseBird();
        bird = birdObj.GetComponent<BirdController>();
        bird.getBirdStatus(model.bounceForce, model.gravity, view.GetScore(),audio.PlayFlapMusic,audio.PlayDieMusic,audio.PlayPingMusic 
        , spawnerPipe.GetSpeed(), spawnerPipe.SetSpeedPipe, spawnBullet.GetBullet , view.SetSkillCoolDown,view.BirdDiedShowPanel,view.SetScore,view.SetTime,view.GetTime);
        index = 0;
    }
    private GameObject choseBird()
    {
        GameObject BirdChose = GameObject.FindGameObjectWithTag("Param");
        Parameter parameter = BirdChose.GetComponent<Parameter>();
        Destroy(BirdChose);
        switch (parameter.type)
        {
            case BirdType.Yellow:
                birdObj = Instantiate( birdList[0].birdPrefab , spawnPosition, Quaternion.identity); 
                break;
            case BirdType.Blue:
                birdObj = Instantiate(birdList[1].birdPrefab, spawnPosition, Quaternion.identity);
                break;
            case BirdType.Red:
                birdObj = Instantiate(birdList[2].birdPrefab, spawnPosition, Quaternion.identity);
                break;
            default:
                Debug.LogError("Invalid bird type!");
                break;
        }
        return birdObj;
    }
   
    private void Update()
    {
        spawnBullet.GetBirdPos(bird.GetBirdPos());
        bird.Fly();
        CheckPipe(); 
        if(Input.GetKeyDown(KeyCode.G)) 
        {
            bird.Skill();
        }
    }
    public void InstructionButton()
    {
        Time.timeScale = 1;
        instuctionButton.gameObject.SetActive(false);
    }
    public void CheckPipe()
    {
        if(bird.hasScored)
        {
            index++;
            bird.hasScored = false;
        }
        if(index > model.pipeNum-1)
        {
            index= 0;
        }
        bird.CheckCollision();
        bird.BirdMoveMent(spawnerPipe.getPipe(index));    
    }    
}
