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

    float vertical, horizontal, mouseX, mouseY, zoom, forwardAmount, sprintSpeed, speedholder;
    private float moveFB, moveLR, vertVelocity, time, turn, maxYClamp, minYClamp;

    public float moveSpeed = 10f;
    public float jumpVelocity = 8f;
    public float zoomSpeed = 4f;
    public float zoomMax = -20f;
    public float zoomMin = -8f;
    public float cameraSpeed = 2f;
    public float rotationSpeed = 10f;

    private bool isJumping = false;
    private bool runningForward = false;
    private bool mousePressed = false;

    // Use this for initialization
    void Start()
    {
        player = transform.GetComponent<CharacterController>();
        myBehaviour = transform.GetComponent<BaseBehaviour>();
        animator = transform.GetComponent<Animator>();
        //Initiate variables
        zoom = -12f;
        time = 0;
        sprintSpeed = moveSpeed * 6f;
        speedholder = moveSpeed;
    }

    void Update()
    {
        time -= Time.deltaTime;
        Moving();
        AnimationControl();
    }

    void FixedUpdate()
    {

        if (Input.GetButton("Jump"))
        {
            isJumping = true;

        }

        Gravity();
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

    void Gravity()
    {
        if (player.isGrounded)
        {
            if (!isJumping)
            {
                vertVelocity = Physics.gravity.y;
            }
            else
            {
                vertVelocity = jumpVelocity;
                animator.SetTrigger("jump");
            }
        }
        else
        {
            vertVelocity += (Physics.gravity.y * Time.deltaTime) * 3;
            vertVelocity = Mathf.Clamp(vertVelocity, -80f, jumpVelocity);
            isJumping = false;
        }
    }
    RaycastHit hit;
    int layerMask = ~(1 << 9);
    void Moving()
    {
        //Lines used for zoom
        zoom += Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;

        if (zoom > zoomMin)
            zoom = zoomMin;
        else if (zoom < zoomMax)
            zoom = zoomMax;

        myCamera.localPosition = new Vector3(0f, 0f, zoom);

        // Camera follows when leftmousebutton is pressed  in order to be able to look around
        if (Input.GetButton("Fire1"))
        {
            mouseX += Input.GetAxis("Mouse X");
            mouseY -= Input.GetAxis("Mouse Y");
            mousePressed = true;
        }
        else
            mousePressed = false;

        //Used for movement with A and D keyboard inputs
        if (Input.GetButton("Turn"))
        {
            turn += Input.GetAxis("Turn");
            float turnrate = Time.deltaTime * (rotationSpeed / 2);
            turnrate = Mathf.Clamp(turnrate, -5f, 5f);
        }
        else if (!mousePressed)
        {
            Quaternion turnAngle = Quaternion.Euler(0f, centerPoint.eulerAngles.y, 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, turnAngle, Time.deltaTime * rotationSpeed / 2);
        }

        //Allows sprinting with the Shift Key
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = sprintSpeed;
        }
        else
            moveSpeed = speedholder;

        //Placement of the camera angle
        maxYClamp = (80f / cameraSpeed);
        minYClamp = (-9.5f / cameraSpeed);
        mouseY = Mathf.Clamp(mouseY, minYClamp, maxYClamp);
        centerPoint.localRotation = Quaternion.Euler(mouseY * cameraSpeed, (mouseX + turn) * cameraSpeed, 0);
        myCamera.LookAt(centerPoint);

        //Movement
        moveLR = Input.GetAxis("Horizontal") * (moveSpeed / 1.5f);
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

        Vector3 movement = new Vector3(moveLR, vertVelocity, moveFB);
        movement = (transform.rotation * movement);
        player.Move(movement * Time.deltaTime);
        centerPoint.position = transform.position + new Vector3(0f, 2f, 0f);

        //Makes the camera look from behind the player while running
        if (Input.GetAxis("Vertical") != 0f)
        {
            Quaternion turnAngle = Quaternion.Euler(0f, centerPoint.eulerAngles.y, 0f);
            //Make it gradually turn to get a smooth movement
            transform.rotation = Quaternion.Slerp(transform.rotation, turnAngle, Time.deltaTime * rotationSpeed);

        }
        if (Physics.Linecast(transform.position + new Vector3(0, 1, 0), myCamera.position, out hit, layerMask))
        {
			print(hit.transform.name);
            myCamera.position = new Vector3(hit.point.x, myCamera.position.y, hit.point.z) + myCamera.forward*2;
        }
    }
}
