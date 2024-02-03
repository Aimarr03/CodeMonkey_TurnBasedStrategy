using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject
{
    public GridSystem gridSystem;
    public GridPosition gridPosition;
    private List<Unit> units;
    public GridObject(GridSystem gridSystem, GridPosition gridPosition){
        this.gridSystem = gridSystem;
        this.gridPosition = gridPosition;
        units = new List<Unit>();
    }
    public void AddUnit(Unit unit){
        units.Add(unit);
    }
    public void RemoveUnit(Unit unit){
        units.Remove(unit);
    }
    public List<Unit> GetUnits(){
        return units;
    }
    public override string ToString()
    {
        string formatUnits = "";
        foreach(Unit unit in units){
            formatUnits += $"{unit},";
        }
        return gridPosition.ToString() + "\n " + formatUnits;
    }
    public bool ContainUnit()
    {
        return units.Count > 0;
    }
    public Unit GetUnit()
    {
        return units[0];
    }
}
