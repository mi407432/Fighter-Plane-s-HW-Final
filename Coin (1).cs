using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private float lifespan = 3f; // Time before the coin disappears

    void Start()
    {
        // Destroy the coin after a few seconds if not collected
        Destroy(gameObject, lifespan);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Award score to the player
            GameObject.Find("GameManager").GetComponent<GameManager>().EarnScore(1);
            Destroy(gameObject); // Destroy the coin when collected
        }
    }
}