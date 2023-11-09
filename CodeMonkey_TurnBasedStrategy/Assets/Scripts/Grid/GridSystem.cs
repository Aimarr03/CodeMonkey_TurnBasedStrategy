using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem
{
    private int width;
    private int height;
    private float cellSize;

    private GridObject[,] gridObjects;
    public GridSystem(int width, int height, float cellSize){
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        gridObjects = new GridObject[width,height];
        for(int x = 0; x<width; x++){
            for(int z = 0; z<height; z++){
                gridObjects[x,z] = new GridObject(this, new GridPosition(x,z));
            }
        }
    }
    public Vector3 GetWorldPosition(GridPosition gridPosition){
        return new Vector3(gridPosition.x , 0, gridPosition.z) * cellSize;
    }
    public GridPosition GetGridPosition(Vector3 worldPosition){
        return new GridPosition(
            Mathf.FloorToInt((worldPosition.x/cellSize)),
            Mathf.FloorToInt((worldPosition.z/cellSize))
        );
    }
    public void CreateDebugPrefab(Transform prefab){
        for(int x = 0; x < width; x++){
            for(int z =0; z< height; z++){
                GridPosition gridPosition = new GridPosition(x,z);
                Transform gameObjectInstantiated = GameObject.Instantiate(prefab, GetWorldPosition(gridPosition), Quaternion.identity);
                gameObjectInstantiated.TryGetComponent<DebugGridObject>(out DebugGridObject debugGrid);
                string formatText = $"x: {x}; z: {z}";
                debugGrid.SetTextMeshPro(formatText);
            }
        }
    }
}
