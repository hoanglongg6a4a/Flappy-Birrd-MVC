using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BirdController : MonoBehaviour
{
    public Text countDownText;
    public GameObject bullet;
    //public GameObject   gameObject;
    private List<GameObject> pipes;
    private GameObject[] pipeHolders;
    private GameObject[] pipesTest;

    private float verticalVelocity = 0f;
    private bool isAlive;
    public float jumpForce;
    private float gravity;
    private const string pipeTag = "Pipe";
    private bool canPressButton;
    public bool hasScored;
    public int score;
    private Bird IBird;
    public BirdType birdType;
    public Action playFlyMusic;
    public Action playDiedMusic;
    public Action playPingMusic;
    private float currentSpeed;
    public Action<float> setSpeed;
    public Action getBullet;
    public Action<int> SetSkillCoolDown;

    void Awake()
    {
        hasScored = false;
        isAlive = true;
        canPressButton = true;
    }
    public void getBirdStatus(float jumbForce, float gravity, int score, Action playFlyMusic, Action playDiedMusic, Action playPingMusic ,float speed, Action<float> setSpeed , Action getBullet,Action<int>SetSkillCoolDown)
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
    public bool CheckAlive()
    {
        return isAlive;
    }
    public void birdMoveMent(GameObject pipe)
    {
        Transform pipeTransform = pipe.transform;
        Renderer pipeRenderer = pipeTransform.GetComponent<Renderer>();
        float maxX = pipeRenderer.bounds.max.x;
        Renderer birdRenderer = gameObject.GetComponent<Renderer>();
        float BirdmaxX = birdRenderer.bounds.max.x;
        if (Mathf.Abs(maxX - BirdmaxX) < 0.01f && !hasScored)
        {
            hasScored = true;
            IncreaseScore();
        }
        if (BirdmaxX > maxX && hasScored)
        {
            hasScored = false;
        }
    }
    public void CheckCollision()
    {
        if (transform.position.y <= -4f)
        {
            isAlive = false;
            transform.position = new Vector3(transform.position.x, -4f, transform.position.z);
            playDiedMusic.Invoke();
            verticalVelocity = 0;
            Time.timeScale = 0;
        }

        pipesTest = GameObject.FindGameObjectsWithTag(pipeTag);
        if (pipesTest.Length > 0)
        {
            foreach (GameObject pipe in pipesTest)
            {
                Renderer birdRenderer = GetComponent<Renderer>();
                Renderer pipeRenderer = pipe.GetComponent<Renderer>();
                Bounds birdBounds = birdRenderer.bounds;
                Bounds pipeBounds = pipeRenderer.bounds;
                if (currentSpeed <= 5)
                {
                    if (birdBounds.Intersects(pipeBounds))
                    {
                        isAlive = false;
                        playDiedMusic.Invoke();
                        verticalVelocity = 0;
                        Time.timeScale = 0;
                    }
                }
            }
        }
    }
    public void fly()
    {
        Vector3 PrevioustPosition = gameObject.transform.position;
        if (!isAlive) return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            verticalVelocity = jumpForce;
            playFlyMusic.Invoke();
            //audioSource.PlayOneShot(flyClip);
        }
        verticalVelocity -= gravity * Time.deltaTime;
        gameObject.transform.position += Vector3.up * verticalVelocity * Time.deltaTime;
        Vector3 currentPosition = gameObject.transform.position;
        checkAngel(PrevioustPosition, currentPosition);
    }
    public void IncreaseScore()
    {
        score++;
        playPingMusic.Invoke();
    }
    public void checkAngel(Vector3 PrevioustPosition, Vector3 currentPosition)
    {
        float angle = Mathf.Lerp(0, (currentPosition.y > PrevioustPosition.y) ? 90 : -90, Mathf.Abs(verticalVelocity) / 9);
        gameObject.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
   
    public abstract void Skill();
    


}