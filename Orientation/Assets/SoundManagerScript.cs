using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public static AudioClip getItem, kill, run, son, start, walk,menuMove;
    public static AudioSource audioSrc;
    
    void Start()
    {
        getItem = Resources.Load<AudioClip>("getItem");
        kill = Resources.Load<AudioClip>("kill");
        run = Resources.Load<AudioClip>("run");
        son = Resources.Load<AudioClip>("son");
        start = Resources.Load<AudioClip>("start");
        walk = Resources.Load<AudioClip>("walk");
        menuMove = Resources.Load<AudioClip>("menuMove");
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "getItem":
                audioSrc.loop = false;
                audioSrc.PlayOneShot((getItem));
                break;
            case "kill":
                audioSrc.loop = false;
                audioSrc.PlayOneShot((kill));
                break;
            case "run":
                audioSrc.loop = true;
                audioSrc.PlayOneShot((run));
                break;
            case "son":
                audioSrc.PlayOneShot((son));
                break;
            case "start":
                audioSrc.loop = false;
                audioSrc.PlayOneShot((start));
                break;
            case "walk":
                audioSrc.loop = true;
                audioSrc.PlayOneShot((walk));
                break;
            case "menuMove":
                audioSrc.loop = false;
                audioSrc.PlayOneShot((menuMove));
                break;
        }
    }
    
}
