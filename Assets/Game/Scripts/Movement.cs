using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Movement Variables")]
    public float speed = 250f;
    public float rotationSpeed = 100f;

    [Space, Header("Jump Variables")]
    public LayerMask ground;
    public float distToGrounded = 1.1f;
    public float jumpForce = 10f;
    public float gravity = 2f;

    Rigidbody rb;
    Animator anim;
    Camera mainCamera;

    float horizontal;
    float vertical;

    float horizontal2;

    Vector3 movement;

    int jumps;
    
    bool doubleJump;

	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        mainCamera = Camera.main;
	}

    void Update()
    {
        Move();
        Rotate();

        anim.SetBool("Grounded", Grounded());

        if (Input.GetKeyDown(KeyCode.Space))
            Jump();

        if (Grounded())
        {
        }
        else
        {
            if(Attack.attacking)
                anim.SetLayerWeight(4, 0);
            else
                anim.SetLayerWeight(4, 1);

            Gravity();
        }
	}

    void Move()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        anim.SetFloat("Horizontal", horizontal);
        anim.SetFloat("Vertical", vertical);

        movement = new Vector3(horizontal, 0, vertical).normalized;
        movement *= speed * Time.deltaTime;
        movement = mainCamera.transform.TransformDirection(movement);

        if (movement == Vector3.zero)
            anim.SetBool("IsIdle", true);
        else
            anim.SetBool("IsIdle", false);

        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);
    }

    void Rotate()
    {
        horizontal2 = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up * horizontal2 * rotationSpeed * Time.deltaTime);
    }

    void Gravity()
    {
        rb.velocity += Physics.gravity * gravity * Time.fixedDeltaTime;
    }

    void Jump()
    {
        if(Grounded())
        {
            doubleJump = true;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            anim.SetTrigger("Jump");
        }
        else if(!Grounded() && doubleJump)
        {
            rb.AddForce(Vector3.up * (jumpForce * 2), ForceMode.Impulse);
            doubleJump = false;
            anim.SetTrigger("Jump");
        }
    }

    bool Grounded()
    {
        return Physics.Raycast(transform.position + Vector3.up, Vector3.down, distToGrounded, ground);
    }
}
