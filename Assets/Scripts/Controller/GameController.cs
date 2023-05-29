using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public enum BirdType
{
    Blue,
    Red,
    Yellow
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
    [SerializeField] private List<BirdInfo> birdList;
    private Vector2 spawnPosition = new Vector2(-1.5f, 0f);
    private GameObject birdObj;
    private Bird bird;
    private int index;
    private const string ParamTag = "Param";
    private const string SaveColorBird = "color bird";
    private void Awake()
    {
        Time.timeScale = 0f;
        ChoseBird();
        spawnerPipe.SetPipeStatus(model.PipeNum,model.Speed);
        spawnBullet.SetBulletStatus(model.BulletNum,model.BulletSpeed);   
        bird = birdObj.GetComponent<Bird>();
        bird.SetBirdStatus(model.BounceForce, model.Gravity,audio.PlayFlapMusic,audio.PlayDieMusic,audio.PlayPingMusic, spawnerPipe.GetSpeed());
        bird.SetAction(spawnerPipe.SetSpeedPipe, spawnBullet.GetBullet, view.SetSkillCoolDown, view.BirdDiedShowPanel, view.SetScore, view.StartCoolDown, view.ResetCoolDown);
        index = 0;
    }
    private GameObject ChoseBird()
    {
        GameObject BirdChose = GameObject.FindGameObjectWithTag(ParamTag);
        if(BirdChose != null )
        {
            Parameter parameter = BirdChose.GetComponent<Parameter>();
            PlayerPrefs.SetInt(SaveColorBird, (int)parameter.type);
            Destroy(BirdChose);
        }
        foreach (BirdInfo birdInfo in birdList)
        {
            if (PlayerPrefs.GetInt(SaveColorBird) == (int)birdInfo.birdType)
            {
                birdObj = Instantiate(birdList[PlayerPrefs.GetInt(SaveColorBird)].birdPrefab, spawnPosition, Quaternion.identity);
                Destroy(BirdChose);
                break;
            }
        }
        return birdObj;
    }
    private void Update()
    {
        spawnBullet.SetBirdPos(bird.GetBirdPos());
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Tap();
        }
        bird.Fly();
        CheckPipe(); 
        if(Input.GetKeyDown(KeyCode.G)) 
        {
            bird.Skill();
        }
    }
    private void CheckPipe()
    {
        if (bird.CheckCollision(spawnerPipe.GetPipe(index)))
        {
            index++;
        }
        if (index > model.PipeNum -1) { index = 0; }
    }
    // Tap in screen to fly 
    public void Tap()
    {
        bird.FlyTap();
    }
    public void MenuButton()
    {
        SceneManager.LoadScene(model.SenceMenuName);
    }
    public void RestartGameButton()
    {
        SceneManager.LoadScene(model.SenceGamePlayName);
    }
    public void InstructionButton()
    {
        Time.timeScale = 1f;
        instuctionButton.gameObject.SetActive(false);
    }
}
