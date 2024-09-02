using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private Animator chickenAnimator;
    [SerializeField]
    Camera main;

    float timer, timerThres = 10;


    [SerializeField]
    private GameObject menu, gameMenu, creditsMenu;
    // Start is called before the first frame update
    void Start()
    {
        chickenAnimator = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        if(timer < timerThres)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
            ChangeIdleAnim();
        }
        transform.position = Vector3.zero;
        transform.forward = -main.transform.forward;
    }

    public void ChangeIdleAnim()
    {
        int oldInt = (int)chickenAnimator.GetFloat("Blend"),
            newInt = Random.Range(0, 4);
        chickenAnimator.SetFloat("Blend", Mathf.Lerp(oldInt, newInt, 3));
    }


    public void Play()
    {
        menu.SetActive(false);
        gameMenu.SetActive(true);  
        creditsMenu.SetActive(false);
    }

    public void PlayGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Credits()
    {
        menu.SetActive(false);
        gameMenu.SetActive(false);
        creditsMenu.SetActive(true);
    }

    public void BackToMenu()
    {
        menu.SetActive(true);
        gameMenu.SetActive(false);
        creditsMenu.SetActive(false);
    }

}
