using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //character controller
    public CharacterController controller;
    //camera movement
    public Transform cam;
    //speed of the movement
    public float speed = 6f;
    //rigidbody
    private Rigidbody playerRb;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    // Update is called once per frame
    void Update()
    {
        //horizontal direction
        float horizontal = Input.GetAxisRaw("Horizontal");
        //vertical direction
        float vertical = Input.GetAxisRaw("Vertical");
        //moves in x, z axis and moves in a normal speed when moved diagonally
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

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
}
