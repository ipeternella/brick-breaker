using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameState : MonoBehaviour
{
    
    // state
    [SerializeField] private TextMeshProUGUI playerScoreText;
    [SerializeField] private int playerScore = 0;
    
    // Singleton implementation
    private static GameState _instance;
    public static GameState Instance => _instance;

    // Initialization hook before start
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
    
    // Start is called before the first frame update
    void Start()
    {
        playerScoreText.text = playerScore.ToString();
    }

    /**
     * Updates player score with given points and also updates the UI score.
     */
    public void AddToPlayerScore(int points)
    {
        playerScore += points;
        playerScoreText.text = playerScore.ToString();
    }
}
