using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdController : MonoBehaviour
{
    public static BirdController instance;
    [SerializeField]
    private GameObject redBird, yellowBird, blueBird;
    public Text countDownText;
    public GameObject bullet;
    public GameObject birdOjc;
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
    public int score ;
    private Bird IBird;
    public BirdType birdType;
    public Action playFlyMusic;
    public Action playDiedMusic;
    public Action playPingMusic;

    //private string KindBird="BirdType";

    void Awake()
    {
     /*   IBird = gameObject.GetComponent<Bird>();
        Vector2 spawnPosition = new Vector2(-1.5f, 0f);
        birdOjc = Instantiate(birdOjc, spawnPosition, Quaternion.identity);*/
        //countDownText.text = "Go";
        //score = 0;
        //ChooseBird();
        hasScored = false;
        isAlive = true;
        canPressButton = true;
        if (instance == null)
        {
            instance = this;
        }
    }   
    public void getBirdStatus(GameObject birdOjc, float jumbForce , float gravity , int score, Action playFlyMusic , Action playDiedMusic , Action playPingMusic)
    {
        this.birdOjc = birdOjc;
        this.jumpForce = jumbForce;
        this.gravity = gravity;
        this.score = score;
        this.playFlyMusic = playFlyMusic;
        this.playDiedMusic = playDiedMusic;
        this.playPingMusic = playPingMusic;

    }    
    public void GetPoolPipe(List<GameObject> pipes)
    {
        this.pipes = pipes;
    }    
    public int getScore()
    {
        return score;
    }    
    public bool CheckAlive()
    {
        return isAlive;
    }    
    void Update()
    {
        birdMoveMent();
    }
    void birdMoveMent()
    {
       Vector3 PrevioustPosition = birdOjc.transform.position;
        if (!isAlive) return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            verticalVelocity = jumpForce;
            playFlyMusic.Invoke();
            //audioSource.PlayOneShot(flyClip);
        }
        verticalVelocity -= gravity * Time.deltaTime;
        birdOjc.transform.position += Vector3.up * verticalVelocity * Time.deltaTime;
        Vector3 currentPosition = birdOjc.transform.position;
        checkAngel(PrevioustPosition, currentPosition);
        if (CheckCollision(birdOjc))
        {
            isAlive = false;              
            //audioSource.PlayOneShot(diedClip);
            playDiedMusic.Invoke();
            verticalVelocity = 0;
            Time.timeScale = 0; 
        }
        else
        {
            pipeHolders = GameObject.FindGameObjectsWithTag("PipeHolder");
            if (pipes.Count > 0)
            {

                foreach (GameObject pipeHolder in pipeHolders)
                {
                    Transform pipeTransform = pipeHolder.transform;
                    Renderer pipeRenderer = pipeTransform.GetComponent<Renderer>();
                    float maxX = pipeRenderer.bounds.max.x;
                    Renderer birdRenderer = birdOjc.GetComponent<Renderer>();
                    float BirdmaxX = birdRenderer.bounds.max.x;
                    if (Mathf.Abs(maxX - BirdmaxX) < 0.02f && !hasScored)
                    {
                        hasScored = true;
                        IncreaseScore();
                    }                   
                    if (BirdmaxX > maxX && hasScored)
                    {
                        hasScored = false;
                    }
                }
            }
        }              
        if (Input.GetKeyDown(KeyCode.G) && canPressButton)
        {
            if (birdType.Equals(BirdType.Red))
            {
                IBird.Skill();
            }
            else
            {     
                StartCoroutine(SkillCoolDown());
            }
        }
        
    }
    bool CheckCollision(GameObject birtOjc)
    {
        if (birtOjc.transform.position.y <= -4f)
        {
            isAlive = false;
            birtOjc.transform.position = new Vector3(birtOjc.transform.position.x, -4f, birtOjc.transform.position.z);
            playDiedMusic.Invoke();
            verticalVelocity = 0; 
            Time.timeScale = 0;
        }
 
        if (pipesTest.Length > 0)
        {
            pipesTest = GameObject.FindGameObjectsWithTag(pipeTag);
            float currentSpeed = PipeHolder.instance.GetSpeed();
            foreach (GameObject pipe in pipesTest)
            {
                Renderer birdRenderer = birtOjc.GetComponent<Renderer>();
                Renderer pipeRenderer = pipe.GetComponent<Renderer>();
                Bounds birdBounds = birdRenderer.bounds;
                Bounds pipeBounds = pipeRenderer.bounds;
                if (currentSpeed <= 5)
                {
                    if(birdBounds.Intersects(pipeBounds))
                    {
                        return true;
                    }
                }
            }
        } 
        return false;
    }
    public void IncreaseScore()
    {
        score++;
        playPingMusic.Invoke();
    }    
    public void checkAngel(Vector3 PrevioustPosition , Vector3 currentPosition)
    {
        float angle = Mathf.Lerp(0, (currentPosition.y > PrevioustPosition.y) ? 90 : -90, Mathf.Abs(verticalVelocity) / 9);
        birdOjc.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    IEnumerator SkillCoolDown()
    {
        canPressButton = false;
        IBird.Skill();
        int countdownValue = 5;
        while (countdownValue >= 0)
        {
            countDownText.text = countdownValue.ToString();
            countdownValue--;
            yield return new WaitForSeconds(1f); 
        }
        countDownText.text = "Go";
        canPressButton = true; 
    }
}
