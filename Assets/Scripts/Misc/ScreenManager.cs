using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour
{
    public const float openDuration = 1f;
    public const float closeDuration = 1f;

    public GameObject screenTransitionCanvas;

    Animator screenTransitionAnim;

    // Use this for initialization
    void Start ()
    {
        screenTransitionAnim = screenTransitionCanvas.GetComponent<Animator>();

        screenTransitionCanvas.SetActive(true);
        open(() =>
        {
            
        });
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void blockEvents()
    {
        screenTransitionCanvas.GetComponent<GraphicRaycaster>().enabled = true;
    }

    void allowEvents()
    {
        screenTransitionCanvas.GetComponent<GraphicRaycaster>().enabled = false;
    }

    public void open(OnTransitionComplete callback)
    {
        blockEvents();

        screenTransitionAnim.SetBool("Open", true);
        StartCoroutine(AzeeTools.executeAfter(() =>
        {
            allowEvents();

            if (callback != null)
                callback();
        }, openDuration));
    }

    public void close(OnTransitionComplete callback)
    {
        blockEvents();

        screenTransitionAnim.SetBool("Open", false);
        StartCoroutine(AzeeTools.executeAfter(() =>
        {
            allowEvents();

            if (callback != null)
                callback();
            
        }, closeDuration));
    }

    public delegate void OnTransitionComplete();
}
