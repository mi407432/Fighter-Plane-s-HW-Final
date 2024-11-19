using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private float horizontalScreenSize = 11.5f;
    private float verticalScreenSize = 7.5f;
    private float speed;
    private int lives;
    private bool hasShield;

    public GameObject bullet;
    public GameManager gameManager;
    public GameObject thruster;
    public GameObject shield;

    // Start is called before the first frame update
    void Start()
    {
        speed = 6f;
        lives = 3;
        hasShield = false;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        GameObject.Find("GameManager").GetComponent<GameManager>().UpdateLivesText(lives);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Shooting();
    }

    void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * Time.deltaTime * speed);
        if (transform.position.x > horizontalScreenSize || transform.position.x <= -horizontalScreenSize)
        {
            transform.position = new Vector3(transform.position.x * -1, transform.position.y, 0);
        }
        if (transform.position.y > verticalScreenSize || transform.position.y < -verticalScreenSize)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y * -1, 0);
        }
    }

    void Shooting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bullet, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        }
    }

    public void LoseALife()
    {
        if (hasShield == false)
        {
            lives--;
        } else if (hasShield == true)
        {
            //lose the shield
            StartCoroutine(LoseShieldAfterDelay(3f));
            //no longer has a shield
            gameManager.UpdatePowerupText("Shield is gone!");
        }
        //lives -= 1;
        //lives = lives - 1;
        GameObject.Find("GameManager").GetComponent<GameManager>().UpdateLivesText(lives);

        if (lives == 0)
        {
            Destroy(this.gameObject);
        }
    }
    IEnumerator SpeedPowerDown()
    {
        yield return new WaitForSeconds(3f);
        speed = 6f;
        yield return new WaitForSeconds(0.5f);
        gameManager.PlayPowerDown();
        thruster.gameObject.SetActive(false);
    }
    IEnumerator LoseShieldAfterDelay(float delay)
    {
        yield return new WaitForSeconds(3f);
        hasShield = false;
        gameManager.UpdatePowerupText("Shield is gone!");
        yield return new WaitForSeconds(0.5f);
        gameManager.PlayPowerDown();
        shield.gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D whatIHit)
    {
        if(whatIHit.tag == "Powerup")
        {
            gameManager.PlayPowerUp();
            int powerupType = Random.Range(1, 3);
            switch (powerupType)
            {
                case 1:
                    //speedup
                    speed = 9f;
                    StartCoroutine(SpeedPowerDown());
                    gameManager.UpdatePowerupText("Picked up Speed!");
                    thruster.gameObject.SetActive(true);
                    break;
                case 2:
                    //shield
                    gameManager.UpdatePowerupText("Picked up Shield!");
                    hasShield = true;
                    shield.gameObject.SetActive(true);
                    break;
            }
        }
    }
}