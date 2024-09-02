using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Obstacle : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private bool isBall;

    SpawnerDark spawner;

    void Start()
    {
        spawner = FindObjectOfType<SpawnerDark>();
        Destroy(gameObject, 15f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isBall && other.gameObject.CompareTag("Player"))
        {
            //Lose
            spawner.UpdateHiScore();
            Debug.Log("Colli");
            SceneManager.LoadScene("DodgeBalls");
        }
    }
}
