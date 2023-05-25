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
    public float Speed = 5f;
    [Tooltip("Nunber of Pipe")]
    public int PipeNum = 2;
    #endregion

    #region Bullet
    [Header("BULLET STATUS")]
    [Tooltip("Nunber of Bullet")]
    [Range(5f, 10f)]
    public int BulletNum = 10;
    [Tooltip("Speed Bullet")]
    public int BulletSpeed = 2;
    #endregion

    #region BIRD
    [Header("BIRD STATUS")]
    [Tooltip("Gravity")]
    public float Gravity = 9.8f;
    [Tooltip("Bounce Force")]
    public float BounceForce = 5f;
    #endregion
}
