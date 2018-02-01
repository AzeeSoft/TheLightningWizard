using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEditor;

public class startScreen : MonoBehaviour
{
    public ScreenManager screenManager;

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

    public GameObject credits;
    public GameObject controls;
    public GameObject us;
    public GameObject splashGGJ;
    public GameObject splashD;
    public GameObject backg;
    public GameObject backbutton;

    public float time = 400f;
    bool timeron;

	private const string FILE_NAME = "Save.dat";
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
            else if (time < 0f)
            {
                backg.SetActive(false);
                us.SetActive(false);
            }
        }

    }

    public void StartButton()
    {
        FindObjectOfType<SoundManager>().Play("MenuButtonSelectSound");

        screenManager.close(() =>
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

            SceneManager.LoadScene("levelOne", LoadSceneMode.Single);//load scene level
        });
    }

    
    public void Controls()
    {
        FindObjectOfType<SoundManager>().Play("MenuButtonSelectSound");
        screenManager.close(() =>
        {
            controls.SetActive(true);
            backbutton.SetActive(true);

            screenManager.open(null);
        });
    }
    public void CreditsButton()
    {
        FindObjectOfType<SoundManager>().Play("MenuButtonSelectSound");
        screenManager.close(() =>
        {
            timeron = true;

            screenManager.open(null);
        });
    }
    public void BackButton()
    {
        FindObjectOfType<SoundManager>().Play("MenuButtonSelectSound");
        screenManager.close(() =>
        {
            controls.SetActive(false);
            backbutton.SetActive(false);

            screenManager.open(null);
        });
    }

    public void ExitGame()
    {
        screenManager.close(() =>
        {
            Application.Quit();
        });
    }
}
