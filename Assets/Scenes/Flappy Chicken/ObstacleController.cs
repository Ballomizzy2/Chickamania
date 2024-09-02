using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{

    [SerializeField]
    private List<GameObject> removableSections = new List<GameObject>();

    //The lsit of possible sections of the obstacle that can be destroyed on awake.
    //Make sure to leave a few on both the the top and bottom.

    [SerializeField]
    private float Speed = 1f;

    private GameManager gameManager;

    private Transform player;
    void Awake()
    {
        SetObstacle();
        gameManager = FindObjectOfType<GameManager>();
        player = FindObjectOfType<Player>().transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.GetHasGameFinished())
            return;
        transform.position = transform.position - (Vector3.forward * Speed);
        if (transform.position.z < player.position.z - 30)
        {

            Destroy(gameObject);
            gameManager.UpdateScore();
        }
        
    }

    private void SetObstacle()
    {
        int n = Random.Range(0, removableSections.Count);
        removableSections[n].SetActive(false);

        /*int r = Random.Range(1, 3);
        if (r > 1) //number of deleting obstacles are more than 1
        {
            if (n == 0) //go down
                removableSections[n + 1].SetActive(false);
            else if (n == removableSections.Count - 1) //go up
                removableSections[n - 1].SetActive(false);
            else
                if (Random.Range(0, 2) == 0)  //go up = 0 or down = 1
                removableSections[n - 1].SetActive(false);
            else
                removableSections[n + 1].SetActive(false);

        }*/
    }
}
