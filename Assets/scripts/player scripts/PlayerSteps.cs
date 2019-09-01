using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSteps : MonoBehaviour
{
    private CharacterController cc;
    private AudioSource footstepSound;
    [SerializeField]
    private AudioClip[] footstepClip;
    [HideInInspector]
    public float volMin, volMax;
    private float accDistance;
    [HideInInspector]
    public float stepDistance;

    void Awake()
    {
        footstepSound = GetComponent<AudioSource>();
        cc = GetComponentInParent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckToPlayFootsteps();
    }

    void CheckToPlayFootsteps()
    {
        if(!cc.isGrounded)
        {
            return;
        }

        if (cc.velocity.sqrMagnitude > 0)
        {
            accDistance += Time.deltaTime;
            if(accDistance > stepDistance)
            {
                footstepSound.volume = Random.Range(volMin, volMax);
                footstepSound.clip = footstepClip[Random.Range(0, footstepClip.Length)];
                footstepSound.Play();
                accDistance = 0;
            }
        }
        else
        {
            accDistance = 0;
        }
    } 
}
