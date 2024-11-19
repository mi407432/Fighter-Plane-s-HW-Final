using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject player;
    public GameObject enemyOne;
    public GameObject cloud;
    public GameObject coinPrefab;
    public GameObject powerup;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI powerupText;
    public int score;

    public AudioClip powerUp;
    public AudioClip powerDown;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(player, transform.position, Quaternion.identity);
        InvokeRepeating("CreateEnemyOne", 1f, 3f);
        CreateSky();
        score = 0;
        scoreText.text = "Score: " + score;
        UpdateLivesText(3);
        InvokeRepeating("SpawnCoin", 2f, 5f);
        StartCoroutine(CreatePowerUp());
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CreateEnemyOne()
    {
        Instantiate(enemyOne, new Vector3(Random.Range(-9f, 9f), 7.5f, 0), Quaternion.Euler(0, 0, 180));
    }
    IEnumerator CreatePowerUp()
    {
        Instantiate(powerup, new Vector3(Random.Range(-9f, 9f), 7.5f, 0), Quaternion.identity);
        yield return new WaitForSeconds(Random.Range(2f, 6f));
        StartCoroutine(CreatePowerUp());
    }
    void CreateSky()
    {
        for (int i = 0; i < 30; i++)
        {
            Instantiate(cloud, transform.position, Quaternion.identity);
        }
    }
    public void EarnScore(int newScore)
    {
        score = score + newScore;
        scoreText.text = "Score: " + score;
    }
    public void UpdateLivesText(int lives)
    {
        livesText.text = "Lives: " + lives;
    }
    void SpawnCoin()
    {
        // Spawn the coin at a random position within screen bounds
        Vector3 coinPosition = new Vector3(Random.Range(-9f, 9f), Random.Range(-7f, 7f), 0);
        Instantiate(coinPrefab, coinPosition, Quaternion.identity);
    }

    public void UpdatePowerupText(string whichPowerup)
    {
        powerupText.text = whichPowerup;
    }

    public void PlayPowerUp()
    {
        AudioSource.PlayClipAtPoint(powerUp, Camera.main.transform.position);
    }

    public void PlayPowerDown()
    {
        AudioSource.PlayClipAtPoint(powerDown, Camera.main.transform.position);
    }
}