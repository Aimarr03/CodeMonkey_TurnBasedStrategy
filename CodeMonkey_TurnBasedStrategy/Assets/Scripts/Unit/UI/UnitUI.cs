using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI UI_ActionPoint;
    [SerializeField] private Unit unit;
    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private Image healthBar;

    private void Start()
    {
        UpdateHealthBar();
        UpdateTextActionPoint();
        ActionSelectedUnit.instance.ActionExecuted += Instance_ActionExecuted;
        healthSystem.TakeDamageEvent += HealthSystem_TakeDamageEvent;
    }

    private void HealthSystem_TakeDamageEvent(object sender, EventArgs e)
    {
        Debug.Log("KONTOL");
        UpdateHealthBar();
    }

    private void Instance_ActionExecuted(object sender, System.EventArgs e)
    {
        UpdateTextActionPoint();
    }

    private void UpdateTextActionPoint()
    {
        UI_ActionPoint.text = unit.GetActionPoint().ToString();
        //Debug.Log(unit.GetActionPoint().ToString());
    }
    private void UpdateHealthBar()
    {
        Debug.Log(healthSystem.PercentageHealth());
        healthBar.fillAmount = healthSystem.PercentageHealth();
    }
}
