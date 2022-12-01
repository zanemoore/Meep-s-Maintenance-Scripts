using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Written by Zane
public class HumanPatrol : MonoBehaviour
{
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;
    [SerializeField] private Transform humanAI;

    [SerializeField] private float humanSpeed;
    [SerializeField] private float idleDuration;

    [SerializeField] private GameObject fov;
    [SerializeField] private Animator anim;

    private bool movingRight;
    private float idleTimer;
    private Vector3 initialScale;

    private void Awake()
    {
        initialScale = humanAI.localScale;
    }

    private void Update()
    {
        if (movingRight)
        {
            // flips fov when the AI is moving right
            fov.transform.localScale = new Vector3(1, 0, 0);

            // detects if the AI has reached the right edge
            if (humanAI.position.x + 0.75 < rightEdge.position.x)
            {
                // the AI moves to the right
                MoveInDirection(1);
            }
            else
            {
                // the AI changes direction
                DirectionChange();
            }
        }
        else
        {
            // flips fov when the AI is moving left
            fov.transform.localScale = new Vector3(-1, 0, 0);

            // detects if the AI has reached the left edge
            if (humanAI.position.x - 0.75 > leftEdge.position.x)
            {
                // the AI moves to the left
                MoveInDirection(-1);
            }
            else
            {
                // the AI changes direction
                DirectionChange();
            }
        }
    }

    private void DirectionChange()
    {
        // deactivates walking animation
        anim.SetBool("Walking", false);

        // changes direction of the AI as soon as it reaches the edge
        if (movingRight)
        {
            humanAI.localScale = new Vector3(Mathf.Abs(initialScale.x) * -1, initialScale.y, initialScale.z);
        }
        else
        {
            humanAI.localScale = new Vector3(Mathf.Abs(initialScale.x) * 1, initialScale.y, initialScale.z);
        }

        // starts idle timer
        idleTimer += Time.deltaTime;

        // checks if AI has idled long enough
        if (idleTimer > idleDuration)
        {
            // allows for AI to change direction
            movingRight = !movingRight;
        }
    }

    private void MoveInDirection(int direction)
    {
        // activates walking animation
        anim.SetBool("Walking", true);

        // resets idle timer
        idleTimer = 0;

        // makes the AI face a direction
        humanAI.localScale = new Vector3(Mathf.Abs(initialScale.x) * direction, initialScale.y, initialScale.z);

        // makes the AI move in the direction it's facing
        humanAI.position = new Vector3(humanAI.position.x + Time.deltaTime * direction * humanSpeed, humanAI.position.y, humanAI.position.z);
    }
}
