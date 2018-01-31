using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Soldier : MonoBehaviour
{
    public enum State
    {
        Idle,
        Marching,
        Attacking,
        Dead
    }

    private const string bulletSpawnerName = "bulletSpawner";


    public GameObject bulletPrefab;

    /// <summary>
    /// Counted in seconds
    /// </summary>
    public float attackWaitTime = 0.5f;


    State currentState = State.Idle;

    Unit parentUnit = null;

    [HideInInspector]
    public Transform bulletSpawnerTransform;

    public Animator anim;
    private bool isDead = false;
    public bool isAlberto = false;

    public ParticleSystem muzzle;

    CharacterController soldierCharacterController;
    NavMeshAgent navMeshAgent;

    SoundManager soundManager;
   

    /// <summary>
    /// Counted in seconds
    /// </summary>
    float lastAttackTime = 0;


    void Awake()
    {
        soldierCharacterController = GetComponent<CharacterController>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        bulletSpawnerTransform = gameObject.transform.Find(bulletSpawnerName);
        soundManager = GameObject.Find("SM").GetComponent<SoundManager>();
    }

	// Use this for initialization
	void Start () {
        setState(State.Idle);

        anim = GetComponentInChildren<Animator>();

	    if (isAlberto)
	    {
	        navMeshAgent.speed = navMeshAgent.speed*1.02f;
	        StartCoroutine(playImTheOne());
	    }
    }
	
	// Update is called once per frame
	void Update () {
	    switch (currentState)
	    {
	        case State.Idle:
                getLost();
	            break;

            case State.Marching:
	            if (parentUnit != null && parentUnit.lastSeenPlayerLocation != null && !isDead)
                    moveTowards(parentUnit.lastSeenPlayerLocation.Value);
	            break;

            case State.Attacking:
                if (parentUnit != null && parentUnit.lastSeenPlayerLocation != null && !isDead)
                    attackTowards(parentUnit.lastSeenPlayerLocation.Value);
                break;

            case State.Dead:
                isDead = true;
                getLost();
                break;
	    }
        
	}

    void LateUpdate()
    {
        if (navMeshAgent.velocity != Vector3.zero)
        {
            if (isAlberto)
            {
                anim.SetBool("isAlberto", true);
            }
            else
            {
                anim.SetBool("isWalking", true);
            }
           
        }
        else
        {
            anim.SetBool("isWalking", false);
            if (isAlberto)
            {
                anim.SetBool("isAlberto", true);
            }
        }
    }

    IEnumerator playImTheOne()
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(3f);
            soundManager.play(soundManager.albertoImTheOne);
        }
    }

    public void setState(State state)
    {
        currentState = state;
    }

    public void setUnit(Unit unit)
    {
        parentUnit = unit;
    }

    void comeToAStop()
    {
      //  anim.SetBool("isWalking", false);
        navMeshAgent.ResetPath();
        navMeshAgent.velocity = Vector3.zero;
    }


    void moveTowards(Vector3 targetPos)
    {
      //  anim.SetBool("isWalking", true);
        NavMeshPath navMeshPath = new NavMeshPath();
        navMeshAgent.CalculatePath(targetPos, navMeshPath);
        navMeshAgent.SetPath(navMeshPath);
    }

    void getLost()
    {
        comeToAStop();
    }

    void attackTowards(Vector3 targetPos)
    {
        if (isDead)
        {
            return;
        }

        if (isAlberto)
        {
            moveTowards(targetPos);
        }
        else
        {
            comeToAStop();

            targetPos.y = transform.position.y;
            transform.LookAt(targetPos);

//        Debug.Log("Now: " + Time.time);
//        Debug.Log("Last: " + lastAttackTime);

            if (Time.time - lastAttackTime > attackWaitTime && !isDead)
            {
                shoot(targetPos);
            }
        }
    }

    void shoot(Vector3 targetPos)
    {
        if (isDead)
        {
            return;
        }

        muzzle.Play();

        anim.SetTrigger("Shoot");
        GameObject bulletGameObject = Instantiate(bulletPrefab, bulletSpawnerTransform.position, bulletSpawnerTransform.rotation);
        SoldierBullet soldierBullet = bulletGameObject.GetComponent<SoldierBullet>();

        soldierBullet.setTarget(targetPos);
        lastAttackTime = Time.time;
    }

    public void Die()
    {
        setUnit(null);
        navMeshAgent.enabled = false;
        soldierCharacterController.enabled = false;
        soldierCharacterController.detectCollisions = false;
        bulletSpawnerTransform.gameObject.SetActive(false);
        GetComponent<BoxCollider>().enabled = false;
        setState(State.Dead);
        anim.SetTrigger("Die");
        StartCoroutine("Despawn");
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(10f);
        Destroy(this.gameObject);
    }
}
