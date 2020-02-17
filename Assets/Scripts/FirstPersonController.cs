using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float WalkSpeed = 12f;
    public float TurnSpeed = 100f;
    public Transform CameraTransform;

    private Rigidbody rb;


    private float curUpDownLook = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Move()
    {
        float moveV = Input.GetAxis("Vertical");
        float moveH = Input.GetAxis("Horizontal");

        Vector3 motion = transform.forward * moveV * WalkSpeed + transform.right * moveH * WalkSpeed;
        motion = motion * Time.deltaTime;

        transform.position = transform.position + motion;
    }

    void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * TurnSpeed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * TurnSpeed * Time.deltaTime;

        curUpDownLook -= Mathf.Clamp(mouseY, -90f, 90f);

        transform.Rotate(Vector3.up, mouseX);
        CameraTransform.localRotation = Quaternion.Euler(curUpDownLook, 0f, 0f);
    }

    void Update()
    {
        Look();
        Move();
    }
}
