using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    CharacterController characterController;
    Animator animator;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(Input.GetButton("Fire1"))
        {
            animator.SetBool("isFire", true);
        }
        else
        {
            animator.SetBool("isFire", false);
        }
    }
}
