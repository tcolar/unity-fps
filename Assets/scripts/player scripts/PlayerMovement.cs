using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController cc;
    private Vector3 moveDirection;
    public float speed = 5;
    private float gravity = 20;
    public float jump_force = 10;
    private float vertVelocity;

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveThePlayer();
    }

    void MoveThePlayer()
    {
        moveDirection = new Vector3(Input.GetAxis(Axis.HORIZONTAL), 0f,
            Input.GetAxis(Axis.VERTICAL));
        // print("hz:" + Input.GetAxis("Horizontal"));
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed * Time.deltaTime;

        ApplyGravity();

        cc.Move(moveDirection);
    }

    void ApplyGravity()
    {
        vertVelocity -= gravity * Time.deltaTime;
        PlayerJump();
        moveDirection.y = vertVelocity * Time.deltaTime;
    }

    void PlayerJump()
    {
        if (cc.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            vertVelocity = jump_force;
        }
    }
}
