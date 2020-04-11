using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{

    // configuration based on the UI
    [SerializeField] public Paddle paddle;  // bound to the Paddle object on the UI
    [SerializeField] public Vector2 initialBallSpeed = new Vector2(2f, 10f);  // bound to the Paddle object on the UI
    [SerializeField] public AudioClip[] bumpAudioClips;  // audio clips defined on the UI of the scene
    [SerializeField] public float bounceRandomnessFactor = 0.5f;
    
    // state
    private Rigidbody2D _rigidBody2D;
    private AudioSource _audioSource;
    public Vector2 distanceToTopOfPaddle;
    public bool hasBallBeenShot = false;

    // inner configuration
    private int MOUSE_PRIMARY_BUTTON = 0;

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        
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
            // checks for mouse clicks
            bool hasMouseClick = Input.GetMouseButtonDown(MOUSE_PRIMARY_BUTTON);
            
            FixBallOnTopOfPaddle(paddle.transform.position, distanceToTopOfPaddle);
            ShootBallOnClick(initialBallSpeed, hasMouseClick);
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
    public void ShootBallOnClick(Vector2 initialBallSpeed, bool hasMouseClick)
    {
        if (hasMouseClick)
        {
            hasBallBeenShot = true;
            _rigidBody2D.velocity = initialBallSpeed;
        }
    }

    /**
     * Computes a random vector to add to the ball's velocity vector in order to avoid
     * repetitive ball collisions throughout the game.
     */
    public Vector2 GetRandomVelocityBounce()
    {

        var randomVelocityX = Random.Range(0, this.bounceRandomnessFactor);
        var randomVelocityY = Random.Range(0, this.bounceRandomnessFactor);
        
        return new Vector2(randomVelocityX, randomVelocityY);
    }
    
    /**
     * Randomly plays ball collision sounds.
     */
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (hasBallBeenShot)
        {
            var randomBumpAudioIndex = Random.Range(0, bumpAudioClips.Length);
            AudioClip bumpAudio = bumpAudioClips[randomBumpAudioIndex];
            
            _audioSource.PlayOneShot(bumpAudio);
            _rigidBody2D.velocity += GetRandomVelocityBounce();
        }
    }
}
