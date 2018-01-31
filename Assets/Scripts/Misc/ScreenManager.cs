using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{

    public Animator screenTransitionAnim;

	// Use this for initialization
	void Start () {
        open();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void open()
    {
        screenTransitionAnim.SetBool("Open", true);
    }

    void close()
    {
        screenTransitionAnim.SetBool("Open", false);
    }
}
