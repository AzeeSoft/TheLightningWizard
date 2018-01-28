using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

using System;
using Random = UnityEngine.Random;


public class SoundManager : MonoBehaviour
{
    public float minInterval = 5f;
    public float maxInterval = 10f;

    public AudioClip[] soldierDialogs;
    public AudioClip[] wizardDialogs;
    public AudioClip[] soldierHits;
    public AudioClip[] wizardHits;
    public AudioClip albertoSsup;
    public AudioClip albertoImTheOne;

    AudioSource audioSource;

    void Awake()
    {/*
        foreach (sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }*/

        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        StartCoroutine("startPlayingDialogs");
    }

    public void Play(string name)
    {/*
        sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
*/
    }

    public void playRandomFrom(AudioClip[] audioClips)
    {
        int index = Random.Range(0, audioClips.Length - 1);
        play(audioClips[index]);
    }

    public void play(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }

    IEnumerator startPlayingDialogs()
    {
        while(gameObject.activeSelf)
        {
            yield return new WaitForSeconds(Random.Range(minInterval, maxInterval));
            if (Random.Range(0, 10) > 5)
            {
                playRandomFrom(wizardDialogs);
            }
            else
            {
                Soldier[] soldiers = Resources.FindObjectsOfTypeAll<Soldier>();
                if (soldiers.Length > 0)
                {
                    playRandomFrom(soldierDialogs);
                }
            }
        }
    }
}
