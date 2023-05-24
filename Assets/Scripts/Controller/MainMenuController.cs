using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    private string scene = "GamePlay";
    private const string BIRD_KIND = "Bird Kind";
    public void ChooseBird(BirdType birdType)
    {
        GameObject gameObject = new ("Param");
        gameObject.tag = "Param";
        Parameter parameter = gameObject.AddComponent<Parameter>();
        parameter.type = birdType;
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene(scene);
    }
    public void GetBird()
    {
        switch (PlayerPrefs.GetInt(BIRD_KIND))
        {
            case 0:
                ChooseBird(BirdType.Blue);
                break;
            case 1:
                ChooseBird(BirdType.Red);
                break;
            case 2:
                ChooseBird(BirdType.Yellow);
                break;
            default:
                Debug.LogError("Invalid bird type!");
                break;
        }
    }    
}
