using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    [SerializeField]
    private GameObject egg;

    [SerializeField]
    Transform point;

    bool theresAShark;

    float timer = 0, timerThres = 0.2f;
    private void Update()
    {
        theresAShark = FindAnyObjectByType<Shark>();




        if (!theresAShark)
            return;
        if(timer < timerThres)
        {
            timer+=Time.deltaTime;
        }
        else
        {
            timer = 0;
            ShootEgg();
        }
    }

    
    public void ShootEgg()
    {
        GameObject eggInst= Instantiate(egg, transform.position, Quaternion.identity);
        eggInst.transform.position = new Vector3(transform.position.x, 0, 0);
        //eggInst.transform.SetParent(null, false);
        //eggInst.transform.Translate(transform.TransformDirection(Vector3.down * 10));
    }
}
