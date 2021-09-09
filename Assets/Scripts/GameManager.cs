using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // State that game start or not
    public bool gameStarted;
    // Create scores
    public int score;
    public Text scoreText;
    public Text highscoreText;

    // High Score will appear in the first place
    private void Awake()
    {
        highscoreText.text = "Best: " + GetHighScore().ToString();
    }

    public void StartGame()
    {
        // Initialize at the beginning of the game
        gameStarted = true;
        // Start to build new road
        FindObjectOfType<Road>().StartBuilding();
    }

    // Function to start the game when user "enter"
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartGame();
        }
    }

    // When end game, the scene will restart at the beginning
    public void EndGame()
    {
        SceneManager.LoadScene(0);
    }

    // Function to store and increase scores
    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
        // Update high score
        if (score > GetHighScore())
        {
            PlayerPrefs.SetInt("Highscore", score);
            highscoreText.text = "Best: " + score.ToString();
        }
    }
    
    // Function to get high score
    public int GetHighScore()
    {
        int i = PlayerPrefs.GetInt("Highscore");
        return i;
    }
}

