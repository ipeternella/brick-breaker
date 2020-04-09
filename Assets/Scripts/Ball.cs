using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    [SerializeField] 
    public Paddle paddle;  // bound to the Paddle object on the UI
    public Vector2 distanceToPaddle;
    
    // Start is called before the first frame update
    void Start()
    {
        var ballPosition = transform.position;
        var paddlePosition = paddle.transform.position;

        distanceToPaddle = ballPosition - paddlePosition;
    }

    // Update is called once per frame
    void Update()
    {
        var paddlePosition = paddle.transform.position;
        transform.position = new Vector2(distanceToPaddle.x + paddlePosition.x, distanceToPaddle.y + paddlePosition.y);
    }
}
