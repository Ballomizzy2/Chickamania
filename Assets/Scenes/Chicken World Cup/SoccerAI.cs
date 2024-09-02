using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public class SoccerAI : MonoBehaviour
{
    Rigidbody rb;

    Rigidbody ballRb;

    float aiSpeed = 2;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ballRb = FindAnyObjectByType<Ball>().GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, 0.96f, Mathf.Clamp(transform.position.z, 6, 40));

        if(ballRb.transform.position.z > 6 && ballRb.velocity.z < 0.5f)
        {
            rb.AddForce((ballRb.transform.position - transform.position) * aiSpeed, ForceMode.Impulse);
        }
    }


}*/

using UnityEngine;

public class SoccerAI : MonoBehaviour
{
    public CharacterController characterController;
    public Transform ball; // Reference to the ball object
    private float movementSpeed = 10.0f; // Adjust the movement speed of the AI paddle

    private Rigidbody rb, ballRb;
    private Vector3 targetPosition;

    private int aiLevel = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ballRb = ball.GetComponent<Rigidbody>();
        characterController = GetComponent<CharacterController>();
        LevelUp();
    }

    void Update()
    {
       /*if (ballRb.transform.position.z < 10 && ballRb.velocity.magnitude < 0.1f)
        {
            ballRb.AddForce((Vector3.forward), ForceMode.Impulse);
            transform.position = new Vector3(0, 0.85f, transform.position.z);
            return;
           
        }*/

        // Calculate the target position based on the ball's position
        if (ball != null)
        {
            targetPosition = new Vector3(ball.position.x, transform.position.y, transform.position.z);//Mathf.Clamp(ball.position.z, 6, 40));

            // Move the AI paddle towards the target position
            Vector3 moveDirection = (targetPosition - transform.position);//.normalized;

            characterController.Move(moveDirection*Time.deltaTime*movementSpeed);// * movementSpeed);
            transform.position = new Vector3(transform.position.x, 0.85f, transform.position.z);
            //rb.velocity = moveDirection * movementSpeed;
        }

    }

    public void LevelUp()
    {
        aiLevel++;
        movementSpeed = aiLevel;
    }

    public void LevelDown()
    {
        if (aiLevel == 1)
            return;
        aiLevel--;
        movementSpeed = aiLevel;
    }

    public int GetLevel() { return aiLevel; }
}

