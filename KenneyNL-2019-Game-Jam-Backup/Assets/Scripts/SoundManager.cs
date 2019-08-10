using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] sounds; // 0 is tank shot, 1 and 2 is enemy explosion, 3 is player explosion, 4 is boss explosion

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void playTankShot()
    {
        audioSource.clip = sounds[0];
        audioSource.Play();
    }

    public void playEnemyExplosion()
    {
        int randomNum = Random.Range(0, 2);
        if(randomNum == 0)
        {
            audioSource.clip = sounds[1];
        }
        else
        {
            audioSource.clip = sounds[2];
        }
        audioSource.Play();
    }

    public void playPlayerExplosion()
    {
        audioSource.clip = sounds[3];
        audioSource.Play();
    }

    public void playBossExplosion()
    {
        audioSource.clip = sounds[4];
        audioSource.Play();
    }
}
