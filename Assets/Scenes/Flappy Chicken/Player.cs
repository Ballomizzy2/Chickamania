using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    private Rigidbody rb;
    private bool isDead;
    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private GameManager gameManager;

    private Animator animator;

    private SoundManager soundManager;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        soundManager = FindObjectOfType<SoundManager>();

        Physics.gravity = Vector3.up * -20;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            transform.localEulerAngles = Vector3.zero;
            transform.localPosition = new Vector3(0,transform.localPosition.y, -5.4f);
            rb.rotation = new Quaternion(Mathf.Clamp(rb.rotation.x, 0, 90), rb.rotation.y, rb.rotation.z, rb.rotation.w);

            //Touch input
            if(Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if(touch.phase == TouchPhase.Began)
                {
                    Jump();
                    animator.SetTrigger("Fly");
                }

            }

            //Desktop Input
            if (Input.GetMouseButtonDown(0))
            {
                Jump();
                animator.SetTrigger("Fly");
            }
        }
        Debug.Log("Gravity = " + Physics.gravity);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Die();
        }
    }   

    public void Die()
    {
        Debug.Log("Died");
        isDead = true;
        gameManager.SetHasGameFinished(true);
        rb.AddTorque(Vector3.right * jumpForce);
        rb.constraints = RigidbodyConstraints.None;
        soundManager.PlaySound("Die");
    }

    public void Jump()
    {
        int rand = Random.Range(0, 2);
        if (rand == 0) soundManager.PlaySound("Hop 1"); else soundManager.PlaySound("Hop 2");
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        rb.AddTorque(Vector3.left * jumpForce/100, ForceMode.Impulse);
    }


}
