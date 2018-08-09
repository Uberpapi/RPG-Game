using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class ThirdPersonMovement : MonoBehaviour
{

    CharacterController player;
    BaseBehaviour myBehaviour;
    Animator animator;

    public Transform centerPoint, myCamera;
    private Vector3 movement;
    float vertical, horizontal, mouseX, mouseY, zoom, forwardAmount, sprintSpeed, speedholder, turnrate;
    private float moveFB, moveLR, vertVelocity, turn, maxYClamp, minYClamp;
    RaycastHit hit;
    int layerMask = (1 << 11);
    public float moveSpeed = 10f;
    public float jumpVelocity = 8f;
    public float zoomSpeed = 4f;
    public float zoomMax = -20f;
    public float zoomMin = -8f;
    public float cameraSpeed = 2f;
    public float rotationSpeed = 10f;

    private bool jumpPressed, runningForward, mouse1Pressed, mouse2Pressed, turnPressed, leftShiftPressed = false;

    // Use this for initialization
    void Start()
    {
        player = transform.GetComponent<CharacterController>();
        myBehaviour = transform.GetComponent<BaseBehaviour>();
        animator = transform.GetComponent<Animator>();
        //Initiate variables
        zoom = -12f;
        sprintSpeed = moveSpeed * 6f;
        speedholder = moveSpeed;
    }

    void Update()
    {
        CheckInputs();
        Moving();
        Gravity(); CameraControl();
        AnimationControl();
    }


    void CheckInputs()
    {
        if (Input.GetButton("Fire1"))
            mouse1Pressed = true;
        else
            mouse1Pressed = false;

        if (Input.GetButton("Fire2"))
        {
            mouse2Pressed = true;
        }
        else
        {
            mouse2Pressed = false;
        }

        if (Input.GetButton("Turn"))
            turnPressed = true;
        else
            turnPressed = false;

        if (Input.GetKey(KeyCode.LeftShift))
            leftShiftPressed = true;
        else
            leftShiftPressed = false;

        if (Input.GetButton("Jump"))
            jumpPressed = true;
        else
            jumpPressed = false;
    }

    void Gravity()
    {
        if (player.isGrounded)
        {
            if (jumpPressed)
            {
                vertVelocity = jumpVelocity;
                animator.SetTrigger("jump");
            }
            else
            {
                vertVelocity = (Physics.gravity.y * Time.deltaTime);
            }
        }
        else
        {
            vertVelocity += (Physics.gravity.y * Time.deltaTime) * 3;
            vertVelocity = Mathf.Clamp(vertVelocity, -80f, jumpVelocity);
            jumpPressed = false;
        }
    }

    void Moving()
    {
        if (leftShiftPressed)
        {
            moveSpeed = sprintSpeed;
        }
        else
            moveSpeed = speedholder;

        moveFB = Input.GetAxis("Vertical");
        if (moveFB < 0)
        { //Used when moving backwards
            moveFB *= moveSpeed / 2;
            runningForward = false;
        }
        else
        { //Used when moving forwards
            moveFB *= moveSpeed;
            runningForward = true;
        }
        moveLR = Input.GetAxis("Horizontal") * (moveSpeed / 1.5f);

        if (turnPressed && !mouse1Pressed && !mouse2Pressed)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, centerPoint.eulerAngles.y, 0f), Time.deltaTime * rotationSpeed / 2);
        }
        else if (turnPressed && mouse2Pressed)
        {
            moveLR = Input.GetAxis("Turn") * (moveSpeed / 1.5f);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, centerPoint.eulerAngles.y, 0f), Time.deltaTime * rotationSpeed);
        }
        else if (turnPressed && mouse1Pressed)
        {
            //we should rotate the player here without rotating the camera, does not work atm
        }

        Vector3 movement = new Vector3(moveLR, vertVelocity, moveFB);
        movement = (transform.rotation * movement);
        player.Move(movement * Time.deltaTime);
        centerPoint.position = transform.position + new Vector3(0f, 2f, 0f);
    }

    void CameraControl()
    {
        maxYClamp = (80f / cameraSpeed);
        minYClamp = (-9.5f / cameraSpeed);
        mouseY = Mathf.Clamp(mouseY, minYClamp, maxYClamp);

        if (mouse2Pressed)
        {
            mouseX += Input.GetAxis("Mouse X");
            mouseY -= Input.GetAxis("Mouse Y");

            centerPoint.localRotation = Quaternion.Euler(mouseY * cameraSpeed, (mouseX + turn) * cameraSpeed, 0);
        }
        else if (mouse1Pressed)
        {
            mouseX += Input.GetAxis("Mouse X");
            mouseY -= Input.GetAxis("Mouse Y");
            centerPoint.localRotation = Quaternion.Euler(mouseY * cameraSpeed, (mouseX + turn) * cameraSpeed, 0);
        }
        else
        {
            turn += Input.GetAxis("Turn");
            centerPoint.localRotation = Quaternion.Euler(mouseY * cameraSpeed, (mouseX + turn) * cameraSpeed, 0);
        }

        zoom += Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        if (zoom > zoomMin)
            zoom = zoomMin;
        else if (zoom < zoomMax)
            zoom = zoomMax;
        myCamera.localPosition = new Vector3(0f, 0f, zoom);
        myCamera.LookAt(centerPoint);

        if (Physics.Linecast(transform.position + new Vector3(0, 1, 0), myCamera.position, out hit, layerMask))
        {
            myCamera.position = new Vector3(hit.point.x, myCamera.position.y, hit.point.z) + myCamera.forward * 2;
        }

    }

    void AnimationControl()
    {
        if (player.velocity == Vector3.zero)
        {
            animator.SetBool("idle", true);
            animator.SetBool("run", false);
            animator.SetBool("backwards", false);
        }
        else
        {
            if (runningForward)
            {
                animator.SetBool("run", true);
                animator.SetBool("idle", false);
                animator.SetBool("backwards", false);
            }
            else
            {
                animator.SetBool("run", false);
                animator.SetBool("idle", false);
                animator.SetBool("backwards", true);
            }
        }
    }
}
