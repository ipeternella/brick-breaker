using UnityEngine;

public class Block : MonoBehaviour
{
    
    // configuration
    [SerializeField] public AudioClip destroyedBlockSound;
    [SerializeField] public float soundVolume = 0.05f;
    [SerializeField] public GameObject destroyedBlockParticlesVFX;
    [SerializeField] public int maxHits;
    [SerializeField] public Sprite[] damageSprites;

    // references to other objects
    private LevelController _levelController;
    private Vector3 _soundPosition;

    // state
    private int _currentHits = 0;
    
    void Start()
    {
        // selects other game object without SCENE binding: programatically via API
        _levelController = FindObjectOfType<LevelController>();
        _soundPosition = FindObjectOfType<Camera>().transform.position;

        // increment the block counter if the block's breakable
        if (CompareTag("Breakable")) _levelController.IncrementBlocksCounter();
    }
    
    /**
     * Destroys the block upon a collision. 
     */
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!CompareTag("Breakable")) return;
        
        // increases number of hits and destroy it, if necessary
        _currentHits++;
            
        if (_currentHits < maxHits)
        {
            // Updates sprite image if block has taken too much damage
            UpdateSpriteIfTooDamaged();
        }
        else
        {
            DestroyItself();    
        }
    }
    
    /**
     * Updates the block damage sprite when necessary based on the amount of taken hits.
     */
    private void UpdateSpriteIfTooDamaged()
    {
        var ix = GetDamageSpriteIndex(this._currentHits, this.maxHits, this.damageSprites.Length);

        this.gameObject.GetComponent<SpriteRenderer>().sprite = damageSprites[ix];
    }
    
    /**
     * Calculates the number of required hits to change to the next damage sprite and based on that,
     * returns the sprite damage index of the sprites array for appropriate rendering.
     */
    private int GetDamageSpriteIndex(int currentHits, int totalHits, int numberOfDamageSprites)
    {
        var numberOfRequiredHitsToChangeSprite = totalHits/numberOfDamageSprites;
        var damageSpriteIndex = currentHits / numberOfRequiredHitsToChangeSprite;

        // returns the right dmg sprite or the last one
        if (damageSpriteIndex < numberOfDamageSprites)
        {
            return damageSpriteIndex;
        }
        return numberOfDamageSprites - 1;
    }
    

    /**
     * Upon a collision, the block must be destroyed. Once a block is destroyed, the blocks counter
     * of the level controller must be decremented, the score must be updated and effects played.
     *
     * The score each blocks gives is:
     *
     *  score = baseBlockValue * maxHits
     *
     * Hence, a block that takes 3 hits gives 3x more points than one that takes one hit.
     */    
    private void DestroyItself()
    {
        // adds player points
        var gameState = FindObjectOfType<GameSession>();  // singleton
        gameState.AddToPlayerScore(maxHits);

        // plays VFX and SFX for the destruction
        PlayDestructionEffects();

        // increments destroyed blocks of the level
        _levelController.DecrementBlocksCounter();
    }

    /**
     * Plays VFX and SFX when a block is destroyed.
     */
    private void PlayDestructionEffects()
    {
        // displays the block destruction particles VFX
        ShowDestroyedBlockParticles();

        // plays destroyed block sound SFX
        AudioSource.PlayClipAtPoint(destroyedBlockSound, _soundPosition, soundVolume);
        this.gameObject.SetActive(false);
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
