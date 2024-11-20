using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    GameObject cam;
    GameObject cameraPosition;
    GameObject player;
        Rigidbody rb;

    [Header("Movement")]
    public float moveSpeed = 1;
    public float cameraSensitivity;

    [Header("Jump Thingys")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMulitpler;
    bool canJump = true;

    [Header("Ground Check")]
    public float groundDrag;
    public float playerHeight;
    public LayerMask WhatIsGround;
    bool grounded;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    //here is values for player movement, dw about these
    float xRotation;
    float yRotation;

    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;

    void Start()
    {
        cam = GameObject.Find("Main Camera");
        cameraPosition = GameObject.Find("camPos");
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // .5 -0.4, 0.9
    void Update()
    {
        Move();
        CameraMove();
        speedControl();

        //raycast to check if the player is on ground
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, WhatIsGround);
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
    }

    void Move()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        
        moveDirection = player.transform.forward * verticalInput + cameraPosition.transform.right * horizontalInput;

        if(grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed, ForceMode.Force);
        else if(!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * airMulitpler, ForceMode.Force);

        if (Input.GetKey(jumpKey) && canJump && grounded)
        {
            canJump = false;

            jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    void CameraMove()
    {
        //get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * cameraSensitivity * 50;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * cameraSensitivity * 50;

        yRotation += mouseX;
        xRotation -= mouseY;

        //stop player from rotating in circles
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //rotate the camera
        cameraPosition.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        player.transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    void speedControl()
    {
        Vector3 flatvel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //limit velocity if needed
        if(flatvel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatvel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
    void jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    void ResetJump()
    {
        canJump = true;
    }
}
