using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    // constants
    private const int MOUSE_PRIMARY_BUTTON = 0;
    
    // fields
    [SerializeField] private Vector2 initialBallSpeed = new Vector2(2f, 10f);
    [SerializeField] private float bounceRandomnessFactor = 0.5f;
    [SerializeField] private AudioClip[] bumpAudioClips;
    
    private Paddle _paddle;
    private Vector2 _initialDistanceToTopOfPaddle;
    private Rigidbody2D _rigidBody2D;
    private AudioSource _audioSource;

    // properties
    public Vector2 InitialBallSpeed { get; set; }
    public Paddle Paddle { get; set; }
    public bool HasBallBeenShot { get; set; } = false;

    private void Start()
    {
        _paddle = FindObjectOfType<Paddle>();
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();

        var ballPosition = transform.position;
        var paddlePosition = _paddle.transform.position;

        _initialDistanceToTopOfPaddle = ballPosition - paddlePosition;  // assumes ball always starts on TOP of the paddle
    }
    
    private void Update()
    {
        // if ball has been shot, no locking or shooting it again!
        if (HasBallBeenShot) return;
        
        var hasMouseClick = Input.GetMouseButtonDown(MOUSE_PRIMARY_BUTTON);
        var paddlePosition = _paddle.transform.position;
            
        FixBallOnTopOfPaddle(paddlePosition, _initialDistanceToTopOfPaddle);
        ShootBallOnClick(initialBallSpeed, hasMouseClick);
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
        if (!hasMouseClick) return;
        
        HasBallBeenShot = true;
        _rigidBody2D.velocity = initialBallSpeed;
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
        if (!HasBallBeenShot) return;  // ball must have been shot first
        
        var randomBumpAudioIndex = Random.Range(0, bumpAudioClips.Length);
        var signVelocityY = Math.Sign(_rigidBody2D.velocity.y);
        var signVelocityX = Math.Sign(_rigidBody2D.velocity.x);
        
        var correctVelocityY = _rigidBody2D.velocity.y;
        var correctVelocityX = _rigidBody2D.velocity.x;
        
        var bumpAudio = bumpAudioClips[randomBumpAudioIndex];
            
        _audioSource.PlayOneShot(bumpAudio);
        // _rigidBody2D.velocity += GetRandomVelocityBounce();

        if (Math.Abs(_rigidBody2D.velocity.y) < 4f) correctVelocityY = 4f * signVelocityY;
        if (Math.Abs(_rigidBody2D.velocity.x) < 4f) correctVelocityX = 4f * signVelocityX;
        
        _rigidBody2D.velocity = new Vector2(correctVelocityX, correctVelocityY);
    }

}
