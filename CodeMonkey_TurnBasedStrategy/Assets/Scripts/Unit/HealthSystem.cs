using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public event EventHandler UnitDie;
    public event EventHandler TakeDamageEvent;
    private int currentHealth;
    [SerializeField] private int maxHealth = 100;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Take Damage " + damage);
        currentHealth = Mathf.Clamp(currentHealth-damage, 0, maxHealth);
        TakeDamageEvent?.Invoke(this, EventArgs.Empty);
        if(currentHealth == 0)
        {
            Debug.Log("Unit Die");
            Die();
        }
    }
    public float PercentageHealth()
    {
        return (float) currentHealth / maxHealth;
    }
    private void Die()
    {
        UnitDie?.Invoke(this, EventArgs.Empty);
    }
}
