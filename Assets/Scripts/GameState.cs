using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    // state
    [SerializeField] private int playerScore = 0;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void AddToPlayerScore(int points)
    {
        playerScore += points;
    }
}
