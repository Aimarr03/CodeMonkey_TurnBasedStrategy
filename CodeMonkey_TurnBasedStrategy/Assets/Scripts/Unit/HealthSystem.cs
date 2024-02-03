using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public event EventHandler UnitDie;
    [SerializeField] private int health = 100;

    public void TakeDamage(int damage)
    {
        health = Mathf.Clamp(health-damage, 0, health);
        if(health == 0)
        {
            Debug.Log("Unit Die");
            Die();
        }
    }
    private void Die()
    {
        UnitDie?.Invoke(this, EventArgs.Empty);
    }
}
