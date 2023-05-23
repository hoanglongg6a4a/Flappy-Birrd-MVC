using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    private void Awake()
    {
        //GameObject[] pipes = spawnerPipe.GetListPipe();
        spawnerPipe.GetPipeStatus(model.pipeNum,model.speed);
        birdController.getBirdStatus(model.bounceForce, model.gravity, view.GetScore(),playFlySound,playDieSound,playPingSound);
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
