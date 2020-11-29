using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPPlayerMovement : MonoBehaviour
{
    //character controller
	public CharacterController controller;
	//cam orientation
	public Transform orientation;
    //walkspeed
	public float walkSpeed = 6f;
    //runspeed
	public float runSpeed = 12f;
	//current speed the player is using
	public float currentSpeed;
	//capsule collider
	CapsuleCollider charCollider;

    //vector3
	Vector3 velocity;
    //gravity
	public float gravity = -9.81f;
    //jump height
    public float jumpHeight = 3f;


    //groundcheck is a gameobject
	public Transform groundCheck;
    //the distance between groundCheck and the groundDistance
	public float groundDistance = 0.4f;
    //groundMask
	public LayerMask groundMask;


    //isGrounded
	bool isGrounded;
	//isCrouching
	bool isCrouching;


	

    // Start is called before the first frame update
    void Start()
    {
		charCollider = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {

		//isgrounded uses physics.checksphere to check if player is grounded
		isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        //gravity
        if(isGrounded && velocity.y < 0)
		{
			velocity.y = -2f;
		}

        //move in the x-axis 
		float x = Input.GetAxis("Horizontal");
        //move in the y-axis
		float z = Input.GetAxis("Vertical");

        //sprint
        if(Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W) && isGrounded)
		{
			currentSpeed = runSpeed;
		}else
		{
			currentSpeed = walkSpeed;
		}

		//movement
		Vector3 move = transform.right * x + transform.forward * z;

		//character controller movement by movement and current speed and changing time
		controller.Move(move * currentSpeed * Time.deltaTime);

		//jump
        if(Input.GetButtonDown("Jump") && isGrounded)
		{
			velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
		}

		//crouch
		if (Input.GetKeyUp(KeyCode.LeftControl) && isGrounded)
		{
			DoCrouch();
		}

		//gravity
		velocity.y += gravity * Time.deltaTime;
		//jump
		controller.Move(velocity * Time.deltaTime);
    }


	// <summary>
    // in progress
    // </summary>
    void DoCrouch()
	{
		isCrouching = true;

		if (isCrouching)
		{
			charCollider.height += 1f;
		}
		else
		{
			charCollider.height -= 1f;
		}
        
	}


}
