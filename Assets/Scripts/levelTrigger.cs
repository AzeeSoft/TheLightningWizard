using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class levelTrigger : MonoBehaviour {


    void OnTriggerEnter(Collider col){
        if (col.tag=="Player"){
            SceneManager.LoadScene("LevelTwo", LoadSceneMode.Single);//load scene level
        }
    }
}
