using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{

    [HideInInspector]
    public GameObject playerGameObject;

    UnitAIManager unitAiManager;

    void Awake()
    {
        playerGameObject = GameObject.FindGameObjectWithTag(Tags.Player);
        unitAiManager = GetComponent<UnitAIManager>();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

}
