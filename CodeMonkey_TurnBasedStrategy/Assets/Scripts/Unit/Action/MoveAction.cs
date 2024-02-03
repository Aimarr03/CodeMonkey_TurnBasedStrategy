using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : BaseAction
{
    public event EventHandler StartMoving;
    public event EventHandler StopMoving;

    private Vector3 targetPosition;
    
    const string IsWalking = "IsWalking";
    private float speed;
    private float rotateSpeed;

    [SerializeField] private int MaxDistance = 4;


    protected override void Awake()
    {
        base.Awake();
        speed = 2f;
        rotateSpeed = 10f;
    }
    void Update()
    {
        if(!isAction)
        {
            return;
        }
        Vector3 MoveDirection = (targetPosition - transform.position).normalized;
        if (Vector3.Distance(targetPosition, transform.position) > 0.01f)
        {
            transform.position += MoveDirection * speed * Time.deltaTime;

        }
        else
        {
            transform.position = targetPosition;
            FinishAction();
            StopMoving?.Invoke(this, EventArgs.Empty);
        }

        transform.forward = Vector3.Lerp(transform.forward, MoveDirection, Time.deltaTime * rotateSpeed);
    }
    public override void TakeAction(GridPosition gridPosition, Action ActComplete)
    {
        targetPosition = LevelGrid.instance.GetWorldPosition(gridPosition);
        StartAction(ActComplete);
        StartMoving?.Invoke(this, EventArgs.Empty);
    }
    

    public override List<GridPosition> GetValidateMovePosition()
    {
        List<GridPosition> list = new List<GridPosition> ();
        GridPosition unitPosition = unit.GetGridPosition();
        for(int x = -MaxDistance; x <= MaxDistance; x++)
        {
            for(int z = -MaxDistance; z <= MaxDistance; z++)
            {
                GridPosition offsetPosition = new GridPosition(x, z);
                GridPosition moveablePosition = unitPosition + offsetPosition;
                if (!LevelGrid.instance.CheckGridBoundary(moveablePosition))
                {
                    continue;
                }
                if (LevelGrid.instance.CheckContainUnit(moveablePosition))
                {
                    continue;
                }
                list.Add(moveablePosition);
                //Debug.Log(moveablePosition);
            }
        }

        return list;
    }

    public override string GetName()
    {
        return "Move";
    }
}
