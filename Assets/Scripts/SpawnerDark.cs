using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnerDark : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> obstacles = new List<GameObject>();

    public float spawnInterval = 2f; // Time interval between spawns
    public float spawnRange = 4f; // Range for random X position of spawned objects

    private float timer;

    private float score = 0;

    [SerializeField]
    private TextMeshPro text;

    [SerializeField] private TextMeshPro hiScoreText;
    private int hiScore;


    private void Start()
    {
        hiScore = GetHiScore();
        UpdateHiScoreText();
    }


    void Update()
    {
        timer += Time.deltaTime;
        score += Random.Range(0.01f, 0.1f);
        text.text = ((int)score).ToString();

        if (timer >= spawnInterval)
        {
            SpawnObject();
            timer = 0f;
        }
    }

    void SpawnObject()
    {
        // Randomly choose between spawning an egg or a rock

        GameObject objectToSpawn;
        float y;
        if (Random.Range(0f,1f) > 0.8f)
        {
            objectToSpawn = obstacles[2];
            y = 0.584f;

        }
        else
        {
            objectToSpawn = Random.Range(0f, 1f) > 0.7f ? obstacles[0] : obstacles[1];
            y = 0.5f;
        }

        
        Vector3 spawnPosition = new Vector3(0.5f, y, 1f);
        Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);
    }

    public void UpdateHiScore(int newScore)
    {
        if (newScore > hiScore)
        {
            hiScore = newScore;
            PlayerPrefs.SetInt("HighScore Dark", hiScore);
            UpdateHiScoreText();
        }
    }

    public void UpdateHiScore()
    {
        int newScore = (int)score;
        if (newScore > hiScore)
        {
            hiScore = newScore;
            PlayerPrefs.SetInt("HighScore Dark", hiScore);
            UpdateHiScoreText();
        }
    }
    private void OnDisable()
    {
        UpdateHiScore((int)score);
    }
    private void UpdateHiScoreText()
    {
        hiScoreText.text = "HI: " + hiScore.ToString();
    }

    private int GetHiScore()
    {
        return PlayerPrefs.GetInt("HighScore Dark", 0);
    }
}
