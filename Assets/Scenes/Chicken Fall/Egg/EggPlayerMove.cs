using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EggPlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed at which the basket moves left or right

    [SerializeField]
    private bool isMobile = false;
    private float mobileMultiplier = 0.1f;


    void Update()
    {
        if (!isMobile)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            Vector3 movement = new Vector3(horizontalInput, 0f, 0f) * moveSpeed * Time.deltaTime;
            transform.Translate(movement);
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -2.5f, 2.5f), transform.position.y, transform.position.z);
        }
        else
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Moved)
                {
                    // Get the change in touch position
                    float horizontalInput = touch.deltaPosition.x * mobileMultiplier;

                    // Calculate movement using touch input
                    Vector3 movement = new Vector3(horizontalInput, 0f, 0f) * moveSpeed * Time.deltaTime;

                    // Move the object
                    transform.Translate(movement);

                    // Clamp the position
                    transform.position = new Vector3(Mathf.Clamp(transform.position.x, -2.5f, 2.5f), transform.position.y, transform.position.z);
                }
            }
        }
    }




}
