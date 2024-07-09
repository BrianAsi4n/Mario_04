using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movevementSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Transform groundCheck;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private bool isGrounded;
    private MovingPlatform currentPlatform;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        GroundCheck();
        MovePlayer();
        JumpControl();
    }

    private void GroundCheck()
    {
        RaycastHit2D hit = Physics2D.CircleCast(groundCheck.position, 0.1f, Vector2.down, 0.1f, LayerMask.GetMask("Ground"));
        isGrounded = hit.collider != null;
        if (hit.collider == null) return;
        if (hit.transform.tag == "moving_platform")
        {
            currentPlatform = hit.transform.gameObject.GetComponent<MovingPlatform>();
            currentPlatform.AttachRigidbody(rb);
        }
        else
        {
            if (currentPlatform != null)
            {
                currentPlatform.DeattachRigidbody(rb);
                currentPlatform = null;
            }
        }
    }

    private void MovePlayer()
    {
        float inputValue = Input.GetAxisRaw("Horizontal");
        float movementValue = inputValue * movevementSpeed;
        rb.velocity = new Vector2(movementValue, rb.velocity.y);
        Debug.Log(rb.velocity);
        if (inputValue < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (inputValue > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    private void JumpControl()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce);
        }
    }
}

