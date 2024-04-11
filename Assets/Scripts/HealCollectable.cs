using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealCollectable : MonoBehaviour
{
    public float healthRegen;
    public bool maxHeal;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!maxHeal)
            {
                PlayerController.instance.TakeHeal(healthRegen);
            }
            else
            {
                PlayerController.instance.TakeHeal(PlayerController.instance.maxHealth);
            }
            Destroy(gameObject);
        }
    }
}
