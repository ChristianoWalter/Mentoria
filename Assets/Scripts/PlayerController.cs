using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed;
    public float jumpForce;

    public Transform foot;
    public LayerMask ground;
    public bool onGround;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, rb.velocity.y);

        onGround = Physics2D.OverlapCircle(foot.position, .2f, ground);

        if (Input.GetButtonDown("Jump") && onGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
}
