using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

	public int curHP;
	public int maxHP = 50;
	public GameManager gameloop;
	public GameObject mana;
	public GameObject health;

	// Use this for initialization
	void Start () {
		curHP = maxHP;
	}

	private void Awake()
	{
		gameloop = GameObject.Find("GM").GetComponent <GameManager> ();
	}

	public void TakeDamage(int amt) {
		if (curHP > 0) {
			curHP -= amt;
			if (curHP <= 0) {
				curHP = 0;
				Die ();
			}
		}
	}

	void Die() {
		if (gameloop) {
			gameloop.Souls++;
			gameloop.numOfEnemies--;
		}
		Debug.Log(mana);

		Destroy (gameObject);
		//transform.position = transform.position.z - 10;
		Instantiate(mana, transform.position , transform.rotation);
		Instantiate(health, transform.position , transform.rotation);
	}
}
