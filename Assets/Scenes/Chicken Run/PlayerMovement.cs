using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody rb;

    [SerializeField]
    private float jumpForce = 5;

    [SerializeField]
    private enum SideOfTheRoad
    {
        Middle, Left, Right
    }

    private SideOfTheRoad sideOfTheRoad;

    private Animator animator;

    private bool isGrounded, justMoved;


    //Touch stuff
    /*Vector3 startTouchPos,
                   endTouchPos,
                   swipeDirection;*/

    private Vector2 fingerDownPosition;
    private Vector2 fingerUpPosition;

    public bool detectSwipeOnlyAfterRelease = false;
    public float minDistanceForSwipe = 20f;

    Spawner spawner;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();

        Physics.gravity = new Vector3(0, -9.8f, 0);

        spawner = FindObjectOfType<Spawner>();
        //Touch stuff
        //startTouchPos = endTouchPos = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        #region Desktop Input
        //Desktop Input
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Jump();
        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft();
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();
        }
        #endregion

        #region Mobile Input
        //Mobile Input
        /* if (Input.touchCount > 0)
         {
             Touch touch = Input.GetTouch(0);
             float swipeThresold = 50f;
             Vector3 startTouchPos, 
                     endTouchPos,
                     swipeDirection;

             startTouchPos = endTouchPos = Vector3.zero;


             switch (touch.phase)
             {
                 case TouchPhase.Began:
                     startTouchPos = touch.position;
                     Debug.Log("Began");
                     break;
                 case TouchPhase.Moved:
                     endTouchPos = touch.position;
                     Debug.Log("Moved");
                     break;
                 //case TouchPhase.Ended:
                   //  endTouchPos = touch.position;
                     //Debug.Log("Ended");
                     //break;
             }

             //Check the x axis

             swipeDirection = startTouchPos - endTouchPos;
             Debug.Log("Swipe Dir: " + swipeDirection + ", Start Pos" + startTouchPos + ", End Pos" + endTouchPos);

             //if x value is negative = left
             if(swipeDirection.x < -swipeThresold)
             {
                 //Swipe Left
                 MoveLeft();
             }
             else if(swipeDirection.x > swipeThresold)
             {
                 //Swipe Right
                 MoveRight();
             }
             else if(swipeDirection.y > swipeThresold)
             {
                 //Swipe Up
                 Jump();
             }
         }*/

        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                fingerUpPosition = touch.position;
                fingerDownPosition = touch.position;
            }

            if (detectSwipeOnlyAfterRelease && touch.phase == TouchPhase.Moved)
            {
                fingerDownPosition = touch.position;
                DetectSwipe();
            }

            if (touch.phase == TouchPhase.Ended)
            {
                fingerDownPosition = touch.position;
                DetectSwipe();
            }
        }
        #endregion


    }

    void DetectSwipe()
    {
        if (SwipeDistanceCheckMet() && !justMoved)
        {
            Vector2 currentSwipe = fingerDownPosition - fingerUpPosition;

            if (currentSwipe.magnitude < minDistanceForSwipe)
                return;

            currentSwipe.Normalize();

            JustMoved(1f);
            // Swipe upwards
            if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
            {
                Debug.Log("Swipe Up");
                Jump();
            }
            // Swipe downwards
            else if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
            {
                Debug.Log("Swipe Down");
            }
            // Swipe left
            else if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {
                Debug.Log("Swipe Left");
                MoveLeft();
            }
            // Swipe right
            else if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {
                Debug.Log("Swipe Right");
                MoveRight();
            }
        }
    }

    private IEnumerator JustMoved(float seconds)
    {
        justMoved = true;
        yield return new WaitForSeconds(seconds);
        justMoved = false;
    }

    bool SwipeDistanceCheckMet()
    {
        return Vector2.Distance(fingerDownPosition, fingerUpPosition) > minDistanceForSwipe;
    }

    private void MoveLeft()
    {
        if (justMoved)
            return;
        float x = 0;
        switch (sideOfTheRoad)
        {
            case SideOfTheRoad.Left:
                x = -6;
                sideOfTheRoad = SideOfTheRoad.Left;
                break;
            case SideOfTheRoad.Right:
                x = 0;
                sideOfTheRoad = SideOfTheRoad.Middle;
                break;
            case SideOfTheRoad.Middle:
                x = -6;
                sideOfTheRoad = SideOfTheRoad.Left;
                break;
        }
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }

    private void MoveRight()
    {
        if (justMoved)
            return;
        float x = 0;
        switch (sideOfTheRoad)
        {
            case SideOfTheRoad.Left:
                x = 0;
                sideOfTheRoad = SideOfTheRoad.Middle;
                break;
            case SideOfTheRoad.Right:
                x = 6;
                sideOfTheRoad = SideOfTheRoad.Right;
                break;
            case SideOfTheRoad.Middle:
                x = 6;
                sideOfTheRoad = SideOfTheRoad.Right;
                break;
        }
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }

    private void Jump()
    {
        if (!isGrounded)
            return;
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        animator.SetTrigger("Jump");
        animator.SetFloat("Jump Type", Random.Range(0, 2));
    }

    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            LoseGame();
        }
    }

    private void LoseGame()
    {
        spawner.UpdateHiScore();
        SceneManager.LoadScene("Chicken Run");

    }
}

