using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager i;

    [SerializeField] bool levelPassed;
    [SerializeField] bool gameOver;
    [SerializeField] int numberOfBricks;
    [SerializeField] int numberOfLives = 2;
    [SerializeField] int currentScore = 0;
    [SerializeField] int currentLevel = 0;


    [SerializeField] TMP_Text livesText;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] Transform gameOverPanel;
    [SerializeField] Transform loadLevelPanel;

    [SerializeField] Ball mainBall;
    [SerializeField] List<GameObject> allLevels;
    GameObject currentLevelObject;
    GameObject[] allBricks;    
    
    private void Awake()
    {
        if(i == null)
        {
            i = this;
        }
        else
        {
            Destroy(i);
        }
    }

    private void Start()
    {
        LoadLevel();

        livesText.text = "Lives: " + numberOfLives;
        scoreText.text = "Score: " + currentScore;
    }

    void CountInitialBricks()
    {
        allBricks = GameObject.FindGameObjectsWithTag("Brick");

        for (int i = 0; i < allBricks.Length; i++)
        {
            var infiniteBrick = allBricks[i].GetComponent<InfiniteBrick>();

            if (!infiniteBrick)
                numberOfBricks++;
        }
    }
 
    public void UpdateNumberOfBricks()
    {
        numberOfBricks--;

        if(numberOfBricks <= 0)
        {
            LevelCleared();

            if(currentLevel < allLevels.Count)
            {
                Invoke("LoadLevel", 3f);
            }
            else
            {
                //GAME OVER
            }
        }
    }

    private void LoadLevel()
    {
        currentLevelObject = Instantiate(allLevels[currentLevel], Vector2.zero, Quaternion.identity);
        CountInitialBricks();
        levelPassed = false;
        loadLevelPanel.gameObject.SetActive(false);
    }

    private void LevelCleared()
    {
        levelPassed = true;
        CleanupLevel();

        currentLevel++;
        loadLevelPanel.gameObject.SetActive(true);
        loadLevelPanel.GetComponentInChildren<TMP_Text>().text = "Load Level " + (currentLevel + 1);
        
        mainBall.ResetBall();
        //Destroy(allLevels[currentLevel].gameObject);
    }

    public void UpdateNumberOfLives(int value = -1)
    {
        numberOfLives += value;
        livesText.text = "Lives: " + numberOfLives;

        if (numberOfLives == 0)
        {
            //game over
            GameOver();
        }
    }

    public void UpdateScore(int value)
    {
        currentScore += value;
        scoreText.text = "Score: " + currentScore;
    }

    void CleanupLevel()
    {
        print("Cleaning");
        currentLevelObject.SetActive(false);
    }

    void GameOver()
    {
        gameOver = true;
        gameOverPanel.gameObject.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("GameScene");
    }

}
