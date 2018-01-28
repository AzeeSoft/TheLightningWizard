using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class pauseMenu : MonoBehaviour {
    
    public GameObject pause;
    private bool isEnabled = false;

    public GameObject optionsVol;
    private GameManager gameloop;
  

    void Start()
    {
        gameloop = GameObject.Find("GM").GetComponent<GameManager>();
//        FindObjectOfType<SoundManager>().Play("LWGameplayTrack");

    }
	
	// Update is called once per frame
	void Update () {
        // Enable pause menu
        if (Input.GetKeyDown(KeyCode.Escape)  && !isEnabled)
        {
            pause.SetActive(true);
            isEnabled = true;
            Time.timeScale = 0;

        }

        // disable pause menu
        else if (Input.GetKeyDown(KeyCode.Escape) && isEnabled)
        {
            pause.SetActive(false);
            isEnabled = false;
            Time.timeScale = 1;
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton7) && !isEnabled)
        {
            pause.SetActive(true);
            isEnabled = true;
            Time.timeScale = 0;
            //quitBtn.SetActive(true);
            //adds.SetActive(false);
        }

        // disable pause menu
        else if (Input.GetKeyDown(KeyCode.JoystickButton7) && isEnabled)
        {
            pause.SetActive(false);
            isEnabled = false;
            Time.timeScale = 1;
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton9) && !isEnabled)
        {
            pause.SetActive(true);
            isEnabled = true;
            Time.timeScale = 0;
            //quitBtn.SetActive(true);
            //adds.SetActive(false);
        }

        // disable pause menu
        else if (Input.GetKeyDown(KeyCode.JoystickButton9) && isEnabled)
        {
            pause.SetActive(false);
            isEnabled = false;
            Time.timeScale = 1;
        }
    }
    public void SaveButton()
    {
         
        gameloop.Save();
        FindObjectOfType<SoundManager>().Play("MenuButtonSelectSound");
    }

    public void Resume(){
        pause.SetActive(false);
        isEnabled = false;
        Time.timeScale = 1;
        FindObjectOfType<SoundManager>().Play("MenuButtonSelectSound");
    }

    public void Options(){
        optionsVol.SetActive(true);
        FindObjectOfType<SoundManager>().Play("MenuButtonSelectSound");
    }

    public void Quit()
    {
        SceneManager.LoadScene("startMenu", LoadSceneMode.Single);//loads start menu
        FindObjectOfType<SoundManager>().Play("MenuButtonSelectSound");
    }

    public void GetVolume(float vol){
        AudioListener.volume = vol;
    }
}
