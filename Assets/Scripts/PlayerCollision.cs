using System;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    GameManager gameManager;
    AudioManager audioManager;
    private void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        audioManager = FindAnyObjectByType<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            audioManager.PlayCoinSound();
            gameManager.addScore(1);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Trap"))
        {
            gameManager.GameOver();
           
        }
        else if (other.CompareTag("Enemy"))
        {
            gameManager.GameOver();
           
        }
        else if (other.CompareTag("Key"))
        {
            Destroy(other.gameObject);
            gameManager.GameWin();
        }
    }
}
