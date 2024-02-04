using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAction : BaseAction
{
    private float spinAmount;
    private Vector3 rotationVector;

    public void Update()
    {
        if (!isAction)
        {
            return;
        }
        float rotationSpeed = 360f * Time.deltaTime;
        transform.eulerAngles += new Vector3(0, rotationSpeed, 0);
        spinAmount += rotationSpeed;
        if(spinAmount >= 360)
        {
            transform.eulerAngles = rotationVector;
            FinishAction();
        }
    }
    public override void TakeAction(GridPosition gridPosition, Action ActComplete)
    {
        spinAmount = 0f;
        rotationVector = transform.eulerAngles;
        StartAction(ActComplete);
    }
    public override string GetName()
    {
        return "Spin";
    }

    public override List<GridPosition> GetValidateMovePosition()
    {
        Unit unit = ActionSelectedUnit.instance.GetUnit();

        GridPosition unitCurrenPosition = unit.GetGridPosition();

        return new List<GridPosition>
        {
            unitCurrenPosition
        };
    }
    public override int ActionPointCost()
    {
        return 1;
    }
}
