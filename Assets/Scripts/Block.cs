using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] public AudioClip destroyedBlockSound;
    [SerializeField] public float soundVolume = 0.05f;
    
    // state
    //private GameState gameState;
    private GameConfig gameConfig;
    private LevelController levelController;

    void Start()
    { 
        // game configs
        gameConfig = FindObjectOfType<GameConfig>();

        // selects other game object without SCENE binding: programatically via API
        levelController = FindObjectOfType<LevelController>();

        // every block on the scene, increments it by one 
        levelController.IncrementBlocksCounter();
    }
    
    /**
     * Destroys the block upon a collision. 
     */
    private void OnCollisionEnter2D(Collision2D other)
    {
        DestroyItself();
    }

    /**
     * Upon a collision, the block must be destroyed. Once a block is destroyed, the blocks counter
     * of the level controller must be decremented.
     */    
    private void DestroyItself()
    {
        // adds player points
        var gameState = FindObjectOfType<GameState>();  // singleton
        gameState.AddToPlayerScore(gameConfig.pointsPerBlock);

        // plays destroyed block sound 
        AudioSource.PlayClipAtPoint(destroyedBlockSound, Camera.current.transform.position, soundVolume);
        Destroy(this.gameObject);

        // increments destroyed blocks of the level
        levelController.DecrementBlocksCounter();
    }
    
}
