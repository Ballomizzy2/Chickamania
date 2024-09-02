using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ball : MonoBehaviour
{
    Rigidbody rb;
    public float forceMagnitude = 10.0f;
    public float maxVelocity = 50.0f;


    private int playerScore, enemyScore;
    private int enemyAILevel = 1;

    private SoccerAI soccerAI;


    //UI
    [SerializeField]
    private TextMeshPro score, aiLevel;

    SoundManager soundManager;

    [SerializeField] private TextMeshPro hiScoreText;
    private int hiScore;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        soccerAI = FindAnyObjectByType<SoccerAI>();
        //rb.velocity = transform.forward * constantSpeed;
        score.text = playerScore + "-" + enemyScore;
        aiLevel.text = "Level " + soccerAI.GetLevel() + " AI";
        soundManager = FindAnyObjectByType<SoundManager>();


        hiScore = GetHiScore();
        UpdateHiScoreText();
    }

    // Update is called once per frame
    void Update()
    {
        // Apply a constant force in the direction of the ball's velocity
        /*if (rb.velocity.magnitude > 0)
        {
            rb.AddForce(rb.velocity.normalized * forceMagnitude, ForceMode.Force);
        }*/
        //   rb.velocity = rb.velocity.normalized * constantSpeed;


        

        if(playerScore >= 3)
        {
            soccerAI.LevelUp();
            playerScore = 0;
            enemyScore = 0;
            LevelUp();
        }

        else if (enemyScore >= 3)
        {
            soccerAI.LevelDown();
            playerScore = 0;
            enemyScore = 0;
            LevelDown();
        }
    }
    private void OnDisable()
    {
        UpdateHiScore(soccerAI.GetLevel());
    }
    private void FixedUpdate()
    {
        Debug.Log(rb.velocity.magnitude);
        //Clamp velocity
        if (rb.velocity.magnitude > maxVelocity)
        {
            rb.velocity = rb.velocity.normalized * maxVelocity;
        }
        
    }

    private void LevelUp()
    {
        aiLevel.text = "Level " + soccerAI.GetLevel() + " AI";
        score.text = playerScore + "-" + enemyScore;
        UpdateHiScore(soccerAI.GetLevel());
    }

    private void LevelDown()
    {
        aiLevel.text = "Level " + soccerAI.GetLevel() + " AI";
        score.text = playerScore + "-" + enemyScore;
    }

    private void OnTriggerEnter(Collider collider)
    {
        /*if ()
        {
            rb.AddForce(collision.contacts[0].normal * forceMagnitude, ForceMode.Force);
        }*/

        if(collider.CompareTag("Player Post"))
        {
            Debug.Log("Enemy Scores");
            transform.position = new Vector3(0, 0.29f, 0);
            rb.velocity = Vector3.zero;
            enemyScore++;
            score.text = playerScore + "-" + enemyScore;
            soundManager.PlaySound("Player");
        }

        if (collider.CompareTag("Enemy Post"))
        {
            Debug.Log("Player Scores");
            transform.position = new Vector3(0, 0.29f, 0);
            rb.velocity = Vector3.zero;
            playerScore++;
            score.text = playerScore + "-" + enemyScore;
            soundManager.PlaySound("Enemy");
        }
    }

    public void UpdateHiScore(int newScore)
    {
        if (newScore > hiScore)
        {
            hiScore = newScore;
            PlayerPrefs.SetInt("HighScore Goal", hiScore);
            UpdateHiScoreText();
        }
    }

    private void UpdateHiScoreText()
    {
        hiScoreText.text = "Hi: Level " + hiScore.ToString();
    }

    private int GetHiScore()
    {
        return PlayerPrefs.GetInt("HighScore Goal", 1);
    }

    /*private void OnTriggerStay(Collider collider)
    {
        if (collider.CompareTag("Bounce"))
        {
            rb.AddForce(collider.contacts[0].normal * forceMagnitude, ForceMode.Force);
        }
    }*/
}

