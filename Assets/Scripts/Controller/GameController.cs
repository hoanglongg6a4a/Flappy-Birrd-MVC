using System.Collections;
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
    [Header("MVC")]
    [SerializeField] GameModel model;
    [SerializeField] GameView view;
    [SerializeField] GameAudio audio;
    [Header("Preference")]
    [SerializeField] Bird bird;
   // [SerializeField] private SpawnBullet spawnBullet;
    [SerializeField] private SpawnerPipe spawnerPipe;
    [SerializeField] private PipeHolder pipeHolder;
    [SerializeField] private BirdController birdController;
    [SerializeField] private Button instuctionButton;
    [SerializeField] private SpawnBird spawnBird;

    private void Awake()
    {
        Time.timeScale = 0;
        //GameObject[] pipes = spawnerPipe.GetListPipe();
        spawnerPipe.GetPipeStatus(model.pipeNum,model.speed);
        birdController.getBirdStatus(spawnBird.ChooseBird(),model.bounceForce, model.gravity, view.GetScore(),playFlySound,playDieSound,playPingSound);
    }
    private void Start()
    {
        birdController.GetPoolPipe(spawnerPipe.GetListPipe());
    }
    private void Update()
    {
        view.SetScore(birdController.getScore());
        view.BirdDiedShowPanel(birdController.getScore(),birdController.CheckAlive());
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
