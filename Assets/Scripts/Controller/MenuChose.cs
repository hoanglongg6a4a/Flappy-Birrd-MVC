using UnityEngine;
using UnityEngine.UI;

public class MenuChose : MonoBehaviour
{
    [SerializeField]
    private GameObject[] birdPreFab;
    private int index;
    private const string BIRD_KIND = "Bird Kind";
    private void Start()
    {
        index = 0;
        ShowObject();
    }
    private void ShowObject()
    {
        for (int i = 0; i < birdPreFab.Length; i++)
        {
            birdPreFab[i].SetActive(i == index);
            PlayerPrefs.SetInt(BIRD_KIND, index);
        }
    }
    // Update is called once per frame
    private void PreviousButoon()
    {
        birdPreFab[index].SetActive(false);
        index--;
        if (index < 0)
        {
            index = birdPreFab.Length - 1;
        }
        birdPreFab[index].SetActive(true);
        PlayerPrefs.SetInt(BIRD_KIND, index);
    }
    private void NextButoon()
    {
        birdPreFab[index].SetActive(false);
        index++;
        if (index >= birdPreFab.Length)
        {
            index = 0;
        }
        birdPreFab[index].SetActive(true);
        PlayerPrefs.SetInt(BIRD_KIND, index);
    }
}
