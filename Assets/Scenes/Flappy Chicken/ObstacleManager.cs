using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField]
    private Transform obstaclePoint;

    [SerializeField]
    private List<GameObject> obstacleTypes = new List<GameObject>();

    [SerializeField]
    private float timer = 0f, timerThresold = 2f;

    [SerializeField]
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.GetHasGameFinished())
            return;
        if(timer <= timerThresold)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
            SpawnObstacle();
        }
    }

    private void SpawnObstacle()
    {
        Instantiate(obstacleTypes[Random.Range(0, obstacleTypes.Count)], obstaclePoint);

    }
}
