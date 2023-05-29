using System;
using UnityEngine;
public abstract class Bird : MonoBehaviour
{
    private float verticalVelocity;
    private bool isAlive;
    private float jumpForce;
    private float gravity;
    private int score;
    protected Action playFlyMusic;
    protected Action playDiedMusic;
    protected Action playPingMusic;
    private float currentSpeed;
    protected Action<float> setSpeed;
    protected Action getBullet;
    protected Action<int> SetSkillCoolDown;
    protected Action<int> DieShowPanel;
    protected Action<int> SetScore;
    protected Action ResetCoolDown, StartCoolDown;
    private Vector3 currentPosition;
    private Vector3 previoustPosition;
    private void Awake()
    {
        score= 0;
        isAlive = true;
    }
    public void SetBirdStatus(float jumpForce, float gravity, Action playFlyMusic, Action playDiedMusic, Action playPingMusic, float currentSpeed)
    {
        this.currentSpeed = currentSpeed;
        this.jumpForce = jumpForce;
        this.gravity = gravity;
        this.playFlyMusic = playFlyMusic;
        this.playDiedMusic = playDiedMusic;
        this.playPingMusic = playPingMusic;
    }
    public void SetAction(Action<float> setSpeed,Action getBullet,Action<int>SetSkillCoolDown,Action<int>DieShowPanel,Action<int> SetScore,Action ResetCoolDown,Action StartCoolDown)
    { 
        this.setSpeed = setSpeed;
        this.getBullet = getBullet;
        this.SetSkillCoolDown = SetSkillCoolDown;
        this.DieShowPanel = DieShowPanel;
        this.SetScore = SetScore;
        this.ResetCoolDown = ResetCoolDown;
        this.StartCoolDown = StartCoolDown;
    }    
    public Vector2 GetBirdPos()
    {
        return transform.position;
    }    
    protected void setCurrentSpeed(float currentSpeed)
    {
        this.currentSpeed = currentSpeed;
    }    
    public bool CheckCollision(PipeHolder pipe)
    {
        Transform child1 = pipe.transform.GetChild(0);
        Transform child2 = pipe.transform.GetChild(1);
        Renderer objectRenderer1 = child1.GetComponent<Renderer>();
        Renderer objectRenderer2 = child2.GetComponent<Renderer>();
        // GetBound for pipe Top,Bottom
        Bounds topColumnBounds = objectRenderer1.bounds;
        Bounds bottomColumnBounds = objectRenderer2.bounds;
        if (transform.position.x > topColumnBounds.min.x && transform.position.x < topColumnBounds.max.x)
        {
            if (transform.position.y <= bottomColumnBounds.max.y && this.currentSpeed <= 5f || transform.position.y >= topColumnBounds.min.y && this.currentSpeed <= 5f)
            {
                isAlive = false;
                DieShowPanel.Invoke(this.score);
                playDiedMusic.Invoke();
                verticalVelocity = 0f;
                Time.timeScale = 0f;
            }
            if (transform.position.y > bottomColumnBounds.max.y || transform.position.y < topColumnBounds.min.y)
            {
                IncreaseScore();
                return true;
            }
            else if(this.currentSpeed > 5f) 
            {
                IncreaseScore();
                return true;
            }
        }
        return false;
    }
    // Bird's gravity and rotation
    public void Fly()
    {
        if (!isAlive) return;
        previoustPosition = transform.position;
        if (transform.position.y <= -4f)
        {
            isAlive = false;
            transform.position = new Vector3(transform.position.x, -4f, transform.position.z);
            DieShowPanel.Invoke(this.score);
            playDiedMusic.Invoke();
            verticalVelocity = 0f;
            Time.timeScale = 0f;
        }
        verticalVelocity -= gravity * Time.deltaTime;
        transform.position += Vector3.up * verticalVelocity * Time.deltaTime;
        currentPosition = transform.position;
        CheckAngle(previoustPosition, currentPosition);
    }
    public void FlyTap()
    {
        verticalVelocity = jumpForce;
        playFlyMusic.Invoke();
    }
    private void IncreaseScore()
    {
        score++;
        SetScore.Invoke(this.score);
        playPingMusic.Invoke();
    }
    private void CheckAngle(Vector3 PrevioustPosition, Vector3 currentPosition)
    {
        float angle = Mathf.Lerp(0f, (currentPosition.y > PrevioustPosition.y) ? 90f : -90f, verticalVelocity / 9f);
        gameObject.transform.rotation = Quaternion.Euler(0f,0f,angle);
    }
    public abstract void Skill();
}