using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour
{
    
    public static LevelGrid instance;
    [SerializeField] private Transform DebugPrefab;
    GridSystem gridSystem;
    private void Awake() {
        if(instance != null){
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    public void Start(){
        gridSystem = new GridSystem(10,10,2);
        gridSystem.CreateDebugPrefab(DebugPrefab);
    }
    private void Update() {

    }
    public void SetUnitAtGridPosition(GridPosition gridPosition, Unit unit){
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        gridObject.AddUnit(unit);
    }
    public List<Unit> GetUnitAtGridPosition(GridPosition gridPosition){
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        return gridObject.GetUnit();
    }
    public void ClearUnitAtGridPosition(GridPosition gridPosition, Unit unit){
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        gridObject.RemoveUnit(unit);
    }
    public void ChangeUnitGridToAnother(GridPosition original, GridPosition destination, Unit unit){
        ClearUnitAtGridPosition(original, unit);
        SetUnitAtGridPosition(destination, unit);
    }
    public GridPosition GetGridPosition(Vector3 WorldPosition) => gridSystem.GetGridPosition(WorldPosition);
}
