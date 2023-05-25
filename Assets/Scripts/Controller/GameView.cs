using UnityEngine;
using UnityEngine.UI;
public class GameView : MonoBehaviour
{
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
    private GameObject[] listMedal;
    private const string HIGH_SCORE = "High Score";
    private void Awake()
    {
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
    private void ShowMedal(int score)
    {
        foreach (GameObject medal in listMedal)
        {
            medal.gameObject.SetActive(false);
        }

        if (score <= 5)
        {
            listMedal[0].gameObject.SetActive(true);
        }
        else if (score > 5 && score <= 10)
        {
            listMedal[0].gameObject.SetActive(true);
        }
        else if (score > 10)
        {
            listMedal[0].gameObject.SetActive(true);
        }
    }
}
