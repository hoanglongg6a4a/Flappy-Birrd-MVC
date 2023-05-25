using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BirdController : MonoBehaviour
{
    public Text countDownText;
    public GameObject bullet;
    private GameObject[] pipes;
    private float verticalVelocity = 0f;
    private bool isAlive;
    public float jumpForce;
    private float gravity;
    private const string pipeTag = "Pipe";
    public bool hasScored;
    public int score;
    public BirdType birdType;
    public Action playFlyMusic;
    public Action playDiedMusic;
    public Action playPingMusic;
    private float currentSpeed;
    public Action<float> setSpeed;
    public Action getBullet;
    public Action<int> SetSkillCoolDown;
    public Action<int> DieShowPanel;
    public Action<int> SetScore;
    public Action SetTime, GetTime;
    void Awake()
    {
        hasScored = false;
        isAlive = true;
    }
    public void getBirdStatus(float jumbForce, float gravity, int score, Action playFlyMusic, Action playDiedMusic, Action playPingMusic ,float speed, Action<float> setSpeed , Action getBullet,Action<int>SetSkillCoolDown,Action<int>DieShowPanel,Action<int>SetScore,Action SetTime,Action GetTime)
    {
        this.jumpForce = jumbForce;
        this.gravity = gravity;
        this.score = score;
        this.playFlyMusic = playFlyMusic;
        this.playDiedMusic = playDiedMusic;
        this.playPingMusic = playPingMusic;
        this.currentSpeed = speed;
        this.setSpeed = setSpeed;
        this.getBullet = getBullet;
        this.SetSkillCoolDown = SetSkillCoolDown;
        this.DieShowPanel = DieShowPanel;
        this.SetScore = SetScore;
        this.SetTime = SetTime;
        this.GetTime = GetTime;
    }
    public Vector2 GetBirdPos()
    {
        return transform.position;
    }    
    public int getScore()
    {
        return score;
    }
    public void setCurrentSpeed(float speed)
    {
        this.currentSpeed = speed;
    }    
    public bool getHasScore()
    {
        return hasScored;
    }
    // Move over Columns and get score check
    public void BirdMoveMent(GameObject pipe)
    {
        Transform pipeTransform = pipe.transform;
        Renderer pipeRenderer = pipeTransform.GetComponent<Renderer>();
        float maxX = pipeRenderer.bounds.max.x;
        Renderer birdRenderer = gameObject.GetComponent<Renderer>();
        float BirdmaxX = birdRenderer.bounds.max.x;
        if (Mathf.Abs(BirdmaxX-maxX) < 0.02f && !hasScored)
        {
            Debug.Log("Var");
            IncreaseScore();
            hasScored = true;
        }
    }
    // Collision check
    public void CheckCollision()
    {
        if (!isAlive) return;
        // Check Ground
        if (transform.position.y <= -4f)
        {
            isAlive = false;
            transform.position = new Vector3(transform.position.x, -4f, transform.position.z);
            DieShowPanel.Invoke(this.score);
            playDiedMusic.Invoke();
            verticalVelocity = 0;
            Time.timeScale = 0;
        }
        pipes = GameObject.FindGameObjectsWithTag(pipeTag);
        foreach (GameObject pipe in pipes)
        {
            Renderer birdRenderer = GetComponent<Renderer>();
            Renderer pipeRenderer = pipe.GetComponent<Renderer>();
            float birdMinX = birdRenderer.bounds.min.x;
            float birdMaxX = birdRenderer.bounds.max.x;
            float birdMinY = birdRenderer.bounds.min.y;
            float birdMaxY = birdRenderer.bounds.max.y;

            float pipeMinX = pipeRenderer.bounds.min.x;
            float pipeMaxX = pipeRenderer.bounds.max.x;
            float pipeMinY = pipeRenderer.bounds.min.y;
            float pipeMaxY = pipeRenderer.bounds.max.y;
            if (currentSpeed <= 5)
            {
                if (birdMinX <= pipeMaxX && birdMaxX >= pipeMinX &&
                    birdMinY <= pipeMaxY && birdMaxY >= pipeMinY)

                {
                    isAlive = false;
                    DieShowPanel.Invoke(this.score);
                    playDiedMusic.Invoke();
                    verticalVelocity = 0;
                    Time.timeScale = 0;
                }
            }
        }
    }
    public void Fly()
    {
        Vector3 PrevioustPosition = gameObject.transform.position;
        if (!isAlive) return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            verticalVelocity = jumpForce;
            playFlyMusic.Invoke();
        }
        verticalVelocity -= gravity * Time.deltaTime;
        gameObject.transform.position += Vector3.up * verticalVelocity * Time.deltaTime;
        Vector3 currentPosition = gameObject.transform.position;
        checkAngle(PrevioustPosition, currentPosition);
    }
    public void IncreaseScore()
    {
        score++;
        SetScore.Invoke(this.score);
        playPingMusic.Invoke();
    }
    public void checkAngle(Vector3 PrevioustPosition, Vector3 currentPosition)
    {
        float angle = Mathf.Lerp(0, (currentPosition.y > PrevioustPosition.y) ? 90 : -90, Mathf.Abs(verticalVelocity) / 9);
        gameObject.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    public abstract void Skill();
}