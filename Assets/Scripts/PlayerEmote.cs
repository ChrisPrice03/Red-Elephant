using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEmote : MonoBehaviour
{
    private Animator animator;
    private readonly string emoteTrigger = "Emote";
    private readonly string isMovingBool = "IsMoving";

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Check if the B key was pressed
        if (Input.GetKeyDown(KeyCode.B))
        {
            // Set the trigger to play the emote animation
            animator.SetTrigger(emoteTrigger);
            // Ensure the IsMoving boolean is set to false
            animator.SetBool(isMovingBool, false);
        }

        // Check if the movement keys are pressed
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) ||
            Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            // If currently emoting, this will force the state back to the default/idle animation
            animator.SetBool(isMovingBool, true);
        }
        else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) ||
                 Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            // When movement keys are released, check if any are still pressed, if not, set IsMoving to false
            if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.A) &&
                !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
            {
                animator.SetBool(isMovingBool, false);
            }
        }
    }
}