using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAction : MonoBehaviour
{
    protected bool isAction;
    protected Unit unit;
    protected Action ActComplete;
    public static event EventHandler ActionStarted;
    public static event EventHandler ActionFinished;

    protected virtual void Awake()
    {
        unit = GetComponent<Unit>();
    }

    public abstract string GetName();

    public virtual bool ValidateMove(GridPosition position)
    {
        List<GridPosition> list = GetValidateMovePosition();
        return list.Contains(position);
    }

    public abstract void TakeAction(GridPosition position, Action action);
    public abstract List<GridPosition> GetValidateMovePosition();

    public virtual int ActionPointCost()
    {
        return 1;
    }
    protected void StartAction(Action action)
    {
        ActComplete = action;
        isAction = true;
        ActionStarted?.Invoke(this, EventArgs.Empty);
    }
    protected void FinishAction()
    {
        isAction = false;
        ActComplete();
        ActionFinished?.Invoke(this, EventArgs.Empty);
    }
    public Unit GetUnit()
    {
        return unit;
    }
}
