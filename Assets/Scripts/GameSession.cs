using TMPro;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    // config
    [SerializeField] private TextMeshProUGUI playerScoreText;
    
    // state
    private static GameSession _instance;
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
        if (_instance != null && _instance != this)
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
    }

    /**
     * Update per-frame.
     */
    void Update()
    {
        Time.timeScale = this.GameSpeed;
        playerScoreText.text = this.PlayerScore.ToString();
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
}
