using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private float timer;

    public enum EnemyState
    {
        WaitingForEnemyTurn,
        EnemyTurn,
        Busy
    }
    private EnemyState currentEnemyState;

    public void Awake()
    {
        currentEnemyState = EnemyState.WaitingForEnemyTurn;
    }
    public void Start()
    {
        TurnSystem.instance.UpdateTurn += Instance_UpdateTurn;
    }

    private void Instance_UpdateTurn(object sender, System.EventArgs e)
    {
        timer = 2f;
        currentEnemyState = EnemyState.EnemyTurn;
    }

    public void Update()
    {
        if(TurnSystem.instance.IsPlayerTurn())
        {
            return;
        }
        switch (currentEnemyState)
        {
            case EnemyState.WaitingForEnemyTurn:
                break;
            case EnemyState.EnemyTurn:
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    if (OnTryEnemyAITakeAction(SetBusyState))
                    {
                        currentEnemyState = EnemyState.Busy;
                    }
                    else
                    {
                        TurnSystem.instance.EndTurn();
                    }
                }
                break;
            case EnemyState.Busy:
                break;
        }
    }
    private void SetBusyState()
    {
        timer = 0.5f;
        currentEnemyState = EnemyState.EnemyTurn;
    }
    public bool OnTryEnemyAITakeAction(Action action)
    {
        foreach(Unit enemy in UnitManager.instance.GetEnemyList())
        {
            ActionSelectedUnit.instance.SetUnit(enemy);
            if(OnTryEnemyAITakeAction(action, enemy))
            {
                return true;
            }
        }
        return false;
    }
    public bool OnTryEnemyAITakeAction(Action action, Unit enemy)
    {
        SpinAction spinAction = enemy.GetSpinAction();
        GridPosition gridPosition = enemy.GetGridPosition();

        if (!spinAction.ValidateMove(gridPosition))
        {
            return false;
        }
        if (!enemy.TryAction(spinAction))
        {
            return false;
        }

        spinAction.TakeAction(gridPosition, action);
        ActionSelectedUnit.instance.TriggerActionExecutedEvent();
        return true;
    }
}
