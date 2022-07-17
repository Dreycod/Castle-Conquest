using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int PlayerLives = 3;
    [SerializeField] int score = 0;
    [SerializeField] Text scoreText, livesText;
    private void Awake() {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;
        if(numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    
    private void Start()
    {
        livesText.text = PlayerLives.ToString();
        scoreText.text = score.ToString();
    }
    public void AddToScore(int value)
    {
        score += value;
        scoreText.text = score.ToString();
    }
    public void AddLives()
    {
        PlayerLives++;
        livesText.text = PlayerLives.ToString();
    }
    public void ProcessPlayerDeath()
    {
        if(PlayerLives > 1)
        {
            TakeLife();
        }
        else 
        {
            ResetGame();
        }
    }
    private void ResetGame()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    private void TakeLife()
    {
        PlayerLives--;
        livesText.text = PlayerLives.ToString();
    }
}
