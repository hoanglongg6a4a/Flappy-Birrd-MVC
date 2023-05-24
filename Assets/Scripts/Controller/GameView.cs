using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    private int score = 0;
    [SerializeField] private Text ScoreText;
    [SerializeField]
    private Button instuctionButton;
    [SerializeField]
    private Text scoreText, endScoreText, bestScoreText;
    [SerializeField]
    private GameObject gameOverPanel;
    [SerializeField] 
    private Text SkillCoolDown;
    [SerializeField]
    private Image goldMedal, silverMedal, bronzeMedal;
    private GameObject[] medals;
    private int coolDownTime;

    public void SetScore(int score)
    {
        ScoreText.text= score.ToString();
    }
    public void BirdDiedShowPanel(int score , bool Alive)
    {
        if (Alive != true)
        {
            gameOverPanel.SetActive(true);
            endScoreText.text = score.ToString();
            ShowMedal(score);
        }
    }
    public void SetSkillCoolDown(int time)
    {
        Debug.Log("In View");
        SkillCoolDown.text = time.ToString();
    }    

    public void ShowMedal(int score)
    {
        medals = new GameObject[] { bronzeMedal.gameObject, silverMedal.gameObject, goldMedal.gameObject };
        foreach (GameObject medal in medals)
        {
            medal.gameObject.SetActive(false);
        }

        if (score <= 5)
        {
            medals[0].gameObject.SetActive(true);
        }
        else if (score > 5 && score <= 10)
        {
            medals[1].gameObject.SetActive(true);
        }
        else if (score > 10)
        {
            medals[2].gameObject.SetActive(true);
        }
    }
    public void MenuButton()
    {
        SceneManager.LoadScene("GameMenu");
    }
    public void RestartGameButton()
    {
        SceneManager.LoadScene("GamePlay");
    }
    public int GetScore()
    {
        return score;
    }

    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
