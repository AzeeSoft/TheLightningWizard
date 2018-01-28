using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportMove : MonoBehaviour
{
    public enum State
    {
        Idle,
        Dabbing,
        Teleporting
    }

    public float teleportRange = 10f;
    public float teleportSpeed = 10f;

    public float dabPercent = 10f;
    public float teleportRangeAjdusment = 10f;

    public float teleportMaxTime = 0.5f;

    public ParticleSystem transmissionParticleSystem;

    public State currentState = State.Idle;

    CharacterController characterController;
    PlayerController playerController;

    bool isTeleporting = false;

    Vector3 teleportSource;
    Vector3 teleportTarget;

    Vector3 oldScale;

    float teleportStartTime;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerController = GetComponent<PlayerController>();
    }

    // Use this for initialization
    void Start ()
	{

	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.Mouse0))
	    {
	        if (playerController.canMove)
	        {
	            Vector3 dir = transform.forward.normalized;
	            RaycastHit raycastHit;
	            bool didHit = Physics.Raycast(transform.position, dir, out raycastHit, teleportRange);

	            float teleportDist = teleportRange; 

                if (didHit)
                {
                    teleportDist = raycastHit.distance;
                }

	            teleportDist -= teleportRangeAjdusment;

	            if (teleportDist < 0)
	                teleportDist = 0;

                Vector3 endPos = transform.position + (dir * teleportDist);

                startTeleport(endPos);
	        }
	    }

	    if (currentState != State.Idle)
	    {
	        if (Time.time - teleportStartTime > teleportMaxTime)
	        {
	            stopTeleport();
	        }
	        else
	        {

	            float totalDist = Vector3.Distance(teleportSource, teleportTarget);
	            float currentDist = Vector3.Distance(teleportSource, transform.position);

	            if (currentDist >= totalDist)
                {
                    stopTeleport();
	            }
	            else
	            {
	                float dabInterval = ((totalDist*dabPercent)/100);

	                Debug.Log("TD: " + totalDist);
	                Debug.Log("CD: " + currentDist);
	                Debug.Log("DI: " + dabInterval);

	                if (currentDist < dabInterval)
	                {
	                    currentState = State.Dabbing;

                        if(transform.localScale != Vector3.zero)
	                        oldScale = transform.localScale;
	                }
	                else if (currentDist >= dabInterval && currentDist < (totalDist - dabInterval))
	                {
	                    currentState = State.Teleporting;
	                    transform.localScale = Vector3.zero;
	                }
	                else
	                {
	                    currentState = State.Dabbing;
	                    transform.localScale = oldScale;
	                }

	                characterController.Move((teleportTarget - transform.position).normalized*teleportSpeed);
	            }
	        }
	    }
	}


    void startTeleport(Vector3 endPos)
    {
        playerController.canMove = false;
        currentState = State.Dabbing;

        teleportSource = transform.position;
        teleportTarget = endPos;

        playerController.anim.SetTrigger("Teleport");

        transmissionParticleSystem.Play();

        teleportStartTime = Time.time;
    }

    void stopTeleport()
    {
        transform.localScale = oldScale;

        playerController.canMove = true;
        currentState = State.Idle;

        transmissionParticleSystem.Stop();
    }
}
