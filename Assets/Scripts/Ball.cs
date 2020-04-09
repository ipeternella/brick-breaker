using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    // configuration based on the UI
    [SerializeField] 
    public Paddle paddle;  // bound to the Paddle object on the UI
    
    [SerializeField] 
    public Vector2 initialBallSpeed = new Vector2(2f, 10f);  // bound to the Paddle object on the UI
    
    // state
    public Vector2 distanceToTopOfPaddle;
    public bool hasBallBeenShot = false;
    
    // inner configuration
    private int MOUSE_PRIMARY_BUTTON = 0;

    // Start is called before the first frame update
    void Start()
    {
        var ballPosition = transform.position;
        var paddlePosition = paddle.transform.position;

        distanceToTopOfPaddle = ballPosition - paddlePosition;  // assumes ball always starts on TOP of the paddle
    }

    // Update is called once per frame
    void Update()
    {
        // attaches ball to the paddle if it hasn't been shot yet 
        if (!hasBallBeenShot)
        {
            FixBallOnTopOfPaddle(paddle.transform.position, distanceToTopOfPaddle);
            ShootBallOnClick(initialBallSpeed);
        }
    }
    
    /**
     * Fixes the ball on top of the paddle before the first mouse click.
     */
    public void FixBallOnTopOfPaddle(Vector2 paddlePosition, Vector2 distanceToPaddle)
    {
        transform.position = paddlePosition + distanceToPaddle;
    }
    
    /**
     * Shoots the ball for the first time upon the first mouse click.
     */
    public void ShootBallOnClick(Vector2 initialBallSpeed)
    {
        if (Input.GetMouseButtonDown(MOUSE_PRIMARY_BUTTON))
        {
            hasBallBeenShot = true;
            gameObject.GetComponent<Rigidbody2D>().velocity = initialBallSpeed;
        }
    }

}
