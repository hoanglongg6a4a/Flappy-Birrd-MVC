using System.Collections.Generic;
using System.Security.Cryptography;
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
        Time.timeScale = 0;
        choseBird();
        spawnerPipe.SetPipeStatus(model.PipeNum,model.Speed);
        spawnBullet.SetBulletStatus(model.BulletNum,model.BulletSpeed);   
        bird = birdObj.GetComponent<Bird>();
        bird.SetBirdStatus(model.BounceForce, model.Gravity,audio.PlayFlapMusic,audio.PlayDieMusic,audio.PlayPingMusic, spawnerPipe.GetSpeed());
        bird.GetAction(spawnerPipe.SetSpeedPipe, spawnBullet.GetBullet, view.SetSkillCoolDown, view.BirdDiedShowPanel, view.SetScore, view.SetTime, view.GetTime);
        index = 0;
    }
    private GameObject choseBird()
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
    private void MenuButton()
    {
        SceneManager.LoadScene("GameMenu");
    }
    private void RestartGameButton()
    {
        SceneManager.LoadScene("GamePlay");
    }
    private void InstructionButton()
    {
        Time.timeScale = 1;
        instuctionButton.gameObject.SetActive(false);
    }
}
