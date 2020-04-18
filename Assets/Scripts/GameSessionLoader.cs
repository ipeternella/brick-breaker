using UnityEngine;

public class GameSessionLoader : MonoBehaviour
{
    // state
    private GameSession _gameSession;
    private GameConfig _gameConfig;
    
    /**
     * Before first frame.
     */
    void Start()
    {
        // obtains game object and class singletons
        this._gameSession = FindObjectOfType<GameSession>();  
        this._gameConfig = GameConfig.Instance; 
        
        StartGameSession();
    }

    /**
     * Starts a game session from scratch (based only on the game mode options). In the future,
     * this method can be used to start a game session from a saved game file.
     */
    private void StartGameSession()
    {
        var gameModeConfig = this._gameConfig.GetGameModeConfig();

        this._gameSession.PlayerLives = (int) gameModeConfig["playerLives"];
        this._gameSession.PointsPerBlock = (int) gameModeConfig["pointsPerBlock"];
        this._gameSession.GameSpeed = (float) gameModeConfig["gameSpeed"];
        this._gameSession.PlayerScore = (int) gameModeConfig["playerScore"];
    }
}
