using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public GameObject cameraGameObject;

	public GameObject LBall;
    public float moveSpeed = 5.0F;
    public float jumpSpeed = 8.0F;
    public float gravityScale = 20.0F;
    public bool canMove = true;
    public bool canCastSpell = true;
    public CharacterController player;
    public Animator anim;
    private Vector3 moveDirection = Vector3.zero;
    private float vertVelocity, moveLR, moveFB;
   

    void Start()
    {
        player = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        cameraGameObject = GameObject.FindGameObjectWithTag("MainCamera");
        canMove = true;
    }
    void FixedUpdate()
    {
        if (canMove == true)
        {
            Movement();
        }
        else
        {

            StopMoving();
        }

        ApplyGravity();
    }
	void Update()
	{
		if (Input.GetButtonDown("Fire2") && canCastSpell)
			lightningBall ();

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
    }
    private void ApplyGravity()
    {
        vertVelocity += Physics.gravity.y * gravityScale * Time.deltaTime;
        //  vertVelocity = Mathf.Clamp(vertVelocity, -500f, jumpForce);
    }
    public void StopMoving()
    {
        vertVelocity = Physics.gravity.y;
        Vector3 movement = new Vector3(0, vertVelocity, 0);
        // movement = character.rotation * movement;
        movement = movement.normalized * moveSpeed;

        player.Move(movement * Time.deltaTime);
    }
    void Movement()
    {

        moveLR = Input.GetAxis("Horizontal") * moveSpeed;
        moveFB = Input.GetAxis("Vertical") * moveSpeed;
        //   Debug.Log(Input.GetAxis(playerInput.HorzInput));

        Vector3 movement = new Vector3(moveLR, vertVelocity, moveFB);
        // movement = character.rotation * movement;
        // movement = movement.normalized * moveSpeed;

        Vector3 playerDir = Vector3.Normalize(cameraGameObject.transform.forward);
        playerDir.y = 0;

        player.transform.forward = playerDir;

        movement = transform.TransformDirection(movement);
        player.Move(movement * Time.deltaTime);

        //rotate to where were moving
        //  this.transform.rotation = Quaternion.LookRotation(movement);
        // this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15F);
        Vector3 facingrotation = Vector3.Normalize(new Vector3(Input.GetAxis("Horizontal"), 0f , Input.GetAxis("Vertical")));
        facingrotation = transform.TransformDirection(facingrotation);
        if (facingrotation != Vector3.zero)
        {
            //This condition prevents from spamming "Look rotation viewing vector is zero" when not moving.
            transform.forward = facingrotation;
        }
    }

	void lightningBlade()
	{

	}

	void lightningBall()
	{
        StartCoroutine("LightningDelay");
		Instantiate(LBall, transform.position, transform.rotation);
	}

    IEnumerator LightningDelay()
    {
        canMove = false;
        canCastSpell = false;
        anim.SetTrigger("LightningBall");
        yield return new WaitForSeconds(1f);
        canMove = true;
        canCastSpell = true;
    }


}
