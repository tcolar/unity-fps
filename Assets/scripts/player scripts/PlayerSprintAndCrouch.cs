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

    private float sprintVol = 1f;
    private float crouchVol = 0.1f;
    private float walkVolMin = 0.2f;
    private float walkVolMax = 0.6f;
    private float walkStepDist = 0.4f;
    private float sprintStepDist = 0.25f;
    private float crouchStepDist = 0.5f;

	private PlayerStats stats;
	private float sprintVal = 100f;
	private float sprintThreshold = 10;
    private bool isSprinting;
    private bool isCrouched;

	void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        lookRoot = transform.GetChild(0);
        playerSteps = GetComponentInChildren<PlayerSteps>();
		stats = GetComponent<PlayerStats>();
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
		if (Input.GetKeyDown(KeyCode.C)) {
            isCrouched = true;
			Crouch();
		}
        else if (Input.GetKeyUp(KeyCode.C))
        {
            isCrouched = false;
            Walk();
        }
        else
        {
            WalkOrSprint();
        }
    }

    void WalkOrSprint()
	{
        sprintVal += (isSprinting ? -1f : 1f) * sprintThreshold * Time.deltaTime;
        if (sprintVal < 0) sprintVal = 0;
        if (sprintVal > 100) sprintVal = 100;
        if (!isCrouched)
        {
            if (Input.GetKeyUp(KeyCode.LeftShift) || sprintVal <= 0)
            {
                Walk();
            }
            else if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                Sprint();
            }
        }
        stats.DisplayStaminaStats(sprintVal);
	}

    void Walk()
	{
        isSprinting = false;
        lookRoot.localPosition = new Vector3(0f, standHeight, 0f);
        playerMovement.speed = moveSpeed;
		playerSteps.stepDistance = walkStepDist;
		playerSteps.volMin = walkVolMin;
		playerSteps.volMax = walkVolMin;
	}

    void Sprint()
    {
        isSprinting = true;
        playerMovement.speed = sprintSpeed;
        playerSteps.stepDistance = sprintStepDist;
        playerSteps.volMin = sprintVol;
        playerSteps.volMax = sprintVol;
		sprintVal -= sprintThreshold * Time.deltaTime;
	}

	void Crouch()
    {
        isSprinting = false;
        lookRoot.localPosition = new Vector3(0f, crouchHeight, 0f);
        playerMovement.speed = crouchSpeed;
        playerSteps.stepDistance = crouchStepDist;
        playerSteps.volMin = crouchVol;
        playerSteps.volMax = crouchVol;
    }
}
