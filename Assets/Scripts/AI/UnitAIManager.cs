using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAIManager : MonoBehaviour
{

    AIManager aiManager;

    public Unit[] units;

    public float lastSeenStopRange = 1f;

    // Use this for initialization
    void Start ()
    {
        aiManager = GetComponent<AIManager>();
    }
	
	// Update is called once per frame
	void Update () {
        foreach (Unit unit in units)
        {
            computeState(unit);
        }
    }

    public void autoFetchUnits()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Unit");

        units = new Unit[gameObjects.Length];
        for (int i = 0; i < gameObjects.Length; i++)
        {
            units[i] = gameObjects[i].GetComponent<Unit>();
        }
    }

    void computeState(Unit unit)
    {
        bool playerInSight = unit.locatePlayer();
        if (playerInSight)
        {
            unit.setState(Unit.State.Engaged);
        }
        else
        {
            if (unit.lastSeenPlayerLocation != null)
            {
                if (unit.getUnitDistanceFromPlayerLastSeen() > lastSeenStopRange)
                {
                    unit.setState(Unit.State.Engaged);
                }
                else
                {
                    unit.setState(Unit.State.Idle);
                }
            }
            else
            {
                unit.setState(Unit.State.Idle);
            }
        }
    }
}
