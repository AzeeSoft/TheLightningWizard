using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelThreeTrig : MonoBehaviour {

	void OnTriggerEnter(Collider col){
		if (col.tag=="Player"){
			SceneManager.LoadScene("LevelThree", LoadSceneMode.Single);//load scene level
		}
	}
}
