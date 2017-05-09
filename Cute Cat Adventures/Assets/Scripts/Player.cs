﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float jump;

    bool isFacingRight;
    bool isJumping;
    float playerSpeed;

    Animator anim;
    Rigidbody2D rg2d;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        rg2d = GetComponent<Rigidbody2D>();
        isFacingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        Move(playerSpeed);
        Flip();

        // Move left
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) || (Input.GetAxis("Horizontal") < 0))
        {
            playerSpeed = -speed;
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow) || (Input.GetAxis("Horizontal") == 0))
        {
            playerSpeed = 0;
        }

        // Move right
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) || (Input.GetAxis("Horizontal") > 0))
        {
            playerSpeed = speed;
        }
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow) || (Input.GetAxis("Horizontal") == 0))
        {
            playerSpeed = 0;
        }

        // Jump
        if((Input.GetKeyDown(KeyCode.Space) || Input.GetAxis("Jump") > 0))
        {
            isJumping = true;
            rg2d.AddForce(new Vector2(rg2d.velocity.x, jump));
            anim.SetInteger("State", 3);
        }
    }

    void Move(float speed)
    {
        if(playerSpeed < 0 && !isJumping || playerSpeed > 0 && !isJumping)
        {
            anim.SetInteger("State", 2);
        }

        if(playerSpeed == 0 && !isJumping)
        {
            anim.SetInteger("State", 0);
        }

        rg2d.velocity = new Vector3(speed, rg2d.velocity.y, 0);
    }

    void Flip()
    {
        if(playerSpeed > 0 && !isFacingRight || playerSpeed < 0 && isFacingRight)
        {
            isFacingRight = !isFacingRight;

            Vector3 scale = transform.localScale;

            scale.x *= -1;

            transform.localScale = scale;
        }
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Ground")
        {
            isJumping = false;
            anim.SetInteger("State", 0);
        }
    }
}
