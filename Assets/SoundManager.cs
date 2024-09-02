using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField]
    private List<Clip> clipList = new List<Clip>();

    [SerializeField]
    Transform Player;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(string name)
    {
        foreach (Clip clip in clipList)
        {
            if (clip.clipName == name)
                AudioSource.PlayClipAtPoint(clip.clip, Player.position, 10f);
        }
    }

    [System.Serializable]
    public struct Clip
    {
        [SerializeField]
        public string clipName;
        public AudioClip clip;
    }
}
