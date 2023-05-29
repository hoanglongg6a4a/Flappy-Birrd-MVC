using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuController : MonoBehaviour
{
    private string scene = "GamePlay";
    private const string BIRD_KIND = "Bird Kind";
    private string tag = "Param";
    private void ChooseBird(BirdType birdType)
    {
        GameObject gameObject = new (tag);
        gameObject.tag = tag;
        Parameter parameter = gameObject.AddComponent<Parameter>();
        parameter.type = birdType;
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene(scene);
    }
    private void GetBird()
    {
        ChooseBird((BirdType)PlayerPrefs.GetInt(BIRD_KIND));
    }    
}
