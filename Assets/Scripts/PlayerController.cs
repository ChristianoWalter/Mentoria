using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : HealthController
{
    public static PlayerController instance;

    public Rigidbody2D rb;
    public float moveSpeed;
    public float jumpForce;
    public bool canMove;
    private Vector2 direction;

    public Transform foot;
    public LayerMask ground;
    public bool onGround;

    public bool canAttack;
    public bool isAttacking;
    public bool isHeavyAttack;
    public int attackDamage;
    public float attackRange;
    public Transform attackPoint;
    public LayerMask enemyLayers;

    public Animator anim;

    private void Awake()
    {
        instance = this;
        canMove = true;
        currentHealth = maxHealth;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        onGround = Physics2D.OverlapCircle(foot.position, .2f, ground);
        anim.SetBool("OnGround", onGround);

        if (canMove)
        {
            rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);
            anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        }
        if ((rb.velocity.x > 0 && transform.localScale.x < 0) || (rb.velocity.x < 0 && transform.localScale.x > 0))
        {
            Vector2 _localScale = transform.localScale;
            _localScale.x *= -1f;
            transform.localScale = _localScale;
        }
    }

    public void MovementAction(InputAction.CallbackContext value)
    {
        direction = value.ReadValue<Vector2>();
    }

    public void JumpAction(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            if (onGround && canMove)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }
    }
    
    public void NormalAttack(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            if (rb.velocity == Vector2.zero && canAttack)
            {
                isHeavyAttack = false;
                isAttacking = true;
                canAttack = false;
                canMove = false;
            }
        }
        else
        {
            return;
        }

    }
    
    public void HeavyAttack(InputAction.CallbackContext value)
    {
        if (value.performed)
        {
            if (rb.velocity == Vector2.zero && canAttack)
            {
                isHeavyAttack = true;
                isAttacking = true;
                canAttack = false;
                canMove = false;
            }
        }
        else
        {
            return;
        }
    }

    public void AttackDamage()
    {
        if (isHeavyAttack)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange * 2, enemyLayers);

            foreach(Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<HealthController>().TakeDamage(attackDamage * 2);
            }
        }
        else
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<HealthController>().TakeDamage(attackDamage);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange * 2);
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
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

    public void ActiveKnockback(Transform _enemy)
    {
        StartCoroutine(KnockbackRoutine(_enemy));
    }

    private IEnumerator KnockbackRoutine(Transform enemy)
    {
        canMove = false;
        isInvencible = true;
        anim.SetTrigger("Hurt");
        Vector2 direction = (enemy.position - transform.position).normalized;
        rb.velocity = direction * -8;
        yield return new WaitForSeconds(.5f);
        canMove = true;
        isInvencible = false;
    }

    protected override void Death()
    {
        Debug.Log("morreu");
    }

}
