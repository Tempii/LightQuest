using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float movementSpeed = 5f;
    [SerializeField] float jumpPower = 5f;
    [SerializeField] float jumpLength = 1.5f;
    [SerializeField] int coyoteFrames = 3;

    [SerializeField] GameObject sword;

    Collider2D jumpCollider;
    Collider2D headCollider;
    Collider2D bodyCollider;
    Animator animator;


    float jumpTimer;
    float horizontalSpeed;
    bool gotSword;

    internal void GetSword()
    {
        sword.GetComponent<SpriteRenderer>().enabled = true;
        gotSword = true;
    }

    float jumpInput;
    bool jumpAvailable;
    Rigidbody2D rb;

    int lastGrounded;
    void Start()
    {
        jumpCollider = GetComponent<CapsuleCollider2D>();
        headCollider = GetComponent<CircleCollider2D>();
        bodyCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();

        rb = GetComponent<Rigidbody2D>();
        jumpTimer = 0;
        jumpAvailable = true;
        lastGrounded = 0;
        gotSword = false;
    }

    private void Update()
    {
        horizontalSpeed = Input.GetAxis("Horizontal") * movementSpeed;

        jumpInput = Input.GetAxis("Jump");
        if(jumpTimer > 0)
        {
            jumpTimer -= Time.deltaTime;
            if(jumpTimer <= 0)
            {
                jumpTimer = 0;
            }
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        //Momentum? -> Fun addition

        Move();

        Jump();
    }

    private void Move()
    {
        //speed up while jumping
        if (!jumpCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            horizontalSpeed *= 1.2f;
        }

        // going right
        if (horizontalSpeed > Mathf.Epsilon)
        {
            transform.localScale = new Vector3(1.5f, 1.5f, 1);
            animator.SetBool("isRunning", true);
        }
        //going left
        else if (horizontalSpeed < -Mathf.Epsilon)
        {
            transform.localScale = new Vector3(-1.5f, 1.5f, 1);
            animator.SetBool("isRunning", true);
        }
        else
        {
            horizontalSpeed = 0;
            animator.SetBool("isRunning", false);
        }

        rb.velocity = new Vector2(horizontalSpeed * Time.deltaTime, rb.velocity.y);
    }

    private void Jump()
    {
        if (jumpCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            lastGrounded = 0;
            animator.SetBool("isJumping", false);
        }
        else
        {
            lastGrounded++;
        }

        if (jumpInput > 0)
        {
            //check for headbump
            if (headCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
            {
                jumpTimer = 0;
                print("headbump");
            }

            if (jumpTimer <= 0)
            {
                if (lastGrounded <= coyoteFrames && jumpAvailable)
                {
                    jumpTimer = jumpLength;
                    jumpAvailable = false;
                    animator.SetBool("isJumping", true);
                }
                else
                {
                    animator.SetBool("isJumping", false);
                    return;
                }
            }


            //rb.velocity += new Vector2(0, jumpPower * jumpTimer)
            // print("Timer: " + jumpTimer);

            float jumpFactor = jumpTimer / jumpLength;
            // float jumpFactor = Mathf.Pow(jumpTimer, 2) / Mathf.Pow(jumpLength, 2);

            rb.velocity = new Vector2(rb.velocity.x, jumpPower * jumpFactor);
            //OnGround?
            //StartJump
            //not pressed
        }
        else
        {
            animator.SetBool("isJumping", false);
            jumpTimer = 0;
            jumpAvailable = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

    }
}
