using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float playerSpeed = 4;
    public Rigidbody2D rb2D;
    //public float dodgeCount;
    //private bool facingRight = true;
    Animator m_Animator;
    public float RunM = 4.5f;



    enum Face
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }

    Face facing = Face.DOWN;

    public string getFacing() { return facing.ToString(); }



    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
       
    }

    void FixedUpdate()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        Vector2 targetVelocity = new Vector2(moveX, moveY);
        GetComponent<Rigidbody2D>().velocity = targetVelocity * playerSpeed;

        m_Animator.SetBool("GunBck", false);
        m_Animator.SetBool("GunRht", false);
        m_Animator.SetBool("GunLft", false);
        m_Animator.SetBool("GunFrnt", false);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            playerSpeed = RunM;
        }
        else
        {
            playerSpeed = 4;
        }

        m_Animator.SetBool("PlayerSwing", false);
        if (moveX < 0)
        {
            facing = Face.LEFT;
           // Debug.Log("you are facing" + facing);

            if (m_Animator.GetInteger("WeaponChanger") == 0)
            {

                m_Animator.SetBool("GunBck", false);
                m_Animator.SetBool("GunRht", false);
                m_Animator.SetBool("GunLft", true);
                m_Animator.SetBool("GunFrnt", false);
            }
            else
            {

            }
        }
        else if (moveX > 0)
        {
            facing = Face.RIGHT;
            //Debug.Log("you are facing" + facing);

            if (m_Animator.GetInteger("WeaponChanger") == 0)
            {

                m_Animator.SetBool("GunBck", false);
                m_Animator.SetBool("GunRht", true);
                m_Animator.SetBool("GunLft", false);
                m_Animator.SetBool("GunFrnt", false);
            }
            else
            {

            }
        }
        if (moveY < 0)
        {
            facing = Face.UP;
            Debug.Log("you are facing" + facing);

            if (m_Animator.GetInteger("WeaponChanger") == 0)
            {
 
                m_Animator.SetBool("GunBck", false);
                m_Animator.SetBool("GunRht", false);
                m_Animator.SetBool("GunLft", false);
                m_Animator.SetBool("GunFrnt", true);
            }
            else
            {

            }
        }
        else if (moveY > 0)
        {
            facing = Face.DOWN;
            //Debug.Log("you are facing " + facing);

            if (m_Animator.GetInteger("WeaponChanger") == 0)
            {

                m_Animator.SetBool("GunBck", true);
                m_Animator.SetBool("GunRht", false);
                m_Animator.SetBool("GunLft", false);
                m_Animator.SetBool("GunFrnt", false);
            }
            else
            {

            }
        }




    }
    public void Swing()
    {
        m_Animator.SetBool("PlayerSwing", true);
    }

}
/*
        if (jump)
        {

            anim.SetTrigger("jump");
            rb2d.AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }
        */




