using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashPushBlocks : MonoBehaviour
{
    private BlockCheck blockCheck;
    private PlayerMovement playerMovement;
    [SerializeField] private bool onRightEnd;
    [SerializeField] private Transform rightEndCap;
    [SerializeField] private Transform leftEndCap;
    [SerializeField] private float speed = 20;
    private bool canMove;


    void Start()
    {
        canMove = false;
        blockCheck = FindObjectOfType<BlockCheck>();
        playerMovement = FindObjectOfType<PlayerMovement>();

        if (transform.position == rightEndCap.position)
        {
            onRightEnd = true;
        }
        else
        {
            onRightEnd = false;
        }
    }


    void Update()
    {
        // Determines if the block can move
        if (playerMovement.isDashing)
        {
            if (blockCheck.isTouchingDashPushBlock)
            {
                if (onRightEnd && !playerMovement.facingRight)
                {
                    canMove = true;
                }
                else if (!onRightEnd && playerMovement.facingRight)
                {
                    canMove = true;
                }
            }
        }

        // Moves the block
        if (canMove)
        {
            if (onRightEnd)
            {
                transform.position = Vector2.MoveTowards(transform.position, leftEndCap.position, speed * Time.deltaTime);
                if (transform.position == leftEndCap.position)
                {
                    onRightEnd = false;
                    canMove = false;
                }
            }
            else if (!onRightEnd)
            {
                transform.position = Vector2.MoveTowards(transform.position, rightEndCap.position, speed * Time.deltaTime);
                if (transform.position == rightEndCap.position)
                {
                    onRightEnd = true;
                    canMove = false;
                }
            }
        }
    }

}
