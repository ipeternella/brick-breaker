using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

public class Block : MonoBehaviour
{
    [SerializeField] public AudioClip destroyedBlockSound;
    [SerializeField] public float soundVolume = 0.05f;
    [SerializeField] public GameObject destroyedBlockParticlesVFX;
    
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

        // plays VFX and SFX for the destruction
        PlayDestructionEffects();

        // increments destroyed blocks of the level
        levelController.DecrementBlocksCounter();
    }

    /**
     * Plays VFX and SFX when a block is destroyed.
     */
    private void PlayDestructionEffects()
    {
        // displays the block destruction particles VFX
        ShowDestroyedBlockParticles();

        // plays destroyed block sound SFX
        AudioSource.PlayClipAtPoint(destroyedBlockSound, Camera.current.transform.position, soundVolume);
        Destroy(this.gameObject);
    }

    /**
     * Method to render destroyed blocks particles system VFX.
     */
    private void ShowDestroyedBlockParticles()
    {
        // using Unity's API to instantiate a new GameObject -- the particles VFX
        Vector3 blockPosition = this.transform.position;
        Quaternion blockRotation = this.transform.rotation;
        
        GameObject destroyedBlockParticles = Instantiate(destroyedBlockParticlesVFX, blockPosition, blockRotation);
    }
}
