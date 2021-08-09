using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public List<GameObject> enemies;

    public bool isGameActive = false;

    public GameObject startGameText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public Button restartButton;
    public float spawnRate = 5.0f;
    private int highScore;
    private int score;
    public GameObject player;

    public float zSpawnRange = 12f;
    public float xSpawnRange = 12f;
    //private float minSpawnDistance = 2f;


    // Start is called before the first frame update
    void Start()
    {
        highScoreText.text = "Highscore " + PlayerPrefs.GetInt("Highscore");      
    }

    // Update is called once per frame
    void Update()
    {
        HighScore();
    }

    IEnumerator SpawnEnemy()
    {
        while(isGameActive)
        {
            
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, enemies.Count);
            Instantiate(enemies[index]);        
        }
    }
   
    public void StartGame(int difficulty)
    {
        isGameActive = true;
        spawnRate = spawnRate / difficulty;
        startGameText.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(true);
        highScoreText.gameObject.SetActive(true);
        score = 0;
        scoreText.text = "Score " + score;
        StartCoroutine(SpawnEnemy());
    }
    public void GameOver()
    {
        isGameActive = false;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score " + score;       
    } 

    public void HighScore()
    {
        if (score > PlayerPrefs.GetInt("Highscore"))
        {       
            PlayerPrefs.SetInt("Highscore", score);
            highScoreText.text = "Highscore " + PlayerPrefs.GetInt("Highscore");
        }
    }

    //public RandomSpawnPos()
    //{
        //Vector3 spawnPositon = new Vector3(Random.Range(-xSpawnRange, xSpawnRange), transform.position.y, Random.Range(-zSpawnRange, zSpawnRange));
            //float randomXPos = Random.Range(-xSpawnRange, xSpawnRange);
            //float randomZPos = Random.Range(-zSpawnRange, zSpawnRange);
        //while(Vector3.Distance(spawnPositon, player.transform.position) < 10)
        //{
          //  spawnPositon = new Vector3(Random.Range(-xSpawnRange, xSpawnRange), transform.position.y, Random.Range(-zSpawnRange, zSpawnRange));
        //}
           
    //}
}
