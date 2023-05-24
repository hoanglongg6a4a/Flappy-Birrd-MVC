using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuChose : MonoBehaviour
{
    [SerializeField]
    private Button previous, next, blue, red, yellow;
    private GameObject[] buttons;
    public GameObject[] birdPreFab;
    private int index;
    private const string BIRD_KIND = "Bird Kind";

    void Start()
    {
        index = 0;
        buttons = new GameObject[] { blue.gameObject, red.gameObject, yellow.gameObject };
        ShowObject();
    }
    private void ShowObject()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            birdPreFab[i].SetActive(i == index);
            PlayerPrefs.SetInt(BIRD_KIND, index);
        }
    }
    // Update is called once per frame
    public void PreviousButoon()
    {
        birdPreFab[index].SetActive(false);
        index--;
        if (index < 0)
        {
            index = buttons.Length - 1;
        }
        birdPreFab[index].SetActive(true);
        PlayerPrefs.SetInt(BIRD_KIND, index);
    }
    public void NextButoon()
    {
        birdPreFab[index].SetActive(false);
        index++;
        if (index >= buttons.Length)
        {
            index = 0;
        }
        birdPreFab[index].SetActive(true);
        PlayerPrefs.SetInt(BIRD_KIND, index);
    }
}
