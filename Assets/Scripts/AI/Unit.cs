using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public enum State
    {
        Idle,
        Engaged
    }


    AIManager aiManager;

    public Soldier[] soldiers;

    public State currentState = State.Idle;

    public Vector3? lastSeenPlayerLocation = null;

    public float readyToAttackRange = 15f;
    public float attackRange = 20f;

    void Awake()
    {
        aiManager = FindObjectOfType<AIManager>();
        soldiers = gameObject.GetComponentsInChildren<Soldier>();
        foreach (Soldier soldier in soldiers)
        {
            soldier.setUnit(this);
        }
    }

	// Use this for initialization
	void Start () {
        setState(State.Idle);
    }
	
	// Update is called once per frame
	void Update () {
	    switch (currentState)
	    {
            case State.Idle:
                getLost();
	            break;
            case State.Engaged:
                engage();
                break;
	    }
	}


    public void setState(State state)
    {
        currentState = state;
    }


    public bool locatePlayer()
    {
        foreach (Soldier soldier in soldiers)
        {
            if (soldier != null && canSoldierSeePlayer(soldier))
            {
                lastSeenPlayerLocation = aiManager.playerGameObject.transform.position;
                return true;
            }
        }

        return false;
    }

    public bool canSoldierSeePlayer(Soldier soldier)
    {
        Vector3 dir = aiManager.playerGameObject.transform.position - soldier.transform.position;

        RaycastHit raycastHit;
        bool didHit = Physics.Raycast(soldier.transform.position, dir, out raycastHit);

        if (didHit && raycastHit.transform.gameObject.CompareTag(Tags.Player))
        {
            return true;
        }

        return false;
    }

    public float getUnitDistanceFromPlayerLastSeen()
    {
        float minDistance = float.MaxValue;
        if (lastSeenPlayerLocation != null)
        {
            foreach (Soldier soldier in soldiers)
            {
                if (soldier != null)
                {
                    float dist = Vector3.Distance(soldier.transform.position, lastSeenPlayerLocation.Value);
                    minDistance = Mathf.Min(minDistance, dist);
                }
            }
        }

        return minDistance;
    }


    public void engage()
    {
        if (lastSeenPlayerLocation != null)
        {
            foreach (Soldier soldier in soldiers)
            {
                float distFromPlayer = Vector3.Distance(soldier.transform.position, lastSeenPlayerLocation.Value);
                if (getUnitDistanceFromPlayerLastSeen() < readyToAttackRange && distFromPlayer < attackRange && canSoldierSeePlayer(soldier))
                {
                    soldier.setState(Soldier.State.Attacking);
                }
                else
                {
                    soldier.setState(Soldier.State.Marching);
                }
            }
        }
    }


    public void getLost()
    {
        foreach (Soldier soldier in soldiers)
        {
            soldier.setState(Soldier.State.Idle);
        }
    }

}
