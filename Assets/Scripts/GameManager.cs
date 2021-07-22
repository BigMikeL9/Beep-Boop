using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// This class/script controls the state of the game. The StartGame, GameOver, Restart Game, score and enemy/extra life spawns.
public class GameManager : MonoBehaviour
{
    public GameObject[] enemyTopPrefab;
    public GameObject[] enemyBottomPrefabs;
    public GameObject[] enemyLeftPrefabs;
    public GameObject[] enemyRightPrefabs;
    public GameObject extraPointPrefab;
    public GameObject extraLifePrefab;
    public GameObject titleScreen;
    public Button restartButton;
    public TextMeshProUGUI gamerOverText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI healthText;
    private PlayerController playerControllerScript;

    public int extraPointCount;
    public int extraHealthCount;
    private int score;
    private int health = 3;

    private float spawnInterval = 1;
    private float spawnDelayLeft = 2.0f;
    private float spawnDelayTop = 0.3f;
    private float spawnDelayRight = 2.0f;
    private float spawnDelayBottom = 0.7f;
    private float healthSpawnInterval = 10.0f;
    private float healthSpawnDelay = 10.0f;

    public bool isGameActive = false;

    
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
  
    }

    // Update is called once per frame
    void Update()
    {
        extraPointCount = GameObject.FindGameObjectsWithTag("Extra Point").Length;
        extraHealthCount = GameObject.FindGameObjectsWithTag("Extra Life").Length;
        SpawnExtraPoint();
        GameOver();
    }

    public void StartGame(int difficulty)
    {
        isGameActive = true;
        score = 0;
        UpdateScore(0);
        UpdateHealth(0);
        if (difficulty == 1)
        {
            InvokeRepeating("SpawnEnemyTop", spawnInterval, spawnDelayTop);
        }

        if (difficulty == 2)
        {
            InvokeRepeating("SpawnEnemyTop", spawnInterval, spawnDelayTop);
            InvokeRepeating("SpawnEnemyBottom", spawnInterval, spawnDelayBottom);
        }

        if (difficulty == 3)
        {
            InvokeRepeating("SpawnEnemyTop", spawnInterval, spawnDelayTop);
            InvokeRepeating("SpawnEnemyBottom", spawnInterval, spawnDelayBottom);
            InvokeRepeating("SpawnEnemyLeft", spawnInterval, spawnDelayLeft);
            InvokeRepeating("SpawnEnemyRight", spawnInterval, spawnDelayRight);
        }
        titleScreen.SetActive(false);

        InvokeRepeating("SpawnExtraLife", healthSpawnInterval, healthSpawnDelay);
       
    }

    public void GameOver()
    {
        if (health <= 0)
        {
            gamerOverText.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
            isGameActive = false;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void SpawnExtraPoint()
    {
        if (extraPointCount == 0 && isGameActive == true)
        {
            Vector3 extraPointSpawnPos = new Vector3(Random.Range(-6, 6), transform.position.y, Random.Range(-4, 4));
            Instantiate(extraPointPrefab, extraPointSpawnPos, extraPointPrefab.transform.rotation);
        }
    }


    void SpawnExtraLife()
    {
        if (extraHealthCount == 0 && isGameActive == true)
        {
            Vector3 extraPointLifePos = new Vector3(Random.Range(-6, 6), transform.position.y, Random.Range(-4, 4));
            Instantiate(extraLifePrefab, extraPointLifePos, extraLifePrefab.transform.rotation);
        } 
    }


    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void UpdateHealth(int healthToMinus)
    {
        health -= healthToMinus;
        healthText.text = "Health: " + health;
    }

    void SpawnEnemyLeft()
    {
        int enemyIndex = Random.Range(0, enemyLeftPrefabs.Length);
        Vector3 spawnPos = new Vector3(-14, transform.position.y, Random.Range(-6, 6));

        Instantiate(enemyLeftPrefabs[enemyIndex], spawnPos, enemyLeftPrefabs[enemyIndex].transform.rotation);
        
    }

    void SpawnEnemyTop()
    {
        int enemyIndex = Random.Range(0, enemyTopPrefab.Length);
        Vector3 spawnPos = new Vector3(Random.Range(-13, 14), transform.position.y, 7);

        Instantiate(enemyTopPrefab[enemyIndex], spawnPos, enemyTopPrefab[enemyIndex].transform.rotation);

    }

    void SpawnEnemyRight()
    {
        int enemyIndex = Random.Range(0, enemyRightPrefabs.Length);
        Vector3 spawnPos = new Vector3(14, transform.position.y, Random.Range(-6, 6));

        Instantiate(enemyRightPrefabs[enemyIndex], spawnPos, enemyRightPrefabs[enemyIndex].transform.rotation);

    }

    void SpawnEnemyBottom()
    {
        int enemyIndex = Random.Range(0, enemyBottomPrefabs.Length);
        Vector3 spawnPos = new Vector3(Random.Range(-13, 14), transform.position.y, -7);

        Instantiate(enemyBottomPrefabs[enemyIndex], spawnPos, enemyBottomPrefabs[enemyIndex].transform.rotation);

    }


}
