using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] public AudioClip destroyedBlockSound;
    [SerializeField] public float soundVolume = 0.05f;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        AudioSource.PlayClipAtPoint(destroyedBlockSound, Camera.current.transform.position, soundVolume);
        Destroy(this.gameObject);
    }
}
