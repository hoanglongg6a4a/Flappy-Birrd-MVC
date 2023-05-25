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
    [SerializeField] 
    private Text ScoreText;
    [SerializeField]
    private Button instuctionButton;
    [SerializeField] 
    Image skillCoolDownImage;
    [SerializeField]
    private Text endScoreText, bestScoreText;
    [SerializeField]
    private GameObject gameOverPanel;
    [SerializeField] 
    private Text SkillCoolDown;
    [SerializeField]
    private Image goldMedal, silverMedal, bronzeMedal;
    private GameObject[] medals;
    private int coolDownTime;
    private const string HIGH_SCORE = "High Score";
    private void Awake()
    {
        //PlayerPrefs.SetInt(HIGH_SCORE, 0);
        SkillCoolDown.text = "Go";
    }
    public void SetScore(int score)
    {
        ScoreText.text= score.ToString();
    }
    public void SetTime()
    {
        skillCoolDownImage.fillAmount = 0;
    }   
    public void GetTime()
    {
        skillCoolDownImage.fillAmount += 0.2f * Time.deltaTime;
    }    
    public void BirdDiedShowPanel(int score)
    {  
        gameOverPanel.SetActive(true);
        endScoreText.text = score.ToString();
        ShowMedal(score);
        if (score > GetHighScore())
        {
            SetHighScore(score);
        }
        bestScoreText.text = GetHighScore().ToString();
    }
    public void SetHighScore(int score)
    {
        PlayerPrefs.SetInt(HIGH_SCORE, score);
    }
    public int GetHighScore()
    {
        return PlayerPrefs.GetInt(HIGH_SCORE);
    }
    public void SetSkillCoolDown(int time)
    {
        SkillCoolDown.text = time.ToString();
        if (time == 0)
        {
            SkillCoolDown.text = "Go";
        }
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

  
}
