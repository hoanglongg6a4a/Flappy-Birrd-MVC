using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    string scene = "GamePlay";
    public void ChooseBird(BirdType birdType)
    {
        GameObject gameObject = new ("Param");
        gameObject.tag = "Param";
        Parameter parameter = gameObject.AddComponent<Parameter>();
        parameter.type = birdType;
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene(scene);
    }
    public void ChoseRedBird()
    {
        ChooseBird(BirdType.Red);
    }
    public void ChoseYellowBird()
    {
        ChooseBird(BirdType.Yellow);
    }
    public void ChoseBlueBird()
    {
        ChooseBird(BirdType.Blue);
    }
}
