using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

using System;


public class SoundManager : MonoBehaviour {
    public sound[] sounds;


void Awake()
    {
        foreach (sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.loop = s.loop;

        }
    }

    public void Play(string name)
    {
        sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();

    }

}
