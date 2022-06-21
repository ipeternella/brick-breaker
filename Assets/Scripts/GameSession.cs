using TMPro;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    // các hiển thị mạng số điểm level màn khi runtime
    // config
    [SerializeField] private TextMeshProUGUI playerScoreText;
    [SerializeField] private TextMeshProUGUI gameLevelText;
    [SerializeField] private TextMeshProUGUI playerLivesText;

    // state
    private static GameSession _instance; // không truyền được
    public static GameSession Instance => _instance; //công khai nên truyền lên trên nếu được truy cập và tuỳ chỉnh

    public int GameLevel { get; set; } // không suất hiện trên ínspector
    public int PlayerScore { get; set; }
    public int PlayerLives { get; set; }
    public int PointsPerBlock { get; set; }
    public float GameSpeed { get; set; }
    
    /**
     * Singleton implementation.
     */
    private void Awake() 
    { 
        // this is not the first instance so destroy it!
        if (_instance != null && _instance != this) //
        { 
            Destroy(this.gameObject);
            return;
        }
        
        // first instance should be kept and do NOT destroy it on load
        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    
    /**
     * Before first frame.
     */
    void Start()
    {
        playerScoreText.text = this.PlayerScore.ToString();
        gameLevelText.text = this.GameLevel.ToString();
        playerLivesText.text = this.PlayerLives.ToString(); //truyền vào text
        StartGameSession();
    }

    /**
     * Update per-frame.
     */
    void Update()
    {
        Time.timeScale = this.GameSpeed;
        
        // UI updates
        playerScoreText.text = this.PlayerScore.ToString();
        gameLevelText.text = this.GameLevel.ToString();
        playerLivesText.text = this.PlayerLives.ToString();
    }

    /**
     * Updates player score with given points and also updates the UI score. The total points that are
     * calculated is based on the basis value (this.PointsPerBlock).
     */
    public void AddToPlayerScore(int blockMaxHits)
    {
        this.PlayerScore += blockMaxHits * this.PointsPerBlock;
        playerScoreText.text = this.PlayerScore.ToString();
    }

    private void StartGameSession() //load snece khi bắt đầu game lấy thông tin từ gameConfib truyền qua gameSession
    {
        var gameModeConfig = GameConfig.Instance.GetGameModeConfig();

        this.PlayerLives = (int)gameModeConfig["playerLives"]; //live
        this.PointsPerBlock = (int)gameModeConfig["pointsPerBlock"]; // điểm khi block
        this.GameSpeed = (float)gameModeConfig["gameSpeed"]; //speed ball
        this.PlayerScore = (int)gameModeConfig["playerScore"];//
        this.GameLevel = (int)gameModeConfig["gameLevel"];
    }
}
