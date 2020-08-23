using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPPlayerMovement1 : MonoBehaviour
{
    //character controller
	public CharacterController controller;
    //walkspeed
	public float walkSpeed = 6f;
    //runspeed
	public float runSpeed = 12f;
	//current speed the player is using
	public float currentSpeed;
	//capsule collider
	//public CapsuleCollider charCollider;

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

    // Start is called before the first frame update
    void Start()
    {
		//charCollider = GetComponent<CapsuleCollider>;
    }

    // Update is called once per frame
    void Update()
    {
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
        if(Input.GetKey(KeyCode.LeftShift) && isGrounded)
		{
			currentSpeed = runSpeed;
		}else
		{
			currentSpeed = walkSpeed;
		}

		Vector3 move = transform.right * x + transform.forward * z;

		controller.Move(move * currentSpeed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
		{
			velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
		}

		velocity.y += gravity * Time.deltaTime;

		controller.Move(velocity * Time.deltaTime);
    }
}
