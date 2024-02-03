using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;
using TMPro;
using UnityEngine;

public class ShootAction : BaseAction
{
    public event EventHandler<ShootingPositionArgs> StartShooting;

    [SerializeField] private int MaxShootingDistance = 4;
    

    public class ShootingPositionArgs : EventArgs
    {
        public Unit targetShoot;
        public Unit shooter;
    }

    private float currentTimer;
    private Unit targetUnit;
    private Vector3 TargetUnitPosition;
    private enum ShootingState
    {
        Aiming,
        Shooting,
        CoolingOff,
        Idle
    }
    private ShootingState currentShootingState;

    private const float MAX_TIME_SHOOTING = 0.5f;
    private const float MAX_TIME_AIMING = 0.2f;
    private const float MAX_TIME_COOLINGOFF = 0.2f;

    private float rotateSpeed = 10f;

    public override string GetName()
    {
        return "Shoot";
    }
    public void Update()
    {
        if (!isAction)
        {
            return;
        }
        currentTimer -= Time.deltaTime;
        Aiming();
        if(currentTimer <= 0)
        {
            NextState();
        }
    }
    private void NextState()
    {
        switch(currentShootingState)
        {
            case ShootingState.Aiming:
                currentTimer = MAX_TIME_SHOOTING;
                currentShootingState = ShootingState.Shooting;
                Shooting();
                break;
            case ShootingState.Shooting:
                currentTimer = MAX_TIME_COOLINGOFF;
                currentShootingState = ShootingState.CoolingOff;
                break;
            case ShootingState.CoolingOff:
                FinishAction();
                currentShootingState = ShootingState.Idle;
                break;
        }
    }
    private void Aiming()
    {
        Vector3 MoveDirection = (TargetUnitPosition - transform.position).normalized;
        transform.forward = Vector3.Lerp(transform.forward, MoveDirection, Time.deltaTime * rotateSpeed);
    }
    private void Shooting()
    {
        StartShooting?.Invoke(this, new ShootingPositionArgs()
        {
            targetShoot = targetUnit,
            shooter = unit
        });
    }
    public override List<GridPosition> GetValidateMovePosition()
    {
        List<GridPosition> list = new List<GridPosition>();
        GridPosition unitPosition = unit.GetGridPosition();
        for (int x = -MaxShootingDistance; x <= MaxShootingDistance; x++)
        {
            for (int z = -MaxShootingDistance; z <= MaxShootingDistance; z++)
            {
                GridPosition offsetPosition = new GridPosition(x, z);
                GridPosition moveablePosition = unitPosition + offsetPosition;

                int totalDistance = Mathf.Abs(x) + Mathf.Abs(z);
                if(totalDistance > MaxShootingDistance)
                {
                    continue;
                }
                if (!LevelGrid.instance.CheckGridBoundary(moveablePosition))
                {
                    continue;
                }
                if (!LevelGrid.instance.CheckContainUnit(moveablePosition))
                {
                    continue;
                }
                
                Unit unitAtGridObject = LevelGrid.instance.GetUnitGridObject(moveablePosition);
                if(unit.IsEnemy() && unitAtGridObject.IsEnemy())
                {
                    continue;
                }
                if(!unit.IsEnemy() && !unitAtGridObject.IsEnemy())
                {
                    continue;
                }
                list.Add(moveablePosition);
                //Debug.Log(moveablePosition);
            }
        }
        return list;
    }

    public override void TakeAction(GridPosition position, Action action)
    {
        StartAction(action);
        currentShootingState = ShootingState.Aiming;
        targetUnit = LevelGrid.instance.GetUnitGridObject(position);
        TargetUnitPosition = targetUnit.transform.position;
        currentTimer = MAX_TIME_AIMING;
        
    }
}
