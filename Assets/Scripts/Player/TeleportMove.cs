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

    public float teleportMinInterval = 0.5f;

    public int damage = 100;

    public GameObject graphicsGameObject;
    public ParticleSystem transmissionParticleSystem;
    public ParticleSystem chidoriParticleSystem;

    public State currentState = State.Idle;

    CharacterController characterController;
    PlayerController playerController;

    Vector3 teleportSource;
    Vector3 teleportTarget;

    Vector3 oldScale;

    float teleportStartTime;

    float lastTeleportEndTime = 0;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerController = GetComponent<PlayerController>();
    }

    // Use this for initialization
    void Start ()
    {
        StartCoroutine("startCheckForShrink");
    }
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetButton("Fire1"))
	    {
	        if (playerController.canMove && Time.time - lastTeleportEndTime > teleportMinInterval)
	        {
	            Vector3 dir = transform.forward.normalized;
	            RaycastHit raycastHit;
	            bool didHit = Physics.Raycast(transform.position, dir, out raycastHit, teleportRange);

	            float teleportDist = teleportRange; 

                if (didHit)
                {
                    if (!raycastHit.collider.gameObject.CompareTag("Enemy"))
                    {
                        teleportDist = raycastHit.distance;
                    }
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

//	                Debug.Log("TD: " + totalDist);
//	                Debug.Log("CD: " + currentDist);
//	                Debug.Log("DI: " + dabInterval);

	                if (currentDist < dabInterval)
	                {
	                    currentState = State.Dabbing;

                        if (graphicsGameObject.transform.localScale != Vector3.zero)
	                        oldScale = graphicsGameObject.transform.localScale;
	                }
	                else if (currentDist >= dabInterval && currentDist < (totalDist - dabInterval))
	                {
	                    currentState = State.Teleporting;
                        graphicsGameObject.transform.localScale = Vector3.zero;
	                }
	                else
	                {
	                    currentState = State.Dabbing;
                        graphicsGameObject.transform.localScale = oldScale;
	                }

	                characterController.Move((teleportTarget - graphicsGameObject.transform.position).normalized * teleportSpeed);
                    Debug.Log((teleportTarget - graphicsGameObject.transform.position).normalized * teleportSpeed);
	            }
	        }
	    }
	}


    void startTeleport(Vector3 endPos)
    {
        playerController.canMove = false;
        characterController.detectCollisions = false;
        GetComponent<BoxCollider>().enabled = true;

        currentState = State.Dabbing;

        teleportSource = transform.position;
        teleportTarget = endPos;

        playerController.anim.SetTrigger("Teleport");

        transmissionParticleSystem.Play();
        chidoriParticleSystem.Play();

        teleportStartTime = Time.time;
    }

    void stopTeleport()
    {
        graphicsGameObject.transform.localScale = oldScale;

        characterController.detectCollisions = true;
        GetComponent<BoxCollider>().enabled = false;

        lastTeleportEndTime = Time.time;
        playerController.canMove = true;
        currentState = State.Idle;


        transmissionParticleSystem.Stop();
        chidoriParticleSystem.Stop();
    }

    IEnumerator startCheckForShrink()
    {
        while (playerController != null)
        {
            yield return new WaitForSeconds(6);
            if (graphicsGameObject.transform.localScale == Vector3.zero)
            {
                oldScale = Vector3.one;
                stopTeleport();
            }
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Teleport attacking");

        if (currentState == State.Dabbing || currentState == State.Teleporting)
        {
            if (collider.gameObject.CompareTag("Enemy"))
            {
                collider.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
            }
        }
    }
}
