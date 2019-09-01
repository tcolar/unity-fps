using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField]
    private Transform playerRoot, lookRoot;

    [SerializeField]
    private bool invert;

    [SerializeField]
    private bool canUnlock = true;

    [SerializeField]
    private float sensivity = 5;

    [SerializeField]
    private int smoothSteps = 10;

    [SerializeField]
    private float smoothWeight = 0.4f;

    [SerializeField]
    private float rollAngle = 10;

    [SerializeField]
    private float rollSpeed = 3;

    // FIX: Video was using a Vector2 for lokk_vertical_limits
    // but that does not render in the unity properly
    // it showd but the values are 0, which breaks looking up & down
    [SerializeField]
    private float lookVerticalMin = -70;

    [SerializeField]
    private float lookVerticalMax = 80;

    private Vector2 lookAngles;

    private Vector2 currentMouseLook;
    private Vector2 smoothMove;

    private float currentRollAngle;

    private int lastLookFrame;

    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        LockAndUnlockCursor();
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            LookAround();
        }
    }

    void LockAndUnlockCursor()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }

        }

    }

    void LookAround()
    {
        currentMouseLook = new Vector2(
            Input.GetAxis(MouseAxis.MOUSE_Y), Input.GetAxis(MouseAxis.MOUSE_X));

        lookAngles.x += currentMouseLook.x * sensivity * (invert ? 1f : -1f);
        lookAngles.y += currentMouseLook.y * sensivity;

        lookAngles.x = Mathf.Clamp(lookAngles.x, lookVerticalMin, lookVerticalMax);

        currentRollAngle =
            Mathf.Lerp(currentRollAngle, Input.GetAxisRaw(MouseAxis.MOUSE_X)
            * rollAngle, Time.deltaTime * rollSpeed);

        lookRoot.localRotation = Quaternion.Euler(lookAngles.x, 0f, 0f);

        // FIX: look_Angles.x was missing in video, can't look up/down
        playerRoot.localRotation = Quaternion.Euler(lookAngles.x, lookAngles.y, 0f);

    }
} 













































