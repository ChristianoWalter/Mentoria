using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public Rigidbody2D rb;
    public float moveSpeed;
    public float jumpForce;

    public Transform foot;
    public LayerMask ground;
    public bool onGround;

    public bool canAttack;
    public bool isAttacking;
    public bool isHeavyAttack;

    public Animator anim;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, rb.velocity.y);
        anim.SetFloat("Speed", Math.Abs(rb.velocity.x));

        if ((rb.velocity.x > 0 && transform.localScale.x < 0) || (rb.velocity.x < 0 && transform.localScale.x > 0))
        {
            Vector2 _localScale = transform.localScale;
            _localScale.x *= -1f;
            transform.localScale = _localScale;
        }

        onGround = Physics2D.OverlapCircle(foot.position, .2f, ground);
        anim.SetBool("OnGround", onGround);

        if (Input.GetButtonDown("Jump") && onGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            isHeavyAttack = false;
            Attack();
        }
        
        if (Input.GetButtonDown("Fire2"))
        {
            isHeavyAttack = true;
            Attack();
        }
    }

    public void Attack()
    {
        if (canAttack)
        {
            isAttacking = true;
            canAttack = false;
        }
        else
        {
            return;
        }
    }

    public void AttackManager()
    {
        if (!canAttack)
        {
            canAttack = true;
        }
        else
        {
            canAttack = false;
        }
    }
}
