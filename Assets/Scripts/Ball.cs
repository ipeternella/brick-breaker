using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    // constants
    private const int MOUSE_PRIMARY_BUTTON = 0;
    
    // fields
    [SerializeField] private Vector2 initialBallSpeed;
    [SerializeField] private float bounceRandomnessFactor = 0.5f;
    [SerializeField] private AudioClip[] bumpAudioClips;
    
    private Paddle _paddle;// tạo 1 paddle
    private Vector2 _initialDistanceToTopOfPaddle;
    private Rigidbody2D _rigidBody2D;
    private AudioSource _audioSource;


    // properties
    public Vector2 InitialBallSpeed { get; set; } //được phép truy cập và sửa
    public Paddle Paddle { get; set; }
    public bool HasBallBeenShot { get; set; } = false;
 
    public void ballInitialPosition()
    {
        var ballPosition = new Vector2 (8 , 1.5f);// dùng var biến toạ độ thành vector
        var paddlePosition =new Vector2 (8 , 0.64f); //vị trí paddle
        _initialDistanceToTopOfPaddle = ballPosition - paddlePosition;  // assumes ball always starts on TOP of the paddle
        _rigidBody2D.velocity = Vector2.zero;
        HasBallBeenShot = false; //kích hoạt nó bay khi click
    }

    private void Awake()
    {
        _paddle = FindObjectOfType<Paddle>(); //cho paddle = 1 đối tượng
        _rigidBody2D = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
        // private void Start() // không ở vị trí khác bởi vì lúc load scene pannel và ball trở về thiết lập tay ban đầu, nếu load level thì đã lưu cứng toạ độ r
        ballInitialPosition();

    }
    
    private void Update()
    {
        // if ball has been shot, no locking or shooting it again!
        if (HasBallBeenShot) return; //true dừng thực hiện các biến, hàm dưới //ban đầu vào game là f và thực hiện hàm dưới 
        
        var hasMouseClick = Input.GetMouseButtonDown(MOUSE_PRIMARY_BUTTON); //biến bool với kết quả khi click
        var paddlePosition = _paddle.transform.position; // 
            
        FixBallOnTopOfPaddle(paddlePosition, _initialDistanceToTopOfPaddle);// vị trí paddle, vị trí bóng trừ vị trí paddle = vector hướng về phía bóng
        ShootBallOnClick(initialBallSpeed, hasMouseClick); //vector hướng bay của ball, khi click thì true
    }
    
    /**
     * Fixes the ball on top of the paddle before the first mouse click.
     */
    public void FixBallOnTopOfPaddle(Vector2 paddlePosition, Vector2 distanceToPaddle) //truyền vị trí paddle ban đầu với khoảng cách ban đầu vào
        //vì mình thiết lập thủ công vị trí pannel với ball ngay trên scene edit đặt ball lên trên pannel nên sẽ lấy vị trí khoảng cách này log đầu game
    {
        transform.position = paddlePosition + distanceToPaddle;
    }
    
    /**
     * Shoots the ball for the first time upon the first mouse click.
     */
    public void ShootBallOnClick(Vector2 initialBallSpeed, bool hasMouseClick)
    {
        if (!hasMouseClick) return; //false dừng thực hiện biến dưới, true ball bay khi click
        
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
        
        return new Vector2(randomVelocityX, randomVelocityY); // trả về kết quả cho hàm
    }
    
    /**
     * Randomly plays ball collision sounds.
     */
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (!HasBallBeenShot) return;  // ball must have been shot first, f khi chưa bắn
        
        var randomBumpAudioIndex = Random.Range(0, bumpAudioClips.Length);
        var signVelocityY = Math.Sign(_rigidBody2D.velocity.y); // âm hoặc dương
        var signVelocityX = Math.Sign(_rigidBody2D.velocity.x);
        
        var correctVelocityY = _rigidBody2D.velocity.y;
        var correctVelocityX = _rigidBody2D.velocity.x;
        
        var bumpAudio = bumpAudioClips[randomBumpAudioIndex];
            
        _audioSource.PlayOneShot(bumpAudio);
        // _rigidBody2D.velocity += GetRandomVelocityBounce();
        //chỉnh lại 2f trước đó là 4
        if (Math.Abs(_rigidBody2D.velocity.y) < 3f) correctVelocityY = 3f * signVelocityY;
        if (Math.Abs(_rigidBody2D.velocity.x) < 3f) correctVelocityX = 3f * signVelocityX;
        
        _rigidBody2D.velocity = new Vector2(correctVelocityX, correctVelocityY);
    }

}
