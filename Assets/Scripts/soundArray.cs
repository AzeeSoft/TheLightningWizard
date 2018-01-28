using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundArray : MonoBehaviour
{

    public AudioClip[] Voice;
    public AudioClip[] enemyDamage;


    public void VoiceOver(){
        int randomClip = Random.Range(0, Voice.Length);
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.clip = Voice[randomClip];
        source.Play();
    }
    public void EnemyDamage(){
        int randomClip = Random.Range(0, enemyDamage.Length);
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.clip = enemyDamage[randomClip];
        source.Play();
    }
   
}

  
