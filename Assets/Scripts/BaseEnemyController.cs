using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseEnemyController : HealthController
{
    public int damage;

    public bool canMove;
    public Transform pointA;
    public Transform pointB;
    public Transform currentPoint;
    public Rigidbody2D rb;
    public float moveSpeed;

    public bool stopped;

    public Animator anim;

    private void Awake()
    {
        currentHealth = maxHealth;
        canMove = true;
        currentPoint = pointB;
    }

    private void Update()
    {
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));

        if (canMove == true)
        {
            if (currentPoint == pointB)
            {
                rb.velocity = new Vector2(-moveSpeed, 0);
            }
            else
            {
                rb.velocity = new Vector2(moveSpeed, 0);
            }
        }
        else
        {
            if (!stopped)
            {
                rb.velocity = Vector2.zero;
                stopped = true;
            }
        }

        if(Vector2.Distance(transform.position, currentPoint.position) < .5f)
        {
            StartCoroutine(SwitchTarget());
        }

        if ((rb.velocity.x > 0 && transform.localScale.x < 0) || (rb.velocity.x < 0 && transform.localScale.x > 0))
        {
            Vector2 _localScale = transform.localScale;
            _localScale.x *= -1f;
            transform.localScale = _localScale;
        }
    }

    private IEnumerator SwitchTarget()
    {
        canMove = false;
        if (currentPoint == pointB) 
        {
            currentPoint = pointA;
        }
        else
        {
            currentPoint = pointB;
        }
        yield return new WaitForSeconds(2f);
        canMove = true;
        stopped = false;
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerController.instance.TakeDamage(damage);
            PlayerController.instance.ActiveKnockback(transform);
        }
    }

    protected override void Death()
    {
        Destroy(gameObject);
        Debug.Log("inimigo morreu");
    }

    protected override void DamageEffect()
    {
        StartCoroutine (DamageRoutine());
    }

    private IEnumerator DamageRoutine()
    {
        canMove = false;
        anim.SetTrigger("Hurt");
        Vector2 direction = (PlayerController.instance.transform.position - transform.position).normalized;
        rb.velocity = direction * -4;
        yield return new WaitForSeconds(3f);
        canMove = true;
        stopped = true;
    }

}
