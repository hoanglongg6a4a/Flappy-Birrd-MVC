using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBird : MonoBehaviour
{
    [SerializeField]
    private GameObject redBird, yellowBird, blueBird;
    private Bird IBird;
    private BirdType birdType;
    private string KindBird = "BirdType";
    private GameObject birdOjc;
    private Vector2 spawnPosition = new Vector2(-1.5f, 0f);
    private GameObject bird;
    void Start()
    {
       
    }
    public GameObject ChooseBird()
    {
        string birdTypeString = PlayerPrefs.GetString(KindBird);
        birdType = (BirdType)Enum.Parse(typeof(BirdType), birdTypeString);
        switch (birdType)
        {
            case BirdType.Yellow:
                bird = Instantiate(yellowBird, spawnPosition, Quaternion.identity);
                return bird;
                break;
            case BirdType.Red:
       
                 bird = Instantiate(redBird, spawnPosition, Quaternion.identity);
                return bird;
                break;
            case BirdType.Blue:
                 bird = Instantiate(blueBird, spawnPosition, Quaternion.identity);
                return bird;
                break;
            default:
                Debug.LogError("Invalid bird type!");
                break;
        }
        return bird;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
