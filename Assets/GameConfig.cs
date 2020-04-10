using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfig : MonoBehaviour
{
    [Range(0.5f, 2f)] [SerializeField] private float gameSpeed = 0.8f;

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;
    }
}
