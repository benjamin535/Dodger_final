using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; 

public class PlayerController : MonoBehaviour
{
    // variables

    private Rigidbody playerRb;
    private int coinsCollected = 0;
    private bool isGrounded = true;

    public float speed = 10f;
    public float jumpForce = 5f;
    public int coinsToWin = 4;
    public GameObject portal;  
 
    // HUD
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI timerText;

    // timer
    public float timeRemaining = 30f;
    private bool timerIsRunning = true;
    //audio
    private AudioSource playerAudio;
    public AudioClip jumpSound;       
    public AudioClip collectSound;  
    public AudioClip powerupSound;  

    // powerup
    private bool hasPowerup = false;   // if player picked YellowDiamond
    private bool usedPowerup = false;  // to track if shield already used


    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        portal.SetActive(false); // hide portal at start
        playerAudio = GetComponent<AudioSource>(); //audio jump and collect
        
        // Save the current scene name as the "last level"
        PlayerPrefs.SetString("LastLevel", SceneManager.GetActiveScene().name);

        

    }
    void Update()// gameplay logic handle player jump, fall off detection, timer countdown
    {
        // Jump (only when grounded)
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            playerAudio.PlayOneShot(jumpSound, 1.0f);//jump audio

        }

        // Timer countdown
        if (timerIsRunning)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerUI();

            if (timeRemaining <= 0)
            {
                timeRemaining = 0;
                timerIsRunning = false;
                SceneManager.LoadScene("GameOver");
            }
        }

        // Fall off map it is GameOver
        if (transform.position.y < -10)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    void FixedUpdate()
    {
        // Get input
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // movement force (ball will roll naturally)
        Vector3 movement = new Vector3(moveX, 0, moveZ);
        playerRb.AddForce(movement * speed);
    }

    private void OnCollisionEnter(Collision collision)  //handles physical collision
    {
        // check if player landed back on the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            
            isGrounded = true;
            }
            // check if player hit an obstacle
            if (collision.gameObject.CompareTag("Obstacle"))
            {   
                // If the player has the shield (YellowDiamond) and not used it yet
                if (hasPowerup && !usedPowerup)

                {
                // Player survives this one collision
                usedPowerup = true;
                hasPowerup = false; // shield consumed
                Debug.Log("Shield absorbed the hit!");
                }
                else
                {
                    // No shield so Game Over
                    SceneManager.LoadScene("GameOver"); 
                }
            
            }
}
    

    void OnTriggerEnter(Collider other)    // detects when the player enters a trigger zone eg: like collecting coins, activating the portal and  picking up powerups.
{
    if (other.CompareTag("Coin"))
    {
        Destroy(other.gameObject); //removes collected coin from scene
        coinsCollected++;
        UpdateCoinUI();  
        playerAudio.PlayOneShot(collectSound, 1.0f);//coin audio

        // tells SpawnManager to spawn a new coin
        FindObjectOfType<SpawnManager>().CoinCollected();
        
        if (coinsCollected >= coinsToWin)
        {
            portal.SetActive(true); //unlock portal after enough coins
        }
    }

    if (other.CompareTag("Portal"))
    {
        // checks which scene player in
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "Level1")
        {
            SceneManager.LoadScene("Level2"); // go to Level 2
        }
        else if (currentScene == "Level2")
        {
            SceneManager.LoadScene("Win"); // finish the game
        }
    }
    if (other.CompareTag("YellowDiamond"))  
    {
        Destroy(other.gameObject);
        hasPowerup = true;  //activates shield
        usedPowerup = false; // reset when hitting 
        playerAudio.PlayOneShot(powerupSound, 1.0f); 
    }
    
}
   //updates HUD
     void UpdateCoinUI()  
    {
        coinText.text = "Coins: " + coinsCollected + "/" + coinsToWin;
    }

    void UpdateTimerUI()
    {
        timerText.text = "Time: " + Mathf.Ceil(timeRemaining);      //Mathf.Ceil() makes it a whole number rounded up  
    }
}
