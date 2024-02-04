using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private const int ACTION_POINTS_MAX = 2;

    private HealthSystem healthSystem;
    private MoveAction moveAction;
    private GridPosition gridPosition;
    private SpinAction spinAction;
    private BaseAction[] actionArray;
    private int actionPoints;

    public static event EventHandler OnAnyResetActionPoints;
    public static event EventHandler OnAnyUnitSpawned;
    public static event EventHandler OnAnyUnitDied;

    [SerializeField] private bool isEnemy = false;
    
    private void Awake() {
        healthSystem = GetComponent<HealthSystem>();
        moveAction = GetComponent<MoveAction>();
        spinAction = GetComponent<SpinAction>();
        actionArray = GetComponents<BaseAction>();
        actionPoints = ACTION_POINTS_MAX;
    }
    private void Start() {
        gridPosition = LevelGrid.instance.GetGridPosition(transform.position);
        LevelGrid.instance.SetUnitAtGridPosition(gridPosition, this);
        TurnSystem.instance.UpdateTurn += Instance_UpdateTurn;
        healthSystem.UnitDie += HealthSystem_UnitDie;
        OnAnyUnitSpawned?.Invoke(this, EventArgs.Empty);
    }

    private void HealthSystem_UnitDie(object sender, EventArgs e)
    {
        LevelGrid.instance.ClearUnitAtGridPosition(gridPosition, this);
        OnAnyUnitDied?.Invoke(this, EventArgs.Empty);
        Destroy(gameObject);
    }

    private void Instance_UpdateTurn(object sender, System.EventArgs e)
    {
        ResetActionPoints();
    }

    private void Update() {
        
        GridPosition newGridPoisiton = LevelGrid.instance.GetGridPosition(transform.position);
        if(newGridPoisiton != gridPosition){
            GridPosition oldGridPosition = gridPosition;
            gridPosition = newGridPoisiton;
            LevelGrid.instance.ChangeUnitGridToAnother(oldGridPosition, newGridPoisiton, this);
        }
    }

    public MoveAction GetMoveAction()
    {
        return moveAction;
    }
    public GridPosition GetGridPosition()
    {
        return gridPosition;
    }
    public SpinAction GetSpinAction()
    {
        return spinAction;
    }
    public BaseAction[] GetActionArray()
    {
        return actionArray;
    }
    public bool TryAction(BaseAction action)
    {
        if(CanUseAction(action))
        {
            UseAction(action.ActionPointCost());
            return true;
        }
        return false;
    }
    private bool CanUseAction(BaseAction action)
    {
        return actionPoints >= action.ActionPointCost();
    }
    private void UseAction(int actionPoints)
    {
        this.actionPoints -= actionPoints;
    }
    public int GetActionPoint()
    {
        return actionPoints;
    }

    private void ResetActionPoints()
    {
        if((isEnemy && !TurnSystem.instance.IsPlayerTurn()) 
            || 
            (!isEnemy && TurnSystem.instance.IsPlayerTurn()))
        {
            actionPoints = ACTION_POINTS_MAX;
            OnAnyResetActionPoints?.Invoke(this, EventArgs.Empty);
        }
    }
    public bool IsEnemy()
    {
        return isEnemy;
    }
    public void TakeDamage(int damage)
    {
        healthSystem.TakeDamage(damage);
    }
}
