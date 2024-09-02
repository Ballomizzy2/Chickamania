using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggController : MonoBehaviour
{
    public bool isEgg; // Set this to true for eggs, false for rocks
    private float movementSpeed = 5;

    EggScore score;

    public bool shootEgg;
    public bool isShark;
    private void Start()
    {
        score = FindAnyObjectByType<EggScore>();
    }
    void Update()
    {
        if(!shootEgg)
        Fall();
        else
            ShootEgg();

    }
    void ShootEgg()
    {
        transform.Translate(Vector3.up * movementSpeed * Time.deltaTime);

        // Add logic here if you want to recycle or destroy objects when they are out of the screen
        Destroy(gameObject, 10);
    }

    void Fall()
    {
        // Implement falling behavior for eggs or rocks
        // For instance, translate downwards using transform.Translate
        transform.Translate(Vector3.down * movementSpeed * Time.deltaTime);

        // Add logic here if you want to recycle or destroy objects when they are out of the screen
        Destroy(gameObject, 10);
    }

    void OnTriggerEnter(Collider other)
    {
        if (isEgg && other.CompareTag("Shark"))
        {
            // Handle catching the egg
            // For example, increase score or perform other actions
            Debug.Log("Destroyed Shark");
            Destroy(gameObject); // Destroy the egg when caught
            Destroy(other.gameObject);
            score.Score();
        }
        if (isEgg && other.CompareTag("Player"))
        {
            // Handle catching the egg
            // For example, increase score or perform other actions
            Debug.Log("Caught an egg!");
            Destroy(gameObject); // Destroy the egg when caught
            score.Score();
        }
        else if (!isEgg && other.CompareTag("Player"))
        {
            // Handle collision with rock (if needed)
            // For example, decrease player health or perform other actions
            Debug.Log("Hit by a rock!");
            Destroy(gameObject); // Destroy the rock on collision
            score.Lose();
        }
    }
}
