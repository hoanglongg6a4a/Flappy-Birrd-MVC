using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModel : MonoBehaviour
{
    #region GAME PLAY
    [Space(8.0f)]
    [Header("Game Play")]
    #endregion

    #region PIPE
    [Header("PIPE STATUS")]
    [Tooltip("Speed Pipe")]
    public float speed = 5;
    [Tooltip("Nunber of Pipe")]
    public int pipeNum = 2;
    #endregion

    #region Bullet
    [Header("BULLET STATUS")]
    [Tooltip("Nunber of Bullet")]
    [Range(5f, 10f)]
    public int bulletNum = 10;
    public int bulletSpeed = 2;
    #endregion

    #region BIRD
    [Header("BIRD STATUS")]
    [Tooltip("Gravity")]
    public float gravity = 9.8f;
    [Tooltip("Bounce Force")]
    public float bounceForce = 5f;
    #endregion
}
