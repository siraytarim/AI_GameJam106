using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class futurePlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator playerAnimator;
    [SerializeField] float moveSpeed, jumpSpeed, jumpFrequency, nextJumpTime;
    [SerializeField] float healt;
    
    bool facingRight = true;
    private bool isGrounded = false;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckRadius;
    [SerializeField] LayerMask groundCheckLayer;

     void Update()
    {
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        OnGroundCheck();
        
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            playerAnimator.SetBool("isGrounded", true);
        }
        
    }
    void OnGroundCheck()
    {
         isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundCheckLayer);
    }
}
