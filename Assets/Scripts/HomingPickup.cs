using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HomingPickup : MonoBehaviour {
	public GameObject player = null;
	public Transform target = null;

	public float speed = 5.0f;
	public float rotateSpeed = 200f;
	public bool pickup = true;

	//private GameLoop gameloop = GameObject.Find("GM").GetComponent <GameLoop> ();
	// Use this for initialization
	void Start () {
		findPlayer();
	}
	IEnumerator MyCoroutine()
	{
		yield return new WaitForSeconds(.5f);
		findPlayer();
	}
	private void findPlayer(){
		player = GameObject.FindGameObjectWithTag("Player");
		target = player.transform;
	}
	
	void Update()
	{
		// Rotate the camera every frame so it keeps looking at the target
		transform.LookAt(target);
		transform.Translate(Vector3.forward * speed * Time.deltaTime);
		
		
	}

	void OnTriggerEnter (Collider collision)
	{
		// Put a particle effect here
		if(collision.gameObject.tag == "Player")
		{
			Debug.Log ("hit");
			//gameloop.Magic++;
			if (pickup == true)
				collision.GetComponent<PlayerHealthandSave> ().manaUp ();
			else 
				collision.GetComponent<PlayerHealthandSave> ().healthUp ();
			
			Destroy(gameObject);
		}

	}
}