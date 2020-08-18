using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //character controller
    public CharacterController controller;

    //camera movement
    public Transform cam;
    //speed of the movement - walk
    public float speed = 6f;
    //speed of the movement - run
    public float runSpeed = 12f;
    //rigidbody
    Rigidbody playerRb;
    
    //jump as vector3
    public Vector3 jump;
    //jumpforce
    public float JumpForce = 2.0f;
    
    //when player is on the ground - Error: player can jump in mid-air
    public bool isGrounded;
    
    

    //smooth turn time
    public float turnSmoothTime = 0.1f;
    //velocity of smooth turn
    float turnSmoothVelocity;
    
    
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //horizontal direction
        float horizontal = Input.GetAxisRaw("Horizontal");
        //vertical direction
        float vertical = Input.GetAxisRaw("Vertical");
        //moves in x, z axis and moves in a normal speed when moved diagonally
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        //creates a tiny sphere below the character's base, if the groundMask is collided with the ground, then isGrounded is true
        //isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            playerRb.AddForce(jump * JumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        //camera rotation
        if (direction.magnitude >= 0.1f)
        { 
            //character rotates through keys and camera direction
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            //function which smoothen the rotation
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * speed * Time.deltaTime);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }
}
