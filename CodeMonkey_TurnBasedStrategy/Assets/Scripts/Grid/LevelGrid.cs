using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    public event EventHandler OnAnyUnitMoveAnyUnit;

    public static LevelGrid instance;
    [SerializeField] private Transform DebugPrefab;
    GridSystem gridSystem;
    private void Awake() {
        if(instance != null){
            Destroy(gameObject);
            return;
        }
        instance = this;
        gridSystem = new GridSystem(10, 10, 2);
        gridSystem.CreateDebugPrefab(DebugPrefab);
    }
    public void Start(){
        
    }
    private void Update() {

    }
    public void SetUnitAtGridPosition(GridPosition gridPosition, Unit unit){
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        if(gridObject != null)
        {
            gridObject.AddUnit(unit);
        }
    }
    public List<Unit> GetUnitAtGridPosition(GridPosition gridPosition){
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        return gridObject.GetUnits();
    }
    public void ClearUnitAtGridPosition(GridPosition gridPosition, Unit unit){
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        if (gridObject != null)
        {
            gridObject.RemoveUnit(unit);
        }
    }
    public void ChangeUnitGridToAnother(GridPosition original, GridPosition destination, Unit unit){
        ClearUnitAtGridPosition(original, unit);
        SetUnitAtGridPosition(destination, unit);
        OnAnyUnitMoveAnyUnit?.Invoke(this, EventArgs.Empty);
    }
    public GridPosition GetGridPosition(Vector3 WorldPosition) => gridSystem.GetGridPosition(WorldPosition);

    public bool CheckGridBoundary(GridPosition gridPosition) => gridSystem.CheckGridBoundary(gridPosition);

    public Vector3 GetWorldPosition(GridPosition gridPosition) => gridSystem.GetWorldPosition(gridPosition);


    public int GetWidth() => gridSystem.GetWidth();

    public int GetHeight() => gridSystem.GetHeight();

    public Unit GetUnitGridObject(GridPosition gridPosition) => gridSystem.GetGridObject(gridPosition).GetUnit();

    public bool CheckContainUnit(GridPosition gridPosition)
    {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        return gridObject.ContainUnit();
    }
}
