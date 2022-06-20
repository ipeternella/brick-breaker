using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    private Rigidbody2D rigidbodyPotion;
    // Start is called before the first frame update
    void Start()
    {
        rigidbodyPotion = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        rigidbodyPotion.AddForce(Vector3.down * 0.5f * rigidbodyPotion.mass);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "LoseCollider")
        {
            Destroy(this.gameObject);
        }  
        if (collision.gameObject.name.StartsWith("Paddle"))
        {
            Destroy(this.gameObject);
        }

        
    }    
}
