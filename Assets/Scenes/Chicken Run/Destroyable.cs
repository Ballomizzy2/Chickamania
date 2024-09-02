using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
    private Transform player;

    
    private float moveSpeed = 50f;

    float timer, timerThres = 120f; 
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().transform;    
    }

    // Update is called once per frame
    void Update()
    {
        IncreaseSpeed();
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - moveSpeed * Time.deltaTime);
        if(transform.position.z < player.position.z - 500)
        {
            Destroy(this.gameObject);
        }
    }

    private void IncreaseSpeed()
    {
        if(timer < timerThres)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
            moveSpeed += 10;
        }
    }
}
