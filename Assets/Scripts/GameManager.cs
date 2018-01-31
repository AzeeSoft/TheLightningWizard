using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    //Player
    public int Level = 1;
    public int pLevel = 0;
    public int nextLevel = 5;
    public int ex = 0;
    public int Speed = 1;
    public int Health = 10;
    public int mHealth = 10;
	public int Mana = 10;
	public int mMana = 10;
	public int Souls = 10;

    //Player Spawner and Exit
    public bool playerIsDead = true;
    public GameObject player;
    public GameObject pSpawner;
    public GameObject pExit;

    //Enemy
    public GameObject Enemy;
    public int numOfEnemies;

    //Array of Enemy Spawners
    public GameObject[] eSpawners;

	public AIManager AI;

    //Inventory
    public Text curLevelText;
    public Text PlayerLevelText;
    public Text exText;
    public Text speedText;
    public Text healthText;
    public Text attackText;
    public Text defenceText;
    public Text magicText;
    //Upgrade button
    //public Button upgradeBtn;
    //Max out button
    //public Button maxOutBtn;
    //health bar
    public Image bar;
    //magic bar
    public Image magicBar;
    //gameover panel
    public GameObject gameover;
    //timer
    public int time = 200;
    public soundArray sound;
    //Opening SaveFile
    private const string FILE_NAME = "Save.dat";
    void Start () {
        //Find All Spawners in level
		//Load();
        //Save();
        AutoFindSpawns();
        SpawnEnemy();
        //spawnPlayer();
        //spawnPlayer();

    }

    private void FixedUpdate()
    {

        if (ex >= nextLevel)
        {
            //Run Levelup and raise next Level
            pLevel++;
            nextLevel += nextLevel;
        }

        if (numOfEnemies <= 0)
        {
            SpawnEnemy();
        }

        try
        {
            bar.fillAmount = (float) Health/(float) mHealth;
            magicBar.fillAmount = (float) Mana/(float) mMana;
        }
        catch (Exception e)
        {
            
        }
    }

    // Update is called once per frame
    public void Update () {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Save();
            print("Game was saved");
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            Load();
            print("Game was loaded");
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            SpawnEnemy();
            print("SpawnEnemy");
        }
        if (Health <= 0)
        {
            gameover.SetActive(true);
            countdown();
            if (time == 0)
            {
                SceneManager.LoadScene("levelOne", LoadSceneMode.Single);   //load scene level
            }
        }

        //curLevelText.text=("Level: " + Level);
        //PlayerLevelText.text = ("Player Level: " + pLevel);
        //exText.text = ("ex: " + ex);
        //speedText.text = ("Speed: " + Speed);
        //healthText.text = ("Health: " + Health+"/"+mHealth);
        //attackText.text = ("Attack: " + minAttack + "/" + maxAttack);
        //defenceText.text = ("Defence: " + minDefence + "/" + maxDefence);
        //magicText.text = ("Mana: " + Mana + "/" + mMana);

        /*if(ex>=10){//make upgrade button visible 
            upgradeBtn.gameObject.SetActive(true);
        }
        else{
            upgradeBtn.gameObject.SetActive(false);
        }

        if(ex>=50){//make max out button visible
            maxOutBtn.gameObject.SetActive(true);
        }
        else{
            maxOutBtn.gameObject.SetActive(false);
        }

        bar = GameObject.FindGameObjectWithTag("healthBar");
        magicBar = GameObject.FindGameObjectWithTag("magicBar").GetComponent<Image>();
		*/


    }
    //Find All Spawners in level
    void AutoFindSpawns()
    {
        eSpawners = GameObject.FindGameObjectsWithTag("Spawners");
        //pSpawner = GameObject.FindGameObjectWithTag("playerSpawner");
        //pExit = GameObject.FindGameObjectWithTag("playerExit");
    }
    //spawnEnemys it takes the level and give the enemies random stats based on the level number
    void SpawnEnemy()
    {  
        foreach (GameObject respawn in eSpawners)
        {
            Instantiate(Enemy, respawn.transform.position, respawn.transform.rotation);
            numOfEnemies = numOfEnemies + 3;
        }
        if (AI)
        {
            AI.autoFetchAIUnits();
        }
    }

    //Spawning in Player;
    void spawnPlayer()
    {
        Instantiate(player, pSpawner.transform.position, pSpawner.transform.rotation);
        Health=mHealth;
        playerIsDead=false;
    }

    public void MagicPlus()
    {
        Mana++;
    }
    
    public void Save()
    {
        StreamWriter sw = File.CreateText(FILE_NAME);

        //Player
        sw.WriteLine(Level);
        sw.WriteLine(pLevel);
        sw.WriteLine(nextLevel);
        sw.WriteLine(ex);
        sw.WriteLine(Speed);
        sw.WriteLine(Health);
        sw.WriteLine(mHealth);

        sw.Close();
        
    }
    public void Load()
    {
        StreamReader sr = File.OpenText(FILE_NAME);
        //Player
        Level = Int32.Parse(sr.ReadLine());
        pLevel = Int32.Parse(sr.ReadLine());
        nextLevel = Int32.Parse(sr.ReadLine());
        ex = Int32.Parse(sr.ReadLine());
        Speed = Int32.Parse(sr.ReadLine());
        Health = Int32.Parse(sr.ReadLine());
        mHealth = Int32.Parse(sr.ReadLine());

        sr.Close();
    }
    void countdown(){
        time--;
        Debug.Log(time);
    }


    
}