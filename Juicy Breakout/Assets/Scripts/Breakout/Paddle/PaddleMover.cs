using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleMover : MonoBehaviour
{
    private Transform t;
    private Camera mainCamera;
    private SpriteRenderer sr;

    public float leftClamp, rightClamp;

    private float yStartPos;
    private float halfWidth;
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
        halfWidth = sr.bounds.size.x;

        PaddleMovement();
    }

    void PaddleMovement()
    {
        leftClamp = 0 + halfWidth + screenBoarderOffset;
        rightClamp = Screen.width - halfWidth - screenBoarderOffset;


        float clampedMouseXPos = Mathf.Clamp(Input.mousePosition.x, leftClamp, rightClamp);
        //Debug.Log("Mouse Position test for clamp values" + clampedMouseXPos);

        float mouseXPosition = mainCamera.ScreenToWorldPoint(new Vector3(clampedMouseXPos, 0, 0)).x; //sets the current cursor location as 0 and then tracks the x value
        t.position = new Vector3(mouseXPosition, yStartPos, 0); //moves the paddle on the x axis to match the cursor's x value
    }
}
