using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
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
   // [SerializeField] private SpawnBullet spawnBullet;
    [SerializeField] private SpawnerPipe spawnerPipe;
    [SerializeField] private PipeHolder pipeHolder;
    [SerializeField] private BirdController birdController;
    [SerializeField] private Button instuctionButton;
    [SerializeField] private SpawnBird spawnBird;
    [SerializeField] private List<BirdInfo> birdList;
    BirdType birdType;
    string birdTypeString;
    private Vector2 spawnPosition = new Vector2(-1.5f, 0f);
    private GameObject birdObj;
    private BirdController bird;
    private void Awake()
    {
        string birdTypeString = PlayerPrefs.GetString("BirdType");
        Time.timeScale = 0;
        spawnerPipe.GetPipeStatus(model.pipeNum,model.speed);
        birdController.getBirdStatus(model.bounceForce, model.gravity, view.GetScore(),playFlySound,playDieSound,playPingSound);
        birdObj = choseBird();
        BirdController bird = birdObj.GetComponent<BirdController>();
    }
    private GameObject choseBird()
    {
        birdList = new List<BirdInfo>(); // Kh?i t?o birdList tr??c khi s? d?ng nó
        birdType = (BirdType)Enum.Parse(typeof(BirdType), birdTypeString);
        switch (birdType)
        {
            case BirdType.Yellow:
                birdObj = Instantiate( birdList[0].birdPrefab , spawnPosition, Quaternion.identity); // S? d?ng birdPrefab c?a BirdInfo[0] thay vì BirdInfo[0] tr?c ti?p
                break;
            case BirdType.Red:
                birdObj = Instantiate(birdList[1].birdPrefab, spawnPosition, Quaternion.identity);
                break;
            case BirdType.Blue:
                birdObj = Instantiate(birdList[2].birdPrefab, spawnPosition, Quaternion.identity);
                break;
            default:
                Debug.LogError("Invalid bird type!");
                break;
        }
        return birdObj;
    }
    private void Start()
    {
        birdController.GetPoolPipe(spawnerPipe.GetListPipe());
    }
    private void Update()
    {
        view.SetScore(birdController.getScore());
        view.BirdDiedShowPanel(birdController.getScore(),birdController.CheckAlive());
        bird.birdMoveMent();
          
    }
    public void InstructionButton()
    {
        Time.timeScale = 1;
        instuctionButton.gameObject.SetActive(false);
    }
    public void playFlySound()
    {
        audio.PlayFlapMusic();
    }
    public void playDieSound()
    {
        audio.PlayDieMusic();
    }
    public void playPingSound()
    {
        audio.PlayPingMusic();
    }
}
