using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScrintAndCrouch : MonoBehaviour {

    private PlayerMovement playerMovement;

    public float sprintSpeed = 10;
    public float moveSpeed = 5;
    public float crouchSpeed = 2;

    private Transform lookRoot;
    private float standHeight = 1.6f;
    private float crouchHeight = 1;

    private bool isCrouching = false;
    
    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        lookRoot = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        Sprint();
        Crouch();
    }

    void Sprint()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) && !isCrouching)
        {
            playerMovement.speed = sprintSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) && !isCrouching)
        {
            playerMovement.speed = moveSpeed;
        }
    }

    void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (isCrouching)
            {
                lookRoot.localPosition = new Vector3(0f, standHeight, 0f);
                playerMovement.speed = moveSpeed;
            }
            else
            {
                lookRoot.localPosition = new Vector3(0f, crouchHeight, 0f);
                playerMovement.speed = crouchSpeed;
            }
            isCrouching = !isCrouching;
        }
    }
}
