using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool gameFinished;

    private int score, hiScore;

    [SerializeField]
    private TextMeshProUGUI scoreText, hiScoreText;
    
    [SerializeField]
    private GameObject RestartButton;

    public void SetHasGameFinished(bool boo)
    {
        gameFinished = boo;

        if (boo)
        {
            RestartButton.SetActive(true);
            if(score > hiScore)
            {
                hiScore = score;
                PlayerPrefs.SetInt("High Score", hiScore);
            }

        }
            
    }

    public bool GetHasGameFinished()
    {
        return gameFinished;
    }

    private void Awake()
    {
        hiScore = GetHiScore();
        hiScoreText.text = "Hi: " + hiScore.ToString();
        RestartButton.SetActive(false);


    }
    private void Update()
    {
        
    }
    public void UpdateScore()
    {
        score += 1;
        scoreText.text = score.ToString();
    }

    private int GetHiScore()
    {
        int n = PlayerPrefs.GetInt("High Score", 0);
        return n;
    }
    public void Restart()
    {
        SceneManager.LoadScene("FlappyChicken");
    }

    public void RotateSkybox()
    {
        
    }

}
