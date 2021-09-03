using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsManager : MonoBehaviour
{
    #region Singleton
    private static BallsManager instance;

    public static BallsManager Instance => instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    #endregion

    [Header ("Ball Variables")]
    [SerializeField] private Ball ballPrefab;
    [SerializeField] private float ySpawnOffset = 0.5f; // height the ball will spawned above the paddle
    public float initialBallSpeed = 250;
    public float ballHorrizontalVelocity = 200; //applied when hitting the paddle

    private Ball initialBall;
    private Rigidbody2D initialBallRb;

    public List<Ball> Balls { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        InitializeBall();
    }

    // Update is called once per frame
    void Update()
    {
        if(!BreakoutGameManager.Instance.IsGameStarted)
        {
            Vector3 paddlePos = Paddle.Instance.transform.position;
            Vector3 ballPos = new Vector3(paddlePos.x, paddlePos.y + ySpawnOffset, 0);
            initialBall.transform.position = ballPos;

            //launch the ball and start the game
            if (Input.GetMouseButtonDown(0))
            {
                initialBallRb.isKinematic = false;
                initialBallRb.AddForce(new Vector2(0, initialBallSpeed)); //launch the ball straight up
                BreakoutGameManager.Instance.IsGameStarted = true;
            }
        }
    }

    private void InitializeBall()
    {
        Vector3 paddlePos = Paddle.Instance.transform.position;
        Vector3 startingPosition = new Vector3(paddlePos.x, paddlePos.y + ySpawnOffset, 0);
        initialBall = Instantiate(ballPrefab, startingPosition, Quaternion.identity);
        initialBallRb = initialBall.GetComponent<Rigidbody2D>();

        this.Balls = new List<Ball>
        {
            initialBall
        };
    }
}
