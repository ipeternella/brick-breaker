using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfig : MonoBehaviour
{
    // configurations for the whole game
    [Range(0.5f, 2f)] [SerializeField] private float gameSpeed = 0.8f;
    [SerializeField] public int pointsPerBlock = 100;
    
    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;
    }
}
