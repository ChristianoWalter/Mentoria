using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public bool isInvencible;
    public float maxHealth;
    public float currentHealth;


    protected virtual void Death()
    {

    }

    protected virtual void DamageEffect()
    {

    }

    public void TakeDamage(float damage)
    {
        if (currentHealth > 0 && !isInvencible)
        {
            currentHealth -= damage;
            DamageEffect();
            Debug.Log(currentHealth);
            if (currentHealth <= 0)
            {
                Death();
            }
        }
        
    }


    public void TakeHeal(float heal) 
    { 
        if(currentHealth < maxHealth)
        {
            currentHealth = Mathf.Clamp(currentHealth + heal, 0, maxHealth);
        }
        Debug.Log(currentHealth);
    }
}
