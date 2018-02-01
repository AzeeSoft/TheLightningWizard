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
            enablePauseMenu();
        }

        // disable pause menu
        else if (Input.GetKeyDown(KeyCode.Escape) && isEnabled)
        {
            disablePauseMenu();
        }
        if (Input.GetKeyDown(KeyCode.JoystickButton7) && !isEnabled)
        {
            enablePauseMenu();
            //quitBtn.SetActive(true);
            //adds.SetActive(false);
        }

        // disable pause menu
        else if (Input.GetKeyDown(KeyCode.JoystickButton7) && isEnabled)
        {
            disablePauseMenu();
        }

        if (Input.GetKeyDown(KeyCode.JoystickButton9) && !isEnabled)
        {
            enablePauseMenu();
            //quitBtn.SetActive(true);
            //adds.SetActive(false);
        }

        // disable pause menu
        else if (Input.GetKeyDown(KeyCode.JoystickButton9) && isEnabled)
        {
            disablePauseMenu();
        }
    }


    void enablePauseMenu()
    {
        isEnabled = true;
        Time.timeScale = 0;
        pause.SetActive(true);
    }

    void disablePauseMenu()
    {
        pause.SetActive(false);
        Time.timeScale = 1;
        isEnabled = false;
    }


    public void SaveButton()
    {
        gameloop.Save();
        FindObjectOfType<SoundManager>().Play("MenuButtonSelectSound");
    }

    public void Resume()
    {
        FindObjectOfType<SoundManager>().Play("MenuButtonSelectSound");
        disablePauseMenu();
    }

    public void Options(){
        optionsVol.SetActive(true);
        FindObjectOfType<SoundManager>().Play("MenuButtonSelectSound");
    }

    public void Quit()
    {
        FindObjectOfType<SoundManager>().Play("MenuButtonSelectSound");
        SceneManager.LoadScene("NewStartMenu", LoadSceneMode.Single);//loads start menu
        Time.timeScale = 1;
    }

    public void GetVolume(float vol){
        AudioListener.volume = vol;
    }
}
