using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EggScore : MonoBehaviour
{

    [SerializeField]
    private bool isInvader = false;
    private TextMeshPro text;
    private float score = 0;

    Camera cam;
    SoundManager soundManager;

    [SerializeField]
    private List<Color> colors = new List<Color>();

    [SerializeField] private TextMeshProUGUI hiScoreText;
    private int hiScore;


    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshPro>();
        cam = Camera.main;
        soundManager = FindAnyObjectByType<SoundManager>();
        if (isInvader)
            score = 3;

        
        hiScore = GetHiScore();
        UpdateHiScoreText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Score()
    {
        score++;
        UpdateHiScore((int)score);
        text.text = score.ToString();
        if(!isInvader)ChangeColor();
    }

    public void Lose()
    {
        if (isInvader)
        {
            if(score > 5)
                score-=5;
            else if (score > 0)
                score = 0;
            soundManager.PlaySound("Bad");
            text.text = score.ToString();
        }
        else
        {
            score = 0;
            text.text = score.ToString();
            cam.backgroundColor = Color.red;
            soundManager.PlaySound("Bad");
        }
    }

    public void ChangeColor()
    {
        cam.backgroundColor = colors[Random.Range(0, colors.Count)];    
    }

    public void UpdateHiScore(int newScore)
    {
        if (newScore > hiScore)
        {
            hiScore = newScore;
            if (isInvader)
                PlayerPrefs.SetInt("Invader Egg", hiScore);
            else
                PlayerPrefs.SetInt("HighScore Egg", hiScore);

            UpdateHiScoreText();
        }
    }

    private void OnDisable()
    {
        UpdateHiScore((int)score);
    }

    private void UpdateHiScoreText()
    {
        hiScoreText.text = hiScore.ToString();
    }

    private int GetHiScore()
    {
        if (isInvader)
            return PlayerPrefs.GetInt("Invader Egg", 0);
        else
            return PlayerPrefs.GetInt("HighScore Egg", 0);
    }
}
