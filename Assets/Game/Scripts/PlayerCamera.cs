using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public Vector3 offset;

    Transform player;
    Transform myCamera;

    private void Start()
    {
        player = GameObject.Find("Player").transform;
        myCamera = Camera.main.transform;
    }

    private void LateUpdate()
    {
        myCamera.LookAt(player);
        myCamera.localPosition = Vector3.Lerp(myCamera.localPosition, player.position + offset, speed * Time.deltaTime);

        transform.position = Vector3.Lerp(transform.position, player.position + offset, 1 * Time.deltaTime);
        transform.Rotate(Vector3.up * rotationSpeed * StateManager.rightStickInputX * Time.deltaTime);
    }
}
