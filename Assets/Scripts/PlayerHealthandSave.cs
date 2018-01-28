using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerHealthandSave : MonoBehaviour {
	//public playerHealth = null;
	
	public bool isThouching;
	// Use this for initialization
	private GameManager gameloop;
	void Start () {
		gameloop = GameObject.Find("GM").GetComponent <GameManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(gameloop.Health <= 0)
			{
				SceneManager.LoadScene("level", LoadSceneMode.Single);
			}
	}
	void FixedUpdate(){
		if(isThouching==true)
		{
			gameloop.Health--;
			if(gameloop.Health <= 0)
			{
				SceneManager.LoadScene("level", LoadSceneMode.Single);
			}
		}
	}
	public void manaUp(){
		if(gameloop.Mana<gameloop.mMana)
			gameloop.Mana++;
	}
	public void healthUp(){
		if(gameloop.Health<gameloop.mHealth)
			gameloop.Health++;
	}
	private void OnTriggerEnter2D(Collider2D collision)
    {
		if(collision.gameObject.tag == "EnemyAttack")
		{
			isThouching=true;
		}
        if(collision.gameObject.tag == "playerExit")
        {
			switch(gameloop.Level)
			{
				case 1:
					SceneManager.LoadScene("level2", LoadSceneMode.Single);
					gameloop.Level=2;
					gameloop.Save();
					break;
				case 2:
					SceneManager.LoadScene("level3", LoadSceneMode.Single);
					gameloop.Level=3;
					gameloop.Save();
					break;
				case 3:
					SceneManager.LoadScene("StartScreen", LoadSceneMode.Single);
					gameloop.Level=1;
					gameloop.Save();
					break;
			}
            //SceneManager.LoadScene("level2", LoadSceneMode.Single);
			//gameloop.Save();
        }
    }
	private void OnTriggerExit2D(Collider2D collision)
    {
		if(collision.gameObject.tag == "EnemyAttack")
		{
			isThouching=false;
		}
	}
}
