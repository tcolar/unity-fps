using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprintAndCrouch : MonoBehaviour {

    private PlayerMovement playerMovement;
    private PlayerSteps playerSteps;

    public float sprintSpeed = 10;
    public float moveSpeed = 5;
    public float crouchSpeed = 2;

    private Transform lookRoot;
    private float standHeight = 1.6f;
    private float crouchHeight = 1;

    private bool isCrouching = false;

    private float sprintVol = 1f;
    private float crouchVol = 0.1f;
    private float walkVolMin = 0.2f;
    private float walkVolMax = 0.6f;
    private float walkStepDist = 0.4f;
    private float sprintStepDist = 0.25f;
    private float crouchStepDist = 0.5f;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        lookRoot = transform.GetChild(0);
        playerSteps = GetComponentInChildren<PlayerSteps>();
    }

    private void Start()
    {
        playerSteps.volMin = walkVolMin;
        playerSteps.volMax = walkVolMax;
        playerSteps.stepDistance = walkStepDist;
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
            playerSteps.stepDistance = sprintStepDist;
            playerSteps.volMin = sprintVol;
            playerSteps.volMax = sprintVol;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) && !isCrouching)
        {
            playerMovement.speed = moveSpeed;
            playerSteps.stepDistance = walkStepDist;
            playerSteps.volMin = walkVolMin;
            playerSteps.volMax = walkVolMin;
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
                playerSteps.stepDistance = walkStepDist;
                playerSteps.volMin = walkVolMin;
                playerSteps.volMax = walkVolMin;
            }
            else
            {
                lookRoot.localPosition = new Vector3(0f, crouchHeight, 0f);
                playerMovement.speed = crouchSpeed;
                playerSteps.stepDistance = crouchStepDist;
                playerSteps.volMin = crouchVol;
                playerSteps.volMax = crouchVol;
            }
            isCrouching = !isCrouching;
        }
    }
}
