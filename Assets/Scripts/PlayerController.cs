﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GroundCheck groundCheck;
    [SerializeField] float speed = 3.0f;
    [SerializeField] float jumpForce = 10.0f;
    private float gravity = -9.81f;
    private float horizontalInput;
    private float forwardInput;
    private float velocity;
    [SerializeField] float gravityScale = 5.0f;
    private Animator playerAnim;
    [SerializeField] GameObject[] playerSprites;
    private GameObject rosieForward;
    private GameObject rosieRight;
    private GameObject rosieLeft;
    private GameObject rosieBack;
    [SerializeField] Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        rosieForward = playerSprites[0];
        rosieRight = playerSprites[1];
        rosieLeft = playerSprites[2];
        rosieBack = playerSprites[3];
    }

    void Update()
    {
        direction = new Vector3(horizontalInput, 0, forwardInput); //Updating and keeping track of player's desired direction
        MovePlayer();
        Walk(direction);
        Jump();
    }

    void MovePlayer()
    {
        // Forward movement from keyboard input (WASD)
        forwardInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * Time.smoothDeltaTime * speed * forwardInput);

        // Horizontal movement from keyboard input (WASD)
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * Time.smoothDeltaTime * speed * horizontalInput);
    }

    void Walk(Vector3 direction)
    {
        playerAnim = GetComponentInChildren<Animator>();
        playerAnim.SetBool("Walk", false);

        if (direction.x > 0)
        {
            playerAnim = rosieRight.GetComponent<Animator>();
            rosieForward.SetActive(false);
            rosieRight.SetActive(true);
            rosieLeft.SetActive(false);
            rosieBack.SetActive(false);
            if (!playerAnim.GetBool("Jump"))
            {
                playerAnim.SetBool("Walk", true);
            }
        } else if (direction.x == 0 && direction.z < 0)
        {
            playerAnim = rosieForward.GetComponent<Animator>();
            rosieForward.SetActive(true);
            rosieRight.SetActive(false);
            rosieLeft.SetActive(false);
            rosieBack.SetActive(false);
            if (!playerAnim.GetBool("Jump"))
            {
                playerAnim.SetBool("Walk", true);
            }
            if (direction.z == 0)
            {
                playerAnim.SetBool("Walk", false);
            }
        } else if (direction.x < 0)
        {
            playerAnim = rosieLeft.GetComponent<Animator>();
            rosieForward.SetActive(false);
            rosieRight.SetActive(false);
            rosieLeft.SetActive(true);
            rosieBack.SetActive(false);
            if (!playerAnim.GetBool("Jump"))
            {
                playerAnim.SetBool("Walk", true);
            }
        }
        else if (direction.x == 0 && direction.z > 0)
        {
            playerAnim = rosieForward.GetComponent<Animator>();
            rosieForward.SetActive(false);
            rosieBack.SetActive(true);
            rosieRight.SetActive(false);
            rosieLeft.SetActive(false);
            if (!playerAnim.GetBool("Jump"))
            {
                playerAnim.SetBool("Walk", true);
            }
            if (direction.z == 0)
            {
                playerAnim.SetBool("Walk", false);
            }
        }
    }

    void Jump() // Jump mechanic without physics using GroundCheck with Raycast
    {
        playerAnim = GetComponentInChildren<Animator>();
        playerAnim.SetBool("Jump", false);
        velocity += gravity * gravityScale * Time.smoothDeltaTime;
        if (groundCheck.isGrounded && velocity < 0)
        {
            velocity = 0;
        }
        if (Input.GetKeyDown(KeyCode.Space) && groundCheck.isGrounded)
        {
            velocity = jumpForce;
            playerAnim.SetBool("Jump", true);
            //groundCheck.isGrounded = false;
        }
        transform.Translate(new Vector3(0, velocity, 0) * Time.smoothDeltaTime);
    }
}
