using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointFollower : MonoBehaviour {

    public Transform[] entryWaypoints;
    public Transform[] loopingWaypoints;
    public float speed = 5f;
    public float waitTime = 2f;

    int currentWayPointIndex = 0;
    bool inEntry = true;
    bool isFirstWayPoint = true;

    Transform nextWayPoint = null;

    float lastWayPointTime = 0;

	// Use this for initialization
	void Start () {
        isFirstWayPoint = true;
        if (entryWaypoints.Length > 0)
        {
            nextWayPoint = entryWaypoints[0];
            currentWayPointIndex = 0;
            inEntry = true;
        } else if(loopingWaypoints.Length >0)
        {
            nextWayPoint = loopingWaypoints[0];
            currentWayPointIndex = 0;
            inEntry = false;
        }  		
	}
	
	// Update is called once per frame
	void Update () {
        if (nextWayPoint != null)
        {
            moveToNextWayPoint();
        }
	}

    void moveToNextWayPoint()
    {
        if (isFirstWayPoint)
        {
            transform.forward = nextWayPoint.forward;
            transform.position = nextWayPoint.position;
            isFirstWayPoint = false;
        }
        else
        {
            if (Time.time - lastWayPointTime > waitTime)
            {
                transform.forward = Vector3.RotateTowards(transform.forward, nextWayPoint.forward, speed * Time.deltaTime * (nextWayPoint.forward - transform.forward).magnitude / (nextWayPoint.position - transform.position).magnitude, 0.0f);

                // move towards the target
                transform.position = Vector3.MoveTowards(transform.position, nextWayPoint.position, speed * Time.deltaTime);
            }
        }


        if (transform.position == nextWayPoint.position)
        {
            currentWayPointIndex++;
            if (inEntry)
            {
                if (currentWayPointIndex < entryWaypoints.Length)
                {
                    nextWayPoint = entryWaypoints[currentWayPointIndex];
                    lastWayPointTime = Time.time;
                } else
                {
                    currentWayPointIndex = 0;
                    nextWayPoint = loopingWaypoints[currentWayPointIndex];
                    inEntry = false;
                    lastWayPointTime = Time.time;
                }
            } else
            {
                if (currentWayPointIndex >= loopingWaypoints.Length)
                {
                    currentWayPointIndex = 0;
                }
                nextWayPoint = loopingWaypoints[currentWayPointIndex];
                lastWayPointTime = Time.time;
            }
        }
    }
}
