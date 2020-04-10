using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    // UI elements
    [SerializeField] int blocksCounter;

    // state
    private SceneLoader _sceneLoader;
    
    private void Start()
    {
        _sceneLoader = FindObjectOfType<SceneLoader>();
    }

    public void IncrementBlocksCounter()
    {
        blocksCounter++;
    }
    
    public void DecrementBlocksCounter()
    {
        blocksCounter--;

        if (blocksCounter <= 0)
        {
            _sceneLoader.LoadNextScene();
        }
    }
    
}
