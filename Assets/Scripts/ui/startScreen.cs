
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class startScreen : MonoBehaviour
{

    public GameObject credits;
    public GameObject controls;
    public GameObject us;
    public GameObject splashGGJ;
    public GameObject splashD;
    public GameObject backg;
    public float time = 400f;
    bool timeron;


    public void Start()
    {

        FindObjectOfType<SoundManager>().Play("LWMenuTrack");
    }

    public void Update()
    {
        if (timeron)
        {
            time -= .5f;
            Debug.Log(+time);
            credits.SetActive(true);
            backg.SetActive(true);
            if (time <= 300f)
            {
                credits.SetActive(false);
                us.SetActive(true);
            }
            if (time == 200f)
            {

                splashD.SetActive(true);
            }
            if (time == 100f)
            {
                splashD.SetActive(false);
                splashGGJ.SetActive(true);
            }
            if (time == 0f)
            {

                splashGGJ.SetActive(false);
                backg.SetActive(false);
                us.SetActive(false);
            }
        }

    }

    public void StartButton()
    {
        SceneManager.LoadScene("level1", LoadSceneMode.Single);//load scene level
        FindObjectOfType<SoundManager>().Play("MenuButtonSelectSound");
    }

    public void LoadButton()
    {
        FindObjectOfType<SoundManager>().Play("MenuButtonSelectSound");
    }
    public void Controls()
    {
        FindObjectOfType<SoundManager>().Play("MenuButtonSelectSound");
        controls.SetActive(true);
    }
    public void CreditsButton()
    {
        timeron = true;

        FindObjectOfType<SoundManager>().Play("MenuButtonSelectSound");



    }

}
