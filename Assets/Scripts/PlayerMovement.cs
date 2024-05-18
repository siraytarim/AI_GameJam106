using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
     Rigidbody2D playerRb;
        Animator playerAnimator;
        public float moveSpeed=1f;
        public float jumpSpeed = 5f, jumpFrequency=1f, nextJumpTime;
        [SerializeField] float healt;
    
        bool facingRight = true;
        public bool isGrounded = false;
        public Transform groundCheckPosition;
        public float groundCheckRadius;
        public LayerMask groundCheckLayer;
         void Start()
        {
            playerRb = GetComponent<Rigidbody2D>();
            playerAnimator=GetComponent<Animator>();
        }
         void Update()
        { 
            HorizontalMove();
            OnGroundCheck();
           // GetDamage();
             
            if(playerRb.velocity.x < 0 && facingRight)
            {
                FlipFace();
            }
            else if(playerRb.velocity.x > 0 && !facingRight)
            {
                FlipFace();
            }
            
            if(Input.GetKeyDown(KeyCode.Space) && isGrounded && (nextJumpTime < Time.time))
            {
                Debug.Log("zıplıyor");
                nextJumpTime = Time.time + jumpFrequency;
                Jump();
            }

            if (Input.GetButtonDown("Jump") && playerRb.velocity.y > 0f)
            {
                playerRb.velocity = new Vector2(playerRb.velocity.x, playerRb.velocity.y * 0.5f);
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                playerAnimator.SetBool("Attacked",true);
            }
    
        } void HorizontalMove()
        {
            playerRb.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, playerRb.velocity.y);
           playerAnimator.SetFloat("isRunning", Mathf.Abs(playerRb.velocity.x));
           // playerAnimator.SetBool("isRunning", true);
    
        }
    
        void FlipFace()
        {
            facingRight = !facingRight;
            Vector3 tempLocalScale=transform.localScale;
            tempLocalScale.x *= -1;
            transform.localScale = tempLocalScale;
        }
    
        void Jump()
        {
            
            //playerRb.AddForce(new Vector2(0f,jumpSpeed)); // add force ile kuvvet ekledik
            playerRb.velocity = new Vector2(playerRb.velocity.x, jumpSpeed);
            playerAnimator.SetBool("isGrounded", true);

        }

        void OnGroundCheck()
        {
            //isGrounded = Physics2D.Raycast(groundCheckPosition.position, Vector2.down, groundCheckRadius, groundCheckLayer);
              //  isGrounded = Physics2D.OverlapCircle(groundCheckPosition.position,groundCheckRadius,groundCheckLayer);
              //playerAnimator.SetBool("isGrounded", isGrounded);
              isGrounded = Physics2D.OverlapCircle(groundCheckPosition.position, 0.2f, groundCheckLayer);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Bullet"))
            {
                healt--;
            }
        }

      /*  void GetDamage()
        {
            healt--;
            if(healt<=0)
                playerAnimator.SetBool("isDead", true);
        }*/
}
