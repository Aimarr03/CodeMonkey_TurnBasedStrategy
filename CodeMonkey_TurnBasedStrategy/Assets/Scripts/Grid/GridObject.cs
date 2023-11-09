using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject
{
    public GridSystem gridSystem;
    public GridPosition gridPosition;
    public GridObject(GridSystem gridSystem, GridPosition gridPosition){
        this.gridSystem = gridSystem;
        this.gridPosition = gridPosition;
    }
}
