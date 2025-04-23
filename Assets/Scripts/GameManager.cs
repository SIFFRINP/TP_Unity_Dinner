using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int score { get; private set; }

    [Header("UI")]
    public TextMeshProUGUI scoreText;

    void Awake()
    {
        score = PlayerPrefs.GetInt("Score", 0);
    }

    void Start()
    {
        if (scoreText == null)
        {
            scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        }

        UpdateScore();
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Score", score);
        PlayerPrefs.Save();
    }
    
    public void AddScore(int points)
    {
        score += points;
        UpdateScore();
    }

    private void UpdateScore()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score : " + score;
        }
    }
}
