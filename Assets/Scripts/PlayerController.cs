using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class/script controls the player's movement, extralife score, and collision with enemy.
public class PlayerController : MonoBehaviour
{
    public float speed;
    private float verticalInput;
    private float horizontalInput;

    public GameObject[] projectilePrefab;
    private GameManager gameManagerScript;
    public AudioClip bark;
    public AudioClip healthDown;
    public AudioClip healthUp;
    private AudioSource playerAudio;

    private float xRangeRight = 11;
    private float xRangeLeft = -11;
    private float zRangeTop = 5;
    private float zRangeBottom = -5;

    public int pointValue = 1;


    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = GameObject.Find("Game Manager").GetComponent<GameManager>();
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        ShootProjectile();
        PlayerBoundaries();
    }


    // Moves the player with WASD/Arrow Keys
    void MovePlayer()
    {
        if (gameManagerScript.isGameActive == true)
        {
            verticalInput = Input.GetAxis("Vertical");
            horizontalInput = Input.GetAxis("Horizontal");

            transform.Translate(Vector3.forward * Time.deltaTime * speed * verticalInput);
            transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);
        }
        
    }

    // When I press SPACE, fires a random projectile forward
    void ShootProjectile()
    {
        if (Input.GetKeyDown(KeyCode.Space) && gameManagerScript.isGameActive == true)
        {
            int projectileIndex = Random.Range(0, projectilePrefab.Length);
            Instantiate(projectilePrefab[projectileIndex], transform.position, projectilePrefab[projectileIndex].transform.rotation);
        }
    }

    // Created top, bottom, left and right boundaries that the player cannot pass
    void PlayerBoundaries()
    {
        if (transform.position.x > xRangeRight)
        {
            transform.position = new Vector3(xRangeRight, transform.position.y, transform.position.z);
        }

        else if (transform.position.x < xRangeLeft)
        {
            transform.position = new Vector3(xRangeLeft, transform.position.y, transform.position.z);
        }

        if (transform.position.z > zRangeTop)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zRangeTop);
        }

        else if (transform.position.z < zRangeBottom)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zRangeBottom);
        }
    }
        
    // When player collides with "Enemy", game STOPS.
    private void OnCollisionEnter(Collision collision)
    {
          if (collision.gameObject.CompareTag("Enemy"))
            {
                Debug.Log("GameOver!!!");
                gameManagerScript.UpdateHealth(1);
                playerAudio.PlayOneShot(healthDown, 0.2f);
            }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Extra Point"))
        {
            Destroy(other.gameObject);
            playerAudio.PlayOneShot(bark, 1.0f);
            gameManagerScript.UpdateScore(pointValue);
        }

        if (other.gameObject.CompareTag("Extra Life"))
        {
            gameManagerScript.UpdateHealth(-1);
            playerAudio.PlayOneShot(healthUp, 0.2f);
            Destroy(other.gameObject);
        }
    }
}
