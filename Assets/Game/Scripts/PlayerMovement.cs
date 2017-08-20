using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float baseSpeed;

    float sprintSpeed;
    float speed;
    Vector3 movement;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        speed = baseSpeed;
        sprintSpeed = baseSpeed * 2.5f;
    }

    private void Update()
    {
        if (StateManager.isDead) return;

        if (!StateManager.isSprinting)
        {
            if(Camera.main.fieldOfView != 60)
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 60, 2 * Time.deltaTime);

            speed = baseSpeed;
        }
        else
        {
            if (Camera.main.fieldOfView != 70)
                Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 70, 2 * Time.deltaTime);

            speed = sprintSpeed;
        }

        movement = new Vector3(StateManager.leftStickInputX * speed * Time.deltaTime, 0, StateManager.leftStickInputY * speed * Time.deltaTime);

        movement = Camera.main.transform.TransformDirection(movement);
        movement.y = 0f;

        if(movement != Vector3.zero)
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);

        rb.velocity = movement;
    }
}
