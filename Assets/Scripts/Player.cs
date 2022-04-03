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

    Collider2D jumpCollider;
    Collider2D headCollider;
    Collider2D bodyCollider;


    float jumpTimer;
    float horizontalSpeed;
    float jumpInput;
    bool jumpAvailable;
    Rigidbody2D rb;

    int lastGrounded;
    void Start()
    {
        jumpCollider = GetComponent<BoxCollider2D>();
        headCollider = GetComponent<CircleCollider2D>();
        bodyCollider = GetComponent<CapsuleCollider2D>();

        rb = GetComponent<Rigidbody2D>();
        jumpTimer = 0;
        jumpAvailable = true;
        lastGrounded = 0;
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

        //speed up while jumping
        if (!jumpCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            horizontalSpeed *= 1.2f;
        }

        rb.velocity = new Vector2(horizontalSpeed * Time.deltaTime, rb.velocity.y);

        Jump();
    }

    private void Jump()
    {
        if (jumpCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            lastGrounded = 0;
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
                }
                else
                {
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
            jumpTimer = 0;
            jumpAvailable = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

    }
}
