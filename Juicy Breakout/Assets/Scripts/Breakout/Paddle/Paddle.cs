using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    #region Singleton
    private static Paddle instance;

    public static Paddle Instance => instance;

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

    private Transform t;
    private Camera mainCamera;
    private SpriteRenderer sr;

    public float leftClamp, rightClamp;
    private float defaultLeftClamp = 90.83893f;
    private float defaultRightClamp = 1829.161f;

    private float yStartPos;
    private float halfWidth;
    private float halfPaddleSize;
    private float defaultPaddleWidth = 10;
    
    public float screenBoarderOffset = 100; // offset from screen boarder

    void Start()
    {
        t = this.transform;
        mainCamera = FindObjectOfType<Camera>();
        sr = this.GetComponent<SpriteRenderer>();
        yStartPos = t.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        //halfWidth = sr.bounds.size.x/2;
        halfPaddleSize = sr.bounds.size.x * sr.sprite.pixelsPerUnit / 2;

        //only move the paddle if the game is not paused
        if (!BreakoutUIManager.Instance.paused)
            PaddleMovement();
    }

    void PaddleMovement()
    {
        leftClamp = 0 + halfPaddleSize;
        rightClamp = Screen.width - halfPaddleSize;

        float clampedMouseXPos = Mathf.Clamp(Input.mousePosition.x, leftClamp, rightClamp);
        float mouseXPosition = mainCamera.ScreenToWorldPoint(new Vector3(clampedMouseXPos, 0, 0)).x; //sets the current cursor location as 0 and then tracks the x value
        t.position = new Vector3(mouseXPosition, yStartPos, 0); //moves the paddle on the x axis to match the cursor's x value
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            Rigidbody2D ballRb = collision.gameObject.GetComponent<Rigidbody2D>();   // Get the balls rigidbody
            Vector3 hitPosition = collision.contacts[0].point;                       // The location the ball hits the paddle
            Vector3 paddleCenter = new Vector3(t.position.x, t.position.y);          // Get the middle of the paddle

            ballRb.velocity = Vector2.zero;

            float difference = paddleCenter.x - hitPosition.x; //get the distance from the center that the ball hit the paddle

            //if the ball hit the left side of the paddle
            if (hitPosition.x < paddleCenter.x)
            {
                ballRb.AddForce(new Vector2(-(Mathf.Abs(difference * BallsManager.Instance.ballHorrizontalVelocity)), BallsManager.Instance.initialBallSpeed));
            }
            else
            {
                ballRb.AddForce(new Vector2((Mathf.Abs(difference * BallsManager.Instance.ballHorrizontalVelocity)), BallsManager.Instance.initialBallSpeed));
            }
        }
    }
}
