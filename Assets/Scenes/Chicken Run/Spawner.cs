using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> tiles = new List<GameObject> ();

    [SerializeField]
    private GameObject lastSpawnedTile;

    [SerializeField]
    private List<GameObject> obstacles = new List<GameObject> ();

    private float l = -6, m = 0, r = 6;

    [SerializeField]
    private Transform spawnPoint;
    //Timer
    private float timer, timerThres =3f;
    private float timer2, timerThres2 =3f;

    [SerializeField]
    private TextMeshProUGUI scoreText;
    private float score;

    [SerializeField] private TextMeshProUGUI hiScoreText;
    private int hiScore;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 2f;
        hiScore = GetHiScore();
        UpdateHiScoreText();
    }

    // Update is called once per frame
    void Update()
    {
        
        // for Obstacles
        if(timer > timerThres)
        {
            timer = 0;
            timerThres = Random.Range(0.5f, 3f);
            SpawnObstacle();
        }
        timer+=Time.deltaTime;

        // for Obstacles
        if (timer2 > timerThres2)
        {
            timer2 = 0;
            SpawnTile();
        }
        timer2 += Time.deltaTime;

        score += Time.deltaTime * 50;
        scoreText.text = ((int)score).ToString();
    }
    private void LateUpdate()
    {
    }

    public void UpdateHiScore(int newScore)
    {
        if (newScore > hiScore)
        {
            hiScore = newScore;
            PlayerPrefs.SetInt("HighScore Run", hiScore);
            UpdateHiScoreText();
        }
    }

    public void UpdateHiScore()
    {
        int newScore = (int)score;
        if (newScore > hiScore)
        {
            hiScore = newScore;
            PlayerPrefs.SetInt("HighScore Run", hiScore);
            UpdateHiScoreText();
        }
    }

    private void UpdateHiScoreText()
    {
        hiScoreText.text = hiScore.ToString();
    }

    private int GetHiScore()
    {
        return PlayerPrefs.GetInt("HighScore Run", 0);
    }

    private void OnDisable()
    {
        UpdateHiScore();
        Time.timeScale = 1;
    }

    void SpawnTile()
    {
        lastSpawnedTile = Instantiate(tiles[0], spawnPoint.position, Quaternion.identity);
    }
    void SpawnObstacle()
    {
        int numberOfObs = Random.Range(0, 3), posOfObs = Random.Range(1,3);

        if(numberOfObs == 1)
        {
            GameObject newGO = Instantiate(obstacles[Random.Range(0, obstacles.Count)], lastSpawnedTile.transform);
            if(posOfObs == 1)
            {
                if(Random.Range(0,2) == 0)
                    newGO.transform.position = new Vector3(l, newGO.transform.localPosition.y, transform.position.z);
                else
                    newGO.transform.position = new Vector3(m, newGO.transform.localPosition.y, transform.position.z);
            }
            else if (posOfObs == 2)
            {
                if (Random.Range(0, 2) == 0)
                    newGO.transform.position = new Vector3(r, newGO.transform.localPosition.y, transform.position.z);
                else
                    newGO.transform.position = new Vector3(m, newGO.transform.localPosition.y, transform.position.z);
            }
        }

        if (numberOfObs == 2)
        {
            string prevpos = null ;
            GameObject newGO = Instantiate(obstacles[Random.Range(0, obstacles.Count)], lastSpawnedTile.transform);
            if (posOfObs == 1)
            {
                if (Random.Range(0, 2) == 0)
                {
                    newGO.transform.position = new Vector3(l, newGO.transform.localPosition.y, transform.position.z);
                    prevpos = "l";
                }
                else
                {
                    newGO.transform.position = new Vector3(m, newGO.transform.localPosition.y, transform.position.z);
                    prevpos = "m";
                }
            }
            else if (posOfObs == 2)
            {
                if (Random.Range(0, 2) == 0)
                {
                    newGO.transform.position = new Vector3(r, newGO.transform.localPosition.y, transform.position.z);
                    prevpos = "r";
                }
                else
                {
                    newGO.transform.position = new Vector3(m, newGO.transform.localPosition.y, transform.position.z);
                    prevpos="m";
                }
            }

            GameObject newGO1 = Instantiate(obstacles[Random.Range(0, obstacles.Count)], lastSpawnedTile.transform);
            if (posOfObs == 1)
            {
                if(prevpos == "m")
                    newGO1.transform.position = new Vector3(l, newGO1.transform.localPosition.y, transform.position.z);
                else if(prevpos == "l")
                    newGO1.transform.position = new Vector3(m, newGO1.transform.localPosition.y, transform.position.z);
            }
            else if (posOfObs == 2)
            {
                if (prevpos == "m")
                    newGO1.transform.position = new Vector3(r, newGO1.transform.localPosition.y, transform.position.z);
                else if (prevpos == "r")
                    newGO1.transform.position = new Vector3(m, newGO1.transform.localPosition.y, transform.position.z);
            }
        }
    }
}
