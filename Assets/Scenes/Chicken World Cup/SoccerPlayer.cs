using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoccerPlayer : MonoBehaviour
{
    Vector3 moveDir;
    Rigidbody rb;
    CharacterController controller;

    float moveSpeed = 5f;

    [SerializeField]
    private bool isMobile;
    private float mobileMultiplier = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMobile)
        {
            moveDir = new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed, 0,Input.GetAxisRaw("Vertical") * moveSpeed);
            controller.Move(transform.TransformDirection(moveDir * Time.deltaTime * moveSpeed));

            transform.position = new Vector3(transform.position.x, 0.96f, Mathf.Clamp(transform.position.z, -40f, -4f));
            transform.eulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
        }


        if (isMobile)
        {
            Vector3 moveDir = Vector3.zero;

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Moved)
                {
                    // Get touch delta position for movement
                    float touchX = touch.deltaPosition.x;
                    float touchY = touch.deltaPosition.y;

                    // Calculate movement using touch input
                    moveDir = new Vector3(touchX * moveSpeed, 0f, touchY * moveSpeed);
                }
            }

            // Applying movement
            CharacterController controller = GetComponent<CharacterController>();
            controller.Move(transform.TransformDirection(moveDir * mobileMultiplier *Time.deltaTime));

            // Clamping Z position and setting Y position
            transform.position = new Vector3(transform.position.x, 0.96f, Mathf.Clamp(transform.position.z, -40f, -4f));

            // Resetting rotation except for Y-axis
            transform.eulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
        }
    }


    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForce(moveDir, ForceMode.Force);
        }
    }*/
}
