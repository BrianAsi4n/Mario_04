using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    [SerializeField] private float movevementSpeed;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float inputValue = Input.GetAxisRaw("Horizontal");
        float movevementValue = inputValue * movevementSpeed;
        rb.velocity = new Vector2(movevementValue, rb.velocity.y);
        Debug.Log(rb.velocity);
        if(inputValue < 0)
        {
            spriteRenderer.flipX = true;
        }else if (inputValue > 0)
        {
            spriteRenderer.flipX= false;
        }
    }
}
