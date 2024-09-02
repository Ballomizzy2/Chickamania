using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDark : MonoBehaviour
{
    Rigidbody rb;
    bool isGrounded;

    [SerializeField]
    private bool isMobile = false;

    // Start is called before the first frame update
    void Start()

    {
        Physics.gravity = new Vector3(0,-5,0);
        rb = GetComponent<Rigidbody>();   

        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMobile)
        {
            if (isGrounded && Input.GetKeyUp(KeyCode.Space))
            {
                rb.AddForce(Vector3.up * 2, ForceMode.Impulse);
            }
        }

        else
        {
            if(Input.touchCount > 0)
            {
                if(Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    if (isGrounded)
                    {
                        rb.AddForce(Vector3.up * 2, ForceMode.Impulse);
                    }
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
    }

    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

}
