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
            StopMoving?.Invoke(this, EventArgs.Empty);
            FinishAction();
        }

        transform.forward = Vector3.Lerp(transform.forward, MoveDirection, Time.deltaTime * rotateSpeed);
    }
    public override void TakeAction(GridPosition gridPosition, Action ActComplete)
    {
        targetPosition = LevelGrid.instance.GetWorldPosition(gridPosition);
        StartMoving?.Invoke(this, EventArgs.Empty);
        StartAction(ActComplete);
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
                int totalDistance = Mathf.Abs(x) + Mathf.Abs(z);
                if (totalDistance > MaxDistance)
                {
                    continue;
                }
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
